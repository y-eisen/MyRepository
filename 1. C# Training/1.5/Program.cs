using System;


class mainClass
{
    public static void Main(string[] args)
    {
        Console.WriteLine("new func");

        IMessenger<Phone> v = new Viber();
        Phone ph = v.DeviceInfo();

        Console.WriteLine(ph.s);


        Console.ReadKey();
    }

    public class Viber : IMessenger<Phone>
    {
        public Phone DeviceInfo()
        {
            Console.WriteLine("Devinfo");
            return new Phone();
        }
    }

    public interface IMessenger<T>
    {
        T DeviceInfo();
    }

    public class Phone
    {
        public string s { get; set; }
        public Phone()
        {
            s = "phone string";
        }
    }

    public class Computer
    {
        public string s { get; set; }
        public Computer()
        {
            s = "Computer string";
        }
    }








public static void Main1(string[] args)
{
    string s;

    s = StringQuestion("Запустим модуль 1-5 про запрос всякого?");
    if (s == "y")
    {
        // (string Name, string LName, int age, string[] Pets, string[] Colors) user = GetUser();
        (string Name, string LName, int age, string[] Pets, string[] FCols) user = GetUser();
        Console.WriteLine("\nНачинаем выводить результаты");
        Console.ReadKey();
    }

    Bus NewBus = new Bus { Load = 11 };
    //NewBus.Load = 0;
    NewBus.PrintStatus();


    int[][] TestObj = new int[2][];
    TestObj[0] = new int[2];
    TestObj[1] = new int[2];
    TestObj[0][0] = 10;

    Console.WriteLine(TestObj[0][0]);

    Console.ReadKey();



}

class Bus
{
    public int? Load;

    public void PrintStatus()
    {
        if (Load.HasValue)
        { Console.WriteLine("Количество пассажиров {0}", Load.Value); }
        else
        { Console.WriteLine("Пусто"); }
    }
}

static (string Name, string LName, int age, string[] Pets, string[] FCols) GetUser()
{
    (string Name, string LName, int age, string[] Pets, string[] FCols) newUser;
    string s;
    int am;
    int i;

    newUser.Name = StringQuestion("Введите имя");
    newUser.LName = StringQuestion("Введите фамилию");
    newUser.age = intQuestion("Ваш возраст");


    //питомцы
    s = StringQuestion("Есть ли у вас питомцы? \nВведите y/n или да/нет").ToLower();
    if (s == "y" || s == "да")
    {
        am = intQuestion("Колиечество питомцев? ");
        var arr = new string[am];
        newUser.Pets = arr;
        for (i = 0; i < am; i++) newUser.Pets[i] = StringQuestion("Кличка животного номер " + (i + 1).ToString());
    }
    else
    {
        var arr = new string[0];
        newUser.Pets = arr;
    }

    //цвета
    am = intQuestion("Сколько у вас любимых цветов? ");
    var arr1 = new string[am];
    newUser.FCols = arr1;
    for (i = 0; i < am; i++) newUser.FCols[i] = StringQuestion("Любимый цвет номер " + (i + 1).ToString());

    return newUser;
}

// функции запроса значений
static int StrToInt(string s)
{
    bool b = int.TryParse(s, out int result);
    return result;
}
static string StringQuestion(string txt)
{
    Console.WriteLine(txt);
    string result = Console.ReadLine();
    return result;
}
static int intQuestion(string txt)
{
    int result;
    string s;
    do
    {
        Console.WriteLine(txt);
        s = Console.ReadLine();
        result = StrToInt(s);
        if (result == 0) Console.WriteLine("Некорректный ввод, попробуем ещё раз.");
    }
    while (result == 0);
    return result;
}

}
