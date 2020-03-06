using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Board.Models;

namespace MyBoard.Models
{
    public class Advert
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Заголовок")]
        public string Title { get; set; }

        [Display(Name = "Категория")]
        public Categor? Category { get; set; }

        [Display(Name = "Новый б/у")]
        public ProductNew? ProductIsNew { get; set; }

        [Display(Name = "Цена")]
        public double Price { get; set; }

        [Display(Name = "Договорная цена")]
        public bool IsNegotiatedPrice { get; set; }

        [Display(Name = "Описание")]
        public string Description { get; set; }

        //[Display(Name = "Фотография")]
        //public string PhotoPath { get; set; }
        public User User { get; set; }

        [Display(Name = "Фотография")]
        public List<AdvertPhoto> AdvertPhotos { get; set; }
        public string UserId { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd'/'MM'/'yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DateStartTime { get; set; }

        [Display(Name = "Город")]
        public City? Cities { get; set; }

        [Display(Name = "Телефон")]
        [Phone]
        public string Phones { get; set; }

    }
}
