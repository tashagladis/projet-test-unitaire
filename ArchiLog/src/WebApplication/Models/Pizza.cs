﻿using APILibrary.Core.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication.Models
{
    public class Pizza : ModelBase
    {


        //[Key]
        //public int ID { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Topping { get; set; }
        [DataType(DataType.Date)]
        [Column(TypeName = "Date")]
        public DateTime DateCreation { get; set; }
    }
}
