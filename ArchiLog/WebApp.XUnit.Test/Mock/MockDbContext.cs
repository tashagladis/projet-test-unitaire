using Microsoft.EntityFrameworkCore;
using WebApp.XUnit.Test.Mock.Models;
using WebApplication.Data;

namespace WebApp.XUnit.Test.Mock
{
    public class MockDbContext : EatDbContext
    {
        public MockDbContext(DbContextOptions options) : base(options)
        {
        }

        public static MockDbContext GetDbContext(bool withData = true)
        {
            var options = new DbContextOptionsBuilder().UseInMemoryDatabase("dbtest").Options;
            var db = new MockDbContext(options);

            if (withData)
            {
                db.Customers.Add(new CustomerMock
                {
                    Email = "AliAhmadr@yahoo.fr",
                    Phone = "65421895154",
                    Lastname = "Charles",
                    Firstname = "Clementine",
                    Genre = "Homme",
                    Address = null,
                    ZipCode = "6854",
                    City = "Limoges",
                });

                db.Customers.Add(new CustomerMock
                {
                    Email = "AliAhmadr@yahoo.fr",
                    Phone = "65421895154",
                    Lastname = "Charles",
                    Firstname = "Zan",
                    Genre = "Homme",
                    Address = null,
                    ZipCode = "6854",
                    City = "Limoges",
                });

                db.Customers.Add(new CustomerMock
                {
                    Email = "AliAhmadr@yahoo.fr",
                    Phone = "65421895154",
                    Lastname = "Kaly",
                    Firstname = "Moli",
                    Genre = "Femme",
                    Address = null,
                    ZipCode = "6854",
                    City = "Limoges",
                });

                db.Customers.Add(new CustomerMock
                {
                    Email = "AliAhmadr@yahoo.fr",
                    Phone = "65421895154",
                    Lastname = "Charles",
                    Firstname = "Jeep",
                    Genre = "Femme",
                    Address = null,
                    ZipCode = "6854",
                    City = "Limoges",
                });


                db.SaveChanges();
            }

            return db;
        }
    }
}
