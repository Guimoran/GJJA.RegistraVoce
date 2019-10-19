using UI = System.Console;
using GJJA.RegistraVoce.Domain;
using System;
using System.Collections.Generic;
using GJJA.RegistraVoce.Domain.Enums;
using System.Data.Common;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using GJJA.RegistraVoce.App.Console.Factories;
using GJJA.Repository.Common.Interfaces;
using GJJA.RegistraVoce.Repository.Entity;
using GJJA.ResgistraVoce.DataAccess.Entity.Context;

namespace GJJA.RegistraVoce.App.Console
{
    class Program
    {
        private static string _connectionString = "";
        private static ServiceProvider _serviceProvider;
        static void Main(string[] args)
        {
            SetUp();

            int opcao = -1;
            UI.WriteLine($"*** Bem-Vindo! ***{Environment.NewLine}");  // OU   UI.WriteLine("*** Bem-Vindo! ***\n");   
            while (opcao != 0)
            {
                UI.WriteLine($"O que você deseja fazer? {Environment.NewLine}");
                UI.WriteLine("1. Listar pessoas");
                UI.WriteLine("2. Inserir pessoas");
                UI.WriteLine("3. Atualizar pessoas");
                UI.WriteLine("4. Deletar pessoas");
                UI.WriteLine("5. Contar  pessoas");
                UI.WriteLine("0. Sair");
                opcao = Convert.ToInt32(UI.ReadLine());
                switch (opcao)
                {
                    case 0:
                        UI.WriteLine("Tchau ;)");
                        break;
                    case 1:
                        ShowPeople();
                        break;

                    case 2:
                        InsertPerson();
                        break;

                    case 3:
                        UpdatePerson();
                        break;

                    case 4:
                        DeletePerson();
                        break;

                    case 5:
                        CountPerson();
                        break;



                    default:
                        UI.WriteLine("Opção inválida!");
                        break;
                }
            }
        }

        private static void CountPerson()
        {
            try
            {
                //PersonDAO personDAO =  new PersonDAO (_connectionString);
                //UI.WriteLine($"Existe(m) {personDAO.Count()} pessoa(s) cadastrada(s):");

                //Refatorando o codigo 
               // RegistraVoceDbContext context = _serviceProvider.GetService<RegistraVoceDbContext>();
                ICrudRepository<Person, int> personRepository = _serviceProvider.GetService<ICrudRepository<Person, int>>();
                UI.WriteLine($"Existe(m) {personRepository.Select().Count()} pessoa(s) cadastrada(s):");

            }
            catch (Exception ex)
            {

                UI.WriteLine($"Houve um erro ao contar pessoas: {ex.Message} ");
            }
        }

        private static void DeletePerson()
        {
            try
            {
                ShowPeople();
                UI.Write("Id da pessoa ser deletada");

                int personId = Convert.ToInt32(UI.ReadLine());
                ICrudRepository<Person, int> personRepository = _serviceProvider.GetService<ICrudRepository<Person, int>>();
                personRepository.DeleteById(personId);

            }
            catch (Exception ex)
            {
                UI.WriteLine($"Houve um erro ao apagara pessoa: {ex.Message} ");
            }

        }



        private static void UpdatePerson()
        {
            try
            {
                // encadeamento de metodo  (ShowPeople())
                ShowPeople();
                UI.Write("ID da pessoa a ser atualizada: ");
                int personId = Convert.ToInt32(UI.ReadLine());
                ICrudRepository<Person, int> personRepository = _serviceProvider.GetService<ICrudRepository<Person, int>>();
                Person p = personRepository.SelectById(personId);
                if (p == null)
                {
                    //  exception especifica 
                    throw new ArgumentException("ID de pessoa inexistente.");
                }
                GetPersonFromUi(p);
                personRepository.Update(p);

            }
            catch (Exception ex)
            {
                UI.WriteLine($"Houve um erro ao atualiza a pessoa: {ex.Message} ");
            }

        }

        private static void InsertPerson()
        {
            try
            {
                UI.WriteLine("** Inserção de pessoas **");
                Person person = new Person();
                GetPersonFromUi(person);
                ICrudRepository<Person, int> personRepository = _serviceProvider.GetService<ICrudRepository<Person, int>>();
                personRepository.Insert(person);
                UI.WriteLine(" *** Pessoa cadastra com sucesso!*** ");
            }
            catch (Exception ex)
            {
                UI.WriteLine($"Houve um erro ao salvar a pessoa: {ex.Message} ");
            }
        }


        private static void ShowPeople()
        {

            ICrudRepository<Person, int> personRepository = _serviceProvider.GetService<ICrudRepository<Person, int>>();
            List<Person> people = personRepository.Select();
            if (people.Count == 0)
            {
                UI.WriteLine("não existem pessoas cadastradas");
            }
            else
            {
                //varrer 
                people.ForEach(person =>
                {
                    UI.WriteLine($"[{person.Id}] {person.Name}");
                });
            }
        }

        private static void GetPersonFromUi(Person person)
        {
            if (string.IsNullOrEmpty(person.Name))
            {
                UI.Write(" - Nome: ");
                person.Name = UI.ReadLine();
                UI.Write(" - Gênero (0 = M, 1 = F, 2 = Indefinido): ");
                person.Gender = (Gender)Convert.ToInt32(UI.ReadLine());
                UI.Write(" - CPF:  ");
                person.DocumentNumber = UI.ReadLine();
                UI.Write(" - RG:  ");
                person.Identification = UI.ReadLine();
                UI.Write(" - Data de nascimento: ");
                person.BirthDate = Convert.ToDateTime(UI.ReadLine());
                UI.Write(" - Estado Civil (0 = Solt, 1 = Cas,  2 = Divorc., 3 = Viuv.): ");
                person.MaritalStatus = (MaritalStatus)Convert.ToInt32(UI.ReadLine());
                UI.Write(" - Endereço: ");
                person.Address = UI.ReadLine();
                UI.Write(" - Telefone: ");
                person.Phone = UI.ReadLine();
            }
            else
            {
                UI.Write($" - Nome ({person.Name}): ");
                person.Name = UI.ReadLine();
                UI.Write($" - Gênero (0 = M, 1 = F, 2 = Indefinido ({person.Gender})): ");
                person.Gender = (Gender)Convert.ToInt32(UI.ReadLine());
                UI.Write($" - CPF ({person.DocumentNumber}): ");
                person.DocumentNumber = UI.ReadLine();
                UI.Write($" - RG ({person.Identification}) :  ");
                person.Identification = UI.ReadLine();
                UI.Write($" - Data de nascimento ({person.BirthDate}): ");
                person.BirthDate = Convert.ToDateTime(UI.ReadLine());
                UI.Write($" - Estado Civil (0 = Solt, 1 = Cas,  2 = Divorc., 3 = Viuv. ({person.MaritalStatus})): ");
                person.MaritalStatus = (MaritalStatus)Convert.ToInt32(UI.ReadLine());
                UI.Write($" - Endereço ({person.Address}): ");
                person.Address = UI.ReadLine();
                UI.Write($" - Telefone ({person.Phone}): ");
                person.Phone = UI.ReadLine();


            }
        }

        private static void SetUp()
        {
            


            IServiceCollection services = new ServiceCollection();
            services.AddSingleton<RegistraVoceDbContext>((provider) =>
            {
                return new RegistraVoceDbContextFactory().CreateDbContext(new string[] { });
            });
            services.AddScoped<ICrudRepository<Person, int>, PersonRepository>();
            _serviceProvider = services.BuildServiceProvider();

        }



    }
}
