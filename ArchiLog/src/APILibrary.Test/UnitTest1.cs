using APILibrary.Test.Mock;
using APILibrary.Test.Mock.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Controllers;

namespace APILibrary.Test
{
    public class Tests
    {

        private MockDbContext _db;
        private CustomerController _controller;

        [SetUp]
        public void Setup()
        {
            _db = MockDbContext.GetDbContext();
            _controller = new CustomerController(_db);
        }

        [Test]
        public async Task TestGetAll()
        {

            var actionResult = await _controller.GetAllAsync();
            var result = actionResult.Result as ObjectResult;
            var values = ((IEnumerable<object>)(result).Value);

            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;

            _db.Customers.Count().Should().Be(values.Count());

        }

        [Test]
        public async Task TestGetBYId()
        {

            var actionResult = await _controller.GetById(2, "");
            var result = actionResult.Result as ObjectResult;

            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;

        }

        //[Test]
        //public async Task TestGetNotFoundtById()
        //{

        //    var actionResult = await _controller.GetById(100, "");
        //    var result = actionResult.Result as ObjectResult;

        //    var okResult = result.Should().BeOfType<NotFoundResult>().Subject;

        //}

        [Test]
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


            var actionResult = await _controller.UpdateItem(customer.ID = 2, customer);
            var result = actionResult.Result as ObjectResult;

            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;

        }

        [Test]
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

            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;

        }

        [Test]
        public async Task TestDelete()
        {

            // Penser à Changer la valeur retounée de la DeleteItem
            var actionResult = await _controller.DeleteItem(1);
            var result = actionResult.Result as ObjectResult;

            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;

        }

        [Test]
        public async Task TestSearch()
        {


            var actionResult = await _controller.Search("*Charles*", "Homme", "");
            var result = actionResult.Result as ObjectResult;

            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;

        }

        public async Task TestNotFoundSearch()
        {


            var actionResult = await _controller.Search("Charles", "Homme", "");
            var result = actionResult.Result as ObjectResult;

            var okResult = result.Should().BeOfType<NotFoundResult>().Subject;

        }




        [Test]
        public async Task TestSort()
        {


            var actionResult = await _controller.Sort("", "lastname", "");
            var result = actionResult.Result as ObjectResult;

            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;

        }

        //[Test]
        //public async Task TestSearch()
        //{

        //    // Penser à Changer la valeur retounée de la DeleteItem
        //    var actionResult = await _controller.Search( "Charles", "Homme", "");
        //    var result = actionResult.Result as ObjectResult;

        //    var okResult = result.Should().BeOfType<OkObjectResult>().Subject;

        //}

    }
}