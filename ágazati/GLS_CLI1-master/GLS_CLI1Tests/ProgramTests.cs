using Microsoft.VisualStudio.TestTools.UnitTesting;
using GLS_CLI1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GLS_CLI1.Tests
{
    [TestClass()]
    public class ProgramTests
    {
        [TestMethod()]
        public void NapiFogyasztasTest()
        {
            int km = 100;
            int liter = 10;
            double elvart = 10;
            double kapott = Program.NapiFogyasztas(km, liter);

             Assert.AreEqual(elvart,kapott);
        }

        [TestMethod()]
        public void NapiFogyasztasTest2()
        {
            int km = 200;
            int liter = 16;
            double elvart = 8;
            double kapott = Program.NapiFogyasztas(km, liter);

            Assert.AreEqual(elvart, kapott);
        }

        [TestMethod()]
        public void NapiFogyasztasTest3()
        {
            int km = 0;
            int liter = 0;
            double elvart = 0;
            double kapott = Program.NapiFogyasztas(km, liter);

            Assert.AreEqual(elvart, kapott);
        }

    }
}