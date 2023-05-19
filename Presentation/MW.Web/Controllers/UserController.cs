using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MW.Application.Interfaces.Repositories;
using MW.Application.Interfaces.Services.Media;
using MW.Application.Interfaces.Services.Messages;
using MW.Application.Models.Membership;
using MW.Application.Utilities.Defaults;
using MW.Application.Utilities.Helpers;
using MW.Domain.Entities.Membership;
using MW.Domain.Enums.Media;

namespace MW.Web.Controllers
{
    public class UserController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileService _fileService;
        private readonly INotificationService _notificationService;
        private readonly IMapper _mapper;

        public UserController(IUnitOfWork unitOfWork, IMapper mapper, IFileService fileService, INotificationService notificationService)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _fileService = fileService;
            _notificationService = notificationService;
        }
        public IActionResult Index()
        {
            return RedirectToAction("List");
        }
        public async Task<IActionResult> List()
        {
            IEnumerable<User> userList = await _unitOfWork.Users.GetAllAsync(showDeactived:true);
            List<UserListModel> userListModel = _mapper.Map<List<UserListModel>>(userList);
            return View(userListModel);
        }
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(UserAddModel userAddModel)
        {
            if (!ModelState.IsValid)
                return View(userAddModel);

            if(await _unitOfWork.Users.GetByEmailAsync(userAddModel.Email) != null)
            {
                _notificationService.ErrorNotification("Email adresi kullanımda. Lütfen yeni bir email adresi giriniz!");
                return View(userAddModel);
            }

            User user = _mapper.Map<User>(userAddModel);
            user.PasswordHash = PasswordHashHelper.HashPassword(userAddModel.Password);

            if (userAddModel.AvatarImage != null)
            {
                var avatarImage = await _fileService.UploadAsync(userAddModel.AvatarImage, String.Concat(userAddModel.FirstName,"-",userAddModel.LastName), RegisteredFileType.AvatarImage);

                user.AvatarImageName = avatarImage.fileName;
            }
            else
                user.AvatarImageName = FileNameDefaults.AvatarImage;


            await _unitOfWork.Users.AddAsync(user);
            _notificationService.SuccessNotification("Kullanıcı başarıyla oluşturuldu!");

            return RedirectToAction("List");
        }
        public async Task<IActionResult> Update(int id)
        {

            User user = await _unitOfWork.Users.GetByIdAsync(id);

            if(user == null || user.Deleted)
            {
                _notificationService.ErrorNotification("Kullanıcı bulunamadı!");
                return RedirectToAction("List");
            }
            
            UserUpdateModel userUpdateModel = _mapper.Map<UserUpdateModel>(user);
            return View(userUpdateModel);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UserUpdateModel userUpdateModel)
        {
            if(!ModelState.IsValid)
                return View(userUpdateModel);

            User currentUser = await _unitOfWork.Users.GetByIdAsync(userUpdateModel.Id);
            currentUser = _mapper.Map(userUpdateModel, currentUser);

            if(!String.IsNullOrEmpty(userUpdateModel.Password))
                currentUser.PasswordHash = PasswordHashHelper.HashPassword(userUpdateModel.Password);

            if (userUpdateModel.AvatarImage != null)
            {
                var avatarImage = await _fileService.UploadAsync(userUpdateModel.AvatarImage, String.Concat(userUpdateModel.FirstName, "-", userUpdateModel.LastName), RegisteredFileType.AvatarImage);

                currentUser.AvatarImageName = avatarImage.fileName;
            }

            await _unitOfWork.Users.UpdateAsync(currentUser);
            _notificationService.SuccessNotification("Kullanıcı başarıyla güncellendi!");

            return RedirectToAction("List");
        }
        public async Task<IActionResult> Delete(int id)
        {
            if (id < 1)
                return BadRequest("Geçersiz istek!");

            User user = await _unitOfWork.Users.GetByIdAsync(id);

            if (user == null || user.Deleted)
                return NotFound("Kullanıcı bulunamadı!");

            await _unitOfWork.Users.DeleteAsync(user.Id);
            return Ok("Kullanıcı başarıyla silindi!");
        }
    }
}
