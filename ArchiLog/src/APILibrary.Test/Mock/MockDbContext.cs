using APILibrary.Test.Mock.Models;
using Microsoft.EntityFrameworkCore;
using System;
using WebApplication.Data;

namespace APILibrary.Test.Mock
{
    public class MockDbContext : EatDbContext
    {
        public MockDbContext(DbContextOptions options) : base(options)
        {
        }

        public static MockDbContext GetDbContext(bool withData = true)
        {
            var options = new DbContextOptionsBuilder().UseInMemoryDatabase("dbtest")
                .Options;
            var db = new MockDbContext(options);

            if (withData)
            {
                db.Customers.Add(new CustomerMock(
                     "AliAhmadr@yahoo.fr", "65421895154", "Charles", "Clementine",
                     "Homme", DateTime.Now, null, "6854", "Limoges"));

                db.Customers.Add(new CustomerMock
                (
                    "AliAhmadr@yahoo.fr", "65421895154", "Charles", "Zan", 
                    "Homme", DateTime.Now, null,  "6854",  "Limoges"
                ));

                db.Customers.Add(new CustomerMock
                (
                    "AliAhmadr@yahoo.fr", "65421895154",  "Kaly",  "Moli",
                     "Femme", DateTime.Now, null, "6854", "Limoges"
                ));

                db.Customers.Add(new CustomerMock
                (
                   "AliAhmadr@yahoo.fr", "65421895154", "Charles", "Jeep",
                    "Femme", DateTime.Now, null, "6854",  "Limoges"
                ));


                db.SaveChanges();
            }

            return db;
        }
    }
}
