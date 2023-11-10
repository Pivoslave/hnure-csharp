using System;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace ConsoleApp1
{
    class MineralWaterAutomat
    {
        public string name;
        public float max_water;
        public float available_water;
        public float price;
        public float available_money;

        // Конструктор, що не допускає нульових та значень менше нуля
        public MineralWaterAutomat(string _n, float _mw, float _aw,  float _am, float _p)
        {
            this.name = _n != "" ? _n : "Тестова Назва";
            this.max_water = _mw > 0 ? _mw : 0.1f;
            this.available_water = _aw >= 0 ? _aw : 0;
            this.price = _p > 0 ? _p : 0.1f;
            this.available_money = _am >= 0 ? _am : 0;
        }

        // Копіюючий конструктор
        public MineralWaterAutomat(MineralWaterAutomat prev)
        {
            this.name = prev.name;
            this.max_water = prev.max_water;
            this.available_water = prev.available_water;
            this.price = prev.price;
            this.available_water = prev.available_water;
        }

        // Метод додавання води
        public void AddWater(float amount)
        {
            if (this.available_water + amount <= this.max_water)
            {
                this.available_water += amount;
                Console.WriteLine($"Додано {amount} літрів води");
            }

            else Console.WriteLine($"Доданої води більше за місткість автомату на {(this.available_water + amount) - this.max_water} літрів");
        }

        // Метод вираховування кількості води за введеною ціною
        float CalcLiters(float money)
        {
            return (money / this.price);
        }

        // Метод, що повертає вирахувану кількість води

        public void DisplayPossibleBought(float money)
        {
            Console.WriteLine($"За введеними грошима ({money} гривень) можна купити {CalcLiters(money)} літрів води");
        }

        // Метод продажу води
        public void SellWater(float money)
        {
            float amountToSell = CalcLiters(money);

            if (amountToSell <= this.available_water)
            {
                this.available_water -= amountToSell;
                this.available_money += money;
                Console.WriteLine($"Продано {amountToSell} літрів води в обмін на {money} гривень");
            }

            else Console.WriteLine("Недостатня кількість води в автоматі, спробуйте пізніше");
        }

        // Метод зняття грошей
        public void MoneyWithdrawal(float money)
        {
            if (money <= this.available_money)
            {
                Console.WriteLine($"Оператором знято {money} гривень");
                this.available_money -= money;
            }

            else Console.WriteLine("У автоматі недостатньо грошей для зняття такої суми!");
        }
    }

    class MainProgram
    {
        static void Main()
        {
            Console.OutputEncoding = UTF8Encoding.UTF8;

            MineralWaterAutomat auto1 = new MineralWaterAutomat("Автомат 1", 30, 15, 0, 2);
            MineralWaterAutomat auto11 = new MineralWaterAutomat(auto1);

            Console.WriteLine($"{auto11.name}, {auto11.max_water}, {auto11.available_water}, {auto11.price}, {auto11.available_money}");

            auto1.AddWater(15);
            auto11.AddWater(16);

            auto11.name = "Автомат 11";

            Console.WriteLine($"\n{auto1.name}, {auto1.max_water}, {auto1.available_water}, {auto1.price}, {auto1.available_money}");
            Console.WriteLine($"{auto11.name}, {auto11.max_water}, {auto11.available_water}, {auto11.price}, {auto11.available_money}\n");

            auto1.DisplayPossibleBought(55);
            auto1.SellWater(14);

            Console.WriteLine($"{auto1.name}, {auto1.max_water}, {auto1.available_water}, {auto1.price}, {auto1.available_money}\n");

            auto1.MoneyWithdrawal(16);
            Console.WriteLine($"{auto1.name}, {auto1.max_water}, {auto1.available_water}, {auto1.price}, {auto1.available_money} \n");

            auto1.MoneyWithdrawal(10);
            Console.WriteLine($"{auto1.name}, {auto1.max_water}, {auto1.available_water}, {auto1.price}, {auto1.available_money}");

        }
    }
}

