using APILibrary.Core.Attributs;
using APILibrary.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication.Models
{
    public class Customer : ModelBase
    {

        public Customer(
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
    }
}
