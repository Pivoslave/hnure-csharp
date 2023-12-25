using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;

namespace model
{
    public class MineralWaterAutomat
    {
        public string name;
        private double max_water;

        public double maxWater
        {
            get => max_water;
            set => max_water = value;
        }

        private double available_water;

        public double availableWater
        {
            get { return available_water; }
            set {
                if (value < 0)
                {
                    OnAlert(0);
                    OnWaterEnd(this);
                }
                else if (value > max_water) OnAlert(1);
                else
                {
                    available_water = value;
                    OnLogAttempt($"Кількість води в автоматі \'{this.name}\' - {value}");
                }
            }
        }

        public delegate void AlertUser(int a);
        public event AlertUser OnAlert;

        public double price;
        private double available_money;

        public double availableMoney
        {
            get => available_money;
            set{
                if (value < 0) { OnAlert(2); }

                else
                {
                    available_money = value;
                    OnLogAttempt($"Кількість грошей в автоматі \'{this.name}\' - {value}");
                }
            }
        }

        private bool card_transaction;

        public bool cardTransaction { get => card_transaction; }
       


        private bool can_buy_bottle;
        public bool canBuyBottle { get => can_buy_bottle; }
        
        private bool can_buy_bubbly;
        public bool canBuyBubbly { get => can_buy_bubbly; }


        private int bottle_amount;

        public int bottleAmount
        {
            get => bottle_amount;
            set{
                if (value < 0) OnBottleEnd?.Invoke();
                else{
                    bottle_amount = value;
                    OnLogAttempt($"Кількість пляшок в автоматі \'{this.name}\' - {value}");
                }
            }
        }

        private int cap_amount;

        public int capAmount
        {
            get => cap_amount;
            set{
                if (value < 0) OnCapEnd?.Invoke();
                else{
                    cap_amount = value;
                    OnLogAttempt($"Кількість кришок в автоматі \'{this.name}\' - {value}");
                }
            }
        }

        public string address;
        private double bottle_price;

        public double bottlePrice
        {
            get => bottle_price;
            set => bottle_price = value;
        }


        public delegate void WaterMoneyChange(double wt);
        public event WaterMoneyChange OnWaterChange, OnMoneyChange;

        public delegate void ComponentEnded();
        public event ComponentEnded OnWaterOverflow, OnBottleEnd, OnCapEnd, onMoneyEnd;

        public delegate void MiscChange(int cp);
        public event MiscChange OnBottleChange, OnCapChange;

        public delegate void Refill(MineralWaterAutomat a);
        public event Refill OnWaterEnd;

        public delegate void LogInfo(string s);
        public event LogInfo OnLogAttempt;

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
            availableWater += amount;
        }

        // Метод вираховування кількості води за введеною ціною
        double CalcLiters(double money)
        {
            return (money / this.price);
        }


        // Метод продажу води
        public void SellWater(double money)
        {
            double amountToSell = CalcLiters(money);

            this.availableWater -= amountToSell;

            if(amountToSell <= this.availableWater) { availableMoney += money; }
        }

        // Метод зняття грошей
        public void MoneyWithdrawal(double money)
        {
            availableMoney -= money;
        }

        public void AddCaps(int amount) { this.capAmount += amount; }
        public void AddBottles(int amount) { this.bottleAmount += amount; }
        
        public string GetAddress() { return address; }

        public string GetName() { return name; }
        public double GetPrice() { return price; }
    }

    public class WaterCompany
    {
        public string name;
        HashSet<MineralWaterAutomat> automats;

        public HashSet<MineralWaterAutomat> Automats
        {
            get => automats;
            set{automats = value; OnAutomatsCountChange(this.automats); }
        }

        double total_money;
        LinkedList<string> logs;
        double waterStorage;
        public string address;
        public string edrpou;

        private string active_automat;
        public string activeAutomat
        {
            get => active_automat;
            set {
            active_automat = value;
                
            }
        }


        public delegate void AutomatsChange(HashSet<MineralWaterAutomat> at);
        public event AutomatsChange OnAutomatsChange;

        public delegate void ChangeFocus(MineralWaterAutomat name);
        public event ChangeFocus OnChangeFocus;

        public delegate void PriceRecalculation(HashSet<MineralWaterAutomat> a);
        public event PriceRecalculation OnAutomatsCountChange;

        public WaterCompany(string _name, string _address, string _edrpou)
        {
            this.name = _name;
            this.address = _address;
            this.edrpou = _edrpou;
            this.logs = new LinkedList<string>();
            this.automats = new HashSet<MineralWaterAutomat>();
            this.total_money = 0;
        }

        public WaterCompany() { this.name = ""; this.address = ""; this.edrpou = ""; this.total_money = 0; this.automats = new HashSet<MineralWaterAutomat>(); this.logs = new LinkedList<string>(); }

        
        public void RemoveAutomat(string address)
        {
            int len = this.automats.Count;
            automats.RemoveWhere(a => a.address == address);

            if(this.automats.Count != len) AddToLog($"Автомат за адресою {address} прибрано!");

            OnRequestAutomatNames(getAutomatNames());
        }

        public void AddWater(double amount)
        {
            waterStorage += amount;
        }

        public void SaveInfo(string path)
        {
            path = path.Substring(0, path.LastIndexOf('.')) + "_CINFO" + path.Substring(path.LastIndexOf('.'), path.Length - path.LastIndexOf('.'));

            System.IO.StreamWriter fs = new System.IO.StreamWriter(path);
            fs.WriteLine($"{this.name}{(char)7}{this.address}{(char)7}{this.edrpou}{(char)7}");
            foreach (MineralWaterAutomat ma in this.automats)
                fs.WriteLine($"{ma.name}{(char)7}{ma.maxWater}{(char)7}{ma.availableWater}{(char)7}{ma.price}{(char)7}{ma.availableMoney}{(char)7}{ma.cardTransaction}{(char)7}" +
                    $"{ma.canBuyBottle}{(char)7}{ma.canBuyBubbly}{(char)7}{ma.bottleAmount}{(char)7}{ma.capAmount}{(char)7}{ma.address}{(char)7}{ma.bottlePrice}");


            fs.Close();
        }

        public void LoadInfo(string path)
        {
            string result = System.IO.File.ReadAllText(path);

            this.name = result.Substring(0, result.IndexOf((char)7));
            result = result.Remove(0, result.IndexOf((char)7) + 1);
            this.address = result.Substring(0, result.IndexOf((char)7));
            result = result.Remove(0, result.IndexOf((char)7) + 1);
            this.edrpou = result.Substring(0, result.IndexOf((char)7));
            result = result.Remove(0, result.IndexOf((char)7) + 3);

            HashSet<MineralWaterAutomat> atms = new HashSet<MineralWaterAutomat>();

            while (result.Length != 0)
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
                result = result.Remove(0, (result.IndexOf('\n') + 1));

                atms.Add(new MineralWaterAutomat(name, maxwater, availablewater, money, price, card, bottle, bubbly, bottles, caps, address, bottleprice));
            }

            OnInfoUpdate(this.name, this.address, this.edrpou);
            string[] names = new string[this.automats.Count];

            int iterator = 0;
            foreach(MineralWaterAutomat ma in this.automats)
            {
                names[iterator++] = ma.name;
            }
            OnRequestAutomatNames(names);
        }

        public delegate void InfoUpdated(string name, string address, string edrpou);
        public event InfoUpdated OnInfoUpdate;

        void RecalcMoney()
        {
            double _money = 0;
            foreach (MineralWaterAutomat ma in this.automats) _money += ma.availableMoney;
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

        //public void SendCourier(MineralWaterAutomat ma)
        //{
        //    ma.availableWater += ma.maxWater;
         //   this.waterStorage -= ma.maxWater;
       // }

        public string[] getAutomatNames()
        {
            string[] names = new string[this.automats.Count];

            int indexer = -1;
            foreach(MineralWaterAutomat ma in automats)
            {
                indexer++;
                names[indexer] = ma.name;
            }

            return names;
        }

        public void changeActiveAutomat(string name)
        {
            this.active_automat = name;
        }

        public void RecalculatePrice(HashSet<MineralWaterAutomat> a)
        {
            double price = 0;
            foreach(MineralWaterAutomat ma in a)
            {
                price += ma.availableMoney;
            }

            this.total_money = price;
        }

        public delegate void ReceiveInfo(MineralWaterAutomat a);
        public event ReceiveInfo OnInfoReceived;

        public void GetActiceAutomat(string name)
        {
            MineralWaterAutomat ma = null;
            foreach (MineralWaterAutomat maa in automats) if (maa.name == name) ma = maa;

            activeAutomat = ma.name;
            OnInfoReceived(ma);
        }

        public void ChangeInfo(int field, double parameter1, double param2)
        {
            MineralWaterAutomat ma = null;
            foreach (MineralWaterAutomat maa in this.automats) if (maa.name == activeAutomat) ma = maa;
            
            switch(field)
            {
                case 1: // кришки + пляшки
                    if (ma.canBuyBottle)
                    {
                        ma.capAmount += (int)parameter1;
                        ma.bottleAmount += (int)param2;
                    }
                    break;

                case 2: //вода
                    ma.availableWater += parameter1;
                    break;

                case 3: // гроші
                    ma.availableMoney -= parameter1;
                    break;

            }

            int index = -1;
            foreach(MineralWaterAutomat maa in this.automats)
            {
                index++;
                if (maa.name == activeAutomat)
                {
                    automats.Remove(maa);
                    break;
                }

               
            }
            automats.Add(ma);
            OnInfoReceived(ma);
        }

        public delegate void AlertUser(int a);
        public event AlertUser OnAlert;

        public delegate void NeedWater(MineralWaterAutomat a);
        public event NeedWater OnNeedWater;


        public void payWater(double liters, double cost, double inserted, bool withBottle)
        {
            MineralWaterAutomat ma = null;

            foreach (MineralWaterAutomat maa in automats) if (maa.name == activeAutomat) ma = maa;

            int bottleamount = ((int)liters == liters ? (int)liters : (int)(liters + 0.5f));
            bottleamount = (bottleamount % 2 == 1 ? bottleamount +1 : bottleamount)/2;

            if (ma.availableWater < liters) { OnAlert(3); OnNeedWater += SendCourier; OnNeedWater(ma); OnNeedWater -= SendCourier; }
            else if (inserted < cost) OnAlert(4);
            else if (withBottle && ma.canBuyBottle && (bottleamount > ma.bottleAmount || bottleamount > ma.capAmount)) OnAlert(5);
            else
            {
                automats.Remove(ma);

                ma.availableWater -= liters;
                ma.availableMoney += cost;

                if (ma.canBuyBottle) { ma.capAmount -= bottleamount; ma.bottleAmount -= bottleamount; }

                Automats.Add(ma);
            }

            OnInfoReceived(ma);
        }

        public void SendCourier(MineralWaterAutomat ma)
        {
            Automats.Remove(ma);

            double waterNeeded = ma.maxWater - ma.availableWater;
            ma.availableWater += waterNeeded;
            this.waterStorage -= waterNeeded;

            Automats.Add(ma);
            OnInfoReceived(ma);
        }

        public void alertDelegation(int a)
        {
            OnAlert(a);
        }


        public void AddWaterToFacility(double liters)
        {
            this.waterStorage += liters;
        }

        public void AddToLog(string action) { this.logs.AddLast(action); }

        public delegate void AutomatNames(string[] names);
        public event AutomatNames OnRequestAutomatNames;

        public void AddAutomat(string _n, double _mw, double _aw, double _am, double _p, bool card_transaction, bool can_buy_bottle, bool can_buy_bubbly, int bottle_amount, int cap_amount, string address, double bottle_price)
        {
            MineralWaterAutomat a = (new MineralWaterAutomat(_n, _mw, _aw, _am, _p, card_transaction, can_buy_bottle, can_buy_bubbly, bottle_amount, cap_amount, address, bottle_price));

            a.OnAlert += this.alertDelegation;
            a.OnWaterEnd += this.SendCourier;
            a.OnLogAttempt += this.AddToLog;

            this.automats.Add(a);
            this.AddToLog($"Автомат {a.name} створено");
            RecalcMoney();
            
            OnRequestAutomatNames(getAutomatNames());
        }

        public void AddAutomat1(MineralWaterAutomat a)
        {
            this.automats.Add(a);
        }

        public void UpdateVisibleInfo(string[] par)
        {
            this.name = par[0];
            this.address = par[1];
            this.edrpou = par[2];

            OnInfoUpdate(this.name, this.address, this.edrpou);
        }
        
    }
}
