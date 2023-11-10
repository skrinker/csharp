using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebServer.Helpers.TemplateEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebServer.Helpers.TemplateEngine.Tests
{
    [TestClass()]
    public class TemplateEngineMethodsTests
    {
        [TestMethod()]
        public void SubstituteStringTest()
        {
            var methods = new TemplateEngineMethods();
            string expected = "Hello World";

            Assert.AreEqual(expected, methods.SubstituteString("Hello @{var}", "World"));
        }

        [TestMethod()]
        public void SubstituteObjectTest()
        {
            var methods = new TemplateEngineMethods();
            string expected = "Hello World";
            Assert.AreEqual(expected, methods.SubstituteObject("Hello @{var}", "World"));

        }
    }
}