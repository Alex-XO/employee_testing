using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp3
{
    public partial class FormRegistretion : Form
    {
        private Person person;

        public FormRegistretion(Person person)
        {
            InitializeComponent();
            this.person = person;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            person.fullName = textBox1.Text;
            person.post = textBox2.Text;

            FormTestTopic newForm = new FormTestTopic(person);
            newForm.Show();
            Hide();
            newForm.Closed += (s, args) => this.Close();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }
    }
}
