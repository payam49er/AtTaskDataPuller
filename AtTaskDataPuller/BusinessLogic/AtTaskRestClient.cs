using System;
using System.Collections.Generic;
using System.Configuration;
using BusinessLogic.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BusinessLogic
{
    public class AtTaskRestClient:IAtTaskRestClient,IDisposable
    {
        private JToken sessionResponse;
        private readonly RestClient client;
        private readonly Log _log;

        /// <summary>
        /// Gets if the instance of the AtTaskRestClient has successfully logged in
        /// </summary>
        public bool IsSignedIn => sessionResponse != null;

        /// <summary>
        /// Gets the session id returned by the login command
        /// </summary>
        public string SessionID => sessionResponse?.Value<string>("sessionID");

        /// <summary>
        /// Gets the ID of the user that is currently logged in
        /// </summary>
        public string UserID => sessionResponse?.Value<string>("userID");

        /// <summary>
        /// If true, every request sent will print the full URL being requested to the console
        /// </summary>
        public bool DebugUrls
        {
            get { return client.DebugUrls; }
            set { client.DebugUrls = value; }
        }


        /// <summary>
        /// Creates a new AtTaskRestClient.
        /// </summary>
        /// <param name="apiPath">
        /// The URL to the api of the AtTask API.
        /// For example:
        /// "http://yourcompany.attask-ondemand.com/attask/api"
        /// </param>
        public AtTaskRestClient(string apiPath)
        {
            if (string.IsNullOrEmpty(apiPath))
            {
                throw new AtTaskException("apiPath cannot be null or empty");
            }
            if (apiPath.EndsWith("/"))
            {
                apiPath = apiPath.Substring(0, apiPath.Length - 1);
            }
            this.client = new RestClient(apiPath);

            _log = new Log();
        }


        /// <summary>
        /// Logs in as the given user. <see cref="AtTaskRestClient"> tracks the session for you so
        /// you do not need to specify the sessionID in the parameters of other commands called by this
        /// <see cref="AtTaskRestClient">.
        /// Throws an AtTaskException if you are already logged in.
        /// </summary>
        /// <param name="username">
        /// A <see cref="System.String"/>
        /// </param>
        /// <param name="password">
        /// A <see cref="System.String"/>
        /// </param>
        public void Login()
        {
            if (IsSignedIn)
            {
                throw new AtTaskException("Cannot sign in: already signed in.");
            }
            var loginData = GrabLoginInfo();
            JToken json = client.DoLogin("/login", "username=" + loginData.Item1, "password=" + loginData.Item2);
            sessionResponse = json["data"];
        }


        /// <summary>
        /// Clears your current session.
        /// Throws an AtTaskException if you are not logged in.
        /// </summary>
        public void Logout()
        {
            if (!IsSignedIn)
            {
                throw new AtTaskException("Cannot log out: not signed in.");
            }
            client.DoLogin("/logout", "sessionID=" + SessionID);
            sessionResponse = null;
        }

        /// <summary>
        /// Throws an exception if the client isn't logged in
        /// </summary>
        private void VerifySignedIn()
        {
            if (!IsSignedIn)
            {
                throw new AtTaskException("You must be signed in");
            }
        }

        public void Dispose()
        {
            if (IsSignedIn)
            {
                this.Logout();
            }
        }

        private Tuple<string, string> GrabLoginInfo()
        {
            var username = ConfigurationManager.AppSettings.Get("username");
            var password = ConfigurationManager.AppSettings.Get("password");

            Tuple<string, string> loginData = new Tuple<string, string>(username, password);
            return loginData;
        }


        /// <summary>
        /// Converts an <see cref="System.Object"/> to a <see cref="System.String[]"/>.
        /// Reflects on all the property names in the given object
        /// and returns a <see cref="System.String[]"/> representation that can be used
        /// with the <see cref="RestClient"/s>
        /// </summary>
        /// <param name="parameters">
        /// The <see cref="System.Object"/> to be reflected on.
        /// </param>
        /// <param name="toAdd">
        /// A <see cref="System.String[]"/> of additional parameters to add to the <see cref="System.String[]"/>
        /// </param>
        /// <returns>
        /// A <see cref="System.String[]"/> 
        /// </returns>
        private string[] parameterObjectToStringArray(object parameters, params string[] toAdd)
        {
            var properties = parameters.GetType().GetProperties();
            List<string> p = new List<string>(properties.Length);
            p.AddRange(toAdd);
            foreach (var prop in properties)
            {
                string line = string.Format("{0}={1}", prop.Name, prop.GetValue(parameters, null));
                p.Add(line);
            }
            return p.ToArray();
        }


        /// <summary>
        /// This method makes sure that the Json data are in valid format
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public bool IsValidJson(string input)
        {
            try
            {
                JToken.Parse(input);
                return true;
            }
            catch (JsonReaderException jsonReaderException)
            {
                Console.WriteLine(jsonReaderException.Message);
                return false;
            }
        }

        public double CountOfRecords(ObjCode objCode, object parameters)
        {
            string request = BuildRequest(objCode, parameters, Operation.Operations.Count);
            JToken count = MakeRequest(request);
            return count.SelectToken("data.count").Value<double>();
        }

        public void Paginate(ObjCode objcode, object parameters, int limit = 2000)
        {
            //todo:I have to fix this in a different way. Do not buggle up Search method 
            double totalCount = CountOfRecords(objcode, parameters);
            int pages = (int) totalCount/limit;
            int reminder = (int) totalCount%limit;
            for (int i = 0; i < pages; i++)
            {
                string request = BuildRequest(objcode, parameters, Operation.Operations.Search);
                MakeRequest(request);
            }

        }


        /// <summary>
        /// Build request that will be sent to the server 
        /// </summary>
        /// <param name="objCode"></param>
        /// <param name="parameters"></param>
        /// <param name="operationType"></param>
        /// <returns></returns>
        public string BuildRequest(ObjCode objCode, object parameters, Operation.Operations operationType)
        {
            string[] fieldsToInclude = parameterObjectToStringArray(parameters, "sessionID=" + SessionID);

            string path = null;

            switch (operationType)
            {
                case Operation.Operations.Search:
                    path = string.Format("/{0}/search", objCode);
                    break;
                case Operation.Operations.Post:
                    path = string.Format("/{0}/post", objCode);
                    break;
                case Operation.Operations.Delete:
                    path = string.Format("/{0}/delete", objCode);
                    break;
                case Operation.Operations.Put:
                    path = string.Format("/{0}/put", objCode);
                    break;
                case Operation.Operations.Count:
                    path = string.Format("/{0}/count", objCode);
                    break;
                default: //the case for the Get, the default operation
                    break;
            }


            if (!string.IsNullOrEmpty(path) && !path.StartsWith("/"))
            {
                path = "/" + path;
            }
            _log.LogIt(string.Format("Request is created as {0}",
                client.url + path + client.ToQueryString(fieldsToInclude)));
            return client.url + path + client.ToQueryString(fieldsToInclude);
        }


        public JToken MakeRequest(string request)
        {
            VerifySignedIn();
            return client.DoRequest(request);
        }
    }
}
