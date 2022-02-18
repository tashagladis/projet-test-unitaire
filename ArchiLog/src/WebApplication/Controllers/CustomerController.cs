using APILibrary.Core.Controllers;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Data;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBaseAPI<Customer, EatDbContext>
    {
        public CustomerController(EatDbContext context) : base(context)
        {
        }
    }
}
