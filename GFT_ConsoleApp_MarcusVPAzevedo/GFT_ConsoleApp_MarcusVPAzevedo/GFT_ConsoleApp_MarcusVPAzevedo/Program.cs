using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;

namespace GFT_ConsoleApp_MarcusVPAzevedo
{
    static class Program
    {
        public static void Main(string[] args)
        {
            //o Unit Test não funcionou por algum motivo
            //Fiz as chamadas por aqui para mostrar a execução, de acordo com as regras de negócio estabelecidas
            string sParametros = "";
            sParametros = "morning, 1, 1, 2, 2, 3, 3, 3, 4, 4, 5";
            Executar(sParametros);
            sParametros = "Morning, 2, 2, 1, 1, 3, 3, ";
            Executar(sParametros);
            sParametros = "Night, 1, 1, 2, 2, 2, 3, 3, 4, 4, 5";
            Executar(sParametros);
            sParametros = "nIght, 2, 2, 1, 1, 3, 3, ";
            Executar(sParametros);
            //

            Menu();
        }

        public static void Executar(string sParametros)
        {
            //trata o parâmetro, independente do conteúdo
            sParametros = sParametros.Replace(" ", "").ToLower();
            string sRetorno = "";
            Retorno retorno = new Retorno();
            StringBuilder sbErros = new StringBuilder();

            // Valida a entrada
            if (ValidarParametros(sParametros))
            {
                List<Retorno> listaRetorno = MontarRetorno(sParametros, sbErros);

                foreach (Retorno lRetorno in listaRetorno)
                {
                    sRetorno += lRetorno.NomeDish + ", ";
                }

                if (!sRetorno.Equals(""))
                {
                    sRetorno = sRetorno.Substring(0, sRetorno.Length - 2);
                }

                Console.WriteLine(sRetorno);

                if (sbErros.Length > 0)
                {
                    string sErros = sbErros.ToString();
                    sErros = sErros.Substring(0, sErros.Length - 2);
                    sErros = "Alguns erros de entrada foram encontrados na sequência: " + sErros.ToString();
                    Console.WriteLine(sErros);
                    Console.WriteLine("\n");
                }

            }
        }
        public static List<Retorno> MontarRetorno(string sParametros, StringBuilder sbErros)
        {
            Retorno retorno = new Retorno();
            List<Retorno> listaRetorno = new List<Retorno>();
            string HoraDoDia = "";
            string[] arrParametros = sParametros.Split(',');

            int contador1 = 0;
            int contador2 = 0;
            int contador3 = 0;
            int contador4 = 0;
            
            //Analisa a posição 0, com a hora do dia
            retorno.Posicao = 0;
            HoraDoDia = arrParametros[0];
            retorno.NomeDish = HoraDoDia;
            listaRetorno.Add(retorno);

            arrParametros = arrParametros.SubArray(1, arrParametros.Count() - 1); 
            //Analisa demais parâmetros, mesmo fora da ordem, em estrutura prevista (1 a 4)
            foreach (var param in arrParametros)
            {
                if (param.Equals("1")) { contador1++; }
                else if (param.Equals("2")) { contador2++; }
                else if (param.Equals("3")) { contador3++; }
                else if (param.Equals("4")) { contador4++; }
                else
                {
                    if (!param.Equals("")) { sbErros.AppendFormat("{0}, ", param); }
                }
            }

            //Filtra parâmetros para montagem de retorno, usando Alias Dish, permitindo manutenção nas descrições
            if (contador1 > 0)
            {
                retorno = new Retorno();
                retorno.Posicao = 1;
                retorno.NomeDish = HoraDoDia.Equals("morning") ?
                        GetDescription(Morning.Dish1) :
                        GetDescription(Night.Dish1);
                listaRetorno.Add(retorno);
            }
            if (contador2 > 0)
            {
                retorno = new Retorno();
                retorno.Posicao = 2;
                retorno.NomeDish = HoraDoDia.Equals("morning") ?
                        GetDescription(Morning.Dish2) :
                        GetDescription(Night.Dish2);
                //multiplas potatoes para night
                if (HoraDoDia.Equals("night"))
                {
                    retorno.NomeDish += (contador2 > 1) ? ("(x" + contador2 + ")") : "";
                }
                listaRetorno.Add(retorno);
            }
            if (contador3 > 0)
            {
                retorno = new Retorno();
                retorno.Posicao = 3;
                retorno.NomeDish = HoraDoDia.Equals("morning") ?
                        GetDescription(Morning.Dish3) :
                        GetDescription(Night.Dish3);
                //multiplos coffees para morning
                if (HoraDoDia.Equals("morning"))
                {
                    retorno.NomeDish += (contador3 > 1) ? ("(x" + contador3 + ")") : "";
                }
                listaRetorno.Add(retorno);
            }
            if (contador4 > 0)
            {
                retorno = new Retorno();
                retorno.Posicao = 4;
                retorno.NomeDish = HoraDoDia.Equals("morning") ?
                        GetDescription(Morning.Dish4) :
                        GetDescription(Night.Dish4);
                listaRetorno.Add(retorno);
            }

            return listaRetorno;
        }
        public static T[] SubArray<T>(this T[] data, int index, int length)
        {
            T[] result = new T[length];
            Array.Copy(data, index, result, 0, length);
            return result;
        }
        public static bool ValidarParametros(string sParametros)
        {
            string[] arrParametros = sParametros.Split(',');
            bool bRetorno = true;
            if (!sParametros.Trim().Equals("") && arrParametros.Count() >= 4 &&
                (arrParametros[0].Equals("morning") || arrParametros[0].Equals("night")))
            { bRetorno = true; }
            else { bRetorno = false; }

            return bRetorno;
        }
        public static string GetDescription(Enum value)
        {
            FieldInfo field = value.GetType().GetField(value.ToString());
            DescriptionAttribute attribute =
                Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;

            return attribute == null ? value.ToString() : attribute.Description;
        }

        public class Retorno
        {
            public int Posicao { get; set; }
            public string NomeDish { get; set; }
        }

        public enum Morning
        {
            [Description("Eggs")]
            Dish1 = 1,
            [Description("Toast")]
            Dish2 = 2,
            [Description("Coffe")]
            Dish3 = 3,
            [Description("Error")]
            Dish4 = 4
        }
        public enum Night
        {
            [Description("Steak")]
            Dish1 = 1,
            [Description("Potato")]
            Dish2 = 2,
            [Description("Wine")]
            Dish3 = 3,
            [Description("Cake")]
            Dish4 = 4
        }

        public static void Menu()
        {
            ConfigurarCorDaFonte(ConsoleColor.Gray);
            Console.WriteLine("Para sair, tecle S");

            OpcaoSelecionada();
        }
        public static void ConfigurarCorDaFonte(ConsoleColor corDaFonte)
        {
            Console.ForegroundColor = corDaFonte;
        }
        public static void OpcaoSelecionada()
        {
            ConsoleKeyInfo keyInfo = new ConsoleKeyInfo();
            keyInfo = Console.ReadKey(true);

            if (keyInfo.Key == ConsoleKey.S)
            {
                Console.Beep();
                return;
            }

            Menu();
        }

    }
}
