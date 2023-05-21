using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MW.Application.Interfaces.Repositories;
using MW.Application.Interfaces.Services.Media;
using MW.Application.Interfaces.Services.Messages;
using MW.Application.Models.Catalog;
using MW.Application.Models.Membership;
using MW.Application.Utilities.Defaults;
using MW.Application.Utilities.Helpers;
using MW.Domain.Entities.Catalog;
using MW.Domain.Entities.Membership;
using MW.Domain.Enums.Media;

namespace MW.Web.Controllers
{
    public class ProductController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileService _fileService;
        private readonly INotificationService _notificationService;
        private readonly IMapper _mapper;

        public ProductController(IUnitOfWork unitOfWork, IMapper mapper, IFileService fileService, INotificationService notificationService)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _fileService = fileService;
            _notificationService = notificationService;
        }
        public async Task<IActionResult> Index()
        {
            return RedirectToAction("List");
        }

        public async Task<IActionResult> List()
        {
            IEnumerable<Product> products = await _unitOfWork.Products.GetAllAsync(showDeactived: true);
            List<ProductListModel> productsModel = _mapper.Map<List<ProductListModel>>(products);
            return View(productsModel);
        }

        public async Task<IActionResult> Add()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(ProductAddModel productAddModel)
        {
            if (!ModelState.IsValid)
                return View(productAddModel);

            if (await _unitOfWork.Products.GetByBarcodeAsync(productAddModel.Barcode) != null)
            {
                _notificationService.ErrorNotification("Ürün barkodu kullanımda. Lütfen yeni bir barkod giriniz!");
                return View(productAddModel);
            }

            Product product = _mapper.Map<Product>(productAddModel);

            if (productAddModel.ProductImage != null)
            {
                var productImage = await _fileService.UploadAsync(productAddModel.ProductImage, productAddModel.Name, RegisteredFileType.ProductImage);
                product.ProductImageName = productImage.fileName;
            }
            else
                product.ProductImageName = FileNameDefaults.ProductImage;


            await _unitOfWork.Products.AddAsync(product);
            _notificationService.SuccessNotification("Ürün başarıyla oluşturuldu!");

            return RedirectToAction("List");
        }
        public async Task<IActionResult> Update(int id)
        {
            Product product = await _unitOfWork.Products.GetByIdAsync(id);

            if (product == null || product.Deleted)
            {
                _notificationService.ErrorNotification("Ürün bulunamadı!");
                return RedirectToAction("List");
            }

            ProductUpdateModel productUpdateModel = _mapper.Map<ProductUpdateModel>(product);
            return View(productUpdateModel);
        }
        [HttpPost]
        public async Task<IActionResult> Update(ProductUpdateModel productUpdateModel)
        {
            if (!ModelState.IsValid)
                return View(productUpdateModel);

            Product product = await _unitOfWork.Products.GetByIdAsync(productUpdateModel.Id);
            product = _mapper.Map(productUpdateModel, product);


            if (productUpdateModel.ProductImage != null)
            {
                var productImage = await _fileService.UploadAsync(productUpdateModel.ProductImage, productUpdateModel.Name, RegisteredFileType.ProductImage);
                product.ProductImageName = productImage.fileName;
            }

            await _unitOfWork.Products.UpdateAsync(product);
            _notificationService.SuccessNotification("Ürün başarıyla güncellendi!");

            return RedirectToAction("List");
        }
        public async Task<IActionResult> Delete(int id)
        {
            if (id < 1)
                return BadRequest("Geçersiz istek!");

            Product product = await _unitOfWork.Products.GetByIdAsync(id);

            if (product == null || product.Deleted)
                return NotFound("Ürün bulunamadı!");

            await _unitOfWork.Products.DeleteAsync(product.Id);
            return Ok("Ürün başarıyla silindi!");
        }

        [HttpPost]
        public async Task<IActionResult> AddStock([FromBody] StockHistoryAddModel stockHistoryAddModel)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState.FirstOrDefault().Value?.Errors.FirstOrDefault()?.ErrorMessage);

            Product product = await _unitOfWork.Products.GetByIdAsync(stockHistoryAddModel.ProductId);

            if (product == null || product.Deleted) return NotFound("Ürün bulumadı!");

            if ((product.Stock + stockHistoryAddModel.Quantity) < 0) return BadRequest("Stok eksiye düşürülemez!");

            StockHistory stockHistory = _mapper.Map<StockHistory>(stockHistoryAddModel);
            stockHistory.Comment = stockHistory.Comment + "(" + HttpContext.User.FindFirst("FullName")?.Value + ")";
            await _unitOfWork.StockHistories.AddAsync(stockHistory);

            await _unitOfWork.Products.AddStockAsync(product.Id, stockHistoryAddModel.Quantity);
            return Ok("Stok başarıyla eklendi!");

        }
    }
}
