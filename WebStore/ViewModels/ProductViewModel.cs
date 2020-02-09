using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebStore.ViewModels
{
    public class ListProductsViewModel
    {        
        [Required]
        public string Name { get; set; }

        [DataType(DataType.MultilineText)]
        [Required]
        public string Description { get; set; }

        [Required]
        public string Category { get; set; }

        [Required]
        public decimal Price { get; set; }
    }
}
