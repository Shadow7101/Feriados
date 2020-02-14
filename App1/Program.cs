using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace App1
{
    class Program
    {
        /// <summary>
        /// http://www.calendario.com.br/api_feriados_municipais_estaduais_nacionais.php
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {



            while (true)
            {
                Console.Clear();
                try
                {
                    Console.WriteLine("Entre com o ano: ");
                    string entrada = Console.ReadLine();
                    int ano = int.Parse(entrada);
                    if (ano < 1998 || ano > DateTime.Now.AddYears(10).Year) throw new Exception("Ano inválido!");

                    var feriados = Feriados(ano);

                    Console.WriteLine("\n\nFeriados: \n\n");

                    foreach (var feriado in feriados)
                    {
                        Console.WriteLine($"{feriado.date} - {feriado.name}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("------------------");
                    Console.WriteLine("Operação inválida!");
                    Console.WriteLine(ex.Message);
                    Console.WriteLine("------------------");

                }


                Console.WriteLine("\n\nPressione qualquer tecla para continuar...");
                Console.ReadKey();
            }
        }


        private static List<FERIADOS> Feriados(Int32 Ano)
        {
            List<FERIADOS> ret = new List<FERIADOS>();
            string url = string.Empty;
            var json_data = string.Empty;
            StringBuilder sb = new StringBuilder();

            using (var w = new WebClient())
            {
                w.Encoding = Encoding.UTF8;
                sb.Append("https://api.calendario.com.br/?json=true&ano=");
                sb.Append(Ano.ToString());
                sb.Append("&ibge=3550308&token=anVsaW8ud29qdGVua29AdGVycmEuY29tLmJyJmhhc2g9MjE1NDY4NDc3");
                url = sb.ToString();
                try
                {
                    json_data = w.DownloadString(url);
                }
                catch (Exception ex)
                {
                }
                //Newtonsoft.Json
                ret = JsonConvert.DeserializeObject<List<FERIADOS>>(json_data);
                return ret;
            }
        }

        [Serializable]
        private class FERIADOS
        {
            public string date { get; set; }
            public string name { get; set; }
            public string link { get; set; }
            public string type { get; set; }
            public string description { get; set; }
            public string type_code { get; set; }
        }
    }
}
