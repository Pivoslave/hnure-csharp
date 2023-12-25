using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Waaaaaaaaaaaaaaaaaaaaaaaaaaaadaaaaaaaaaaaaaaaaa
{
    public partial class Form3 : Form
    {
        Form1 mainformhandler;

        public Form3()
        {
            InitializeComponent();
        }

        public Form3(Form1 mainform)
        {
            InitializeComponent();
            this.mainformhandler = mainform;
        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            mainformhandler.SendAddressForDeletion(textBox1.Text);
            this.Close();
        }
    }
}
