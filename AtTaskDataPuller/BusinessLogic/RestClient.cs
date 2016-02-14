using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using Newtonsoft.Json.Linq;

namespace BusinessLogic
{
    public class RestClient
    {
        public string url;
        public bool DebugUrls { get; set; }

        /// <summary>
        /// Creates a new RestClient that sends requests to the given root URL.
        /// </summary>
        /// <param name="url">
        /// A <see cref="System.String"/>
        /// </param>
        public RestClient(string url)
        {
            if (url.EndsWith("/"))
            {
                this.url = url.Substring(0, url.Length - 1);
            }
            else
            {
                this.url = url;
            }
        }

        /// <summary>
        /// Making the login 
        /// </summary>
        /// <param name="path"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public JToken DoLogin(string path, params string[] parameters)
        {
            List<string> list = parameters.ToList();
            list.Insert(0, "method=post");

            if (!path.StartsWith("/"))
            {
                path = "/" + path;
            }
            string fullUrl = url + path + ToQueryString(parameters);

            return DoRequest(fullUrl);
        }


        /// <summary>
        /// Sending requests to the server
        /// </summary>
        /// <param name="fullUrl"></param>
        /// <returns></returns>
        public JToken DoRequest(string fullUrl)
        {
            if (DebugUrls) Console.WriteLine("Requesting: {0}", fullUrl);

            WebRequest request = HttpWebRequest.CreateDefault(new Uri(fullUrl));
            using (WebResponse response = request.GetResponse())
            {
                using (Stream responseStream = response.GetResponseStream())
                {
                    return ReadResponse(responseStream);
                }
            }
        }

        /// <summary>
        /// Converts the given <see cref="System.String[]"/> to query string format.
        /// </summary>
        /// <returns>
        /// If the parameters array contains ["item1", "item2"] the result will be "?item1&item2"
        /// </returns>
        public string ToQueryString(string[] parameters, int limit = 100)
        {
            StringBuilder sb = new StringBuilder();
            parameters.ToList().ForEach(s => sb.Append(s).Append("&"));
            if (sb.Length > 0)
            {
                sb.Remove(sb.Length - 1, 1);
            }

            return limit > 100 ? "&" + sb : "?" + sb;
        }



        /// <summary>
        /// Reads the given stream to the end then creates a new JToken containing all the data read.
        /// </summary>
        /// <param name="stream">
        /// A <see cref="Stream"/> that provides JSON data.
        /// </param>
        /// <returns>
        /// A <see cref="JToken"/>
        /// </returns>
        private JToken ReadResponse(Stream stream)
        {
            StreamReader reader = new StreamReader(stream);
            string body = reader.ReadToEnd();
            return JObject.Parse(body);
        }

    }
}
