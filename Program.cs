using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice2
{
    /// <summary>
    /// Класс Клиент банка</summary>
    /// <remarks>
    /// Абстрактный класс</remarks>
    abstract class Client
    {
        /// <summary>
        /// Хранилище для свойства имени или названия</summary>
        protected string Name;
        /// <summary>
        /// Хранилище для свойства даты начала работы с банком</summary>
        protected DateTime Date;
        /// <summary>
        /// Хранилище для свойства величины баланса или займа клиента</summary>
        protected int Amount;
        /// <summary>
        /// Конструктор класса</summary>
        /// <param name="Name"> Имя или название</param>
        /// <param name="Date"> Дата начала работы с банком</param>
        /// <param name="Amount"> Величина баланса или займа клиента</param>
        protected Client (string Name, DateTime Date, int Amount)
        {
            this.Name = Name;
            this.Date = Date;
            this.Amount = Amount;
        }
        /// <summary>
        /// Печать информации о клиенте банка</summary>
        public abstract void PrintInfo();
        /// <summary>
        /// Печать информации о клиенте банка</summary>
        /// <remarks>
        /// Исходя из данной даты начала работы с ним</remarks>
        /// <param name="Date"> Дата начала работы с банком</param>
        public virtual void PrintInfo(DateTime Date)
        {
            if (this.Date == Date)
                PrintInfo();
        }
    }

    /// <summary>
    /// Класс Вкладчик</summary>
    /// <remarks>
    /// Хранит информацию о клиенте банка, может выдать информацию на экран</remarks>
    class Contributor : Client
    {
        /// <summary>
        /// Хранилище для свойства процента по вкладу</summary>
        protected double InterestRate;
        /// <summary>
        /// Конструктор класса. </summary>
        /// <param name="Name"> Имя</param>
        /// <param name="Date"> Дата начала работы с банком</param>
        /// <param name="Amount"> Величина вклада</param>
        /// <param name="InterestRate"> Процент по вкладу</param>
        public Contributor(string Name, DateTime Date, int Amount, double InterestRate) : base(Name, Date, Amount)
        {
            this.InterestRate = InterestRate;
        }
        /// <summary>
        /// Печать информации о вкладчике</summary>
        override public void PrintInfo()
        {
            Console.WriteLine(String.Format(
                        "{0, 30}|{1, 20}|{2, 20}|{3, 20}",
                        Name,
                        Date,
                        Amount,
                        InterestRate
                    ));
        }
    }

    /// <summary>
    /// Класс Заемщик</summary>
    /// <remarks>
    /// Хранит информацию о клиенте банка, может выдать информацию на экран</remarks>
    class Debtor : Client
    {
        /// <summary>
        /// Хранилище для свойства остатка долга</summary>
        private int LoanBalance;
        /// <summary>
        /// Хранилище для свойства процента по займу</summary>
        private double InterestRate;
        /// <summary>
        /// Конструктор класса. </summary>
        /// <param name="Name"> Имя</param>
        /// <param name="Date"> Дата начала работы с банком</param>
        /// <param name="Amount"> Величина займа</param>
        /// <param name="InterestRate"> Процент по кредиту</param>
        /// <param name="LoanBalance"> Остаток долга</param>
        public Debtor (string Name, DateTime Date, int Amount, double InterestRate, int LoanBalance) : base(Name, Date, Amount)
        {
            this.InterestRate = InterestRate;
            this.LoanBalance = LoanBalance;
        }
        /// <summary>
        /// Печать информации о заемщике</summary>
        override public void PrintInfo()
        {
            Console.WriteLine(String.Format(
                        "{0, 30}|{1, 20}|{2, 20}|{3, 20}|{4, 20}",
                        Name,
                        Date,
                        Amount,
                        InterestRate,
                        LoanBalance
                    ));
        }
    }

    /// <summary>
    /// Класс Организация</summary>
    /// <remarks>
    /// Хранит информацию о клиенте банка, может выдать информацию на экран</remarks>
    class Organisation : Client
    {
        /// <summary>
        /// Хранилище для свойства номер счета</summary>
        private readonly int Id;
        /// <summary>
        /// Конструктор класса. </summary>
        /// <param name="Name"> Название</param>
        /// <param name="Date"> Дата начала работы с банком</param>
        /// <param name="Id"> Номер счета</param>
        /// <param name="Amount"> Сумма на счету</param>
        public Organisation(string Name, DateTime Date, int Id, int Amount) : base(Name, Date, Amount)
        {
            this.Id = Id;
        }
        /// <summary>
        /// Печать информации о компании</summary>
        override public void PrintInfo()
        {
            Console.WriteLine(String.Format(
                        "{0, 30}|{1, 20}|{2, 20}|{3, 20}",
                        Name,
                        Date,
                        Id,
                        Amount
                    ));
        }
    }

    class Program
    {
        /// <summary>
        /// Точка входа для приложения.
        /// </summary>
        /// <param name="args"> Список аргументов командной строки</param>
        static void Main(string[] args)
        {
            int n = 0;
            DateTime date;
            using (StreamReader sr = new StreamReader(".../input.txt"))
            {
                while (sr.ReadLine() != null)
                {
                    n++;
                }
            }
            Client[] ClientArray = new Client[n];
            using (StreamReader sr = new StreamReader(".../input.txt"))
            {
                int c = 0;
                string line = "";
                while ((line = sr.ReadLine()) != null)
                {
                    string[] LineArray = line.Split(' ');
                    switch (LineArray[0])
                    {
                        case "Вкладчик":
                            {
                                ClientArray[c] = new Contributor(LineArray[1], Convert.ToDateTime(LineArray[2]), Convert.ToInt32(LineArray[3]), Convert.ToDouble(LineArray[4]));
                                break;
                            }
                        case "Заемщик":
                            {
                                ClientArray[c] = new Debtor(LineArray[1], Convert.ToDateTime(LineArray[2]), Convert.ToInt32(LineArray[3]), Convert.ToDouble(LineArray[4]), Convert.ToInt32(LineArray[5]));
                                break;
                            }
                        case "Организация":
                            {
                                ClientArray[c] = new Organisation(LineArray[1], Convert.ToDateTime(LineArray[2]), Convert.ToInt32(LineArray[3]), Convert.ToInt32(LineArray[4]));
                                break;
                            }
                    }
                    c++;
                }
            }
            Console.WriteLine(String.Format(
                        "{0}{1, 30}|{2, 20}|{3, 20}|{4, 20}",
                        "Вкладчики:\n",
                        "Фамилия",
                        "Дата открытия вклада",
                        "Размер вклада",
                        "Процентная ставка"
                    ));
            for (int i = 0; i < n; i++)
            {
                if (ClientArray[i] is Contributor)
                    ClientArray[i].PrintInfo();
            }
            Console.WriteLine();
            Console.WriteLine(String.Format(
                        "{0}{1, 30}|{2, 20}|{3, 20}|{4, 20}|{5, 20}",
                        "Заемщики:\n",
                        "Фамилия",
                        "Дата выдачи кредита",
                        "Размер кредита",
                        "Процентная ставка",
                        "Остаток долга"
                    ));
            for (int i = 0; i < n; i++)
            {
                if (ClientArray[i] is Debtor)
                    ClientArray[i].PrintInfo();
            }
            Console.WriteLine();
            Console.WriteLine(String.Format(
                        "{0}{1, 30}|{2, 20}|{3, 20}|{4, 20}",
                        "Организации:\n",
                        "Название",
                        "Дата открытия счета",
                        "Номер счета",
                        "Сумма на счету"
                    ));
            for (int i = 0; i < n; i++)
            {
                if (ClientArray[i] is Organisation)
                    ClientArray[i].PrintInfo();
            }
            Console.WriteLine();
            Console.WriteLine("Введите дату, по которой хотите найти клиента (mm/dd/yy):");
            date = Convert.ToDateTime(Console.ReadLine());
            Console.WriteLine(String.Format(
                        "{0}{1, 30}|{2, 20}|{3, 20}|{4, 20}",
                        "Вкладчики:\n",
                        "Фамилия",
                        "Дата открытия вклада",
                        "Размер вклада",
                        "Процентная ставка"
                    ));
            for (int i = 0; i < n; i++)
            {
                if (ClientArray[i] is Contributor)
                    ClientArray[i].PrintInfo(date);
            }
            Console.WriteLine();
            Console.WriteLine(String.Format(
                        "{0}{1, 30}|{2, 20}|{3, 20}|{4, 20}|{5, 20}",
                        "Заемщики:\n",
                        "Фамилия",
                        "Дата выдачи кредита",
                        "Размер кредита",
                        "Процентная ставка",
                        "Остаток долга"
                    ));
            for (int i = 0; i < n; i++)
            {
                if (ClientArray[i] is Debtor)
                    ClientArray[i].PrintInfo(date);
            }
            Console.WriteLine();
            Console.WriteLine(String.Format(
                        "{0}{1, 30}|{2, 20}|{3, 20}|{4, 20}",
                        "Организации:\n",
                        "Название",
                        "Дата открытия счета",
                        "Номер счета",
                        "Сумма на счету"
                    ));
            for (int i = 0; i < n; i++)
            {
                if (ClientArray[i] is Organisation)
                    ClientArray[i].PrintInfo(date);
            }
        }
    }
}
