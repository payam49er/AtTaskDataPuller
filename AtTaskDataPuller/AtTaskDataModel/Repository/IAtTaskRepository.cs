using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtTaskDataModel.Repository
{
    public interface IAtTaskRepository
    {
        IEnumerable<AtTaskModel> GetAtTaskObjects();
        AtTaskModel GetAtTaskObjectById(string atTaskId);
        void InsertOneRecord(AtTaskModel atTaskData);
        void InsertManyRecords(IEnumerable<AtTaskModel> atTaskData);
        void DeleteAtTaskRecord(string atTaskId);
        void UpdateAtTaskRecord(AtTaskModel atTaskObject);
        void Save();
    }
}
