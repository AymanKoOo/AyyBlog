using Core.Entites.Base;
using Core.Interfaces;
using Core.Interfaces.Base;
using Infrastructure.Data;
using Infrastructure.Repoo.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Repoo
{
    public class CategoryRepo: GenericRepo<category>,ICategoryRepo
    {
        private readonly DataContext _dbContext;
 

        public CategoryRepo(DataContext dbcontext) : base(dbcontext)
        {
            this._dbContext = dbcontext;
        }

        public void AddCat(category model)
        {
            _dbContext.categories.Add(model);
        }
    }
}
