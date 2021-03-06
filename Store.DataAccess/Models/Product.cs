﻿using System;
using System.Collections.Generic;

namespace Store.DataAccess.Models
{
    public partial class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public string Price { get; set; }
        public int? CategoryId { get; set; }
        public bool? IsActive { get; set; }
    }
}
