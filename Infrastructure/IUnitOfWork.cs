
using System;
using TanvirArjel.EFCore.GenericRepository;


namespace Infrastructure
{
    public interface IUnitOfWork : IDisposable
    {
        public DBContext Context { get; }
        public IRepository Repository { get; }
    }

    public class UnitOfWork(
     DBContext dBContext,
     IRepository repository) : IUnitOfWork
    {
        public bool IsDisposed { get; set; }

        public DBContext Context { get => dBContext; }
        public IRepository Repository { get => repository; }

        #region dispose
        public void Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// https://docs.microsoft.com/en-us/dotnet/standard/garbage-collection/implementing-dispose
        /// </summary>
        protected virtual void Dispose(bool disposing)
        {
            if (IsDisposed)
            {
                return;
            }

            IsDisposed = true;
        }

        ~UnitOfWork()
        {
            Dispose(false);
        }

        #endregion

    }
}
