using Core.Interfaces;
using Core.Interfaces.Base;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Repoo.Base
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _dbContext;

        public IAdminRepo Admin { get;}

      //  public ICategoryRepo Category { get; }

        public IPostRepo Post { get; }

        public ITagRepo Tag { get; }

        public ICategoryRepo Category { get; }

        public UnitOfWork(DataContext context, IAdminRepo adminRepo, IPostRepo postRepo, ITagRepo tagRepo,ICategoryRepo Category)
        {
            this._dbContext = context;
            this.Admin = adminRepo;
            this.Category = Category;
            this.Post = postRepo;
            this.Tag = Tag;
        }

        public void Dispose()
        {
        }

        public void save()
        {
            _dbContext.SaveChanges();
        }
    }
}
