using Core.Entites.Base;
using Core.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Interfaces
{
    public interface ICategoryRepo:IGenericRepo<category>
    {
        public void AddCat(category model);

    }
}
