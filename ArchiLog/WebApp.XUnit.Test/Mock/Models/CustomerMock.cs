using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication.Models;

namespace WebApp.XUnit.Test.Mock.Models
{
    public class CustomerMock : Customer
    {
        public CustomerMock(string email, string phone, string lastname, string firstname, string genre, DateTime birthday, string address, string zipCode, string city) : base(email, phone, lastname, firstname, genre, birthday, address, zipCode, city)
        {
        }
    }
}
