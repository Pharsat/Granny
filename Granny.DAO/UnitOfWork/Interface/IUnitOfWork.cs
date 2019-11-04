using Granny.DAO.Context;
using System;

namespace Granny.DAO.UnitOfWork.Interface
{
    public interface IUnitOfWork : IDisposable
    {
        GrannyContext grannyContext { get; }

        void Save();
    }
}
