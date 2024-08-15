using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MicroCrm.Domain.Models;
using MicroCrm.Service;
using MicroCrm.WebUI.Extensions;
using URF.Core.Abstractions;
using MediatR;
using MicroCrm.Application.Quotations.Queries;
using MicroCrm.Application.Quotations.Commands;
using Microsoft.AspNetCore.Mvc.Rendering;
using MicroCrm.WebUI.Data.Models;
using MicroCrm.WebUI.Data;
using MicroCrm.Application.AqDetails.Commands;

namespace MicroCrm.WebUI.Controllers
{
  public class QuotationsController : Controller
  {
    private readonly IMediator mediator;
    private readonly IQuotationService _quotationService;
    private readonly IContactService _contactService;
    private readonly ICompanyService _companyService;
    private readonly IProductService _productService;
    private readonly IAqDetailService _aqDetailService;
    private readonly IUnitOfWork unitOfWork;
    private readonly ILogger<QuotationsController> _logger;
    private readonly IWebHostEnvironment _webHostEnvironment;
    private readonly ApplicationDbContext _dbContext;
    public QuotationsController(
          IMediator mediator,
          IQuotationService quotationService,
          IContactService contactService,
          ICompanyService companyService,
          IProductService productService,
          IAqDetailService aqDetailService,
          IUnitOfWork unitOfWork,
          IWebHostEnvironment webHostEnvironment,
              ApplicationDbContext dbContext,
          ILogger<QuotationsController> logger)
    {
      this.mediator = mediator;
      _quotationService = quotationService;
      _contactService = contactService;
      _companyService = companyService;
      _productService = productService;
      _aqDetailService = aqDetailService;
      _dbContext = dbContext;
      this.unitOfWork = unitOfWork;
      _logger = logger;
      _webHostEnvironment = webHostEnvironment;
    }

    // GET: Quotations
    public async Task<IActionResult> Index()
    {
      var selectlist = new List<SelectListItem>();
      string filterRules = "";
      var filters = PredicateBuilder.FromFilter<Company>(filterRules);
      var datalist = (await _companyService.Query(filters)
                           .OrderBy(n => n.OrderBy($"{"Id"}  {"desc"}"))
                           .SelectAsync())
                           .Select(n => new
                           {
                             Id = n.Id,
                             FullName = n.FullName
                           }).ToList();
      foreach (var item in datalist)
      {
          selectlist.Add(new SelectListItem() { Text = item.FullName, Value = item.Id.ToString() });
      }
      ViewBag.Companies = selectlist;

      return View();
    }

    // GET: Quotations
    public virtual ActionResult List(int id)
    {
      return PartialView();
    }
    //data source
    public async Task<JsonResult> GetData(int page = 1, int rows = 10, string sort = "Id", string order = "asc", string filterRules = "")
    //public async Task<JsonResult> GetData(QuotationPaginationQuery request)
    {
      try
      {
        var filters = PredicateBuilder.FromFilter<Quotation>(filterRules);
        var total = await _quotationService
                             .Query(filters).CountAsync();
        var pagerows = (await _quotationService
                             .Query(filters)
                           .OrderBy(n => n.OrderBy($"{sort} {order}"))
                           .Skip((page - 1) * rows).Take(rows).SelectAsync())
                           .ToList();
        var pagelist = new { total = total, rows = pagerows };
        return Json(pagelist);
        //var result= await this.mediator.Send(request);
        //return Json(result);
      }
      catch (Exception e)
      {
        return Json(new { success = false, err = e.Message });
      }
    }

    public async Task<IActionResult> AddOrEdit(int id = 0, int companyId = 0, int contactId = 0)
    {
      Quotation model = new Quotation();

      if (id > 0)
        model = _quotationService.Queryable().FirstOrDefault(e => e.Id.Equals(id));

      var selectlist = new List<SelectListItem>();
      string filterRules = "";
      var filters = PredicateBuilder.FromFilter<Company>(filterRules);
      var datalist = (await _companyService.Query(filters)
                           .OrderBy(n => n.OrderBy($"{"Id"}  {"desc"}"))
                           .SelectAsync())
                           .Select(n => new
                           {
                             Id = n.Id,
                             FullName = n.FullName
                           }).ToList();
      foreach (var item in datalist)
      {
        selectlist.Add(new SelectListItem() { Text = item.FullName, Value = item.Id.ToString() });
      }
      ViewBag.Companies = selectlist;

      selectlist = new List<SelectListItem>();
      if (companyId > 0)
      {
        var datalist1 = _contactService.Queryable().Where(e => e.CompanyId.Equals(companyId))
                           .OrderBy(n => n.Name)
                           .Select(n => new { Id = n.Id, Name = n.Name, Title = n.Title, Department = n.Department }).ToList();
        foreach (var item in datalist1)
        {
          selectlist.Add(new SelectListItem() { Text = item.Name + " (" + item.Title + "-" + item.Department + ")", Value = item.Id.ToString() });
        }
      }
      else if (model.CompanyId > 0)
      {
        var datalist1 = _contactService.Queryable().Where(e => e.CompanyId.Equals(model.CompanyId))
                           .OrderBy(n => n.Name)
                           .Select(n => new { Id = n.Id, Name = n.Name, Title = n.Title, Department = n.Department }).ToList();
        foreach (var item in datalist1)
        {
          selectlist.Add(new SelectListItem() { Text = item.Name + " (" + item.Title + "-" + item.Department + ")", Value = item.Id.ToString() });
        }
      }
      ViewBag.Contacts = selectlist;

      var role = (string)ViewBag.Role;
      selectlist = new List<SelectListItem>();
      selectlist.Add(new SelectListItem() { Text = "Pending", Value = "0" });
      selectlist.Add(new SelectListItem() { Text = "On - going", Value = "1" });
      selectlist.Add(new SelectListItem() { Text = "LOST", Value = "2" });
      selectlist.Add(new SelectListItem() { Text = "SUBMIT", Value = "3" });
      if (role.ToLower() == "admin")
        selectlist.Add(new SelectListItem() { Text = "SOLD", Value = "4" });
      ViewBag.ProjectStatus = selectlist;

      if (contactId > 0)
      {
        var contact = _contactService.Queryable().FirstOrDefault(e => e.Id.Equals(contactId));
        if(contact != null)
        {
          model.Department = contact.Department;
          model.BusinessPhone = contact.BusinessPhone;
          model.Ext = contact.Ext;
          model.Phone = contact.Phone;
          model.Email = contact.Email;
        }
      }

        return PartialView(model);
    }

    public async Task<IActionResult> AddOrEditAqDetail(int id = 0, int aqId = 0)
    {
      AqDetail model = new AqDetail();

      if (id > 0)
        model = _aqDetailService.Queryable().FirstOrDefault(e => e.Id.Equals(id));
      else if (aqId > 0)
        model.QaId = aqId;

      var selectlist = new List<SelectListItem>();
      var filters1 = PredicateBuilder.FromFilter<Product>("");
      var datalist1 = (await _productService.Query(filters1)
                           .OrderBy(n => n.OrderBy($"{"Id"}  {"desc"}"))
                           .SelectAsync())
                           .Select(n => new
                           {
                             Id = n.Id,
                             Name = n.Name
                           }).ToList();
      foreach (var item in datalist1)
      {
        selectlist.Add(new SelectListItem() { Text = item.Name, Value = item.Id.ToString() });
      }
      ViewBag.Products = selectlist;

      var role = (string)ViewBag.Role;
      selectlist = new List<SelectListItem>();
      selectlist.Add(new SelectListItem() { Text = "Pending", Value = "0" });
      selectlist.Add(new SelectListItem() { Text = "On - going", Value = "1" });
      selectlist.Add(new SelectListItem() { Text = "LOST", Value = "2" });
      selectlist.Add(new SelectListItem() { Text = "SUBMIT", Value = "3" });
      if (role.ToLower() == "admin")
        selectlist.Add(new SelectListItem() { Text = "SOLD", Value = "4" });
      ViewBag.ProjectStatus = selectlist;

      return PartialView(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    //public async Task<IActionResult> AddOrEdit(Quotation request)
    public async Task<IActionResult> AddOrEdit(CreateOrEditQuotationCommand request)
    {
      //if (ModelState.IsValid)
      //{
      //  //Insert
      //  if (request.Id == 0)
      //  {
      //    this.quotationService.Insert(request);
      //    await this.unitOfWork.SaveChangesAsync();

      //  }
      //  //Update
      //  else
      //  {
      //    try
      //    {
      //      this.quotationService.Update(request);
      //      await this.unitOfWork.SaveChangesAsync();
      //    }
      //    catch (DbUpdateConcurrencyException)
      //    {
      //      if (!await this.quotationService.ExistsAsync(request.Id))
      //      { return NotFound(); }
      //      else
      //      { throw; }
      //    }
      //  }
      //  return Json(new { success = true });
      //}
      //else
      //{
      //  var modelStateErrors = string.Join(",", this.ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(n => n.ErrorMessage)));
      //  return Json(new { success = false, err = modelStateErrors });
      //}
      try
      {
        await this.mediator.Send(request);
        return Json(new { success = true });
      }
      catch (Exception e)
      {
        return Json(new { success = false, err = e.Message });
      }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddOrEditAqDetail(CreateOrEditAqDetailCommand request)
    {
      try
      {
        await this.mediator.Send(request);
        return Json(new { success = true });
      }
      catch (Exception e)
      {
        return Json(new { success = false, err = e.Message });
      }
    }

    //Delete current record
    //GET: Quotations/Delete/:id
    [HttpGet]
    public async Task<JsonResult> Delete(int id)
    {
      try
      {
        await _quotationService.DeleteAsync(id);
        await this.unitOfWork.SaveChangesAsync();
        return Json(new { success = true });
      }

      catch (Exception e)
      {
        return Json(new { success = false, err = e.GetBaseException().Message });
      }
    }
    //Delete the selected records
    [HttpPost]
    //public async Task<JsonResult> DeleteChecked(int[] id)
    public async Task<JsonResult> DeleteChecked(DeleteQuotationCommand request)
    {
      //try
      //{
      //  foreach (var key in id)
      //  {
      //    await _quotationService.DeleteAsync(key);
      //  }
      //  await this.unitOfWork.SaveChangesAsync();
      //  return Json(new { success = true });
      //}
      //catch (Exception e)
      //{
      //  return Json(new { success = false, err = e.GetBaseException().Message });
      //}

      try
      {

        await this.mediator.Send(request);
        return Json(new { success = true });
      }
      catch (Exception e)
      {
        return Json(new { success = false, err = e.GetBaseException().Message });
      }
    }
    //Accept datagrid edit data
    [HttpPost]
    public async Task<JsonResult> AcceptChanges(Quotation[] quotations)
    {
      if (ModelState.IsValid)
      {
        try
        {
          foreach (var item in quotations)
          {

            _quotationService.ApplyChanges(item);
          }
          var result = await this.unitOfWork.SaveChangesAsync();
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
    public async Task<ActionResult> ExportExcel(string filterRules = "", string sort = "Id", string order = "asc")
    {
      var fileName = "quotations_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";
      var filters = PredicateBuilder.FromFilter<Quotation>(filterRules);
      var stream = await _quotationService.Export(filters, sort, order);
      return File(stream, "application/vnd.ms-excel", fileName);
    }
    //UploadImportExcel
    [HttpPost]
    public async Task<JsonResult> ImportExcel(List<IFormFile> uploadfiles)
    {
      try
      {
        var total = 0m;
        var watch = new Stopwatch();
        watch.Start();
        foreach (var formFile in uploadfiles)
        {
          if (formFile.Length > 0)
          {
            var ext = System.IO.Path.GetExtension(formFile.FileName);
            var stream = new MemoryStream();
            await formFile.CopyToAsync(stream);
            stream.Seek(0, SeekOrigin.Begin);
            await _quotationService.ImportData(stream);
            total = await this.unitOfWork.SaveChangesAsync();

          }
        }

        watch.Stop();
        //Get the total running Logged measured by the current instance (in milliseconds)
        var elapsedTime = watch.ElapsedMilliseconds.ToString();
        return Json(new { success = true, total, elapsedTime });
      }
      catch (Exception e)
      {
        this.Response.StatusCode = 500;
        return Json(new { success = false, err = e.GetBaseException().Message });
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
    public async Task<IActionResult> Print(int id)
    {
      PrintQuotation model = new PrintQuotation();
      return PartialView(model);
    }
  }
}
