using System;
namespace GJJA.Repository.Common.Interfaces
{
    public interface IDeleteRepository<TDomain, TKey>
        where TDomain : class
    {
        void Delete (TDomain domain);
        void DeleteById(TKey id);
    }

}