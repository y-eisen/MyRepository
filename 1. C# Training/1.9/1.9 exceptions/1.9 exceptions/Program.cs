public class MainClass
{
    public static void Main1(string[] args)
    {
        Exception exception = new Exception("Новое исключение");
        exception.Data.Add("Дата создания исключения : ", DateTime.Now);

        exception.HelpLink = "WWW.ya.ru";

        try
        {
            throw new RankException("Сообщение об ошибке");
        }

        catch (RankException ex)
        {
            Console.WriteLine(ex.GetType());
        }

        finally
        {
            Console.Read();
        }
        Console.ReadKey();


    }


}

public class Program
{
    public delegate int SumDelegate(int a, int b, int c);

    static void Main2(string[] args)
    {
        SumDelegate sss = Sum;
        sss.Invoke(1, 10, 50);
        sss.Invoke(1, 1, 2);
        Console.WriteLine(sss(10,5,2));
        Console.ReadKey();
    }

    static int Sum(int a, int b, int c)
    {
        return a + b + c;
    }
}

public class Program22
{
    public delegate void ShowDelegate();
    static void Main3(string[] args)
    {

        ShowDelegate showDelegate = ShowMessage1;
        showDelegate += ShowMessage2;
        showDelegate += ShowMessage3;
        showDelegate += ShowMessage4;

        showDelegate -= ShowMessage2;

        showDelegate.Invoke();

        Console.ReadKey();
    }

    static void ShowMessage1()
    {
        Console.WriteLine("Метод 1");
    }

    static void ShowMessage2()
    {
        Console.WriteLine("Метод 2");
    }

    static void ShowMessage3()
    {
        Console.WriteLine("Метод 3");
    }

    static void ShowMessage4()
    {
        Console.WriteLine("Метод 4");
    }
}

class Program123
{
    static void Main111(string[] args)
    {
        Func<int,int,string> Addition = AddNumbers;
        string result = Addition(10, 20);
        Console.WriteLine(result);
        Console.ReadKey();
    }

    private static string AddNumbers(int param1, int param2)
    {
        return "ясно";
    }
}

class eventClass
{
    public delegate void Notify();  // делегат                
    public class ProcessBusinessLogic
    {
        public event Notify ProcessCompleted; // событие

        public void StartProcess()
        {
            Console.WriteLine("Процесс начат!");
            OnProcessCompleted();
        }

        protected virtual void OnProcessCompleted() //protected virtual method
        {
            ProcessCompleted.Invoke();
        }
    }

    

    static void Main111111(string[] args)
    {
        Console.WriteLine("Старт");

        ProcessBusinessLogic bl = new ProcessBusinessLogic();
        bl.ProcessCompleted += bl_ProcessCompleted; // регистрируем событие
        bl.StartProcess();

        Console.ReadKey();
    }


    public static void bl_ProcessCompleted()
    {
        Console.WriteLine("Процесс завершён!");
    }


}

class Programm
{ 
    static void Main(string[] args)
    {
        /*NumberReader nbr = new NumberReader();

        nbr.numbEvent += ShowNumb;


        while (true)
        {
            try
            {
                nbr.read();
            }
            catch (FormatException)
            {
                Console.WriteLine("некорректное значение"); //Console.ReadKey();
            }
        }
        */

        _exceptions.TestSolution.GoProgramm();
    }

    static void ShowNumb(int number)
    {
        switch(number)
        {
            case 1: Console.WriteLine("Это 1"); break;
            case 2: Console.WriteLine("Это 2"); break;
        }
        //Console.ReadKey();
    }
}

public class NumberReader
{
    public delegate void NumbDel(int numb);
    public event NumbDel numbEvent;

    public void read()
    {
        Console.WriteLine();
        Console.WriteLine("Ввести значение 1 или 2: ");
        int number = Convert.ToInt32(Console.ReadLine());
        if (number != 1 && number != 2) throw new FormatException();
        NumbEntered(number);
    }

    protected virtual void NumbEntered(int numb)
    {
        numbEvent?.Invoke(numb); 
    }
}