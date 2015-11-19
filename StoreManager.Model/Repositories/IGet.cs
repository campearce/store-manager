using System;
using System.Collections.Generic;

namespace StoreManager.Model.Repositories
{
    public interface IGet<T>
    {
        IEnumerable<T> All();
        IEnumerable<T> Find(Func<T, bool> predicate);
        T GetByName(string name);
        T Get(string id);
    }
}
