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
    

    public partial class Form1 : Form
    {
        MineralWaterAutomat[] automat_array = new MineralWaterAutomat[2];
        double InsertedMoney = 0;
        double liters = 3;
        int operatorMode = 0;

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

            MineralWaterAutomat test_1 = new MineralWaterAutomat("Test Automat", 30, 20, 15, 2, true, true, true, 10, 12, "Тестова Вулиця 14, секція 88", 1);
            MineralWaterAutomat test_2 = new MineralWaterAutomat("Test Automat 2", 23, 12, 6, 3, false, false, false, 0, 0, "Тестова Вулиця 22, секція 8", 0);

            automat_array[0] = test_1;
            automat_array[1] = test_2;

            foreach(MineralWaterAutomat a in automat_array)
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
                price = automat_array[comboBox1.SelectedIndex].price * liters;
            }

            if (radioButton1.Enabled && radioButton1.Checked) price *= 1.1d;

            if (checkBox1.Enabled && checkBox1.Checked) price += automat_array[comboBox1.SelectedIndex].bottle_price;

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
                    hideableTextbox1.Text = $"{automat_array[comboBox1.SelectedIndex].max_water} л.";
                    break;

                case 3:
                    hideablePanel.Visible = true;
                    hideableLabel1.Visible = true;
                    hideableTextbox1.Visible = true;
                    hideableLabel1.Text = "У авт-ті:";
                    hideableLabel2.Text = "Грошей:";
                    hideableTextbox1.Enabled = false;
                    hideableTextbox1.Text = $"{automat_array[comboBox1.SelectedIndex].available_money} грн.";
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
            switch (opcode)
            {
                case 1:

                    if (Int32.TryParse(hideableTextbox1.Text, out int res1) && Int32.TryParse(hideableTextbox2.Text, out int res2))
                    {
                        automat_array[comboBox1.SelectedIndex].bottle_amount += res1;
                        automat_array[comboBox1.SelectedIndex].cap_amount += res2;
                        panelMode(0);
                        refresher();
                    }

                    else MessageBox.Show("Одне зі значень введено неправильно!");

                    break;

                case 2:

                    if (Double.TryParse(hideableTextbox2.Text, out double res))
                    {
                        if (automat_array[comboBox1.SelectedIndex].available_water + res <= automat_array[comboBox1.SelectedIndex].max_water)
                        {
                            automat_array[comboBox1.SelectedIndex].available_water += res;
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
                        if (automat_array[comboBox1.SelectedIndex].available_money - ress >= 0)
                        {
                            automat_array[comboBox1.SelectedIndex].available_money -= ress;
                            panelMode(0);
                            refresher();
                        }

                        else MessageBox.Show("Введена сума більша за наявну!");
                    }

                    else MessageBox.Show("Суму введено неправильно!");

                    break;
            }
        }

        public void refresher()
        {
            int index = comboBox1.SelectedIndex;

            if (automat_array[index].can_buy_bubbly == false) { radioButton1.Enabled = false; radioButton2.Enabled = false; }
            else { radioButton1.Enabled = true; radioButton2.Enabled = true; }

            if (automat_array[index].card_transaction == false) { radioButton3.Enabled = false; radioButton4.Enabled = false; }
            else { radioButton3.Enabled = true; radioButton4.Enabled = true; }

            if (automat_array[index].can_buy_bottle == false) { checkBox1.Enabled = false; } else { checkBox1.Enabled = true; }

            wateramoutbox.Text = $"{automat_array[index].available_water} літрів";
            waterpricebox.Text = $"{automat_array[index].price} грн.";
            bottlepricebox.Text = $"{automat_array[index].bottle_price} грн.";
            bottleamountbox.Text = $"{automat_array[index].bottle_amount} шт.";
            capsbox.Text = $"{automat_array[index].cap_amount} шт.";
            adressbox.Text = $"{automat_array[index].address}";

            textBox2.Text = $"{liters * automat_array[index].price} грн.";
        }

        private void transactHandler()
        {
            if (radioButton4.Checked)
            {
                string trye = textBox2.Text.Replace(" грн.", "");
                Double.TryParse(trye, out double pri);
                Double.TryParse(textBox3.Text, out double ins);

                string tryl = textBox1.Text.Replace(" л", "");
                Int32.TryParse(tryl, out int sliters);

                if (ins - pri >= 0 && automat_array[comboBox1.SelectedIndex].available_water - sliters >= 0)
                {
                    DialogResult dr = MessageBox.Show("Розрахунок виконано успішно!");
                    if (dr == DialogResult.OK)
                    {
                        automat_array[comboBox1.SelectedIndex].available_money += sliters * automat_array[comboBox1.SelectedIndex].price;
                        automat_array[comboBox1.SelectedIndex].available_water -= sliters;

                        refresher();
                    }
                }

                else if (automat_array[comboBox1.SelectedIndex].available_water - sliters < 0) MessageBox.Show("У автоматі недостатньо води!");
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
                    automat_array[comboBox1.SelectedIndex].available_money += sliters * automat_array[comboBox1.SelectedIndex].price;
                    automat_array[comboBox1.SelectedIndex].available_water -= sliters;

                    refresher();
                }
            }
        }

        public void AddAutomatToList(MineralWaterAutomat a)
        {
            Array.Resize<MineralWaterAutomat>(ref automat_array, automat_array.Length + 1);
            automat_array[automat_array.Length - 1] = a;

            int keep = comboBox1.SelectedIndex;

            comboBox1.Items.Clear();

            foreach (MineralWaterAutomat aa in automat_array)
            {
                comboBox1.Items.Add(aa.name);
            }

            comboBox1.SelectedIndex = keep;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2(this);
            f2.ShowDialog();
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

        //// Копіюючий конструктор
        //public MineralWaterAutomat(MineralWaterAutomat prev)
        //{
        //    this.name = prev.name;
        //    this.max_water = prev.max_water;
        //    this.available_water = prev.available_water;
        //    this.price = prev.price;
        //    this.available_water = prev.available_water;

        //}

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

    public class Card_NonBubbly
    {
        public float liters;
        public float money;
    }

    public class Cash_NonBubbly : Card_NonBubbly
    {
        public float available_money;
        public float remainder;
    }

    public class Card_NonBubbly_Bottled
    {
        public float amount;
        public float money;
        public int bottle_amount;
        public int cap_amount;
    }

    public class Cash_NonBubbly_Bottled : Card_NonBubbly_Bottled
    {
        public float available_money;
        public float remainder;
    }
}
