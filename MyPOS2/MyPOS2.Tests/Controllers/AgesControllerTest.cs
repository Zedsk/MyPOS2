using System;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyPOS2.Controllers;
using MyPOS2.Data.Entity;

namespace MyPOS2.Tests.Controllers
{
    [TestClass]
    public class AgesControllerTest
    {
        //[TestMethod]
        //public void Index()
        //{
        //    // Arrange
        //    var controller = new AgesController();

        //    // Act
        //    var result = controller.Index() as ViewResult;
        //    //var model = result.Model as AGE;
        //    //// Assert
        //    //Assert.AreEqual("Index", result.ViewBag.Title);
        //    //Assert.AreEqual("Index", result.ViewName);
        //    //Assert.AreEqual("AGE>", result.Model);
        //    //Assert.AreEqual("IEnumerable<MyPOS2.Data.Entity.AGE>", result.Model);
        //    //Assert.IsNotNull(result);
        //}

        //[TestMethod]
        //public void Details()
        //{
        //    // Arrange
        //    AgesController controller = new AgesController();

        //    int? id = null;
        //    // Act
        //    ViewResult result = controller.Details(id) as ViewResult;

        //    // Assert
        //    Assert.AreEqual(result);
        //}

        [TestMethod]
        public void Create()
        {
            // Arrange
            AgesController controller = new AgesController();

            // Act
            ViewResult result = controller.Create() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }
    }
}
