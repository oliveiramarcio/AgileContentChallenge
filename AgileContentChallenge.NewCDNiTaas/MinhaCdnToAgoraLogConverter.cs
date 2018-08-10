using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;

namespace AgileContentChallenge.NewCDNiTaas
{
    public static class MinhaCdnToAgoraLogConverter
    {
        public static AgoraLog ConvertLog(string url, string outputFile)
        {
            if (string.IsNullOrWhiteSpace(url))
            {
                throw new ArgumentNullException("url");
            }
            else if (!Uri.IsWellFormedUriString(url, UriKind.Absolute))
            {
                throw new UriFormatException(url);
            }
            else if (string.IsNullOrWhiteSpace(outputFile))
            {
                throw new ArgumentNullException("output file");
            }

            using (WebClient webClient = new WebClient())
            {
                try
                {
                    string[] minhaCdnLog = webClient.DownloadString(url).Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

                    if (minhaCdnLog.Count() == 0)
                    {
                        throw new ArgumentException("Invalid download file.");
                    }

                    var logLines = new List<AgoraLogLine>();

                    foreach (string line in minhaCdnLog)
                    {
                        string[] log = line.Split('|');

                        if (log.Count() != 5)
                        {
                            throw new ArgumentException("Invalid download file format.");
                        }

                        string[] request = log[3].Replace("\"", "").Split(' ');

                        if (request.Count() != 3)
                        {
                            throw new ArgumentException("Invalid download file format.");
                        }

                        logLines.Add(
                            new AgoraLogLine(
                                new HttpMethod(request[0]),
                                (HttpStatusCode)Convert.ToInt32(log[1]),
                                request[1],
                                Convert.ToInt32(Math.Ceiling(Convert.ToDouble(log[4], CultureInfo.InvariantCulture) * 100) / 100),
                                Convert.ToInt32(log[0]),
                                log[2].ToUpper().Equals("INVALIDATE") ? "REFRESH_HIT" : log[2]));
                    }

                    if (!Directory.Exists(Path.GetDirectoryName(outputFile)))
                    {
                        Directory.CreateDirectory(Path.GetDirectoryName(outputFile));
                    }

                    AgoraLog agoraLog = new AgoraLog(logLines);

                    StreamWriter file = new StreamWriter(outputFile, true, Encoding.UTF8);
                    file.WriteLine(agoraLog.ToString());
                    file.Flush();
                    file.Close();

                    return agoraLog;
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }
    }
}