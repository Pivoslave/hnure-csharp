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
    public partial class Form1 : Form
    {
        MineralWaterAutomat[] automat_array = new MineralWaterAutomat[2];
        double InsertedMoney = 0;
        double liters = 3;
        int operatorMode = 0;
        public WaterCompany curComp = new WaterCompany("тест", "тест", "тест");

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

            curComp.AddAutomat(test_2);
            curComp.AddAutomat(test_1);


            foreach (MineralWaterAutomat a in curComp.GetAutomats())
            {
                comboBox1.Items.Add(a.name);
            }

            textBox1.Text = $"{liters} л";
            textBox2.Text = $"{liters} грн.";

            comboBox1.SelectedIndex= 0 ;

            hideablePanel.Visible = false;

            radioButton2.Checked = true;
            radioButton4.Checked = true;

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            transactHandler();
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

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            refresher();
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

            if(comboBox1.SelectedIndex >= 0)
            {
                MineralWaterAutomat ma = null;

                foreach (MineralWaterAutomat maa in curComp.GetAutomats()) { if (maa.name == comboBox1.SelectedItem.ToString()) ma = maa; }

                price = ma.price * liters;
            }

            if (radioButton1.Enabled && radioButton1.Checked) price *= 1.1d;

            if (checkBox1.Enabled && checkBox1.Checked) price += getCurAutomat().bottle_price;

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


        private void panelMode(int opmode)
        {
            MineralWaterAutomat ma = null;

            foreach (MineralWaterAutomat maa in curComp.GetAutomats()) { if (maa.name == comboBox1.SelectedItem.ToString()) ma = maa; }

            switch (opmode)
            {
                case 1:
                    hideablePanel.Visible = true;
                    hideableLabel1.Text = "Пляшок:";
                    hideableLabel2.Text = "Кришок:";
                    break;

                case 2:
                    hideablePanel.Visible = true;
                    hideableLabel1.Visible = true;
                    hideableTextbox1.Visible = true;
                    hideableLabel2.Text = "Літрів:";
                    hideableLabel1.Text = "Макс.:";
                    hideableTextbox1.Enabled = false;
                    hideableTextbox1.Text = $"{ma.max_water} л.";
                    break;

                case 3:
                    hideablePanel.Visible = true;
                    hideableLabel1.Visible = true;
                    hideableTextbox1.Visible = true;
                    hideableLabel1.Text = "У авт-ті:";
                    hideableLabel2.Text = "Грошей:";
                    hideableTextbox1.Enabled = false;
                    hideableTextbox1.Text = $"{ma.available_money} грн.";
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

        private void hideableOK_Click(object sender, EventArgs e)
        {
            operationHandler(operatorMode);
        }

        private void hideableCancel_Click(object sender, EventArgs e)
        {
            panelMode(0);
        }

        private void operationHandler(int opcode)
        {
            if (curComp.GetAutomats().Count != 0)
            {

                MineralWaterAutomat ma = null;

                foreach (MineralWaterAutomat maa in curComp.GetAutomats()) { if (maa.name == comboBox1.SelectedItem.ToString()) ma = maa; }

                switch (opcode)
                {
                    case 1:

                        if (Int32.TryParse(hideableTextbox1.Text, out int res1) && Int32.TryParse(hideableTextbox2.Text, out int res2))
                        {
                            ma.bottle_amount += res1;
                            ma.cap_amount += res2;
                            panelMode(0);
                            refresher();
                        }

                        else MessageBox.Show("Одне зі значень введено неправильно!");

                        break;

                    case 2:

                        if (Double.TryParse(hideableTextbox2.Text, out double res))
                        {
                            if (ma.available_water + res <= ma.max_water)
                            {
                                ma.available_water += res;
                                panelMode(0);
                                refresher();
                            }

                            else MessageBox.Show("Введена кількість води перенаповнить автомат!");
                        }

                        else MessageBox.Show("Неправильно введено кількість літрів!");

                        break;

                    case 3:

                        if (Double.TryParse(hideableTextbox2.Text, out double ress))
                        {
                            if (ma.available_money - ress >= 0)
                            {
                                ma.available_money -= ress;
                                panelMode(0);
                                refresher();
                            }

                            else MessageBox.Show("Введена сума більша за наявну!");
                        }

                        else MessageBox.Show("Суму введено неправильно!");

                        break;
                }
            }
        }

        public void refresher()
        {
            textBox5.Text = curComp.name;
            textBox6.Text = curComp.address;
            textBox7.Text = curComp.edrpou;

            if (curComp.GetAutomats().Count != 0)
            {
                MineralWaterAutomat ma = null;

                foreach (MineralWaterAutomat maa in curComp.GetAutomats()) { if (maa.name == comboBox1.SelectedItem.ToString()) ma = maa; }

                if (ma.can_buy_bubbly == false) { radioButton1.Enabled = false; radioButton2.Enabled = false; }
                else { radioButton1.Enabled = true; radioButton2.Enabled = true; }

                if (ma.card_transaction == false) { radioButton3.Enabled = false; radioButton4.Enabled = false; }
                else { radioButton3.Enabled = true; radioButton4.Enabled = true; }

                if (ma.can_buy_bottle == false) { checkBox1.Enabled = false; } else { checkBox1.Enabled = true; }

                wateramoutbox.Text = $"{ma.available_water} літрів";
                waterpricebox.Text = $"{ma.price} грн.";
                bottlepricebox.Text = $"{ma.bottle_price} грн.";
                bottleamountbox.Text = $"{ma.bottle_amount} шт.";
                capsbox.Text = $"{ma.cap_amount} шт.";
                adressbox.Text = $"{ma.address}";

                textBox2.Text = $"{liters * ma.price} грн.";
            }
        }

        private void transactHandler()
        {
            MineralWaterAutomat ma = getCurAutomat();

            if (radioButton4.Checked)
            {
                string trye = textBox2.Text.Replace(" грн.", "");
                Double.TryParse(trye, out double pri);
                Double.TryParse(textBox3.Text, out double ins);

                string tryl = textBox1.Text.Replace(" л", "");
                Int32.TryParse(tryl, out int sliters);

                if (ins - pri >= 0 && ma.available_water - sliters >= 0)
                {
                    DialogResult dr = MessageBox.Show("Розрахунок виконано успішно!");
                    if (dr == DialogResult.OK)
                    {
                        ma.available_money += sliters * ma.price;
                        ma.available_water -= sliters;

                        refresher();
                    }

                    curComp.getLog().AddLast($"\"{ma.name}\": Куплено {sliters} літрів води за {sliters * ma.price} гривень");
                }

                else if (ma.available_water - sliters < 0) MessageBox.Show("У автоматі недостатньо води!");
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
                    ma.available_money += sliters * ma.price;
                    ma.available_water -= sliters;

                    refresher();
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

        private void button10_Click(object sender, EventArgs e)
        {
            curComp.newInfo(textBox5.Text, textBox6.Text, textBox7.Text);
        }

        private void button14_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3(this);
            form3.ShowDialog(); 
        }

        private void button11_Click(object sender, EventArgs e)
        {
            if(saveFileDialog1.ShowDialog() == DialogResult.OK) { curComp.SaveInfo(saveFileDialog1.FileName); }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK) { curComp.LoadInfo(openFileDialog1.FileName); }
            refresher();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            Form4 f4 = new Form4(this);
            f4.ShowDialog();
        }
    }


    public class MineralWaterAutomat
    {
        public string name;
        public double max_water;
        public double available_water;
        public double price;
        public double available_money;
        public bool card_transaction;
        public bool can_buy_bottle;
        public bool can_buy_bubbly;
        public int bottle_amount;
        public int cap_amount;
        public string address;
        public double bottle_price;


        // Конструктор, що не допускає нульових та значень менше нуля
        public MineralWaterAutomat(string _n, double _mw, double _aw, double _am, double _p, bool card_transaction, bool can_buy_bottle, bool can_buy_bubbly, int bottle_amount, int cap_amount, string address, double bottle_price)
        {
            this.name = _n != "" ? _n : "Тестова Назва";
            this.max_water = _mw > 0 ? _mw : 0.1f;
            this.available_water = _aw >= 0 ? _aw : 0;
            this.price = _p > 0 ? _p : 0.1f;
            this.available_money = _am >= 0 ? _am : 0;
            this.card_transaction = card_transaction;
            this.can_buy_bottle = can_buy_bottle;
            this.can_buy_bubbly = can_buy_bubbly;
            this.bottle_amount = bottle_amount;
            this.cap_amount = cap_amount;
            this.address = address;
            this.bottle_price = bottle_price;
        }

        // Метод додавання води
        public void AddWater(double amount)
        {
            if (this.available_water + amount <= this.max_water)
            {
                this.available_water += amount;
                Console.WriteLine($"Додано {amount} літрів води");
            }

            else Console.WriteLine($"Доданої води більше за місткість автомату на {(this.available_water + amount) - this.max_water} літрів");
        }

        // Метод вираховування кількості води за введеною ціною
        double CalcLiters(double money)
        {
            return (money / this.price);
        }

        // Метод, що повертає вирахувану кількість води

        public void DisplayPossibleBought(float money)
        {
            Console.WriteLine($"За введеними грошима ({money} гривень) можна купити {CalcLiters(money)} літрів води");
        }

        // Метод продажу води
        public void SellWater(double money)
        {
            double amountToSell = CalcLiters(money);

            if (amountToSell <= this.available_water)
            {
                this.available_water -= amountToSell;
                this.available_money += money;
                Console.WriteLine($"Продано {amountToSell} літрів води в обмін на {money} гривень");
            }

            else Console.WriteLine("Недостатня кількість води в автоматі, спробуйте пізніше");
        }

        // Метод зняття грошей
        public void MoneyWithdrawal(double money)
        {
            if (money <= this.available_money)
            {
                Console.WriteLine($"Оператором знято {money} гривень");
                this.available_money -= money;
            }

            else Console.WriteLine("У автоматі недостатньо грошей для зняття такої суми!");
        }
    }

    public class WaterCompany
    {
        public string name;
        HashSet<MineralWaterAutomat> automats;
        double total_money;
        LinkedList<string> logs;
        double waterStorage;
        public string address;
        public string edrpou;

        public WaterCompany(string _name, string _address, string _edrpou){
            this.name = _name;
            this.address = _address;
            this.edrpou= _edrpou;
            this.logs = new LinkedList<string>();
            this.automats= new HashSet<MineralWaterAutomat>();
            this.total_money = 0;
        }

        public WaterCompany() { this.name = ""; this.address = ""; this.edrpou = ""; this.total_money= 0; this.automats = new HashSet<MineralWaterAutomat>(); this.logs = new LinkedList<string>(); }

        public void AddAutomat(MineralWaterAutomat a) { this.automats.Add(a); RecalcMoney(); }
        public bool RemoveAutomat(string address){
            int formerL = automats.Count;
            automats.RemoveWhere(a => a.address == address);
            return formerL != automats.Count ? true : false;
        }

        public void AddWater(double amount)
        {
            waterStorage += amount;
        }

        public void SaveInfo(string path)
        {
            System.IO.StreamWriter fs;

            try
            {
                fs = new System.IO.StreamWriter(path);
                throw new NotImplementedException();
            }
            catch
            {

            }

            finally { fs.Close(); }
            fs.WriteLine($"{this.name}{(char)7}{this.address}{(char)7}{this.edrpou}{(char)7}");
            foreach (MineralWaterAutomat ma in this.automats)
                fs.WriteLine($"{ma.name}{(char)7}{ma.max_water}{(char)7}{ma.available_water}{(char)7}{ma.price}{(char)7}{ma.available_money}{(char)7}{ma.card_transaction}{(char)7}" +
                    $"{ma.can_buy_bottle}{(char)7}{ma.can_buy_bubbly}{(char)7}{ma.bottle_amount}{(char)7}{ma.cap_amount}{(char)7}{ma.address}{(char)7}{ma.bottle_price}");


            
        }

        public void LoadInfo(string path)
        {
            string result = File.ReadAllText(path);

            this.name = result.Substring(0, result.IndexOf((char)7));
            result = result.Remove(0, result.IndexOf((char)7) + 1);
            this.address = result.Substring(0, result.IndexOf((char)7));
            result = result.Remove(0, result.IndexOf((char)7) + 1);
            this.edrpou = result.Substring(0, result.IndexOf((char)7));
            result = result.Remove(0, result.IndexOf((char)7) + 3);

            HashSet<MineralWaterAutomat> atms = new HashSet<MineralWaterAutomat>();

            while(result.Length != 0)
            {
                string name = result.Substring(0, result.IndexOf((char)7));
                result = result.Remove(0, result.IndexOf((char)7) + 1);
                Double.TryParse(result.Substring(0, result.IndexOf((char)7)), out double maxwater);
                result = result.Remove(0, result.IndexOf((char)7) + 1);
                Double.TryParse(result.Substring(0, result.IndexOf((char)7)), out double availablewater);
                result = result.Remove(0, result.IndexOf((char)7) + 1);
                Double.TryParse(result.Substring(0, result.IndexOf((char)7)), out double price);
                result = result.Remove(0, result.IndexOf((char)7) + 1);
                Double.TryParse(result.Substring(0, result.IndexOf((char)7)), out double money);
                result = result.Remove(0, result.IndexOf((char)7) + 1);
                bool card = result.Substring(0, result.IndexOf((char)7)) == "True" ? true : false;
                result = result.Remove(0, result.IndexOf((char)7) + 1);
                bool bottle = result.Substring(0, result.IndexOf((char)7)) == "True" ? true : false;
                result = result.Remove(0, result.IndexOf((char)7) + 1);
                bool bubbly = result.Substring(0, result.IndexOf((char)7)) == "True" ? true : false;
                result = result.Remove(0, result.IndexOf((char)7) + 1);
                Int32.TryParse(result.Substring(0, result.IndexOf((char)7)), out int bottles);
                result = result.Remove(0, result.IndexOf((char)7) + 1);
                Int32.TryParse(result.Substring(0, result.IndexOf((char)7)), out int caps);
                result = result.Remove(0, result.IndexOf((char)7) + 1);
                string address = result.Substring(0, result.IndexOf((char)7));
                result = result.Remove(0, result.IndexOf((char)7) + 1);
                Double.TryParse(result.Substring(0, result.IndexOf((char)7) == -1 ? result.IndexOf('\r') : result.IndexOf((char)7)), out double bottleprice);
                result = result.Remove(0, (result.IndexOf('\n')+1));

                atms.Add(new MineralWaterAutomat(name, maxwater, availablewater, money, price, card, bottle, bubbly, bottles, caps, address, bottleprice));
            }

        }

        void RecalcMoney()
        {
            double _money = 0;
            foreach (MineralWaterAutomat ma in this.automats) _money += ma.available_money;
            this.total_money = _money;
        }

        public void newInfo(string newname, string newaddress, string newedrpou)
        {
            this.name = newname;
            this.address = newaddress;
            this.edrpou = newedrpou;
        }

        public HashSet<MineralWaterAutomat> GetAutomats() { return this.automats; }

        public LinkedList<string> getLog()
        {
            return this.logs;
        }
    }

    public struct nbcash
    {
        double price;
        double liters;
        double payed;
        double remainder;
    }

    public struct nbcard
    {
        double liters;
        double price;
    }

    public struct nbcashb
    {
        double liters;
        int bottles;
        double price;
        double payed;
        double remainder;
    }

    public struct nbcardb
    {
        double liters;
        int bottles;
        double price;
    }

    public struct bcash
    {
        double price;
        double liters;
        double payed;
        double remainder;
    }

    public struct bcard
    {
        double liters;
        double price;
    }

    public struct bcashb
    {
        double liters;
        int bottles;
        double price;
        double payed;
        double remainder;
    }

    public struct bcardb
    {
        double liters;
        int bottles;
        double price;
    }
}
