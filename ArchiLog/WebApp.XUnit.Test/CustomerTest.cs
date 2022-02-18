using System;
using System.Text;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.XUnit.Test.Mock;
using WebApp.XUnit.Test.Mock.Models;
using WebApplication.Controllers;
using Xunit;
using System.Dynamic;
using System.Reflection;
using APILibrary.Core.Attributs;

namespace WebApp.XUnit.Test
{
    public class CustomerTest
    {
        private MockDbContext _db;
        private CustomerController _controller;

        public CustomerTest()
        {
            _db = MockDbContext.GetDbContext();
            _controller = new CustomerController(_db);
        }

        [Fact]
        public async Task TestGetAll()
        {
            var actionResult = await _controller.GetAllAsync("", "", "");
            var result = actionResult.Result as ObjectResult;
            var values = result?.Value as IEnumerable<object>;

            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;

            _db.Customers.Count().Should().Be(values?.Count());
        }


        [Fact]
        public async Task TestGetBYId()
        {

            var actionResult = await _controller.GetById(2, "");
            var result = actionResult.Result as ObjectResult;

            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;

        }

        [Fact]
        public async Task TestPut()
        {
            CustomerMock customer = new CustomerMock
            (
                "AliAhmadr@yahoo.fr",
                "65421895154",
                "Fouret",
                "Jeanne",
                "Autres",
                DateTime.Now,
                null,
                "6854",
                "Limoges"
            );
            customer.ID = 2;
            var actionResult = await _controller.UpdateItem(2, customer);
            var result = actionResult.Result as ObjectResult;

            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
        }

        [Fact]
        public async Task TestCreate()
        {
            CustomerMock customer = new CustomerMock
            (
                "AliAhmadr@yahoo.fr",
                "65421895154",
                "Maria",
                "Julia",
                "Autres",
                DateTime.Now,
                null,
                "6854",
                "Limoges"
            );

            var actionResult = await _controller.CreateItem(customer);
            var result = actionResult.Result as ObjectResult;

            var okResult = result.Should().BeOfType<CreatedResult>().Subject;

        }

        [Fact]
        public async Task TestDelete()
        {
            // Penser à Changer la valeur retounée de la DeleteItem
            var actionResult = await _controller.RemoveItem(1);
            var result = actionResult.Result as ObjectResult;

            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;

        }

        //[Fact]
        //public async Task TestSearch()
        //{
        //    var actionResult = await _controller.Search("*Charles*", "Homme", "");
        //    var result = actionResult.Result as ObjectResult;

        //    var okResult = result.Should().BeOfType<OkObjectResult>().Subject;

        //}

        //[Fact]
        //public async Task TestNotFoundSearch()
        //{
        //    var actionResult = await _controller.Search("Charles", "Homme", "");
        //    var result = actionResult.Result as ObjectResult;

        //    var okResult = result.Should().BeOfType<NotFoundObjectResult>().Subject;

        //}


        //[Fact]
        //public async Task TestSort()
        //{
        //    var actionResult = await _controller.Sort("", "lastname", "");
        //    var result = actionResult.Result as ObjectResult;

        //    var okResult = result.Should().BeOfType<OkObjectResult>().Subject;

        //}
    }
}
