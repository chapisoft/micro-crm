using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MicroCrm.Service;
using MicroCrm.WebUI.Data;
using MicroCrm.WebUI.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MicroCrm.WebUI.Controllers
{
  public class PrintController : Controller
  {
    private readonly IQuotationService _quotationService;
    private readonly IAqDetailService _aqDetailService;
    private readonly ICompanyService _companyService;
    private readonly IProductService _productService;
    private readonly ILogger<PrintController> _logger;
    private readonly ApplicationDbContext _dbContext;
    public PrintController(
          IQuotationService quotationService,
          IAqDetailService aqDetailService,
          ICompanyService companyService,
          IProductService productService,
              ApplicationDbContext dbContext,
          ILogger<PrintController> logger)
    {
      _quotationService = quotationService;
      _aqDetailService = aqDetailService;
      _companyService = companyService;
      _productService = productService;
      _dbContext = dbContext;
      _logger = logger;
    }
    public async Task<IActionResult> Index(int id, string lang)
    {
      PrintQuotation model = new PrintQuotation();
      model.Lang = lang;
      try
      {
        model.Tenant = await _dbContext.Tenants.FindAsync(int.Parse(ViewBag.TenantId.ToString()));
        model.Info = await _quotationService.FindAsync(id);
        model.Company = await _companyService.FindAsync(model.Info.CompanyId);
        var aqs = _aqDetailService.Queryable().Where(e => e.QaId.Equals(id) && e.Subsidiary == 0).ToList();
        List<QuotationItem> list = new List<QuotationItem>();
        QuotationItem item = new QuotationItem();
        foreach(var aq in aqs)
        {
          item = new QuotationItem();
          item.PartNo = aq.PartNo;
          item.ItemName = aq.ItemName;
          item.Qty = aq.Qty;
          item.Price = aq.Price;
          item.ShipFee = aq.ShipFee;
          item.Margin = aq.Margin;
          item.Discount = aq.Discount;
          item.Vat = aq.Vat;
          item.ImportTax = aq.ImportTax;
          item.OtherFee = aq.OtherFee;
          item.Exchange = aq.Exchange;
          item.Amount = aq.Amount;
          //var pro = _productService.Queryable().FirstOrDefault(e => e.Id.Equals(aq.ProductId));
          item.Description = aq.Description;
          item.Image = aq.ImagePath;
          item.Unit = aq.Unit;
          item.NameEn = aq.NameEn;
          item.DescriptionEn = aq.DescriptionEn;
          list.Add(item);
        }
        model.Details = list;

        aqs = _aqDetailService.Queryable().Where(e => e.QaId.Equals(id) && e.Subsidiary == 1).ToList();
        list = new List<QuotationItem>();
        if (aqs.Any())
        {
          item = new QuotationItem();
          foreach (var aq in aqs)
          {
            item = new QuotationItem();
            item.PartNo = aq.PartNo;
            item.ItemName = aq.ItemName;
            item.Qty = aq.Qty;
            item.Price = aq.Price;
            item.ShipFee = aq.ShipFee;
            item.Margin = aq.Margin;
            item.Discount = aq.Discount;
            item.Vat = aq.Vat;
            item.ImportTax = aq.ImportTax;
            item.OtherFee = aq.OtherFee;
            item.Exchange = aq.Exchange;
            item.Amount = aq.Amount;
            //var pro = _productService.Queryable().FirstOrDefault(e => e.Id.Equals(aq.ProductId));
            item.Description = aq.Description;
            item.Image = aq.ImagePath;
            item.Unit = aq.Unit;
            item.NameEn = aq.NameEn;
            item.DescriptionEn = aq.DescriptionEn;
            list.Add(item);
          }
        }
        model.Subsidiary = list;
      }
      catch (Exception ex)
      {
        throw;
      }
      return View(model);
    }
  }
}
