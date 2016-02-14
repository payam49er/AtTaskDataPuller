using Newtonsoft.Json.Linq;

namespace BusinessLogic.Interfaces
{
    public interface IAtTaskRestClient
    {
        /// <summary>
        /// Gets if the instance of the AtTaskRestClient has successfully logged in
        /// </summary>
        bool IsSignedIn { get; }

        double CountOfRecords(ObjCode objCode, object parameters);
        void Paginate(ObjCode objcode, object parameters, int limit = 2000);

        /// <summary>
        /// Build request that will be sent to the server 
        /// </summary>
        /// <param name="objCode"></param>
        /// <param name="parameters"></param>
        /// <param name="operationType"></param>
        /// <returns></returns>
        string BuildRequest(ObjCode objCode, object parameters, Operation.Operations operationType);

        JToken MakeRequest(string request);
    }
}
