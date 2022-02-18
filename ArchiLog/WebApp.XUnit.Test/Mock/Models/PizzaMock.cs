using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication.Models;

namespace WebApp.XUnit.Test.Mock.Models
{
   public class PizzaMock: Pizza
    {
        public PizzaMock(string name, decimal? price, string topping, DateTime? dateCreation): base( name,  price, topping, dateCreation)
        {
            
        }
       
    }
}
