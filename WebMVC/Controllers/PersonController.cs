using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;
using GJJA.RegistraVoce.Domain;
using GJJA.RegistraVoce.Domain.Enums;
using WebMVC.Models;
using WebMVC.Factories;
using System;
using GJJA.Repository.Common.Interfaces;
using GJJA.RegistraVoce.Repository.Entity;
using GJJA.ResgistraVoce.DataAccess.Entity.Context;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;

namespace WebMVC.Controllers
{
    public class PersonController : Controller
    {

        public PersonController()
        {
            SetUp();
        }

     private static ServiceProvider _serviceProvider;
        public IActionResult Index()
        {
             ICrudRepository<Person, int> personRepository = _serviceProvider.GetService<ICrudRepository<Person, int>>();
            List<Person> people = personRepository.Select();
            ListPersonModel model = new ListPersonModel(people);

            return View(model);

            
        }

        public IActionResult DeletePerson(Int32 Id)
        {
            return View();
        }



        public IActionResult Welcome()
        {
            var model = new PersonModel();
            model.Id = 1;
            model.Name = "Janaira";
            model.Gender = Gender.Male;
            model.DocumentNumber = "11223344";
            model.Identification = "22334455";
            model.BirthDate = new DateTime(1990, 01, 03);
            model.Address = "Minha Rua, Meu numero";
            model.Phone = "12981386001";
            model.MaritalStatus = MaritalStatus.Single;
            return View(model);

        }

         private static void SetUp()
        {
            


            IServiceCollection services = new ServiceCollection();
            services.AddSingleton<RegistraVoceDbContext>((provider) =>
            {
                return new WebMVCDbContextFactory().CreateDbContext(new string[] { });
            });
            services.AddScoped<ICrudRepository<Person, int>, PersonRepository>();
            _serviceProvider = services.BuildServiceProvider();

        }
       
    }
}