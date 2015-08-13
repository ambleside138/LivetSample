using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FormTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            textBox1.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            textBox1.AutoCompleteSource = AutoCompleteSource.CustomSource;

            textBox1.AutoCompleteCustomSource.AddRange(new string[] { "アーガメイト20%ゼリー25g", "アーテン散1%", "アイデイト錠50mg", "キサラタン点眼液0.005%", "キプレス細粒4mg", "サアミオン散1%", "サイレース錠1mg" });



            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var Persons = new List<Person>(Enumerable.Range(0, 100000).Select(i => new Person { Name = "TEST" + i, Age = i }));

            listBox1.DisplayMember = "Display";
            listBox1.DataSource = Persons;
        }
    }
}
