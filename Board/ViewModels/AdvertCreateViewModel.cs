﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Board.Models;
using Microsoft.AspNetCore.Http;
using MyBoard.Models;
using City = MyBoard.Models.City;

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

        public DateTime DateStart { get; set; }

        public City? City { get; set; }

        [Required]
        [Phone]
        public string Phone { get; set; }
    }
}
