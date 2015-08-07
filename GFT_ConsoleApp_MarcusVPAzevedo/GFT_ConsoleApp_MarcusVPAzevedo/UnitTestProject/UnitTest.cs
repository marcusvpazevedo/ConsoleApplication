using NUnit.Framework;
using GFT_ConsoleApp_MarcusVPAzevedo.TesteConsole;

namespace UnitTestProject
{
    [TestFixture]
    public class UnitTest
    {
        [Test]
        public void TesteConsoleSample1()
        {
            GFT_ConsoleApp_MarcusVPAzevedo.TesteConsole.Program gft;
            gft = new GFT_ConsoleApp_MarcusVPAzevedo.TesteConsole.Program();
            //Input: morning, 1, 2, 3
            //Output: eggs, toast, coffee
            gft.ValidarExecutar("morning, 1, 2, 3");
        }
        [Test]
        public void TesteConsoleSample2()
        {
            GFT_ConsoleApp_MarcusVPAzevedo.TesteConsole.Program gft;
            gft = new GFT_ConsoleApp_MarcusVPAzevedo.TesteConsole.Program();
            //Input: morning, 2, 1, 3
            //Output: eggs, toast, coffee
            gft.ValidarExecutar("morning, 2, 1, 3");
        }
        [Test]
        public void TesteConsoleSample3()
        {
            GFT_ConsoleApp_MarcusVPAzevedo.TesteConsole.Program gft;
            gft = new GFT_ConsoleApp_MarcusVPAzevedo.TesteConsole.Program();
            //Input: morning, 1, 2, 3, 4
            //Output: eggs, toast, coffee, error
            gft.ValidarExecutar("morning, 1, 2, 3, 4");
        }
        [Test]
        public void TesteConsoleSample4()
        {
            GFT_ConsoleApp_MarcusVPAzevedo.TesteConsole.Program gft;
            gft = new GFT_ConsoleApp_MarcusVPAzevedo.TesteConsole.Program();
            //Input: morning, 1, 2, 3, 3, 3
            //Output: eggs, toast, coffee(x3)
            gft.ValidarExecutar("morning, 1, 2, 3, 3, 3");
        }
        [Test]
        public void TesteConsoleSample5()
        {
            GFT_ConsoleApp_MarcusVPAzevedo.TesteConsole.Program gft;
            gft = new GFT_ConsoleApp_MarcusVPAzevedo.TesteConsole.Program();
            //Input: night, 1, 2, 3, 4
            //Output:  steak, potato, wine, cake
            gft.ValidarExecutar("night, 1, 2, 3, 4");
        }
        [Test]
        public void TesteConsoleSample6()
        {
            GFT_ConsoleApp_MarcusVPAzevedo.TesteConsole.Program gft;
            gft = new GFT_ConsoleApp_MarcusVPAzevedo.TesteConsole.Program();
            //Input: night, 1, 2, 2, 4
            //Output steak, potato(x2), cake
            gft.ValidarExecutar("night, 1, 2, 2, 4");
        }
    }
}