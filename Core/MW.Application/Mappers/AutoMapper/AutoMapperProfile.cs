﻿using AutoMapper;
using MW.Application.Models.Catalog;
using MW.Application.Models.Membership;
using MW.Domain.Entities.Catalog;
using MW.Domain.Entities.Membership;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MW.Application.Mappers.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<UserUpdateModel, User>().ForMember(user=>user.AvatarImageName,opt=>opt.Ignore()).ReverseMap();
            CreateMap<User, UserListModel>().ReverseMap();
            CreateMap<User, UserAddModel>().ReverseMap();

            CreateMap<ProductUpdateModel, Product>().ForMember(product => product.ProductImageName, opt => opt.Ignore()).ReverseMap();
            CreateMap<Product, ProductListModel>().ReverseMap();
            CreateMap<Product, ProductAddModel>().ReverseMap();

            CreateMap<StockHistory, StockHistoryAddModel>().ReverseMap();
        }

    }
}
