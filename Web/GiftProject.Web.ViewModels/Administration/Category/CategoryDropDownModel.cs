﻿namespace GiftProject.Web.ViewModels.Administration.Category
{
    using System.Collections.Generic;
    using System.ComponentModel;

    using GiftProject.Services.Mapping;
    using GiftProject.Web.ViewModels.Product;

    public class CategoryDropDownModel :IMapFrom<Data.Models.Category>
    {
        public int Id { get; set; }

        [DisplayName("Категория")]
        public string Name { get; set; }

        public IEnumerable<ProductsViewModel> Products { get; set; }
    }
}
