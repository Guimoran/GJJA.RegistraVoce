using System;
using GJJA.RegistraVoce.Domain.Enums;

namespace GJJA.RegistraVoce.Domain
{ //POCO - POCO significa Plain Old CLR Object.
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Gender Gender { get; set; }   // CTROL PONTO PARA ADICIONAR USING
        public string DocumentNumber  { get; set; }
        public string Identification { get; set; }
        public DateTime BirthDate  { get; set; } // CTROL PONTO PARA ADICIONAR USING
        public MaritalStatus  MaritalStatus { get; set; }
        public string Address  { get; set; }
        public string Phone { get; set; }

       // public string Bla {get; set;}
    }

}