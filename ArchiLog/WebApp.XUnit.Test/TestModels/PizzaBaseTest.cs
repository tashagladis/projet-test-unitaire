using FluentAssertions;
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

        [Theory(DisplayName = "je ne peux pas créer une Pizza sans le champ Name")]
        [InlineData("")]
        [InlineData(null)]

        public void on_ne_peut_pas_creer_une_pizza_sans_les_champ_Name(string name)
        {
            //Arrange

            //Act
            var resultCreatePizzaWithoutName = Pizza.Create(
                name,
                50,
                "test",
                DateTime.Now);

            //Assert
            Assert.True(resultCreatePizzaWithoutName.IsFailure);
            resultCreatePizzaWithoutName.Error.Should().Be("Le champ Name est requis");
        }

        [Theory(DisplayName = "je ne peux pas créer une Pizza sans le champ Price")]
        [InlineData(0)]
        [InlineData(null)]

        public void on_ne_peut_pas_creer_une_pizza_sans_les_champ_Price(int price)
        {
            //Arrange

            //Act
            var resultCreatePizzaWithoutPrice = Pizza.Create(
                "pizza1",
                price,
                "test",
                DateTime.Now);

            //Assert
            Assert.True(resultCreatePizzaWithoutPrice.IsFailure);
            resultCreatePizzaWithoutPrice.Error.Should().Be("Le champ Price est requis");
        }

        [Theory(DisplayName = "je ne peux pas créer une Pizza sans le champ Topping")]
        [InlineData("")]
        [InlineData(null)]

        public void on_ne_peut_pas_creer_une_pizza_sans_les_champ_Topping(string topping)
        {
            //Arrange

            //Act
            var resultCreatePizzaWithoutTopping = Pizza.Create(
                "pizza1",
                25,
                topping,
                DateTime.Now);

            //Assert
            Assert.True(resultCreatePizzaWithoutTopping.IsFailure);
            resultCreatePizzaWithoutTopping.Error.Should().Be("Le champ Topping est requis");
        }

        [Theory(DisplayName = "je ne peux pas créer une Pizza sans le champ Date de Creation")]
        [InlineData(null)]

        public void on_ne_peut_pas_creer_une_pizza_sans_les_champ_DateCreation(DateTime? dateCreation)
        {
            //Arrange

            //Act
            var resultCreatePizzaWithoutTopping = Pizza.Create(
                "pizza1",
                25,
                "test",
               dateCreation);

            //Assert
            Assert.True(resultCreatePizzaWithoutTopping.IsFailure);
            resultCreatePizzaWithoutTopping.Error.Should().Be("Le champ DateCreation est requis");
        }

    }
}
