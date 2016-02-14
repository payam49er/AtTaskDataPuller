using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    internal class AtTaskException:Exception
    {     
        public AtTaskException(string message) : base(message){}
    }
}
