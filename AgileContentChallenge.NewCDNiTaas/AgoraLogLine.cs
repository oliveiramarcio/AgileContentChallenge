using System;
using System.Net;
using System.Net.Http;

namespace AgileContentChallenge.NewCDNiTaas
{
    public class AgoraLogLine
    {
        public HttpMethod HttpMethod { get; protected set; }
        public HttpStatusCode StatusCode { get; protected set; }
        public string UriPath { get; protected set; }
        public int TimeTaken { get; protected set; }
        public int ResponseSize { get; protected set; }
        public string CacheStatus { get; protected set; }

        public AgoraLogLine(HttpMethod httpMethod, HttpStatusCode statusCode, string uriPath,
            int timeTaken, int responseSize, string cacheStatus)
        {
            if (httpMethod == null)
            {
                throw new ArgumentNullException("http method");
            }
            else if (string.IsNullOrWhiteSpace(uriPath))
            {
                throw new ArgumentNullException("uri path");
            }
            else if (timeTaken < 0)
            {
                throw new ArgumentOutOfRangeException("time taken");
            }
            else if (responseSize < 0)
            {
                throw new ArgumentOutOfRangeException("response size");
            }
            else if (string.IsNullOrWhiteSpace(cacheStatus))
            {
                throw new ArgumentNullException("cache status");
            }
            
            this.HttpMethod = httpMethod;
            this.StatusCode = statusCode;
            this.UriPath = uriPath;
            this.TimeTaken = timeTaken;
            this.ResponseSize = responseSize;
            this.CacheStatus = cacheStatus;
        }
    }
}