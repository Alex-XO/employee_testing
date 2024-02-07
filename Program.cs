using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp3
{
    static class Program
    {

        static void Main()
        {
            
            RanProgram();
        }

        static void RanProgram()
        {
            var person = new Person();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            var formRegistretion = new FormRegistretion(person);
            Application.Run(formRegistretion);
        }

        static void TestCheck()
        {
            var Encoding = System.Text.Encoding.GetEncoding(65001);
            var pathToTopic = System.IO.Directory.GetCurrentDirectory() + @"/Test";
            string[] listTopic = Directory.GetDirectories(pathToTopic);

            foreach (string topic in listTopic)
            {
                var pathToTestFolder = topic;
                Console.WriteLine(pathToTestFolder);
                string[] listTest = Directory.GetFiles(pathToTestFolder);

                foreach (string test in listTest)
                {
                    var reader = new System.IO.StreamReader(test, Encoding);
                    var testName = reader.ReadLine();
                    Console.WriteLine();
                    Console.WriteLine(testName);
                    Console.WriteLine(test);

                    while (!reader.EndOfStream)
                    {

                        Console.WriteLine("Вопрос: " + reader.ReadLine());
                        Console.WriteLine("Ответ1: " + reader.ReadLine());
                        Console.WriteLine("Ответ2: " + reader.ReadLine());
                        Console.WriteLine("Ответ3: " + reader.ReadLine());
                        Console.WriteLine("ПравОтв: " + int.Parse(reader.ReadLine()));
                    }
                }
            }
            Console.WriteLine("ВСЁ СУПЕР");
        }
    }
}

