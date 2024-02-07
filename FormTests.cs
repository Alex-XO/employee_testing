using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Spire.Xls;
using System.Windows.Forms;

namespace WindowsFormsApp3
{
    public partial class FormTests : Form
    {


        int quection_count;
        int correct_answers;
        int wrong_answers;
        string selectedTopic;
        string[] array;

        int correct_answers_number;
        int selected_response;


        System.IO.StreamReader Read;
        private Person person;

        public FormTests(string selectedTopic)
        {
            InitializeComponent();
            this.selectedTopic = selectedTopic;
        }

        public FormTests(string selectedTopic, Person person) : this(selectedTopic)
        {
            this.person = person;
        }

        void start()
        {
            var Encoding = System.Text.Encoding.GetEncoding(65001);
            try
            {
                var pathToTestFolder = System.IO.Directory.GetCurrentDirectory() + @"/Test/" + selectedTopic;
                string[] listTest = Directory.GetFiles(pathToTestFolder);

                Random rnd = new Random();
                int index = rnd.Next(0, listTest.Length);

                var pathToTest = listTest[index];

                Read = new System.IO.StreamReader(pathToTest, Encoding);
                this.Text = Read.ReadLine(); 
               
                quection_count = 0;
                correct_answers = 0;
                wrong_answers = 0;
            
                array = new String[11];
            }
            catch (Exception)
            {  
                MessageBox.Show("Oшибка");
            }
            Question();

        }
      

        void Question()
        {
            label1.Text = Read.ReadLine();
          
            radioButton1.Text = Read.ReadLine();
            radioButton2.Text = Read.ReadLine();
            radioButton3.Text = Read.ReadLine();
          
            correct_answers_number = int.Parse(Read.ReadLine());
           
            radioButton1.Checked = false;
            radioButton2.Checked = false;
            radioButton3.Checked = false;
       
            button1.Enabled = false;
            quection_count = quection_count + 1;
          
            if (Read.EndOfStream == true) button1.Text = "Завершить";

        }

        void SwitchStatus(object sender, EventArgs e)
        {
          
            button1.Enabled = true; button1.Focus();
            RadioButton Switch = (RadioButton)sender;
            var tmp = Switch.Name;
           
            selected_response = int.Parse(tmp.Substring(11));
        }







        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
     
            if (selected_response == correct_answers_number) correct_answers =
                                               correct_answers + 1;
            if (selected_response != correct_answers_number)
            {
               
                wrong_answers = wrong_answers + 1;
                
                array[wrong_answers] = label1.Text;
            }
            if (button1.Text == "Начать тестирование сначала")
            {
                button1.Text = "Следующий вопрос";
              
                radioButton1.Visible = true;
                radioButton2.Visible = true;
                radioButton3.Visible = true;
              
                start(); return;
            }
            if (button1.Text == "Завершить")
            {
            
                Read.Close();
                
                radioButton1.Visible = false;
                radioButton2.Visible = false;
                radioButton3.Visible = false;
               
                label1.Text = String.Format("Тестирование завершено.\n" +
                    "Правильных ответов: {0} из {1}.\n" +
                    "Набранные балы: {2:F2}.", correct_answers,
                    quection_count, (correct_answers * 5.0F) / quection_count);

                button1.Hide();
              
                var Str = "Список ошибок " +
                          ":\n\n";
                for (int i = 1; i <= wrong_answers; i++)
                    Str = Str + array[i] + "\n";

                printExel(person.fullName, person.post, selectedTopic, correct_answers, Str);
             
                if (wrong_answers != 0) MessageBox.Show(
                                          Str, "Тестирование завершено");
            } 
            if (button1.Text == "Следующий вопрос") Question();

        }

        public static void printExel(string fullName, string post, string testName, int score, string description)
        {
            Workbook workbook = new Workbook();
            workbook.LoadFromFile("Результаты.xlsx");
            Worksheet sheet = workbook.Worksheets[0];
            var index = 1;

            while (!sheet["A" + index].IsBlank)
            {
                index++;
                Console.WriteLine("Index = " + index);
            }
            Console.WriteLine("PrintIn = " + index);

            sheet["A" + index].Value = fullName;
            sheet["B" + index].Value = post;
            sheet["C" + index].Value = testName;
            sheet["D" + index].Value = score.ToString();
            sheet["E" + index].Value = description;

            Console.WriteLine("SveFile");
            workbook.SaveToFile("Результаты.xlsx");
        } 

        private void Form1_Load(object sender, EventArgs e)
        {
            button1.Text = "Следующий вопрос";
                      
            radioButton1.CheckedChanged += new EventHandler(SwitchStatus);
            radioButton2.CheckedChanged += new EventHandler(SwitchStatus);
            radioButton3.CheckedChanged += new EventHandler(SwitchStatus);
            start();

        }
       
    }
}
