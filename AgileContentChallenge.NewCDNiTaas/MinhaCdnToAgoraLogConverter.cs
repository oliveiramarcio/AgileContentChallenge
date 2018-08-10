using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;

namespace AgileContentChallenge.NewCDNiTaas
{
    public static class MinhaCdnToAgoraLogConverter
    {
        public static AgoraLog ConvertLog(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
            {
                throw new ArgumentNullException("url");
            }
            else if (!Uri.IsWellFormedUriString(url, UriKind.Absolute))
            {
                throw new UriFormatException(url);
            }

            using (WebClient webClient = new WebClient())
            {
                try
                {
                    string[] minhaCdnLog = webClient.DownloadString(url).Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

                    if (minhaCdnLog.Count() == 0)
                    {
                        throw new NullReferenceException("Invalid download file!");
                    }

                    var logLines = new List<AgoraLogLine>();

                    foreach (string line in minhaCdnLog)
                    {
                        string[] log = line.Split('|');
                        string[] request = log[3].Replace("\"", "").Split(' ');

                        logLines.Add(
                            new AgoraLogLine(
                                new HttpMethod(request[0]),
                                (HttpStatusCode)Convert.ToInt32(log[1]),
                                request[1],
                                Convert.ToInt32(Math.Ceiling(Convert.ToDouble(log[4], CultureInfo.InvariantCulture) * 100) / 100),
                                Convert.ToInt32(log[0]),
                                log[2].ToUpper().Equals("INVALIDATE") ? "REFRESH_HIT" : log[2]));
                    }

                    return new AgoraLog(logLines);
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }
    }
}