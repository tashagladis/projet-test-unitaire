using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.XUnit.Test.Mock;
using WebApp.XUnit.Test.Mock.Models;
using WebApplication.Controllers;
using WebApplication.Models;
using Xunit;

namespace WebApp.XUnit.Test
{
    [Trait("Opération sur customer", "")]
    public class CustomerTest
    {
        private MockDbContext _db;
        private CustomerController _controller;

        public CustomerTest()
        {
            _db = MockDbContext.GetDbContext();
            _controller = new CustomerController(_db);
        }

        [Fact(DisplayName = "Je peux recuperer tous les customers")]
        public async Task Je_peux_recuperer_tous_les_customers()
        {
            var actionResult = await _controller.GetAllAsync("", "", "");
            var result = actionResult.Result as ObjectResult;
            var values = result?.Value as IEnumerable<object>;

            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            var correctNumber = values?.Count().Should().Be(_db.Customers.Count());
        }


        [Fact(DisplayName = "Je peux recuperer un customer")]
        public async Task Je_peux_recuperer_un_customer()
        {

            var actionResult = await _controller.GetById(2, "");
            var result = actionResult.Result as ObjectResult;

            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;

        }

        [Fact(DisplayName = "Je peux modifier un customer")]
        public async Task Je_peux_modifier_un_customer()
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

        [Fact(DisplayName = "Je peux creer un customer")]
        public async Task Je_peux_creer_un_customer()
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

        [Fact(DisplayName = "Je peux supprimer un customer")]
        public async Task Je_peux_supprimer_un_customer()
        {
            // Penser à Changer la valeur retounée de la DeleteItem
            var actionResult = await _controller.RemoveItem(1);
            var result = actionResult.Result as ObjectResult;

            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;

        }


        [Fact(DisplayName = "Je peux pas creer un customer sans email")]
        public async Task Je_peux_pas_creer_un_customer_sans_email()
        {
            CustomerMock customer = new CustomerMock
           (
               null,
               "654218951541",
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

            var okResult = result.Should().BeOfType<BadRequestObjectResult>().Subject;

        }

        [Fact(DisplayName = "Je peux pas modifier un customer avec un id inexistant")]
        public async Task Je_peux_pas_modifier_un_customer_avec_un_id_inexistant()
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
            customer.ID = 120;
            var actionResult = await _controller.UpdateItem(120, customer);
            var result = actionResult.Result as ObjectResult;

            var okResult = result.Should().BeOfType<NotFoundObjectResult>().Subject;
        }

        [Fact(DisplayName = "Je peux pas modifier un customer avec un id different")]
        public async Task Je_peux_pas_modifier_un_customer_avec_un_id_different()
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
            var actionResult = await _controller.UpdateItem(3, customer);
            var result = actionResult.Result as ObjectResult;

            var okResult = result.Should().BeOfType<BadRequestObjectResult>().Subject;
        }

        [Fact(DisplayName = "Je peux pas supprimer un customer")]
        public async Task Je_peux_pas_supprimer_un_customer()
        {
            var actionResult = await _controller.RemoveItem(120);
            var result = actionResult.Result as ObjectResult;

            var okResult = result.Should().BeOfType<NotFoundObjectResult>().Subject;

        }

        [Fact(DisplayName = "Je peux recuperer un customer avec fields")]
        public async Task Je_peux_recuperer_un_customer_avec_fields()
        {

            var actionResult = await _controller.GetById(2, "genre");
            var result = actionResult.Result as ObjectResult;

            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;

        }

        [Fact(DisplayName = "Je peux pas recuperer un customer avec fields")]
        public async Task Je_peux_pas_recuperer_un_customer_avec_fields()
        {

            var actionResult = await _controller.GetById(120, "genre");
            var result = actionResult.Result as ObjectResult;

            var okResult = result.Should().BeOfType<NotFoundObjectResult>().Subject;

        }

        [Fact(DisplayName = "Je peux pas recuperer un customer inexistant")]
        public async Task Je_peux_pas_recuperer_un_customer_inexistant()
        {

            var actionResult = await _controller.GetById(120, "");
            var result = actionResult.Result as ObjectResult;

            var okResult = result.Should().BeOfType<NotFoundObjectResult>().Subject;

        }

        //public class Calculator
        //{
        //    public int Add(int value1, int value2)
        //    {
        //        return value1 + value2;
        //    }
        //}

        //[Theory]
        //[InlineData(1, 2, 3)]
        //[InlineData(-4, -6, -10)]
        //[InlineData(-2, 2, 0)]
        //[InlineData(int.MinValue, -1, int.MaxValue)]
        //public void Add_SumOfNumbers_True(int value1, int value2, int expected)
        //{
        //    //Arrange
        //    var calculator = new Calculator();


        //    //Act
        //    var result = calculator.Add(value1, value2);


        //    //Assert
        //    Assert.Equal(expected, result);
        //}

    }
}
