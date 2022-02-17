using System;
using System.Collections.Generic;
using System.Text;
using WebApplication.Models;

namespace APILibrary.Test.Mock.Models
{
    public class CustomerMock : Customer
    {
        public CustomerMock(string email, string phone, string lastname, string firstname, string genre, DateTime birthday, string address, string zipCode, string city) : base(email, phone, lastname, firstname, genre, birthday, address, zipCode, city)
        {
        }
    }
}
