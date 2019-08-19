using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TaxWebApp.Controllers;
using TaxService.Models;
using TaxService.Controllers;
using System.Collections.Generic;
using System.Linq;

namespace TaxCalculatorUnitTest
{
    [TestClass]
    public class Tax
    {
        [TestMethod]
        public void DBConnectionTest()
        {
            TaxCalculatorDBEntities dBEntities = new TaxCalculatorDBEntities();

            CalculatedTax tx = dBEntities.CalculatedTaxes.Find(5);

            Assert.IsTrue(tx != null);
        }
    }
}
