﻿using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Board.Models;
using Board.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.EntityFrameworkCore;
using MyBoard.Models;
using MyBoard.ViewModels;
using X.PagedList;

namespace MyBoard.Controllers
{
    //[Authorize]
    public class HomeController : Controller
    {
        private readonly IWebHostEnvironment _hostEnvironment;

        public AppDbContext Context { get; }

        public HomeController(AppDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _hostEnvironment = hostEnvironment;
            Context = context;
        }
        [AllowAnonymous]
        public async Task<IActionResult> Index(int page = 1)
        {
            int pageSize = 9;

            IQueryable<Advert> source = Context.Adverts.AsQueryable();
            var count = await source.CountAsync();
            var items = await source.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
            IndexViewModel viewModel = new IndexViewModel
            {
                PageViewModel = pageViewModel,
                Adverts = items
            };
            return View(viewModel);
            //return View();
        }

        [Authorize]
        public async Task<IActionResult> List()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var query = await Context.Adverts.Where(x => x.UserId == userId).ToListAsync();

            return View(query);
        }

        public async Task<IActionResult> ListAll(int page=1)
        {
            //var query = await Context.Adverts.ToListAsync();
            //return View(query);
            int pageSize = 9;

            IQueryable<Advert> source = Context.Adverts.AsQueryable();
            var count = await source.CountAsync();
            var items = await source.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
            IndexViewModel viewModel = new IndexViewModel
            {
                PageViewModel = pageViewModel,
                Adverts = items
            };
            return View(viewModel);
        }

        [HttpGet]
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public IActionResult Create(AdvertCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = ProcessUploadedFile(model);

                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                Advert newAdvert = new Advert
                {
                    UserId = userId,
                    Title = model.Title,
                    Category = model.Category,
                    ProductIsNew = model.ProductIsNew,
                    Price = model.Price,
                    IsNegotiatedPrice = model.IsNegotiatedPrice,
                    Description = model.Description,
                    PhotoPath = uniqueFileName
                };

                Context.Add(newAdvert);
                Context.SaveChanges();
                return RedirectToAction("Details", "Home", new { Id = newAdvert.Id });
            }

            return View();
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id != null)
            {
                Advert advert = await Context.Adverts.FirstOrDefaultAsync(p => p.Id == id);
                if (advert != null)
                    return View(advert);
            }
            return NotFound();
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult SetLanguage(string culture, string returnUrl)
        {
            Response.Cookies.Append(
              CookieRequestCultureProvider.DefaultCookieName,
              CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
              new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );

            return LocalRedirect(returnUrl);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            Advert advert = await Context.Adverts.FirstOrDefaultAsync(p => p.Id == id);
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            AdvertEditViewModel advertEditViewModel = new AdvertEditViewModel
            {
                UserId = userId,
                Id = advert.Id,
                Title = advert.Title,
                Category = advert.Category,
                ProductIsNew = advert.ProductIsNew,
                Price = advert.Price,
                IsNegotiatedPrice = advert.IsNegotiatedPrice,
                Description = advert.Description,
                ExistingPhotoPath = advert.PhotoPath
            };

            return View(advertEditViewModel);
        }

        private string ProcessUploadedFile(AdvertCreateViewModel model)
        {
            string uniqueFileName = null;
            if (model.Photos != null && model.Photos.Count > 0)
            {
                foreach (IFormFile photo in model.Photos)
                {
                    string uploadsFolder = Path.Combine(_hostEnvironment.WebRootPath, "images");
                    uniqueFileName = Guid.NewGuid() + "_" + photo.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    using var fileStream = new FileStream(filePath, FileMode.Create);
                    photo.CopyTo(fileStream);
                }

            }

            return uniqueFileName;
        }

        [HttpPost]
        public async Task<IActionResult> Edit(AdvertEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                Advert advert = await Context.Adverts.FirstOrDefaultAsync(p => p.Id == model.Id);
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                advert.Title = model.Title;
                advert.Category = model.Category;
                advert.ProductIsNew = model.ProductIsNew;
                advert.Price = model.Price;
                advert.IsNegotiatedPrice = model.IsNegotiatedPrice;
                advert.Description = model.Description;
                advert.UserId = userId;

                if (model.Photos != null)
                {
                    if (model.ExistingPhotoPath != null)
                    {
                        string filePath = Path.Combine(_hostEnvironment.WebRootPath, "images", model.ExistingPhotoPath);
                        System.IO.File.Delete(filePath);
                    }
                    advert.PhotoPath = ProcessUploadedFile(model);
                }

                Context.Update(advert);
                Context.SaveChanges();
                return RedirectToAction("List", "Home");
            }

            return View();
        }

        [HttpGet]
        [ActionName("Delete")]
        public async Task<IActionResult> ConfirmDelete(int? id)
        {
            if (id != null)
            {
                Advert advert = await Context.Adverts.FirstOrDefaultAsync(p => p.Id == id);
                if (advert != null)
                    return View(advert);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id != null)
            {
                Advert advert = await Context.Adverts.FirstOrDefaultAsync(p => p.Id == id);
                if (advert != null)
                {
                    Context.Adverts.Remove(advert);
                    await Context.SaveChangesAsync();
                    return RedirectToAction("List");
                }
            }
            return NotFound();
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [Authorize]
        [AcceptVerbs("Get", "Post")]
        public IActionResult ConvertToPdf(string sourceUrl)
        {
            SelectPdf.HtmlToPdf converter = new SelectPdf.HtmlToPdf();
            SelectPdf.PdfDocument doc = converter.ConvertUrl(sourceUrl);

            MemoryStream ms = new MemoryStream();
            doc.Save(ms);
            doc.Close();

            ms.Position = 0;

            FileStreamResult fileStreamResult = new FileStreamResult(ms, "application/pdf")
            {
                FileDownloadName = "Advert.pdf"
            };

            return fileStreamResult;
        }

        [HttpGet]
        public IActionResult ConvertToPdfAjax(string sourceUrl)
        {

            SelectPdf.HtmlToPdf converter = new SelectPdf.HtmlToPdf();
            SelectPdf.PdfDocument doc = converter.ConvertUrl(sourceUrl);

            MemoryStream ms = new MemoryStream();
            doc.Save(ms);
            doc.Close();

            ms.Position = 0;

            FileStreamResult fileStreamResult = new FileStreamResult(ms, "application/pdf")
            {
                FileDownloadName = "Advert.pdf"
            };

            return fileStreamResult;
        }

        [HttpGet]
        public IActionResult Search(string keyword, int page = 1)
        {
            if (keyword == null || keyword.Length < 3 || keyword.Length > 20)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                int pageSize = 8;
                var product = Context.Adverts.Where(p => p.Title.Contains(keyword));
                var model = new PagedList<Advert>(product.Include(x => x.Category), page,
                  pageSize);

                ViewBag.keyword = keyword;
                return View(model);
            }
        }

    }
}
