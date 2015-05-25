using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

namespace VendingMachine
{
    [TestClass]
    public class BrainTest
    {
        [TestMethod]
        public void ShouldInstantiate()
        {
            Brain o = new Brain();

        }
    }
}
