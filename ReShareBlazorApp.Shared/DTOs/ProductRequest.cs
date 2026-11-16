using ReShareBlazorApp.Api.Date.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReShareBlazorApp.Shared.DTOs
{
    public class ProductRequest
    {
        public int Number { get; set; } = 50;
        public ProductCategory? Category { get; set; }
        public string? Query { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
    }
}
