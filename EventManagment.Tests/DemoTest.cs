using EventManagmentApi.Controllers;
using EventManagmentApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;
using Xunit;

namespace EventManagment.Tests
{
    public class DemoTest
    {
        [Fact]
        public void Test1()
        {
            Assert.True(2 == 2); //test
        }

        [Fact]
        public async Task EventIntegraionTest()
        {
            // Create DB context
            var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

            var optionBuilder = new DbContextOptionsBuilder<EventContext>();
            optionBuilder.UseSqlServer(configuration["ConnectionStrings:DefaultConnection"]);

            var context = new EventContext(optionBuilder.Options);

            //Create TestDB
            await context.Database.EnsureDeletedAsync();
            await context.Database.EnsureCreatedAsync();

            // Create Controller
            var controller = new EventsController(context);
            // Add Events
            await controller.Add(new Event { Date = DateTime.UtcNow, Location = "Addis Ababa", Description = "Learining" });

            // Check: Does GetAll return the added customers?
            var okResult = await controller.GetAll();
            //var result = okResult.ToArray() as OkObjectResult;
        }
    }
}
