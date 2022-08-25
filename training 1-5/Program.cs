using System;
class mainClass
{
    public static void Main(string[] args)
    {
        // (string Name, string LName, int age, string[] Pets, string[] Colors) user = GetUser();
        (string Name, string LName, int age, string[] Pets, string[] FCols) user = GetUser();

        Console.WriteLine("\nНачинаем выводить результаты");
        Console.ReadKey();

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
        bool b=int.TryParse(s, out int result);
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
            if (result==0) Console.WriteLine("Некорректный ввод, попробуем ещё раз.");
        }
        while (result== 0);
        return result;
    }


}
