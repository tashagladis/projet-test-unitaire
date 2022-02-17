using FluentAssertions;
using System;
using WebApplication.Models;
using Xunit;

namespace WebApp.XUnit.Test
{

    [Trait("creation d'une commande", "")]
    public class OrderTest
    {
        [Fact(DisplayName = "je peux creer une commande")]
        public void Je_peux_creer_une_commande()
        {
            //given
            var pizza1 = new Pizza("pizza1", 50, "test", DateTime.Now);
            var pizza2 = new Pizza("pizza2", 50, "test", DateTime.Now.AddDays(1));
            var pizza = new[] { pizza1, pizza2 };
            var customer = new Customer
            (
                 
                 "AliAhmadr@yahoo.fr",
                 "65421895154",
                 "Fouret",
                 "Jeanne",
                 "Autres",
                 DateTime.Now,
                 "2 bd rue jean miche",
                 "6854",
                 "Limoges"
            );

            //when
            var result = Order.Create(customer, pizza);

            //then

            result.IsSuccess.Should().BeTrue();
        }

        [Fact(DisplayName = "je ne peux pas creer une commande sans customer")]
        public void Je_ne_peux_pas_creer_une_commande_sans_customer()
        {
            //given
            var pizza1 = new Pizza("pizza1", 50, "test", DateTime.Now);
            var pizza2 = new Pizza("pizza2", 50, "test", DateTime.Now.AddDays(1));
            var pizza = new[] { pizza1, pizza2 };
            //when
            var result = Order.Create(null, pizza);

            //then
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be("Le champ Customer ne doit pas etre null");
        }

        [Fact(DisplayName = "je ne peux pas creer une commande sans pizza")]
        public void Je_ne_peux_pas_creer_une_commande_sans_pizza()
        {
            //given
            var customer = new Customer
            (
                
                 "AliAhmadr@yahoo.fr",
                 "65421895154",
                 "Fouret",
                 "Jeanne",
                 "Autres",
                 DateTime.Now,
                 "2 bd rue jean miche",
                 "6854",
                 "Limoges"
            );

            //when
            var result = Order.Create(customer, null);

            //then
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be("Le champ Pizza ne doit pas etre null");
        }

        [Fact(DisplayName = "je ne peux pas creer une commande sans pizza")]
        public void Je_ne_peux_pas_creer_une_commande_avec_moins_de_une_pizza()
        {
            //given
            var customer = new Customer
            (
                 
                 "AliAhmadr@yahoo.fr",
                 "65421895154",
                 "Fouret",
                 "Jeanne",
                 "Autres",
                 DateTime.Now,
                 "2 bd rue jean miche",
                 "6854",
                 "Limoges"
            );

            //une commande avec 0 pizza => tableau de pizza vide 
            var pizza = Array.Empty<Pizza>();


            //when
            var result = Order.Create(customer, pizza);

            //then
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be("Il faut au moins une pizza afin de créer la commande");
        }
    }
}
