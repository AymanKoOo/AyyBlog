using Core.Entites;
using Core.Interfaces;
using Infrastructure.Data;
using Infrastructure.Repoo.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Repoo
{
    public class TagRepo : GenericRepo<Tag>, ITagRepo
    {
        private readonly DataContext dataContext;

        public TagRepo(DataContext dataContext) : base(dataContext)
        {
            this.dataContext = dataContext;
        }
   
    }
}
