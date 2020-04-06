using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MyBoard.ViewModels;

namespace MyBoard.Models
{
  public class AutoMapping : Profile
  {
    public AutoMapping()
    {
      CreateMap<Advert, AdvertViewModel>().ReverseMap();
    }
  }
}
