using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AgileContentChallenge.NewCDNiTaas
{
    public class AgoraLog
    {
        public string Version { get; protected set; }
        public string Provider { get; protected set; }
        public IList<AgoraLogLine> Lines { get; protected set; }

        public AgoraLog()
        { }

        public AgoraLog(IList<AgoraLogLine> lines)
        {
            if ((lines == null) || (lines.Count() == 0))
            {
                throw new ArgumentNullException("lines");
            }

            this.Version = "1.0";
            this.Provider = "\"MINHA CDN\"";
            this.Lines = lines;
        }

        public override string ToString()
        {
            StringBuilder log = new StringBuilder(string.Format("# Version: {0}\r\n", this.Version));
            log.AppendLine(string.Format("# Date: {0:dd/MM/yyyy HH:mm:ss}", DateTime.Now));
            log.AppendLine("# Fields: provider http-method status-code uri-path time-taken response-size cache-status");

            foreach (var line in this.Lines)
            {
                log.AppendLine(string.Format("{0} {1} {2} {3} {4} {5} {6}", this.Provider, line.HttpMethod.Method.ToUpper(), (int)line.StatusCode, line.UriPath, line.TimeTaken, line.ResponseSize, line.CacheStatus));
            }

            return log.ToString();
        }
    }
}