using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;

namespace GFT_ConsoleApp_MarcusVPAzevedo.TesteConsole
{
    public class Program
    {
        ClassificacaoPratos estrutura = new ClassificacaoPratos();

        public static void Main()
        {
            Program program = new Program();
            program.Menu();
        }

        public void ValidarExecutar(string sParametros)
        {
            SelecaoPratos retorno = new SelecaoPratos();
            StringBuilder sbErrosParaConsole = new StringBuilder();
            string sRetornoParaConsole = "";

            sParametros = TratarParametros(sParametros);

            // Valida a entrada
            if (ValidarParametros(sParametros))
            {
                List<SelecaoPratos> listaRetornos = MontarRetorno(sParametros, sbErrosParaConsole, ref sRetornoParaConsole);
                MostrarResultados(sRetornoParaConsole, sbErrosParaConsole);
            }
        }

        private string TratarParametros(string sParametros)
        {
            //trata o parâmetro, independente do conteúdo
            sParametros = sParametros.Replace(" ", "").ToLower();
            return sParametros;
        }
        private bool ValidarParametros(string sParametros)
        {            
            bool bRetorno = true;
            if (!sParametros.Trim().Equals(""))
            {
                string[] arrParametros = sParametros.Split(',');
                if (arrParametros.Count() >= 4 && (arrParametros[0].Equals("morning") || arrParametros[0].Equals("night")))
                { bRetorno = true; }
                else 
                { bRetorno = false; }
            }
            else { bRetorno = false; }

            return bRetorno;
        }

        private List<SelecaoPratos> MontarRetorno(string sParametros, StringBuilder sbErros, ref string sRetornoParaConsole)
        {
            SelecaoPratos retorno = new SelecaoPratos();
            List<SelecaoPratos> listaRetornos = new List<SelecaoPratos>();
            string horaDoDia = "";
            string[] arrParametros = sParametros.Split(',');

            //Analisa a posição 0, com a hora do dia
            retorno.Posicao = 0;
            horaDoDia = arrParametros[0];
            retorno.NomeDish = horaDoDia;
            listaRetornos.Add(retorno);

            ClassificarPratos(arrParametros, sbErros);
            SelecionarPratos(horaDoDia, listaRetornos);

            foreach (SelecaoPratos lRetorno in listaRetornos)
            {
                sRetornoParaConsole += lRetorno.NomeDish + ", ";
            }

            if (!sRetornoParaConsole.Equals(""))
            {
                sRetornoParaConsole = sRetornoParaConsole.Substring(0, sRetornoParaConsole.Length - 2);
            }

            return listaRetornos;
        }

        private void MostrarResultados(string sRetornoParaConsole, StringBuilder sbErrosParaConsole)
        {
            Console.WriteLine(sRetornoParaConsole);

            if (sbErrosParaConsole.Length > 0)
            {
                string sErros = sbErrosParaConsole.ToString();
                sErros = sErros.Substring(0, sErros.Length - 2);
                sErros = "Alguns erros de entrada foram encontrados na sequência: " + sErros.ToString();
                Console.WriteLine(sErros);
            }
            Console.WriteLine("\n");
        }
        private void ClassificarPratos(string[] arrParametros, StringBuilder sbErros)
        {
            estrutura = new ClassificacaoPratos();
            //Analisa demais parâmetros, mesmo fora da ordem, em estrutura prevista (1 a 4)
            foreach (var param in arrParametros)
            {
                if (param.Equals("morning") || param.Equals("night")) { }
                else if (param.Equals("1")) { estrutura.contador1++; }
                else if (param.Equals("2")) { estrutura.contador2++; }
                else if (param.Equals("3")) { estrutura.contador3++; }
                else if (param.Equals("4")) { estrutura.contador4++; }
                else
                {
                    if (!param.Equals("")) { sbErros.AppendFormat("{0}, ", param); }
                }
            }
        }
        private void SelecionarPratos(string horaDoDia, List<SelecaoPratos> listaRetorno)
        {
            SelecaoPratos retorno = new SelecaoPratos();

            //Filtra parâmetros para montagem de retorno
            if (estrutura.contador1 > 0)
            {
                retorno = new SelecaoPratos();
                retorno.Posicao = 1;
                retorno.NomeDish = horaDoDia.Equals("morning") ? "Eggs" : "Steak";
                listaRetorno.Add(retorno);
            }
            if (estrutura.contador2 > 0)
            {
                retorno = new SelecaoPratos();
                retorno.Posicao = 2;
                retorno.NomeDish = horaDoDia.Equals("morning") ? "Toast" : "Potato";
                //multiplas potatoes para night
                if (horaDoDia.Equals("night"))
                {
                    retorno.NomeDish += (estrutura.contador2 > 1) ? ("(x" + estrutura.contador2 + ")") : "";
                }
                listaRetorno.Add(retorno);
            }
            if (estrutura.contador3 > 0)
            {
                retorno = new SelecaoPratos();
                retorno.Posicao = 3;
                retorno.NomeDish = horaDoDia.Equals("morning") ? "Coffee" : "Wine";
                //multiplos coffees para morning
                if (horaDoDia.Equals("morning"))
                {
                    retorno.NomeDish += (estrutura.contador3 > 1) ? ("(x" + estrutura.contador3 + ")") : "";
                }
                listaRetorno.Add(retorno);
            }
            if (estrutura.contador4 > 0)
            {
                retorno = new SelecaoPratos();
                retorno.Posicao = 4;
                retorno.NomeDish = horaDoDia.Equals("morning") ? "Error" : "Cake";
                listaRetorno.Add(retorno);
            }
        }
        private class ClassificacaoPratos
        {
            private int _contador1;
            private int _contador2;
            private int _contador3;
            private int _contador4;

            public int contador1
            {
                get { return _contador1; }
                set { _contador1 = value; }
            }
            public int contador2
            {
                get { return _contador2; }
                set { _contador2 = value; }
            }
            public int contador3
            {
                get { return _contador3; }
                set { _contador3 = value; }
            }
            public int contador4
            {
                get { return _contador4; }
                set { _contador4 = value; }
            }
        }
        private class SelecaoPratos
        {
            private int _Posicao;
            private string _NomeDish;

            public int Posicao
            {
                get { return _Posicao; }
                set { _Posicao = value; }
            }
            public string NomeDish
            {
                get { return _NomeDish; }
                set { _NomeDish = value; }
            }
        }

        private void ConfigurarCorDaFonte(ConsoleColor corDaFonte)
        {
            Console.ForegroundColor = corDaFonte;
        }
        private void OpcaoSelecionada()
        {
            ConsoleKeyInfo keyInfo = new ConsoleKeyInfo();
            keyInfo = Console.ReadKey(true);

            if (keyInfo.Key == ConsoleKey.A) { ValidarExecutar("Morning, 1, 2, 3"); }
            if (keyInfo.Key == ConsoleKey.B) { ValidarExecutar("Morning, 2, 1, 3"); }
            if (keyInfo.Key == ConsoleKey.C) { ValidarExecutar("Morning, 1, 2, 3, 4"); }
            if (keyInfo.Key == ConsoleKey.D) { ValidarExecutar("Morning, 1, 2, 3, 3, 3"); }
            if (keyInfo.Key == ConsoleKey.E) { ValidarExecutar("Night, 1, 2, 3, 4"); }
            if (keyInfo.Key == ConsoleKey.F) { ValidarExecutar("Night, 1, 2, 2, 4"); }
            if (keyInfo.Key == ConsoleKey.S) { return; }

            Menu();
        }
        private void Menu()
        {
            ConfigurarCorDaFonte(ConsoleColor.Gray);
            Console.WriteLine("Para opção de Menu: 'Morning', 1, 2, 3, tecle 1");
            Console.WriteLine("Para opção de Menu: 'Morning', 2, 1, 3, tecle 2");
            Console.WriteLine("Para opção de Menu: 'Morning', 1, 2, 3, 4, tecle 3");
            Console.WriteLine("Para opção de Menu: 'Morning', 1, 2, 3, 3, 3, tecle 4");
            Console.WriteLine("Para opção de Menu: 'Night', 1, 2, 3, 4, tecle 5");
            Console.WriteLine("Para opção de Menu: 'Night', 1, 2, 2, 4, tecle 6");
            Console.WriteLine("Para sair, tecle S");
            Console.WriteLine("\n");

            OpcaoSelecionada();
        }
    }
}
