using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using model;

namespace Waaaaaaaaaaaaaaaaaaaaaaaaaaaadaaaaaaaaaaaaaaaaa
{
    public partial class Form1 : Form
    {
        MineralWaterAutomat[] automat_array = new MineralWaterAutomat[2];
        double InsertedMoney = 0;
        double liters = 3;
        int operatorMode = 0;
        public WaterCompany curComp = new WaterCompany("тест", "тест", "тест");
        double[] miscInfo = new double[2];
        double selBottlePrice = 0;

        public Form1()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            textBox5.Text = curComp.name;
            textBox6.Text = curComp.address;
            textBox7.Text = curComp.edrpou;

            MineralWaterAutomat test_1 = new MineralWaterAutomat("Test Automat", 30, 20, 15, 2, true, true, true, 10, 12, "Тестова Вулиця 14, секція 88", 1);
            MineralWaterAutomat test_2 = new MineralWaterAutomat("Test Automat 2", 23, 12, 6, 3, false, false, false, 0, 0, "Тестова Вулиця 22, секція 8", 0);

            curComp.AddAutomat1(test_2);
            curComp.AddAutomat1(test_1);


            setCombo(curComp.getAutomatNames());

            textBox1.Text = $"{liters} л";
            textBox2.Text = $"{liters} грн.";

            hideablePanel.Visible = false;

            radioButton2.Checked = true;
            radioButton4.Checked = true;

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        public delegate void TransactRequest(double l, double c, double i, bool bottleBuy);
        public event TransactRequest OnTransactRequest;

        private void button1_Click(object sender, EventArgs e)
        {
            string trye = textBox2.Text.Replace(" грн.", "");
            string tryl = textBox1.Text.Replace(" л", "");

            if (radioButton4.Checked)
            {
                if( Double.TryParse(trye, out double pri) &&
                Double.TryParse(textBox3.Text, out double ins) &&
                Double.TryParse(tryl, out double sliters)) OnTransactRequest(sliters, pri, ins, checkBox1.Checked);
            }

            else
            {
                if(Double.TryParse(trye, out double pri) && Double.TryParse(tryl, out double sliters))
                {
                    MessageBox.Show("Очікую на картку...");
                    OnTransactRequest(sliters, pri, sliters * pri, checkBox1.Checked);
                }
            }
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            recalculatePrice();
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            recalculatePrice();
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            recalculatePrice();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            recalculatePrice();
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            recalculatePrice();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        public delegate void AutomatInfo (string name);
        public event AutomatInfo OnRequestInfo;
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            OnRequestInfo(comboBox1.Text);
            recalculatePrice();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            liters = liters >= 0.5d ? liters - 0.5d: liters;

            textBox1.Text = $"{liters} л";
        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {
            recalculatePrice();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            liters = liters >= 1 ? liters - 1 : liters;

            textBox1.Text = $"{liters} л";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            liters += 0.5d;
            textBox1.Text = $"{liters} л";
        }

        private void button5_Click(object sender, EventArgs e)
        {
            liters += 1;
            textBox1.Text = $"{liters} л";
        }


        private void recalculatePrice()
        {
            double price = 0;

            Double.TryParse(waterpricebox.Text, out price);
            price *= liters;

            if (radioButton1.Enabled && radioButton1.Checked) price *= 1.1d;

            if (checkBox1.Enabled && checkBox1.Checked) price += selBottlePrice;

            textBox2.Text = $"{price}";

            Double.TryParse(textBox3.Text, out double ins);
            string trye = textBox2.Text.Replace(" грн.", "");
            Double.TryParse(trye, out double pri);



            if (ins >= pri) textBox4.Text = $"{ins - pri} грн.";
            else textBox4.Text = "Недостатньо коштів!";
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            Double.TryParse(textBox3.Text, out double ins);
            string trye = textBox2.Text.Replace(" грн.", "");
            Double.TryParse(trye, out double pri);



            if (ins >= pri) textBox4.Text = $"{ins - pri} грн.";
            else textBox4.Text = "Недостатньо коштів!";
        }

        private void button7_Click(object sender, EventArgs e)
        {
            operatorMode = 1;
            panelMode(operatorMode);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            operatorMode = 2;
            panelMode(operatorMode);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            operatorMode = 3;
            panelMode(operatorMode);
        }

        public delegate void requestField(int field);
        public event requestField OnRequestField;

        private void panelMode(int opmode)
        {
            hideableTextbox1.Text = null;
            hideableTextbox2.Text = null;


            switch (opmode)
            {
                case 1:
                    hideablePanel.Visible = true;
                    hideableLabel1.Text = "Пляшок:";
                    hideableLabel2.Text = "Кришок:";
                    hideableTextbox1.Enabled = true;
                    hideableTextbox1.Text = "";
                    break;

                case 2:
                    hideablePanel.Visible = true;
                    hideableLabel1.Visible = true;
                    hideableTextbox1.Visible = true;
                    hideableLabel2.Text = "Літрів:";
                    hideableLabel1.Text = "Макс.:";
                    hideableTextbox1.Enabled = false;
                    hideableTextbox1.Text = $"{miscInfo[0]} л.";
                    break;

                case 3:
                    hideablePanel.Visible = true;
                    hideableLabel1.Visible = true;
                    hideableTextbox1.Visible = true;
                    hideableLabel1.Text = "У авт-ті:";
                    hideableLabel2.Text = "Грошей:";
                    hideableTextbox1.Enabled = false;
                    hideableTextbox1.Text = $"{miscInfo[1]} грн.";
                    break;

                default:
                    hideableLabel1.Visible = true;
                    hideableLabel2.Visible = true;
                    hideableTextbox1.Visible = true;
                    hideableTextbox1.Enabled = true;
                    hideableTextbox1.Text = "";
                    hideableTextbox2.Text = "";
                    hideablePanel.Visible = false;
                    break;
            }
        }

        public delegate void ChangeField (int opcode, double p1, double p2);
        public event ChangeField OnRequestChangeField;


        private void hideableOK_Click(object sender, EventArgs e)
        {
            switch (operatorMode)
            {
                case 1:
                    if(Int32.TryParse(hideableTextbox1.Text, out int caps) && Int32.TryParse(hideableTextbox2.Text, out int bottles))
                        OnRequestChangeField(1, caps, bottles);
                    break;

                case 2:
                    if (Double.TryParse(hideableTextbox2.Text, out double moneh)) OnRequestChangeField(2, moneh, 0);
                    break;
                case 3:
                    if (Double.TryParse(hideableTextbox2.Text, out double water)) OnRequestChangeField(3, water, 0);
                    break;
            }

            if (operatorMode == 2) hideableTextbox1.Text = $"{miscInfo[0]} л.";
            else if (operatorMode == 3) hideableTextbox1.Text = $"{miscInfo[1]} грн.";
            hideablePanel.Visible = false;

        }

        private void hideableCancel_Click(object sender, EventArgs e)
        {
            panelMode(0);
        }

        private void operationHandler(int opcode)
        {

        }

        public void ChangeInfo(MineralWaterAutomat ma)
        {
            
        }

        public delegate void Transaction(double l, double c, double i);
        public event Transaction OnTransact;

        public void transactHandler(MineralWaterAutomat ma)
        {
            if (radioButton4.Checked)
            {
                string trye = textBox2.Text.Replace(" грн.", "");
                Double.TryParse(trye, out double pri);
                Double.TryParse(textBox3.Text, out double ins);

                string tryl = textBox1.Text.Replace(" л", "");
                Int32.TryParse(tryl, out int sliters);

                if (ins - pri >= 0 && ma.availableWater - sliters >= 0)
                {
                    //OnTransact(sliters, sliters * ma.price);

                    curComp.getLog().AddLast($"\"{ma.name}\": Куплено {sliters} літрів води за {sliters * ma.price} гривень");
                }

                else if (ma.availableWater - sliters < 0) MessageBox.Show("У автоматі недостатньо води!");
                else if (ins - pri < 0) MessageBox.Show("Введено недостатньо коштів!");
            }

            else if (radioButton3.Checked)
            {
                string trye = textBox2.Text.Replace(" грн.", "");
                Double.TryParse(trye, out double pri);
                Double.TryParse(textBox3.Text, out double ins);

                string tryl = textBox1.Text.Replace(" л", "");
                Int32.TryParse(tryl, out int sliters);

                DialogResult cardRes = MessageBox.Show("Очікую на картку...");
                if(cardRes == DialogResult.OK)
                {
                    ma.availableMoney += sliters * ma.price;
                    ma.availableWater -= sliters;
                }

                curComp.getLog().AddLast($"\"{ma.name}\": Куплено {sliters} літрів води за {sliters * ma.price} гривень");
            }
        }

        private MineralWaterAutomat getCurAutomat()
        {
            MineralWaterAutomat ma = null;

            foreach (MineralWaterAutomat maa in curComp.GetAutomats()) { if (maa.name == comboBox1.SelectedItem.ToString()) ma = maa; }
            return ma;
        }

        public void ReloadCombobox()
        {
            int keep = comboBox1.SelectedIndex;

            comboBox1.Items.Clear();

            foreach (MineralWaterAutomat aa in curComp.GetAutomats())
            {
                comboBox1.Items.Add(aa.name);
            }

            comboBox1.SelectedIndex = comboBox1.Items.Count != 0 ? keep : -1;
            if (comboBox1.SelectedIndex == -1) comboBox1.Text = null;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2(this);
            f2.ShowDialog();
        }

        public delegate void ChangeVisibleInfo(string[] par);
        public event ChangeVisibleInfo OnChangeCompanyInfo;

        private void button10_Click(object sender, EventArgs e)
        {
            OnChangeCompanyInfo(new string[] { textBox5.Text, textBox6.Text, textBox7.Text });
        }

        private void button14_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3(this);
            form3.ShowDialog(); 
        }

        private void button11_Click(object sender, EventArgs e)
        {
            if(saveFileDialog1.ShowDialog() == DialogResult.OK) { OnRequestSave(saveFileDialog1.FileName); }
        }

        public delegate void FileInfo(string path);
        public event FileInfo OnRequestSave;
        public event FileInfo OnRequestLoad;

        private void button13_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK) { OnRequestLoad(openFileDialog1.FileName); }
            
        }

        private void button12_Click(object sender, EventArgs e)
        {
            LinkedList<string> curLog = OnLogsRequested();
            Form4 f4 = new Form4(this, curLog);
            f4.ShowDialog();
        }

        public void setCombo(string[] names)
        {
            int lastindex = comboBox1.SelectedIndex;

            comboBox1.Items.Clear();
            comboBox1.Items.AddRange(names);

            comboBox1.SelectedIndex = names.Length == 0 ? -1 : 0;
        }


        public void AutomatInfoHandle(MineralWaterAutomat ma)
        {
            wateramoutbox.Text = $"{ma.availableWater}/{ma.maxWater}";
            waterpricebox.Text = $"{ma.GetPrice()}";
            bottlepricebox.Text = $"{ma.bottlePrice}";
            bottleamountbox.Text = $"{ma.bottleAmount}";
            capsbox.Text = $"{ma.capAmount}";
            adressbox.Text = $"{ma.GetAddress()}";
            groupBox1.Enabled = ma.canBuyBubbly;
            radioButton2.Checked = true;
            groupBox2.Enabled = ma.cardTransaction;
            radioButton4.Checked = true;
            checkBox1.Enabled = ma.canBuyBottle;
            checkBox1.Checked = ma.canBuyBottle ? checkBox1.Checked : false;
            miscInfo[0] = ma.maxWater;
            miscInfo[1] = ma.availableMoney;
            selBottlePrice = ma.bottlePrice;
        }

        public void ShowAlert(int al)
        {
            switch(al)
            {
                case 0: MessageBox.Show("Недостатньо води, посилаємо кур'єра...");
                    break;
                case 1: MessageBox.Show("Така кількість води переповнить автомат!"); break;
                case 2: MessageBox.Show("У автоматі недостатньо коштів!"); break;
                case 3: MessageBox.Show(("Недостатньо води, посилаємо кур'єра...")); break;
                case 4: MessageBox.Show("Недостатньо внесених коштів!"); break;
                case 5: MessageBox.Show("Недостатньо кришок чи пляшок у автоматі!"); break;
            }
        }

        public delegate void RequestFacilityAdd(double l);
        public event RequestFacilityAdd OnRequestWaterADD;

        private void button15_Click(object sender, EventArgs e)
        {
            OnRequestWaterADD(100); 
        }

        private void button16_Click(object sender, EventArgs e)
        {
            OnRequestWaterADD(200);
        }

        public void UpdateCompanyInfo(string name, string address, string edrpou)
        {
            textBox5.Text = name;
            textBox6.Text = address;
            textBox7.Text = edrpou;
        }

        public delegate void AutomatForm(string _n, double _mw, double _aw, double _am, double _p, bool card_transaction, bool can_buy_bottle, bool can_buy_bubbly, int bottle_amount, int cap_amount, string address, double bottle_price);
        public event AutomatForm OnCreateAutomat;

        public void SendNewAutomatInfo(string _n, double _mw, double _aw, double _am, double _p, bool card_transaction, bool can_buy_bottle, bool can_buy_bubbly, int bottle_amount, int cap_amount, string address, double bottle_price)
        {
            OnCreateAutomat(_n, _mw, _aw, _am, _p, card_transaction, can_buy_bottle, can_buy_bubbly, bottle_amount, cap_amount, address, bottle_price);
        }

        public delegate LinkedList<string> LogsRequested();
        public event LogsRequested OnLogsRequested;

        public delegate void DeletionRequested(string address);
        public event DeletionRequested OnDeletionRequested;

        public void SendAddressForDeletion(string address)
        {
            OnDeletionRequested(address);
        }
    }
}
