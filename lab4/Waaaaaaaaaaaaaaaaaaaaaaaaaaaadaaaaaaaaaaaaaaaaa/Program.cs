using model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Waaaaaaaaaaaaaaaaaaaaaaaaaaaadaaaaaaaaaaaaaaaaa
{
    internal static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Form1 mainform = new Form1();

            WaterCompany wc = new WaterCompany("тест", "тест", "тест");
            wc.OnAutomatsCountChange += wc.RecalculatePrice;
            wc.OnRequestAutomatNames += mainform.setCombo;

            mainform.OnRequestInfo += wc.GetActiceAutomat;
            wc.OnInfoReceived += mainform.AutomatInfoHandle;
            mainform.OnRequestChangeField += wc.ChangeInfo;
            mainform.OnTransactRequest += wc.payWater;

            
            wc.OnAlert += mainform.ShowAlert;
            mainform.OnRequestWaterADD += wc.AddWaterToFacility;
            mainform.OnRequestLoad += wc.LoadInfo;
            mainform.OnRequestSave += wc.SaveInfo;
            wc.OnInfoUpdate += mainform.UpdateCompanyInfo;


            wc.AddAutomat("Test Automat", 30, 20, 15, 2, true, true, true, 10, 12, "Тестова Вулиця 14, секція 88", 1);
            wc.AddAutomat("Test Automat 2", 23, 12, 6, 3, false, false, false, 0, 0, "Тестова Вулиця 22, секція 8", 0);

            mainform.OnCreateAutomat += wc.AddAutomat;
            mainform.OnLogsRequested += wc.getLog;
            mainform.OnChangeCompanyInfo += wc.UpdateVisibleInfo;
            mainform.OnDeletionRequested += wc.RemoveAutomat;

            Application.Run(mainform);
        }
    }
}
