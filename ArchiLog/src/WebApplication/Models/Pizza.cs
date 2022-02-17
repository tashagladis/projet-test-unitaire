using APILibrary.Core.Models;
using Ardalis.GuardClauses;
using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication.Models
{
    public class Pizza : ModelBase
    {
        public Pizza(string name, decimal price, string topping, DateTime dateCreation)
        {
            Name = name;
            Price = price;
            Topping = topping;
            DateCreation = DateTime.Now;
        }


        //[Key]
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Topping { get; set; }
        [DataType(DataType.Date)]
        [Column(TypeName = "Date")]
        public DateTime DateCreation { get; set; }


        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return this.Name;
            yield return this.Price;
            yield return this.Topping;
            yield return this.DateCreation;

        }

        public static Result<Pizza> Create(string name, decimal price, string topping, DateTime dateCreation)
        {

            Guard.Against.NullOrEmpty(name, nameof(name));
            Guard.Against.Negative(price, nameof(price));
            Guard.Against.NullOrEmpty(topping, nameof(topping));
            Guard.Against.OutOfSQLDateRange(dateCreation, nameof(dateCreation));           


            return Result.Success(new Pizza(name, price, topping, dateCreation));
        }
    }
}
