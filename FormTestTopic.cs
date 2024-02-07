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
using System.Windows.Forms;

namespace WindowsFormsApp3
{
    public partial class FormTestTopic : Form
    {
        private Person person;

        public FormTestTopic(Person person)
        {
            this.person = person;
            InitializeComponent();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {            
            if (comboBox1.SelectedItem == null)
            {
                return;
            }

            var selectedItem = comboBox1.SelectedItem.ToString();

            FormTests newForm = new FormTests(selectedItem, person);
            newForm.Show();
            Hide();
            newForm.Closed += (s, args) => this.Close();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            var pathToTestFolder = System.IO.Directory.GetCurrentDirectory() + @"/Test";
            string[] listTest = Directory.GetDirectories(pathToTestFolder);

            string[] topic = new string[listTest.Length];

            for (int i = 0; i < listTest.Length; i++)
            {
                topic[i] = Path.GetFileName(listTest[i]);
            }

            Console.WriteLine(string.Join(",", topic));
            comboBox1.Items.AddRange(topic);
        }
    }
}
