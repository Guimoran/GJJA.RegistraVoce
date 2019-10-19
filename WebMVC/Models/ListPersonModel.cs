using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using GJJA.RegistraVoce.Domain;
using WebMVC.Models;

namespace WebMVC.Models
{
     public class ListPersonModel
     {  
          public List<Person> Lista {get; set; }
          public ListPersonModel()
          {
              Lista = new List<Person>();
          }

          public ListPersonModel (List<Person> lista)
          {
              Lista = new List<Person>();
              foreach(Person person in lista)
              {
                  Lista.Add(person);
              }
          }
    }
}