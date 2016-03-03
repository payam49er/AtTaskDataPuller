using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace BusinessLogic.Interfaces
{
    public interface IAtTaskRestClient
    {
        /// <summary>
        /// Gets if the instance of the AtTaskRestClient has successfully logged in
        /// </summary>
        bool IsSignedIn { get; }

        Task<double> CountOfRecordsAsync(ObjCode objCode, object parameters);
        void Paginate(ObjCode objcode, object parameters, int limit = 2000);

        /// <summary>
        /// Build request that will be sent to the server 
        /// </summary>
        /// <param name="objCode"></param>
        /// <param name="parameters"></param>
        /// <param name="operationType"></param>
        /// <returns></returns>
        Task<string> BuildRequestAsync(ObjCode objCode, object parameters, Operation.Operations operationType);

        Task<JToken> MakeRequestAsync(string request);
    }
}
