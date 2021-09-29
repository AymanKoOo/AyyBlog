using Core.Interfaces.Base;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Repoo.Base
{
    public class GenericRepo<T> :IGenericRepo<T> where T : class
    {
        private readonly DataContext _dbcontext;

        private DbSet<T> table = null;

        public GenericRepo(DataContext dbcontext)
        {
            _dbcontext = dbcontext;
            table = _dbcontext.Set<T>();
        }

    }
}
