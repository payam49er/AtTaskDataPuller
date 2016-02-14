using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Interfaces
{
    public interface ILog
    {
        bool IsLoggingActivated();
        void ActivateLogging();
        void DeActivateLogging();
        void SetLoggingPath(string path);
        void LogIt(string message);
        void LogIt(Exception exception);
    }
}
