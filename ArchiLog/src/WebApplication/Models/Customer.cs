using APILibrary.Core.Attributs;
using APILibrary.Core.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication.Models
{
    public class Customer : ModelBase
    {


        //public int ID { get; set; }
        // rendre obligatoire l'élément en question
        // errormessage : pour personnaliser le message d'erreur
        [Required(ErrorMessage = "L'email est obligatoire.")]
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
    }
}
