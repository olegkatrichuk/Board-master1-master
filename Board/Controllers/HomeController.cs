using System;
using System.ComponentModel.DataAnnotations;
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

namespace MyBoard.Controllers
{
    public class HomeController : Controller
    {
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly AppDbContext _context;

       
        public HomeController(AppDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _hostEnvironment = hostEnvironment;
            _context = context;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index(int page = 1)
        {
            Log logger = new Log(this , "Hello");
            
            logger.WriteToFile();

            var result = _context.Adverts.OrderByDescending(x => x.DateStartTime);
            int pageSize = 9;

            var count = await result.CountAsync();
            var items = await result.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
            IndexViewModel viewModel = new IndexViewModel
            {
                PageViewModel = pageViewModel,
                Adverts = items
            };
            return View(viewModel);
        }

        [Authorize]
        public async Task<IActionResult> List()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var query = await _context.Adverts.Where(x => x.UserId == userId).ToListAsync();
            query.Reverse();
            return View(query);
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
                    PhotoPath = uniqueFileName,
                    DateStartTime = DateTime.Now,
                    City = model.Citis,
                    Phone = model.Phone
                };

                _context.Add(newAdvert);
                _context.SaveChanges();
                return RedirectToAction("Details", "Home", new { Id = newAdvert.Id });
            }

            return View();
        }
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id != null)
            {
                Advert advert = await _context.Adverts.FirstOrDefaultAsync(p => p.Id == id);
                if (advert != null)
                    return View(advert);
            }
            return NotFound();
        }
        [AllowAnonymous]
        public async Task<IActionResult> DetailsAdvert(int? id)
        {
            if (id != null)
            {
                Advert advert = await _context.Adverts.FirstOrDefaultAsync(p => p.Id == id);
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
            Advert advert = await _context.Adverts.FirstOrDefaultAsync(p => p.Id == id);
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
                ExistingPhotoPath = advert.PhotoPath,
                DateStart = DateTime.Now,
                Citis = advert.City,
                Phone = advert.Phone
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
                Advert advert = await _context.Adverts.FirstOrDefaultAsync(p => p.Id == model.Id);
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                advert.Title = model.Title;
                advert.Category = model.Category;
                advert.ProductIsNew = model.ProductIsNew;
                advert.Price = model.Price;
                advert.IsNegotiatedPrice = model.IsNegotiatedPrice;
                advert.Description = model.Description;
                advert.UserId = userId;
                advert.DateStartTime = DateTime.Now;
                advert.City = model.Citis;
                advert.Phone = model.Phone;

                if (model.Photos != null)
                {
                    if (model.ExistingPhotoPath != null)
                    {
                        string filePath = Path.Combine(_hostEnvironment.WebRootPath, "images", model.ExistingPhotoPath);
                        System.IO.File.Delete(filePath);
                    }
                    advert.PhotoPath = ProcessUploadedFile(model);
                }

                _context.Update(advert);
                _context.SaveChanges();
                return RedirectToAction("List", "Home", new { Id = advert.Id });
            }

            return View();
        }

        [HttpGet]
        [ActionName("Delete")]
        public async Task<IActionResult> ConfirmDelete(int? id)
        {
            if (id != null)
            {
                Advert advert = await _context.Adverts.FirstOrDefaultAsync(p => p.Id == id);
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
                Advert advert = await _context.Adverts.FirstOrDefaultAsync(p => p.Id == id);
                if (advert != null)
                {
                    _context.Adverts.Remove(advert);
                    await _context.SaveChangesAsync();
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

        [HttpPost]
        public async Task<IActionResult> Search(string keyword, string selectedCitys, int page = 1)
        {
            if (selectedCitys == null)
            {
                return RedirectToAction("Index", "Home");
            }
            if (keyword == null || keyword.Length < 3 || keyword.Length > 20)
            {
                return RedirectToAction("Index", "Home");
            }

            City city = (City)System.Enum.Parse(typeof(City), selectedCitys);

            var result = _context.Adverts.OrderByDescending(x => x.DateStartTime);
            int pageSize = 9;

            var product = result.Where(p => p.Title.Contains(keyword)).Where(p => p.City == city);

            var count = await product.CountAsync();
            var items = await product.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
            IndexViewModel viewModel = new IndexViewModel
            {
                PageViewModel = pageViewModel,
                Adverts = items
            };
            ViewBag.citykeyword = selectedCitys;
            ViewBag.keyword = keyword;
            return View(viewModel);
        }

        public async Task<IActionResult> SearchByCategory(string keyword, int page = 1)
        {
            if (keyword == null || keyword.Length < 3 || keyword.Length > 20)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                Categor category = (Categor)System.Enum.Parse(typeof(Categor), keyword);

                var result = _context.Adverts.OrderByDescending(x => x.DateStartTime).Where(advert => advert.Category == category); ;

                int pageSize = 9;

                var count = await result.CountAsync();
                var items = await result.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

                PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
                IndexViewModel viewModel = new IndexViewModel
                {
                    PageViewModel = pageViewModel,
                    Adverts = items
                };

                ViewBag.keyword = keyword;
                return View(viewModel);

            }
        }
    }
}
