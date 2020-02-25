using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Board.Models;
using Microsoft.AspNetCore.Http;

namespace Board.ViewModels
{
    public class AdvertCreateViewModel
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public Categor? Category { get; set; }

        public ProductNew? ProductIsNew { get; set; }
        [Required]
        public double Price { get; set; }

        public bool IsNegotiatedPrice { get; set; }
        [Required]
        public string Description { get; set; }

        public List<IFormFile> Photos { get; set; }

        public string UserId { get; set; }
    }
}
