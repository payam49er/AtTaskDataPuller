using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtTaskDataModel.Repository
{
    public class AtTaskRepository:IAtTaskRepository,IDisposable
    {
        private readonly AtTaskContext _context;
        private bool _disposed = false;

        public AtTaskRepository(AtTaskContext context )
            {
            _context = context;
            }

        public IEnumerable<AtTaskModel> GetAtTaskObjects()
            {
            return _context.AtTaskModels.ToList();
            }

        public AtTaskModel GetAtTaskObjectById( string atTaskId )
            {
            return _context.AtTaskModels.Find(atTaskId);
            }

        public void InsertOneRecord( AtTaskModel atTaskData )
            {
            _context.AtTaskModels.Add(atTaskData);
            }

        public void InsertManyRecords( IEnumerable<AtTaskModel> atTaskData )
            {
            _context.AtTaskModels.AddRange(atTaskData);
            }

        public void DeleteAtTaskRecord( string atTaskId )
            {
            AtTaskModel atTask = _context.AtTaskModels.Find(atTaskId);
            _context.AtTaskModels.Remove(atTask);
            }

        public void UpdateAtTaskRecord( AtTaskModel atTaskObject )
            {
            _context.Entry(atTaskObject).State = EntityState.Modified;
            }

        public void Save()
            {
            _context.SaveChanges();
            }

        protected virtual void Dispose( bool disposing )
            {
            if (!_disposed)
                {
                if (disposing)
                    {
                    _context.Dispose();
                    }
                }
            this._disposed = true;
            }

        public void Dispose()
            {
            Dispose(true);
            GC.SuppressFinalize(this);
            }
        }
}
