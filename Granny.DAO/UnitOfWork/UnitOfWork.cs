using System;
using System.Threading.Tasks;
using Granny.DAO.Context;
using Granny.DAO.UnitOfWork.Interface;

namespace Granny.DAO.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        public GrannyContext grannyContext { get; private set; }

        public UnitOfWork()
        {
            grannyContext = new GrannyContext();
        }

        public void Save()
        {
            grannyContext.SaveChanges();
        }

        public async Task SaveAsync()
        {
            await grannyContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                grannyContext.Dispose();
            }
        }

    }
}
