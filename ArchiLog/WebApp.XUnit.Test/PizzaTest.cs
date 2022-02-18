using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.XUnit.Test.Mock;
using WebApp.XUnit.Test.Mock.Models;
using WebApplication.Controllers;
using Xunit;

namespace WebApp.XUnit.Test
{

    public class PizzaTest
    {
        private MockDbContext _db;
        private PizzaController _controller;

        public PizzaTest()
        {
            _db = MockDbContext.GetDbContext();
            _controller = new PizzaController(_db);
        }

        [Fact]
        public async Task TestGetAll()
        {
            var actionResult = await _controller.GetAllAsync();
            var result = actionResult.Result as ObjectResult;
            var values = result?.Value as IEnumerable<object>;

            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;

            _db.Pizzas.Count().Should().Be(values?.Count());
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
            PizzaMock pizza = new PizzaMock
            (
                "Pizza2",
                 23,
                 "test2",
                 DateTime.Now
             );

            var actionResult = await _controller.UpdateItem(pizza.ID = 2, pizza);
            var result = actionResult.Result as ObjectResult;

            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;

        }

        [Fact]
        public async Task TestCreate()
        {
            PizzaMock pizza = new PizzaMock
            (
                "Pizza5",
                 23,
                 "test",
                 DateTime.Now
             );

            var actionResult = await _controller.CreateItem(pizza);
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
    }
}
