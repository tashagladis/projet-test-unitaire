using FluentAssertions;
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
            //Arrange

            //Act
            var result = Customer.Create("test@gmail.com", "085412368", "jean michel", "jean", "homme", DateTime.Now, "2 rue gabriel péri test", "05428", "Paris");

            //Assert
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

        [Theory(DisplayName = "je ne peux pas créer un customer sans le champ Email")]
        [InlineData("")]
        [InlineData(null)]

        public void on_ne_peut_pas_creer_un_customer_sans_le_chmap_Email(string email)
        {
            //Arrange

            //Act
            var resultCreateCustomerWithoutEmail = Customer.Create(
                 email,
                 "65421895154",
                 "Fouret",
                 "Jeanne",
                 "Autres",
                 DateTime.Now,
                 "2 bd rue jean miche",
                 "6854",
                 "Limoges");

            //Assert
            Assert.True(resultCreateCustomerWithoutEmail.IsFailure);
            resultCreateCustomerWithoutEmail.Error.Should().Be("Le champ Email est requis");
        }
        
        [Theory(DisplayName = "je ne peux pas créer un customer sans le champ Phone")]
        [InlineData("")]
        [InlineData(null)]

        public void on_ne_peut_pas_creer_un_customer_sans_le_chmap_Phone(string phone)
        {
            //Arrange

            //Act
            var resultCreateCustomerWithoutPhone = Customer.Create(
                 "test@gmail.com",
                 phone,
                 "Fouret",
                 "Jeanne",
                 "Autres",
                 DateTime.Now,
                 "2 bd rue jean miche",
                 "6854",
                 "Limoges");

            //Assert
            Assert.True(resultCreateCustomerWithoutPhone.IsFailure);
            resultCreateCustomerWithoutPhone.Error.Should().Be("Le champ Phone est requis");
        }

        [Theory(DisplayName = "je ne peux pas créer un customer sans le champ Lastname")]
        [InlineData("")]
        [InlineData(null)]

        public void on_ne_peut_pas_creer_un_customer_sans_les_champ_Lastname(string lastname)
        {
            //Arrange

            //Act
            var resultCreateCustomerWithoutLastname = Customer.Create(
                 "test@gmail.com",
                 "085412368",
                 lastname,
                 "jean",
                 "Autres",
                 DateTime.Now,
                 "2 bd rue jean miche",
                 "6854",
                 "Limoges");

            //Assert
            Assert.True(resultCreateCustomerWithoutLastname.IsFailure);
            resultCreateCustomerWithoutLastname.Error.Should().Be("Le champ Lastname est requis");
        }
        
        
        [Theory(DisplayName = "je ne peux pas créer un customer sans le champ Firstname")]
        [InlineData("")]
        [InlineData(null)]

        public void on_ne_peut_pas_creer_un_customer_sans_les_champ_Firstname(string firstname)
        {
            //Arrange

            //Act
            var resultCreateCustomerWithoutFirstname = Customer.Create(
                 "test@gmail.com",
                 "085412368",
                 "test",
                 firstname,
                 "Autres",
                 DateTime.Now,
                 "2 bd rue jean miche",
                 "6854",
                 "Limoges");

            //Assert
            Assert.True(resultCreateCustomerWithoutFirstname.IsFailure);
            resultCreateCustomerWithoutFirstname.Error.Should().Be("Le champ Firstname est requis");
        }
    }
}
