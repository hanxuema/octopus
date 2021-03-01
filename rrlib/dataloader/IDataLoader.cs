using System;
using System.Collections.Generic; 

namespace rrlib.Dataloader
{
    public interface IDataLoader<T>
    {
         List<T> Loader();
    }
}