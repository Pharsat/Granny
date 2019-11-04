using Granny.DAO.Context;
using Granny.DAO.UnitOfWork.Interface;

namespace Granny.DAO.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        public GrannyContext grannyContext { get; private set; }

        public UnitOfWork()
        {
            this.grannyContext = new GrannyContext();
        }

        public void Dispose()
        {
            this.grannyContext.Dispose();
        }

        public void Save()
        {
            this.grannyContext.SaveChanges();
        }
    }
}
