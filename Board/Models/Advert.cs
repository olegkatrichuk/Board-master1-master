using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Board.Models;

namespace MyBoard.Models
{
    public class Advert
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public Categor? Category { get; set; }

        public ProductNew? ProductIsNew { get; set; }

        public double Price { get; set; }

        public bool IsNegotiatedPrice { get; set; }

        public string Description { get; set; }

        public User User { get; set; }

        public List<AdvertPhoto> AdvertPhotos { get; set; }

        public string UserId { get; set; }

        public DateTime DateStartTime { get; set; }

        public Citi Cities { get; set; }

        public string Phones { get; set; }

    }
}
