using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces.Base
{
    public interface IUnitOfWork : IDisposable
    {

        IAdminRepo Admin { get; }
        IPostRepo Post { get; }
        ICommentRepo comment { get; }
        ICategoryRepo Category { get; }
        ITagRepo Tag { get; }
        void save();
   
    }
}
