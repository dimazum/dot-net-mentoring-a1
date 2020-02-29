using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NorthwindSite.Models
{
    public class BaseViewModel<T>
    {
        public BaseViewModel(T currentModel)
        {
            CurrentModel = currentModel;
        }
        public T CurrentModel { get; }
    }
 
}
