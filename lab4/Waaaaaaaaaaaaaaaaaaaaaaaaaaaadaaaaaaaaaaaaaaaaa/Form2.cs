using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using model;

namespace Waaaaaaaaaaaaaaaaaaaaaaaaaaaadaaaaaaaaaaaaaaaaa
{
    public partial class Form2 : Form
    {
        Form1 mainFormHandle = null;

        public Form2()
        {
            InitializeComponent();
        }

        public Form2(Form1 f1)
        {
            InitializeComponent();
            mainFormHandle = f1;
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            textBox4.Enabled = checkBox3.Checked;
        }

        

        private void button1_Click(object sender, EventArgs e)
        {
            double bottle_price = 0;

            if(Double.TryParse(textBox2.Text, out double max_water) && Double.TryParse(textBox3.Text, out double water_price) &&
                (textBox4.Enabled && Double.TryParse(textBox4.Text, out bottle_price) || !textBox4.Enabled))
            {
                if (mainFormHandle != null)
                {
                   // mainFormHandle.curComp.AddAutomat1(new MineralWaterAutomat(textBox1.Text, max_water, 0, 0, water_price, checkBox1.Checked,
                    //    checkBox3.Checked, checkBox2.Checked, 0, 0, textBox5.Text, checkBox3.Checked ? bottle_price : 0));

                    mainFormHandle.SendNewAutomatInfo(textBox1.Text, max_water, 0, 0, water_price, checkBox1.Checked,
                        checkBox3.Checked, checkBox2.Checked, 0, 0, textBox5.Text, checkBox3.Checked ? bottle_price : 0);

                    //mainFormHandle.OnCreateAutomat(textBox1.Text, max_water, 0, 0, water_price, checkBox1.Checked, checkBox3.Checked, checkBox2.Checked, 0, 0, textBox5.Text, bottle_price);
                    this.Close();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            textBox4.Enabled = false;
        }
    }
}
