﻿using APILibrary.Core.Attributs;
using APILibrary.Core.Models;
using Ardalis.GuardClauses;
using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication.Models
{
    public class Customer : ModelBase
    {

        public Customer(string email,string phone,string lastname,string firstname,string genre,DateTime birthday,string address,string zipCode,string city)
        {
            Email = email;
            Phone = phone;
            Lastname = lastname;
            Firstname = firstname;
            Genre = genre;
            Birthday = birthday;
            Address = address;
            ZipCode = zipCode;
            City = city;
        }


        //public int ID { get; set; }
        // rendre obligatoire l'élément en question
        // errormessage : pour personnaliser le message d'erreur
        // [Required(ErrorMessage = "L'email est obligatoire.")]
        [NotJson]
        public string Email { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        public string Lastname { get; set; }
        [Required]
        public string Firstname { get; set; }
        public string Genre { get; set; }

        [DataType(DataType.Date)]
        [Column(TypeName = "Date")]
        public DateTime Birthday { get; set; }
        public string Address { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }


        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return this.Email;
            yield return this.Phone;
            yield return this.Lastname;
            yield return this.Firstname;
            yield return this.Genre;
            yield return this.Birthday;
            yield return this.Address;
            yield return this.ZipCode;
            yield return this.City;
        }

        public static Result<Customer> Create(
            string email,
            string phone,
            string lastname,
            string firstname,
            string genre,
            DateTime birthday,
            string address,
            string zipCode,
            string city)
        {
           
            Guard.Against.NullOrEmpty(email, nameof(email));
            Guard.Against.NullOrEmpty(phone, nameof(phone));
            Guard.Against.NullOrEmpty(lastname, nameof(lastname));
            Guard.Against.NullOrEmpty(firstname, nameof(firstname));
            Guard.Against.NullOrEmpty(genre, nameof(genre));
            Guard.Against.OutOfSQLDateRange(birthday, nameof(birthday));
            Guard.Against.NullOrEmpty(address, nameof(address));
            Guard.Against.NullOrEmpty(zipCode, nameof(zipCode));
            Guard.Against.NullOrEmpty(city, nameof(city));


            return Result.Success(new Customer(email, phone, lastname, firstname, genre, birthday, address, zipCode, city));
        }
    }
}