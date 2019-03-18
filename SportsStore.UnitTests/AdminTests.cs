using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;
using SportsStore.WebUI.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SportsStore.UnitTests
{
    [TestClass]
    public class AdminTests
    {
        [TestMethod]
        public void Index_Contains_All_Products()
        {
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[]
            {
                new Product{ProductID = 1, Name = "P1"},
                new Product{ProductID = 2, Name = "P2"},
                new Product{ProductID = 3, Name = "P3" }
            }.AsQueryable());

            AdminController target = new AdminController(mock.Object);

            Product[] result = ((IEnumerable<Product>)target.Index().ViewData
                .Model).ToArray();

            Assert.AreEqual(result.Length, 3);
            Assert.AreEqual("P1", result[0].Name);
            Assert.AreEqual("P2", result[1].Name);
            Assert.AreEqual("P3", result[2].Name);
        }

        [TestMethod]
        public void Can_Edit_Product()
        {
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[]
            {
            new Product {ProductID = 1, Name = "P1"},
            new Product {ProductID = 2, Name = "P2"},
            new Product {ProductID = 3, Name = "P3"}
            }.AsQueryable());

            AdminController target = new AdminController(mock.Object);

            Product p1 = target.Edit(1).ViewData.Model as Product;
            Product p2 = target.Edit(2).ViewData.Model as Product;
            Product p3 = target.Edit(3).ViewData.Model as Product;

            Assert.AreEqual(1, p1.ProductID);
            Assert.AreEqual(2, p2.ProductID);
            Assert.AreEqual(3, p3.ProductID);
        }

        [TestMethod]
        public void Cannot_Edit_Nonexistent_Product()
        {
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[]
            {
            new Product {ProductID = 1, Name = "P1"},
            new Product {ProductID = 2, Name = "P2"},
            new Product {ProductID = 2, Name = "P3"}
            }.AsQueryable());

            AdminController target = new AdminController(mock.Object);

            Product result = target.Edit(4).ViewData.Model as Product;

            Assert.IsNull(result);
        }

        [TestMethod]
        public void Can_Save_Valid_Changes()
        {
            //preparation - creating new repo imitation 
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            //preparation - creating controller
            AdminController target = new AdminController(mock.Object);
            //preparation - creating product
            Product product = new Product { Name = "Test" };

            //working - trying to save product
            ActionResult result = target.Edit(product, null);

            //asserts - check if there was called repository
            mock.Verify(m => m.SaveProduct(product));
            //asserts - verify type of get object
            Assert.IsNotInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public void Cannot_Save_Invalid_Changes()
        {
            //preparation - creating new repo imitation 
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            //preparation - creating controller
            AdminController target = new AdminController(mock.Object);
            //preparation - creating product
            Product product = new Product { Name = "Test" };
            //preparation - adding error to model state
            target.ModelState.AddModelError("error", "error");

            //working - trying to save product
            ActionResult result = target.Edit(product, null);

            //asserts - check if there was called repository
            mock.Verify(m => m.SaveProduct(It.IsAny<Product>()), Times.Never());
            //asserts - verify type of get object
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public void Can_Delete_Valid_Products()
        {
            //preparation - creating products
            Product product = new Product { ProductID = 2, Name = "Test" };

            //preparation - creating imitation of repo
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[]
            {
                new Product {ProductID = 1, Name = "P1"},
                new Product {ProductID = 3, Name = "P3"}
            }.AsQueryable());

            //preparation - creating controller
            AdminController target = new AdminController(mock.Object);

            //deleting product
            target.Delete(product.ProductID);

            //asserts - make sure, that method
            //has been called with correct product
            mock.Verify(m => m.DeleteProduct(product.ProductID));
        }

 
    }
}
