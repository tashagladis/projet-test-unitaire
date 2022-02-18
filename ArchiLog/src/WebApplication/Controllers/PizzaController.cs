using APILibrary.Core.Controllers;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Data;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PizzaController : ControllerBaseAPI<Pizza, EatDbContext>
    {
        public PizzaController(EatDbContext context) : base(context)
        {

        }
    }
}
