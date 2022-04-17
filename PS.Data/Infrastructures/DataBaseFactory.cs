using System;
using System.Collections.Generic;
using System.Text;

namespace PS.Data.Infrastructures
{
    public class DataBaseFactory : Disposable, IDataBaseFactory
    {
        public PSContext DataContext => new PSContext();


        public override void DisposeCore()
        {
            DataContext.Dispose();
        }
    }
}
