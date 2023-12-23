using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Waaaaaaaaaaaaaaaaaaaaaaaaaaaadaaaaaaaaaaaaaaaaa
{
    public partial class Form4 : Form
    {
        Form1 mainformhandler;

        public Form4()
        {
            InitializeComponent();
        }

        public Form4(Form1 mainformhandler)
        {
            InitializeComponent();
            this.mainformhandler = mainformhandler;
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            int iterator = 0;

            foreach (string s in mainformhandler.curComp.getLog())
            {
                iterator++;
                richTextBox1.Text += s + (iterator == mainformhandler.curComp.getLog().Count ? "" : "\n");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                StreamWriter fs = new StreamWriter(saveFileDialog1.FileName);

                fs.Write(richTextBox1.Text);
                fs.Close();

                MessageBox.Show("Успішно збережено!");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK) richTextBox1.Text = File.ReadAllText(openFileDialog1.FileName);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
