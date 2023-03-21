using Payroll_test.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Payroll_test.Controllers
{
    public class SalaryController : Controller
    {
        // GET: Salary
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GrossSalaryToNet()
        {
            return View();
        }
        [HttpPost]
        public ActionResult GrossSalaryToNet(Salary model)
        {
            // tien tru 3 loai bao hiem
            model.premium = Decimal.Multiply(model.premiumSalary, (decimal)0.105) ;

            //so tien bi tinh thue thu nhap neu >11tr
            model.incomeBeforeTax = model.salary - (decimal)model.premium;
            if (model.incomeBeforeTax > 11000000)
            {
                //thu nhập chịu thuế
                model.incomeTaxes = model.incomeBeforeTax - 11000000 - (model.numberOfDependents * 4400000);
                if (model.incomeTaxes > 0)
                {
                    if (model.incomeTaxes <= 5000000)
                    {
                        model.personalIncomeTax = Decimal.Multiply(model.incomeTaxes, (decimal)0.05);
                    }
                    else if (model.incomeTaxes > 5000000 && model.incomeTaxes <= 10000000)
                    {
                        model.personalIncomeTax = Decimal.Multiply(model.incomeTaxes, (decimal)0.1) - 250000;
                    }
                    else if (model.incomeTaxes > 10000000 && model.incomeTaxes <= 18000000)
                    {
                        model.personalIncomeTax = Decimal.Multiply(model.incomeTaxes, (decimal)0.15) - 750000;
                    }
                    else if (model.incomeTaxes > 18000000 && model.incomeTaxes <= 32000000)
                    {
                        model.personalIncomeTax = Decimal.Multiply(model.incomeTaxes, (decimal)0.2) - 1650000;
                    }
                    else if (model.incomeTaxes > 32000000 && model.incomeTaxes <= 52000000)
                    {
                        model.personalIncomeTax = Decimal.Multiply(model.incomeTaxes, (decimal)0.25) - 3250000;
                    }
                    else if (model.incomeTaxes > 52000000 && model.incomeTaxes <= 80000000)
                    {
                        model.personalIncomeTax = Decimal.Multiply(model.incomeTaxes, (decimal)0.3) - 5850000;
                    }
                    else if (model.incomeTaxes > 80000000)
                    {
                        model.personalIncomeTax = Decimal.Multiply(model.incomeTaxes, (decimal)0.35) - 9850000;
                    }
                }
                else
                {
                    model.incomeTaxes = 0;
                }
                model.finalSalary = model.salary - (decimal)model.premium - model.personalIncomeTax;
            }
            return View(model);
        }

        public ActionResult NetSalaryToGross()
        {
            return View();
        }
    }
}