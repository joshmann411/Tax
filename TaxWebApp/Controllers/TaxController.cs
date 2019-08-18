using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using TaxWebApp.Models;

namespace TaxWebApp.Controllers
{
    public class TaxController : Controller
    {
        // GET: Tax
        public ActionResult Index()
        {
            IEnumerable<CalculatedTaxes> empList;
            HttpResponseMessage response = GlobalVariables.TaxCalculatorClient.GetAsync("CalculatedTaxes").Result;
            empList = response.Content.ReadAsAsync<IEnumerable<CalculatedTaxes>>().Result;

            return View(empList);
        }

        public ActionResult NewTaxOrEdit(int id = 0)
        {
            if (id == 0)
            {
                return View(new CalculatedTaxes());
            }
            else
            {
                //GetCalculatedTax
                HttpResponseMessage response = GlobalVariables.TaxCalculatorClient.GetAsync("CalculatedTaxes/"+id.ToString()).Result;
                return View(response.Content.ReadAsAsync<CalculatedTaxes>().Result);
            }
        }

        [HttpPost]
        public ActionResult NewTaxOrEdit(CalculatedTaxes newTax)
        {
            //Do tax calculations
            string areaCode = newTax.PoastalCode;
            int annualIncome = newTax.AnnualIncome.Value;

            DateTime dt = new DateTime();
            newTax.DateStmp = dt.ToString();

            newTax.TaxCalculated = taxLogic(areaCode, annualIncome);

            if(newTax.Id == 0)
            {
                HttpResponseMessage response = GlobalVariables.TaxCalculatorClient.PostAsJsonAsync("CalculatedTaxes", newTax).Result;
                TempData["SuccessMessage"] = "Saved Successfully";
            }
            else
            {
                HttpResponseMessage response = GlobalVariables.TaxCalculatorClient.PutAsJsonAsync("CalculatedTaxes/" + newTax.Id, newTax).Result;
                TempData["SuccessMessage"] = "Updated Successfully";
            }
           
            return RedirectToAction("Index");
        }

        public int taxLogic(string code, int income)
        {
            double tax = 0;

            if(code == "7441" || code == "1000") //Progressive Tax
            {
                if(income >= 0 && income <= 8350)
                {
                    tax = 0.1 * income;
                }
                else if(income >= 8351 && income <= 33950)
                {
                    tax = 0.15 * income;
                }
                else if (income >= 33951 && income <= 82250)
                {
                    tax = 0.25 * income;
                }
                else if (income >= 82251 && income <= 171550)
                {
                    tax = 0.28 * income;
                }
                else if (income >= 171551 && income <= 372950)
                {
                    tax = 0.33 * income;
                }
                else if(income >= 372950)
                {
                    tax = 0.35 * income;
                }
            }
            else if(code == "A100" && income >= 200000)
            {
                tax = (income >= 200000) ? (10000) : (0.05 * income);
            }
            else
            {
                tax = 0.175 * income;
            }

            return Convert.ToInt32(tax);
        }
        public ActionResult Delete(int id)
        {
            HttpResponseMessage response = GlobalVariables.TaxCalculatorClient.DeleteAsync("CalculatedTaxes/"+id.ToString()).Result;
            TempData["SuccessMessage"] = "Deleted Successfully";
            return RedirectToAction("Index");
        }
    }
}