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
using MicroCrm.Application.Contacts.Queries;
using MicroCrm.Application.Contacts.Commands;

namespace MicroCrm.WebUI.Controllers
{
  public class ContactsController : Controller
  {
    private readonly IMediator mediator;
    private readonly IContactService contactService;
    private readonly IUnitOfWork unitOfWork;
    private readonly ILogger<ContactsController> _logger;
    private readonly IWebHostEnvironment _webHostEnvironment;
    public ContactsController(
          IMediator mediator,
          IContactService contactService,
          IUnitOfWork unitOfWork,
          IWebHostEnvironment webHostEnvironment,
          ILogger<ContactsController> logger)
    {
      this.mediator = mediator;
      this.contactService = contactService;
      this.unitOfWork = unitOfWork;
      this._logger = logger;
      this._webHostEnvironment = webHostEnvironment;
    }

    // GET: Contacts
    public IActionResult Index() => View();

    // GET: Contacts
    public virtual ActionResult List(int id)
    {
      return PartialView();
    }
    //data source
    //public async Task<JsonResult> GetData(int page = 1, int rows = 10, string sort = "Id", string order = "asc", string filterRules = "")
    public async Task<JsonResult> GetData(ContactPaginationQuery request)
    {
      try { 
        //var filters = PredicateBuilder.FromFilter<Contact>(filterRules);
        //var total = await this.contactService
        //                     .Query(filters).CountAsync();
        //var pagerows = (await this.contactService
        //                     .Query(filters)
        //                   .OrderBy(n => n.OrderBy($"{sort} {order}"))
        //                   .Skip(page - 1).Take(rows).SelectAsync())
        //                   .ToList();
        //var pagelist = new { total = total, rows = pagerows };
        //return Json(pagelist);
        var result= await this.mediator.Send(request);
        return Json(result);
      }
      catch (Exception e)
      {
        return Json(new { success = false, err = e.Message });
      }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    //public async Task<IActionResult> AddOrEdit(Contact request)
    public async Task<IActionResult> AddOrEdit(CreateOrEditContactCommand request)
    {
      //if (ModelState.IsValid)
      //{
      //  //Insert
      //  if (request.Id == 0)
      //  {
      //    this.contactService.Insert(request);
      //    await this.unitOfWork.SaveChangesAsync();

      //  }
      //  //Update
      //  else
      //  {
      //    try
      //    {
      //      this.contactService.Update(request);
      //      await this.unitOfWork.SaveChangesAsync();
      //    }
      //    catch (DbUpdateConcurrencyException)
      //    {
      //      if (!await this.contactService.ExistsAsync(request.Id))
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

      //Delete current record
      //GET: Contacts/Delete/:id
      [HttpGet]
    public async Task<JsonResult> Delete(int id)
    {
      try
      {
        await this.contactService.DeleteAsync(id);
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
    public async Task<JsonResult> DeleteChecked(DeleteContactCommand request)
    {
      //try
      //{
      //  foreach (var key in id)
      //  {
      //    await this.contactService.DeleteAsync(key);
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
    public async Task<JsonResult> AcceptChanges(Contact[] contacts)
    {
      if (ModelState.IsValid)
      {
        try
        {
          foreach (var item in contacts)
          {

            this.contactService.ApplyChanges(item);
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
      var fileName = "contacts_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";
      var filters = PredicateBuilder.FromFilter<Contact>(filterRules);
      var stream = await this.contactService.Export(filters, sort, order);
      return File(stream, "application/vnd.ms-excel", fileName);
    }
    //UploadImportExcel
    [HttpPost]
    public async Task<JsonResult> ImportExcel(List<IFormFile> uploadfiles) {
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
                  await this.contactService.ImportData(stream);
            total =await this.unitOfWork.SaveChangesAsync();
    
          }
        }

        watch.Stop();
        //Get the total running Logged measured by the current instance (in milliseconds)
        var elapsedTime = watch.ElapsedMilliseconds.ToString();
        return Json(new { success = true, total, elapsedTime });
      }
      catch (Exception e) {
        this.Response.StatusCode = 500;
        return Json(new { success = false, err=e.GetBaseException().Message });
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
  }
}
