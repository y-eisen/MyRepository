using System;
using System.Linq;


public class Programm
{

    public static void Main(string[] args)
    {
        /* нет, первое задание я не понял. что такое массив исключения???
        Exception[] exs = new Exception[5];
        exs[0] = new NameException("Ошибка имени 1");
        exs[1] = new ArgumentNullException("Нельзя передавать аргумент null");
        exs[2] = new ArgumentOutOfRangeException("Аргемент за пределами допустимых значений");
        exs[3] = new NotImplementedException("Отсутствует реализация");
        exs[4] = new DivideByZeroException("Ошибка деления на ноль");
        */

        string s="1";
        List<string> lst = new List<string>();
       

        while (s != "exit" && lst.Count<5) 
        {
            Console.WriteLine("\nВведите фамилию и имя:");
            s = Console.ReadLine();

            try
            {
                if (checkName(s,lst)) lst.Add(s);
            }
            catch (Exception ex) when (ex is NameException | ex is ArgumentNullException)
            { Console.WriteLine(ex.Message); }
            //catch (Exception ex) when ()
            //{ Console.WriteLine(ex.Message); }
            catch (ArgumentOutOfRangeException ex)
            { Console.WriteLine(ex.Message); }
            catch (DuplicateWaitObjectException ex)
            { Console.WriteLine(ex.Message); }
            catch (TimeoutException ex)
            { Console.WriteLine(ex.Message); }

            //Console.WriteLine(lst.Count);
        }


        if (s == "exit") Environment.Exit(0);

        CommunitySorting cs = new CommunitySorting();
        cs.ItsTimeForSorting += SortingMethod;

        try
        { cs.StartProcess(ref lst); }
        catch (ArgumentOutOfRangeException ex)
        { Console.WriteLine(ex.Message); Console.ReadKey(); Environment.Exit(0); }

        Console.WriteLine("\n\nИтого творческий коллектив, отсортированный {0} порядке:",lst[5]);
        Console.WriteLine("{0}\n{1}\n{2}\n{3}\n{4}",lst[0], lst[1], lst[2], lst[3], lst[4]);

        Console.ReadKey();
    }

    public static void SortingMethod(direction ddd, List<string> lst)
    {
        if (lst.Count != 5) throw new ArgumentOutOfRangeException("Не получилось, это упражнение ровно на 5 значений, а передано другое количество");
        if (ddd == direction.ahead) lst.Add(" в прямом алфавитном ");
        else lst.Add(" в обратном алфавитном ");
        string sss;

        for (int i1 = 0; i1 < 4; i1++)
        {
            for (int i2 = i1+1; i2 < 5; i2++)
            {
                if ((lst[i1].CompareTo(lst[i2]) > 0 && ddd==direction.ahead) || (lst[i1].CompareTo(lst[i2]) < 0 && ddd == direction.back))
                {
                    sss = lst[i1];
                    lst[i1] = lst[i2];
                    lst[i2] = sss;
                }
            }
        }        
    }


    static bool checkName(string s, List <string> lst)
    {   
        s = s.Replace("  ", " ").Replace("  ", " ").Trim();
        if (s.ToLower() == "exit") throw new TimeoutException("Программа прервана пользователем");

        if (s.IndexOf(" ")!=s.LastIndexOf(" ")) throw new NameException("Нужны фамилия и имя. Ожидается ввод двух слов.");
        if (s.IndexOf(" ") ==-1) throw new NameException("Необходимы фамилия и имя. Введено одно слово вместо двух.");
        if (s == "") throw new ArgumentNullException("ФИО не может быть пустым");
        if (s.Length > 20) throw new ArgumentOutOfRangeException("Слишком длинное имя");
        if (s.Length < 5) throw new ArgumentOutOfRangeException("Слишком короткое имя");
        if (lst.Contains(s)) throw new DuplicateWaitObjectException("Данный персонаж уже присутствует");
        for (byte b = 0; b < 10; b++)
        { if (s.IndexOf(b.ToString()) > 0) throw new NameException("Имя не может содержать цифры");        }
        
        Console.WriteLine("К творческому коллективу присоединился новый персонаж: {0}",s);
        return true;
    }

    public delegate void SortCommunity(direction ddd, List<string> lst);

    public class CommunitySorting
    {
        public event SortCommunity ItsTimeForSorting; 

        public void StartProcess(ref List<string> lst)
        {
            string st = "0";
            Console.WriteLine("\n\nКак сортировать? По алфавиту (1) или в обратную сторону (2):");
            while (st != "1" && st != "2")
            {
                st = Console.ReadLine().Trim();
                if (st != "1" && st != "2") Console.WriteLine("Нет, нужно ввести 1 или 2");
            }
            Console.WriteLine("\n\n Сортируем...");
            if (st == "1") sortingProcess(direction.ahead, lst);
            if (st == "2") sortingProcess(direction.back, lst);
        }

        protected virtual void sortingProcess(direction ddd, List<string> lst) 
        {
            ItsTimeForSorting?.Invoke(ddd,lst);
        }
    }

    public enum direction : byte { ahead, back }

}


public class NameException : ArgumentException
{
    public NameException() : base("Неизвестная ошибка ввода ФИО")
    {   HelpLink = "google.com"; }

    public NameException(string _exceptionMessage) : base(_exceptionMessage)
    {   HelpLink = "google.com";    }
}


