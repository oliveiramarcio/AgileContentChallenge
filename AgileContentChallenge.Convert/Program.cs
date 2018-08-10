using AgileContentChallenge.NewCDNiTaas;
using System;
using System.IO;
using System.Linq;
using System.Text;

namespace AgileContentChallenge.Convert
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("...::: Agile Content Challenge :::...\r\n");
            Console.WriteLine("* 2 - New CDN iTaas:\r\n");

            if ((args.Count() == 0) || (string.IsNullOrWhiteSpace(args[0])) || (string.IsNullOrWhiteSpace(args[1])) || (!Uri.IsWellFormedUriString(args[0], UriKind.Absolute)))
            {
                Console.WriteLine(">> Usage: convert url outputFile\r\n");
            }
            else
            {
                Console.WriteLine("Downloading and converting log file:\r\n+ {0} +\r\nPlease wait...\r\n", args[0]);

                string agoraLog = MinhaCdnToAgoraLogConverter.ConvertLog(args[0]).ToString();
                Console.WriteLine(agoraLog);

                if (!Directory.Exists(Path.GetDirectoryName(args[1])))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(args[1]));
                }

                StreamWriter file = new StreamWriter(args[1], true, Encoding.UTF8);
                file.WriteLine(agoraLog);
                file.Flush();
                file.Close();

                Console.WriteLine("+ Log saved: {0}. +\r\n", args[1]);
            }

            Console.WriteLine("Pressione qualquer tecla para sair...");
            Console.ReadKey();
        }
    }
}