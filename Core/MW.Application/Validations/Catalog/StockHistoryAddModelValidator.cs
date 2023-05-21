using FluentValidation;
using MW.Application.Models.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MW.Application.Validations.Catalog
{
    internal class StockHistoryAddModelValidator : AbstractValidator<StockHistoryAddModel>
    {
        public StockHistoryAddModelValidator()
        {
            RuleFor(stock => stock.ProductId).NotNull().WithMessage("Ürün boş olamaz!")
                                            .NotEmpty().WithMessage("Ürün boş olamaz!");

            RuleFor(stock => stock.Quantity).NotNull().WithMessage("Stok adedi boş olamaz!")
                                            .NotEmpty().WithMessage("Stok adedi boş olamaz!")
                                            .NotEqual(0).WithMessage("Stok adedi 0'dan büyük girilmelidir!");


        }
    }
}
