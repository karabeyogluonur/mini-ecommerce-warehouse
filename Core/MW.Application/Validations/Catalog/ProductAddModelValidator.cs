using FluentValidation;
using MW.Application.Models.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MW.Application.Validations.Catalog
{
    public class ProductAddModelValidator : AbstractValidator<ProductAddModel>
    {
        public ProductAddModelValidator()
        {
            RuleFor(product => product.Name).NotEmpty().WithMessage("Ürün adı boş geçilemez.")
                                            .NotNull().WithMessage("Ürün adı boş geçilemez.")
                                            .Length(3, 120).WithMessage("Ürün adı 3 ile 120 karakter arasında olmalıdır.");

            RuleFor(product => product.Barcode).NotEmpty().WithMessage("Ürün barkodu boş geçilemez.")
                                               .NotNull().WithMessage("Ürün barkodu boş geçilemez.");

            RuleFor(product => product.WarehouseCode).NotEmpty().WithMessage("Ürün depo kodu boş geçilemez.")
                                            .NotNull().WithMessage("Ürün depo kodu boş geçilemez.")
                                            .Length(2, 50).WithMessage("Ürün depo kodu 2 ile 50 karakter arasında olmalıdır.");

            RuleFor(product => product.SalePrice).NotNull().WithMessage("Ürün satış fiyatı boş geçilemez.")
                                                 .NotEmpty().WithMessage("Ürün satış fiyatı boş geçilemez.")
                                                 .GreaterThan(0).WithMessage("Geçerli bir satış fiyatı giriniz.");

            RuleFor(product => product.PurchasePrice).NotNull().WithMessage("Ürün alış fiyatı boş geçilemez.")
                                                 .NotEmpty().WithMessage("Ürün alış fiyatı boş geçilemez.")
                                                 .GreaterThan(0).WithMessage("Geçerli bir alış fiyatı giriniz.");

        }
    }
}
