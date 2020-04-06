﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Azure.Storage;
using Azure.Storage.Blobs;
using Board.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyBoard.Models;
using MyBoard.ViewModels;
using cloudscribe.Pagination.Models;
using MimeKit;
using MimeKit.Text;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using MyBoard.Helpers;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;

namespace MyBoard.Controllers
{
  public class HomeController : Controller
  {
    private readonly IWebHostEnvironment _hostEnvironment;
    private readonly IConfiguration _configuration;
    private readonly IMapper _mapper;
    private readonly AzureStorageConfig _storageConfig;
    private readonly AppDbContext _context;

    public HomeController(AppDbContext context, IWebHostEnvironment hostEnvironment, IConfiguration configuration, IOptions<AzureStorageConfig> config,IMapper mapper)
    {
      _hostEnvironment = hostEnvironment;
      _configuration = configuration;
      _mapper = mapper;
      _context = context;
      _storageConfig = config.Value;
    }

    [AllowAnonymous]
    public IActionResult Index(int pageNumber = 1, int size = 9)
    {
      //var logger = new Log(this, "Hello");
      //logger.WriteToFile();

      int excludeRecords = (size * pageNumber) - size;

      var result = _context.Adverts.OrderByDescending(x => x.DateStartTime).Include(c => c.AdvertPhotos).Skip(excludeRecords).Take(size);

      var res = new PagedResult<Advert>
      {
        Data = result.AsNoTracking().ToList(),
        TotalItems = _context.Adverts.Count(),
        PageNumber = pageNumber,
        PageSize = size
      };

      return View(res);
    }

    [Authorize]
    public async Task<IActionResult> List()
    {
      var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

      var query = await _context.Adverts.Include(c => c.AdvertPhotos).Where(x => x.UserId == userId).ToListAsync();
      query.Reverse();
      return View(query);
    }

    [HttpGet]
    [Authorize]
    public IActionResult Create()
    {
      List<TreeViewNode> nodes = new List<TreeViewNode>();

      foreach (Citi type in _context.Citi)
      {
        nodes.Add(new TreeViewNode { Id = type.Id, Parent = "#", Name = type.Name, NameUa = type.NameUa});
      }

      //foreach (State subType in _context.States)
      //{
      //  nodes.Add(new TreeViewNode { Id = subType.Id, Parent = subType.CitiId.ToString()});
      //}

      ViewBag.All = nodes;
      return View();
    }

    [HttpPost]
    [Authorize]
    public IActionResult Create(AdvertCreateViewModel model)
    {
      if (ModelState.IsValid)
      {
        List<AdvertPhoto> listPath = new List<AdvertPhoto>();
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        
        AdvertViewModel newAdvert = new AdvertViewModel
        {
          UserId = userId,
          Title = model.Title,
          Category = model.Category,
          ProductIsNew = model.ProductIsNew,
          Price = model.Price,
          IsNegotiatedPrice = model.IsNegotiatedPrice,
          Description = model.Description,
          AdvertPhotos = listPath,
          DateStartTime = DateTime.Now,
          Cities = model.City,
          Phones = model.Phone
        };

        foreach (string photo in model.Photos ?? new List<string>())
        {
          listPath.Add(new AdvertPhoto()
          {
            PhotoPath = photo
          });
        }

        newAdvert.AdvertPhotos = listPath;
        var result = _mapper.Map<Advert>(newAdvert);
        _context.Adverts.Add(result);
        _context.SaveChanges();
        return RedirectToAction("Details", "Home", new { result.Id });
      }

      return View();
    }

    public async Task<IActionResult> Details(int? id)
    {
      if (id != null)
      {
        Advert advert = await _context.Adverts.Include(c => c.AdvertPhotos).FirstOrDefaultAsync(p => p.Id == id);
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
        Advert advert = await _context.Adverts.Include(c => c.AdvertPhotos)
            .FirstOrDefaultAsync(p => p.Id == id);
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
          DateStart = DateTime.Now,
          City = advert.Cities,
          Phone = advert.Phones,
          ImageStrings = advert.AdvertPhotos.Select(x => x.PhotoPath).ToList()
        };
        return View(advertEditViewModel);
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
        new CookieOptions
        {
          Expires = DateTimeOffset.UtcNow.AddYears(1),
        }
      );

      return LocalRedirect(returnUrl);
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> Edit(int? id)
    {
      Advert advert = await _context.Adverts.Include(c => c.AdvertPhotos).FirstOrDefaultAsync(p => p.Id == id);
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
        DateStart = DateTime.Now,
        City = advert.Cities,
        Phone = advert.Phones,
        ImageStrings = advert.AdvertPhotos.Select(x => x.PhotoPath).ToList()
      };

      return View(advertEditViewModel);
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Edit([FromFormAttribute]AdvertEditViewModel model)
    {
      if (ModelState.IsValid)
      {

        Advert advert = await _context.Adverts.Include(c => c.AdvertPhotos).FirstOrDefaultAsync(p => p.Id == model.Id);
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        List<AdvertPhoto> listPath = advert.AdvertPhotos;

        advert.Title = model.Title;
        advert.Category = model.Category;
        advert.ProductIsNew = model.ProductIsNew;
        advert.Price = model.Price;
        advert.IsNegotiatedPrice = model.IsNegotiatedPrice;
        advert.Description = model.Description;
        advert.UserId = userId;
        advert.DateStartTime = DateTime.Now;
        advert.Cities = model.City;
        advert.Phones = model.Phone;

        foreach (var photo in model.PhotoUrlToDelete ?? new List<string>())
        {
          AdvertPhoto advertPhoto =
              _context.AdvertPhotos.FirstOrDefault(p => p.PhotoPath == photo && p.AdvertId == advert.Id);

          if (!string.IsNullOrEmpty(photo) && advertPhoto != null)
          {
            string filePath = Path.Combine("https://", _storageConfig.AccountName, ".blob.core.windows.net/", _storageConfig.ImageContainer, photo);
            UploadImageUtilities.DeleteImage(filePath);
            _context.AdvertPhotos.Remove(advertPhoto);
            _context.SaveChanges();
          }
        }

        foreach (string photo in model.Photos ?? new List<string>())
        {
          listPath.Add(new AdvertPhoto()
          {
            PhotoPath = photo
          });
        }

        advert.AdvertPhotos = listPath;

        _context.Adverts.Update(advert);
        _context.SaveChanges();
        return RedirectToAction("List", "Home", new { advert.Id });
      }
      return View();
    }

    [HttpGet]
    [ActionName("Delete")]
    public async Task<IActionResult> ConfirmDelete(int? id)
    {
      if (id != null)
      {
        Advert advert = await _context.Adverts.Include(c => c.AdvertPhotos).FirstOrDefaultAsync(p => p.Id == id);
        if (advert != null)
          return View(advert);
      }
      return NotFound();
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Delete(int? id)
    {
      if (id != null)
      {
        Advert advert = await _context.Adverts.Include(c => c.AdvertPhotos).FirstOrDefaultAsync(p => p.Id == id);
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

    [AllowAnonymous]
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
    [AllowAnonymous]
    public async Task<IActionResult> Search(string keyword, string selectedCity, int page = 1)
    {
      if (selectedCity == null)
      {
        return RedirectToAction("Index", "Home");
      }
      if (keyword == null || keyword.Length < 2 || keyword.Length > 20)
      {
        return RedirectToAction("Index", "Home");
      }
    
      var result = _context.Adverts.OrderByDescending(x => x.DateStartTime).Include(c => c.AdvertPhotos);
      int pageSize = 9;

      string str1 = keyword.Remove(2);
      string str2 = keyword.Substring(1, keyword.Length - 1);

      var product = result.Where(t => t.Title.StartsWith(str1) || t.Title.EndsWith(str2) || t.Title.Contains(keyword)).Where(p => p.Cities.ToString()==selectedCity);

      //var count = await product.CountAsync();
      //var items = await product.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

      //PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
      //IndexViewModel viewModel = new IndexViewModel
      //{
      //  PageViewModel = pageViewModel,
      //  Adverts = items
      //};

      ViewBag.citykeyword = selectedCity;
      ViewBag.keyword = keyword;
      //return View(viewModel);
      return View();
    }

    [AllowAnonymous]
    public async Task<IActionResult> SearchByCategory(string keyword, int page = 1)
    {
      if (keyword == null || keyword.Length < 3 || keyword.Length > 20)
      {
        return RedirectToAction("Index", "Home");
      }

     
      Categor category = (Categor)System.Enum.Parse(typeof(Categor), keyword);

      var result = _context.Adverts.OrderByDescending(x => x.DateStartTime).Where(advert => advert.Category == category).Include(c => c.AdvertPhotos);

      int pageSize = 3;

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

    [HttpGet]
    public IActionResult Contact()
    {
      return View();
    }

    [HttpPost]
    public string Contact(ContactViewModel contactViewModel)
    {
      if (ModelState.IsValid)
      {
        try
        {
          var message = new MimeMessage();
          message.To.Add(new MailboxAddress("Board", "malt22222222@gmail.com"));
          message.From.Add(new MailboxAddress(contactViewModel.Name, contactViewModel.Email));
          message.Subject = contactViewModel.Subject;
          message.Body = new TextPart(TextFormat.Html)
          {
            Text = contactViewModel.Message + " Message was sent by: " + contactViewModel.Name + " E-mail: " + contactViewModel.Email
          };

          using var emailClient = new SmtpClient();
          emailClient.Connect("smtp.gmail.com", 587, false);
          emailClient.Authenticate("malt22222222@gmail.com", "Ruslan280222@");
          emailClient.Send(message);
          emailClient.Disconnect(true);
        }
        catch (Exception ex)
        {
          ModelState.Clear();
          ViewBag.Message = $" Oops! We have a problem here {ex.Message}";
        }
      }
      return "Письмо успешно отправлено";
    }
  }
}

