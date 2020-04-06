using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Board.Models;
using Microsoft.AspNetCore.Mvc;
using MyBoard.Models;

namespace MyBoard.Controllers
{
    public class RegionController : Controller
    {
      private readonly AppDbContext _context;

      public RegionController(AppDbContext context)
      {
        _context = context;
      }

      public List<State> GetRegion(int id)
      {
        var result = _context.States.Where(a => a.CitiId == id);
        return result.ToList();
      }

      public IActionResult Index()
        {
            return View();
        }


    }
}