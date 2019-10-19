using GJJA.RegistraVoce.Domain;
using GJJA.Repository.Common.Entity;
using GJJA.ResgistraVoce.DataAccess.Entity.Context;
using Microsoft.EntityFrameworkCore;

namespace GJJA.RegistraVoce.Repository.Entity
{
    public class PersonRepository : EntityRepositoryBase<Person, int>
    {
        public PersonRepository(RegistraVoceDbContext context) : base(context)
        {
        }
    }

}