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
        public decimal baseSalary = 1490000;
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
            if(model.premiumSalary < model.area)
            {
                model.premiumSalary = 0;
                ViewData["infoError"] = "Mức lương đóng bảo hiểm phải lớn hơn hoặc bằng mức lương tối thiểu vùng!";
                return View(model);
            }

            //bảo hiểm lúc đầu
            model.socialInsurance = Decimal.Multiply(model.premiumSalary, (decimal)0.08);
            model.healthInsurance = Decimal.Multiply(model.premiumSalary, (decimal)0.015);
            model.unemploymentInsurance = Decimal.Multiply(model.premiumSalary, (decimal)0.01);

            //tinh lai 3 loai bao hiem// neu lớn hơn 20 lần mức lương cơ sở thì tính = mức lương cs *20

            if (model.premiumSalary > 29800000)
            {
                model.socialInsurance = Decimal.Multiply(Decimal.Multiply(baseSalary, (decimal)20), (decimal)0.08);
                model.healthInsurance = Decimal.Multiply(Decimal.Multiply(baseSalary, (decimal)20), (decimal)0.015);
            }
            if(model.premiumSalary> Decimal.Multiply(model.area, (decimal)20))
            {
                model.unemploymentInsurance = Decimal.Multiply(Decimal.Multiply(model.area, (decimal)20), (decimal)0.01);
            }

            // tien tru 3 loai bao hiem
            model.premium = model.socialInsurance + model.healthInsurance + model.unemploymentInsurance;

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
        [HttpPost]
        public ActionResult NetSalaryToGross(Salary model)
        {

            if (model.premiumSalary < model.area)
            {
                model.premiumSalary = 0;
                ViewData["infoError"] = "Mức lương đóng bảo hiểm phải lớn hơn hoặc bằng mức lương tối thiểu vùng!";
                return View(model);
            }


            //bảo hiểm lúc đầu
            model.socialInsurance = Decimal.Multiply(model.premiumSalary, (decimal)0.08);
            model.healthInsurance = Decimal.Multiply(model.premiumSalary, (decimal)0.015);
            model.unemploymentInsurance = Decimal.Multiply(model.premiumSalary, (decimal)0.01);

            //tinh lai 3 loai bao hiem// neu lớn hơn 20 lần mức lương cơ sở thì tính = mức lương cs *20

            if (model.premiumSalary > 29800000)
            {
                model.socialInsurance = Decimal.Multiply(Decimal.Multiply(baseSalary, (decimal)20), (decimal)0.08);
                model.healthInsurance = Decimal.Multiply(Decimal.Multiply(baseSalary, (decimal)20), (decimal)0.015);
            }
            if (model.premiumSalary > Decimal.Multiply(model.area, (decimal)20))
            {
                model.unemploymentInsurance = Decimal.Multiply(Decimal.Multiply(model.area, (decimal)20), (decimal)0.01);
            }

            // tien tru 3 loai bao hiem
            model.premium = model.socialInsurance + model.healthInsurance + model.unemploymentInsurance;


            //thu nhập làm căn cứ quy đổi (TNQD)
            decimal TNQD;
            TNQD = model.salary - 11000000 - Decimal.Multiply(model.numberOfDependents, (decimal)4400000);
            if(TNQD > 0)
            {
                if(TNQD <= 4750000)
                {
                    model.incomeTaxes = TNQD / (decimal)0.95;
                }
                else if(TNQD > 4750000 && TNQD <= 9250000){
                    model.incomeTaxes = (TNQD - 250000) / (decimal)0.9;
                }
                else if (TNQD > 9250000 && TNQD <= 16050000)
                {
                    model.incomeTaxes = (TNQD - 750000) / (decimal)0.85;
                }
                else if (TNQD > 16050000 && TNQD <= 27250000)
                {
                    model.incomeTaxes = (TNQD - 1650000) / (decimal)0.8;
                }
                else if (TNQD > 27250000 && TNQD <= 42250000)
                {
                    model.incomeTaxes = (TNQD - 3250000) / (decimal)0.75;
                }
                else if(TNQD > 42250000 && TNQD <= 61850000)
                {
                    model.incomeTaxes = (TNQD - 5850000) / (decimal)0.7;
                }
                else if(TNQD > 61850000)
                {
                    model.incomeTaxes = (TNQD - 9850000) / (decimal)0.65;
                }    
            }
            else
            {
                model.incomeTaxes = 0;
                model.personalIncomeTax = 0;
            }

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
            model.incomeBeforeTax = model.personalIncomeTax + model.salary;
            model.finalSalary = (decimal)model.premium + model.personalIncomeTax + model.salary;
            return View(model);
        }
    }
}