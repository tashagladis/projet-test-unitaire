using System;
using WebApplication.Models;
using Xunit;

namespace WebApp.XUnit.Test.TestModels
{
    [Trait("Création d'un Customer", "")]
    public class CustomerBaseTest
    {

        [Fact(DisplayName = "je peux créer un customer si tous les champs sont renseignés")]
        public void on_peut_creer_un_customer_avec_infos_correctes()
        {
            var result = Customer.Create("test@gmail.com", "085412368", "jean michel", "jean", "homme", DateTime.Now, "2 rue gabriel péri test", "05428", "Paris");

            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Value);
            Assert.NotNull(result.Value.Email);
            Assert.NotNull(result.Value.Phone);
            Assert.NotNull(result.Value.Lastname);
            Assert.NotNull(result.Value.Firstname);
            Assert.NotNull(result.Value.Genre);
            Assert.NotNull(result.Value.Genre);
            Assert.NotNull(result.Value.Birthday);
            Assert.NotNull(result.Value.Address);
            Assert.NotNull(result.Value.ZipCode);
            Assert.NotNull(result.Value.City);
        }

    }
}
