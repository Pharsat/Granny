using Granny.DAO.Context;
using System;
using System.Threading.Tasks;

namespace Granny.DAO.UnitOfWork.Interface
{
    public interface IUnitOfWork : IDisposable
    {
        GrannyContext grannyContext { get; }

        void Save();

        Task SaveAsync();
    }
}
