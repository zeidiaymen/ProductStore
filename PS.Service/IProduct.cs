using PS.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace PS.Service
{
public     interface IProduct:IService<Product>
    {
        IList<Product> FindMost5ExpensiveProds();
        float UnavailableProdPercentage();
        IList<Product> GetProdByClient(Client c);
        void DeleteOldProducts();


    }
}
