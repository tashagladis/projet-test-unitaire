using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
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
        public async Task Je_peux_faire_un_getallasync()
        {
            var actionResult = await _controller.GetAllAsync("", "", "");
            var result = actionResult.Result as ObjectResult;
            var values = result?.Value as IEnumerable<object>;

            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            var correctNumber = values?.Count().Should().Be(_db.Pizzas.Count());
        }

        [Fact]
        public async Task Je_peux_faire_un_getallasync_avec_tri_asc_et_desc()
        {
            var actionResult = await _controller.GetAllAsync("", "price", "name");
            var result = actionResult.Result as ObjectResult;
            var values = result?.Value as IEnumerable<object>;

            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
        }

        [Fact]
        public async Task Je_peux_faire_un_getallasync_avec_des_filtres()
        {
            var query2 = new QueryCollection(new Dictionary<string, StringValues>()
            {
                { "name", "Pizza2" }
            });

            var actionResult = await _controller.GetAllAsync("", "", "", query2);
            var result = actionResult.Result as ObjectResult;
            var values = result?.Value as IEnumerable<object>;

            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
        }

        [Fact]
        public async Task Je_peux_faire_un_getallasync_avec_rendu_partiel()
        {
            var actionResult = await _controller.GetAllAsync("name");
            var result = actionResult.Result as ObjectResult;
            var values = result?.Value as IEnumerable<object>;

            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
        }

        [Fact]
        public async Task Je_peux_faire_une_recherche_normale()
        {
            var query2 = new QueryCollection(new Dictionary<string, StringValues>()
            {
                { "name", "Pizza*" }
            });
            var actionResult = await _controller.SearchAsync("", "", "", query2);
            var result = actionResult.Result as ObjectResult;
            var values = result?.Value as IEnumerable<object>;

            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
        }

        [Fact]
        public async Task Je_peux_faire_une_recherche_avec_tri()
        {
            var query2 = new QueryCollection(new Dictionary<string, StringValues>()
            {
                { "name", "Pizza*" }
            });
            var actionResult = await _controller.SearchAsync("", "price", "name", query2);
            var result = actionResult.Result as ObjectResult;
            var values = result?.Value as IEnumerable<object>;

            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
        }

        [Fact]
        public async Task Je_peux_faire_une_recherche_avec_des_filtres()
        {
            var query2 = new QueryCollection(new Dictionary<string, StringValues>()
            {
                { "name", "Pizza*" },
                {"topping", "test" }
            });
            var actionResult = await _controller.SearchAsync("", "", "", query2);
            var result = actionResult.Result as ObjectResult;
            var values = result?.Value as IEnumerable<object>;

            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
        }

        [Fact]
        public async Task Je_peux_faire_une_recherche_avec_un_rendu_partiel()
        {
            var query2 = new QueryCollection(new Dictionary<string, StringValues>()
            {
                { "name", "Pizza*" },
                {"topping", "test" }
            });
            var actionResult = await _controller.SearchAsync("name", "", "", query2);
            var result = actionResult.Result as ObjectResult;
            var values = result?.Value as IEnumerable<object>;

            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
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
