using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ClientAuthenticationVIaRawUrl
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {

                string html = string.Empty;
                string url = @"https://raw.githubusercontent.com/hammadsajid786/cl-DotNet-javscript-hlo/master/clientRequestFile";

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.AutomaticDecompression = DecompressionMethods.GZip;

                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    html = reader.ReadToEnd();

                    if (html.Contains("success"))
                    {
                        Console.WriteLine("You are successfully logged in");
                    }
                    else
                    {
                        Console.WriteLine("Invalid credentials");
                    }
                }

                Console.WriteLine(html);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.ReadKey();
        }
    }
}
