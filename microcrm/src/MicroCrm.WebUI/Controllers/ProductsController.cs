using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MicroCrm.Domain.Models;
using MicroCrm.Service;
using MicroCrm.WebUI.Extensions;
using URF.Core.Abstractions;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using MediatR;
using Newtonsoft.Json;
using MicroCrm.Application.Photos.Commands;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MicroCrm.WebUI.Controllers
{
  public class ProductsController : Controller
  {
    private readonly IProductService _productService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<ProductsController> _logger;
    private readonly IWebHostEnvironment _webHostEnvironment;
    private readonly IMediator _mediator;
    public ProductsController(IProductService productService,
          IUnitOfWork unitOfWork,
          IWebHostEnvironment webHostEnvironment,
          IMediator mediator,
          ILogger<ProductsController> logger)
    {
      _productService = productService;
      _unitOfWork = unitOfWork;
      _mediator = mediator;
      _logger = logger;
      _webHostEnvironment = webHostEnvironment;
    }

    // GET: Products
    public IActionResult Index()
    {
      var selectlist = new List<SelectListItem>();
      selectlist.Add(new SelectListItem() { Text = "No", Value = "0" });
      selectlist.Add(new SelectListItem() { Text = "Yes", Value = "1" });
      ViewBag.IsPrivate = selectlist;

      return View();
    }
    //data source
    public async Task<JsonResult> GetData(int page = 1, int rows = 10, string sort = "Id", string order = "asc", string filterRules = "")
    {
      try
      {
        var filters = PredicateBuilder.FromFilter<Product>(filterRules);
        var total = await _productService
                             .Query(filters).CountAsync();
        var pagerows = (await _productService
                             .Query(filters)
                           .OrderBy(n => n.OrderBy($"{sort} {order}"))
                           .Skip((page - 1) * rows).Take(rows).SelectAsync())
                           .ToList();
        var pagelist = new { total = total, rows = pagerows };
        return Json(pagelist);
      }
      catch (Exception e)
      {
        throw e;
      }

    }
    //Edit 
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<JsonResult> Edit(Product product)
    {
      if (ModelState.IsValid)
      {
        try
        {
          _productService.Update(product);
          var result = await _unitOfWork.SaveChangesAsync();
          return Json(new { success = true, result = result });
        }
        catch (Exception e)
        {
          return Json(new { success = false, err = e.GetBaseException().Message });
        }
      }
      else
      {
        var modelStateErrors = string.Join(",", this.ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(n => n.ErrorMessage)));
        return Json(new { success = false, err = modelStateErrors });
        //DisplayErrorMessage(modelStateErrors);
      }
      //return View(work);
    }
    //新建
    [HttpPost]
    [ValidateAntiForgeryToken]

    public async Task<JsonResult> Create([Bind("Name,Model,Unit,UnitPrice,Description")] Product product)
    {
      if (ModelState.IsValid)
      {
        try
        {
          _productService.Insert(product);
          var result = await _unitOfWork.SaveChangesAsync();

          return Json(new { success = true, result = result });
        }
        catch (Exception e)
        {
          return Json(new { success = false, err = e.GetBaseException().Message });
        }

        //DisplaySuccessMessage("Has update a Work record");
        //return RedirectToAction("Index");
      }
      else
      {
        var modelStateErrors = string.Join(",", this.ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(n => n.ErrorMessage)));
        return Json(new { success = false, err = modelStateErrors });
        //DisplayErrorMessage(modelStateErrors);
      }
      //return View(work);
    }
    //新建
    [HttpPost]
    [ValidateAntiForgeryToken]

    public async Task<JsonResult> Clone([Bind("Name,Model,Unit,UnitPrice,Description")] Product product)
    {
      if (ModelState.IsValid)
      {
        try
        {
          product.Id = 0;
          _productService.Insert(product);
          var result = await _unitOfWork.SaveChangesAsync();

          return Json(new { success = true, result = result });
        }
        catch (Exception e)
        {
          return Json(new { success = false, err = e.GetBaseException().Message });
        }

        //DisplaySuccessMessage("Has update a Work record");
        //return RedirectToAction("Index");
      }
      else
      {
        var modelStateErrors = string.Join(",", this.ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(n => n.ErrorMessage)));
        return Json(new { success = false, err = modelStateErrors });
        //DisplayErrorMessage(modelStateErrors);
      }
      //return View(work);
    }
    //Delete current record
    //GET: Products/Delete/:id
    [HttpGet]
    public async Task<JsonResult> Delete(int id)
    {
      try
      {
        await _productService.DeleteAsync(id);
        await _unitOfWork.SaveChangesAsync();
        return Json(new { success = true });
      }

      catch (Exception e)
      {
        return Json(new { success = false, err = e.GetBaseException().Message });
      }
    }
    //Delete the selected records
    [HttpPost]
    public async Task<JsonResult> DeleteChecked(int[] id)
    {
      try
      {
        foreach (var key in id)
        {
          await _productService.DeleteAsync(key);
        }
        await _unitOfWork.SaveChangesAsync();
        return Json(new { success = true });
      }
      catch (Exception e)
      {
        return Json(new { success = false, err = e.GetBaseException().Message });
      }
    }
    //Accept datagrid edit data
    [HttpPost]
    public async Task<JsonResult> AcceptChanges(Product[] products)
    {
      if (ModelState.IsValid)
      {
        try
        {
          foreach (var item in products)
          {
            _productService.ApplyChanges(item);
          }
          var result = await _unitOfWork.SaveChangesAsync();
          return Json(new { success = true, result });
        }
        catch (Exception e)
        {
          return Json(new { success = false, err = e.GetBaseException().Message });
        }
      }
      else
      {
        var modelStateErrors = string.Join(",", ModelState.Keys.SelectMany(key => ModelState[key].Errors.Select(n => n.ErrorMessage)));
        return Json(new { success = false, err = modelStateErrors });
      }

    }
    //ExportExcel
    [HttpPost]
    public async Task<IActionResult> ExportExcel(string filterRules = "", string sort = "Id", string order = "asc")
    {
      var fileName = "Product" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";
      var filters = PredicateBuilder.FromFilter<Product>(filterRules);
      var stream = await _productService.Export(filters, sort, order);
      return File(stream, "application/vnd.ms-excel", fileName);
    }
    //Importexcel
    [HttpPost]
    public async Task<IActionResult> ImportExcel()
    {
      try
      {
        var watch = new Stopwatch();
        watch.Start();
        var total = 0;
        if (Request.Form.Files.Count > 0)
        {
          for (var i = 0; i < this.Request.Form.Files.Count; i++)
          {
            var model = Request.Form["model"].FirstOrDefault() ?? "product";
            var folder = Request.Form["folder"].FirstOrDefault() ?? "product";
            var autosave = Convert.ToBoolean(Request.Form["autosave"].FirstOrDefault());
            var properties = (Request.Form["properties"].FirstOrDefault()?.Split(','));
            var file = Request.Form.Files[i];
            var filename = file.FileName;
            var contenttype = file.ContentType;
            var size = file.Length;
            var ext = Path.GetExtension(filename);
            var path = Path.Combine(this._webHostEnvironment.ContentRootPath, "UploadFiles", folder);
            if (!Directory.Exists(path))
            {
              Directory.CreateDirectory(path);
            }

            await _productService.ImportData(file.OpenReadStream());
            total = await _unitOfWork.SaveChangesAsync();

            if (autosave)
            {
              var filepath = Path.Combine(path, filename);
              file.OpenReadStream().Position = 0;

              using (var stream = System.IO.File.Create(filepath))
              {
                await file.CopyToAsync(stream);
              }
            }

          }
        }
        watch.Stop();
        return Json(new { success = true, total = total, elapsedTime = watch.ElapsedMilliseconds });
      }
      catch (Exception e)
      {
        Response.StatusCode = 500;
        this._logger.LogError(e, "ExcelImportFail");
        return this.Json(new { success = false, err = e.GetBaseException().Message });
      }
    }
    //Download the template
    public async Task<IActionResult> Download(string file)
    {

      this.Response.Cookies.Append("fileDownload", "true");
      var path = Path.Combine(this._webHostEnvironment.ContentRootPath, file);
      var downloadFile = new FileInfo(path);
      if (downloadFile.Exists)
      {
        var fileName = downloadFile.Name;
        var mimeType = MimeTypeConvert.FromExtension(downloadFile.Extension);
        var fileContent = new byte[Convert.ToInt32(downloadFile.Length)];
        using (var fs = downloadFile.Open(FileMode.Open, FileAccess.Read, FileShare.Read))
        {
          await fs.ReadAsync(fileContent, 0, Convert.ToInt32(downloadFile.Length));
        }
        return this.File(fileContent, mimeType, fileName);
      }
      else
      {
        throw new FileNotFoundException($"File {file} does not exist!");
      }
    }
    public async Task<JsonResult> SetProduct(int id = 0)
    {
      try
      {
        if (id > 0)
          HttpContext.Session.SetInt32("ProductId", id);
        return Json(new { success = true });
      }
      catch (Exception e)
      {
        return Json(new { success = false, err = e.Message });
      }
    }

    [HttpPost]
    public async Task<JsonResult> Upload(List<IFormFile> file, string name, string tag)
    {
      try
      {
        var fi = file[0];
        var path = Path.Combine(_webHostEnvironment.WebRootPath, "photos\\products");
        var filename = fi.FileName;
        var stream = new MemoryStream();
        await fi.CopyToAsync(stream);
        stream.Seek(0, SeekOrigin.Begin);
        var request = new AddPhotoCommand()
        {
          FileName = filename,
          Stream = stream,
          Path = path,
          Size = stream.Length
        };
        await _mediator.Send(request);

        return Json(new { success = true, result = "photos/products/" + filename });
      }
      catch (Exception e)
      {
        return Json(new { success = false, err = e.GetBaseException().Message });
      }
    }

    public async Task<JsonResult> GetById(int id)
    {
      try
      {
        var pro = _productService.Queryable().FirstOrDefault(e => e.Id.Equals(id));

        return Json(new { success = true, result = JsonConvert.SerializeObject(pro) });
      }
      catch (Exception e)
      {
        return Json(new { success = false, err = e.GetBaseException().Message });
      }
    }

  }
}
