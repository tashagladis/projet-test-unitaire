using System;
using WebApplication.Models;
using Xunit;

namespace WebApp.XUnit.Test.TestModels
{
    [Trait("Création d'une Pizza", "")]
    public class PizzaBaseTest
    {

        [Fact(DisplayName = "je peux créer une pizza si tous les champs sont renseignés")]
        public void on_peut_creer_une_pizza_avec_infos_correctes()
        {
            var result = Pizza.Create("Pizza1", 50, "test", DateTime.Now);

            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Value);
            Assert.NotNull(result.Value.Name);
            Assert.NotEqual(0, result.Value.Price);
            Assert.NotNull(result.Value.Topping);
            Assert.NotNull(result.Value.DateCreation);
        }
    }
}
