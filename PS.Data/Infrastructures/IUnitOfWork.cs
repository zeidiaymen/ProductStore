using PS.Data.Infrastructures;
using System;
using System.Collections.Generic;
using System.Text;

namespace PS.Data.Infrastructure
{
    public interface IUnitOfWork : IDisposable
    {
        void Commit();
        //void Dispose(); Hidden from IDisposable interface
        IRepository<T> getRepository<T>() where T : class;
    }
}
