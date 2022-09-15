using System;
using System.IO;
using System.Linq;

class mainClass
{ 

    public static void Main(string[] args)
    {
        //Console.WriteLine("Какое упражнение будем делать?");
        //string s = Console.ReadLine();


        //Unit_1_8_1.MainFunction1();
        Unit_1_8_1.mainFunction2();

        
        
        Console.ReadKey();
    }
}

//======================
//======================
//======================
//======================

public class Unit_1_8_1
{
    public static void MainFunction1()
    {
        byte timeLag = 0;
        bool deleted;

        DirectoryInfo mainD = getFolder();

        try
        { Console.WriteLine("Имя папки успешно задано: " + mainD.FullName); }
        catch
        {   //Console.WriteLine(e.Message);
            Console.WriteLine("Завершаем работу приложения");
            Console.ReadKey();
            Environment.Exit(0);
        }

        Console.WriteLine("\nПродолжаем");

        Console.WriteLine("Сейчас {0}", DateTime.Now);

        //сначала переберём файлы
        FileInfo[] fs = mainD.GetFiles();
        foreach (var f in fs)
        {
            DateTime d = File.GetLastAccessTime(f.FullName);
            Console.WriteLine("\nФайл {0} последний раз использовался в {1}", f.FullName, d);
            TimeSpan delta = DateTime.Now - d;
            if (delta.Minutes > timeLag)
            {
                Console.WriteLine("Это больше заданного лага ({0} мин.), пробуем удалить", timeLag);
                try
                { f.Delete(); Console.WriteLine("Файл успешно удалён"); }
                catch (Exception e)
                { Console.WriteLine("Удаление не удалось, ошибка {0}", e.Message); }
            }
            else { Console.WriteLine("Это в пределах заданного лага ({0} мин.), оставляем", timeLag); }
        }

        //теперь папки
        DirectoryInfo[] ds = mainD.GetDirectories();
        foreach (var dr in ds)
        {
            DateTime d = File.GetLastAccessTime(dr.FullName);
            Console.WriteLine("\nПапка {0} последний раз использовалась в {1}", dr.FullName, d);
            TimeSpan delta = DateTime.Now - d;
            if (delta.Minutes >= timeLag)
            {
                Console.WriteLine("Это больше заданного лага ({0} мин.), пробуем удалить", timeLag);
                try
                {
                    deleted = clearFolder(dr);
                    if (deleted)
                    { dr.Delete(); Console.WriteLine("Папка успешно удалена"); }
                    else { Console.WriteLine("Не удалось полностью очистить папку"); }
                }
                catch (Exception e)
                { Console.WriteLine("Удаление не удалось, ошибка {0}", e.Message); }
            }
            else { Console.WriteLine("Это в пределах заданного лага ({0} мин.), оставляем", timeLag); }

        }
        Console.WriteLine("\nЗакончили упражнение");

    }

    public static void mainFunction2()
    { 
    
    }

   //======================
    public static DirectoryInfo getFolder()
    {
        Console.WriteLine("Введите путь к папке: ");
        string s="";
        bool exitFlag = false;
        DirectoryInfo di=null;

        do
        {   s = Console.ReadLine();
            if (s.ToLower() == "quit") { di = null; break; }

            if (s == "") { Console.WriteLine("Необходимо ввести значение\nПробуем ещё раз или заканчиваем (quit)?"); continue; }
            
            //проверим существование
            di = new DirectoryInfo(s);
            if (!di.Exists) { Console.WriteLine("Папки не существует.\nПробуем ещё раз или заканчиваем (quit)?"); continue; }

            //проверим доступ
            try
            { FileInfo[] fi = di.GetFiles(); }
            catch (Exception e)
            { Console.WriteLine("Проблемы с доступом к папке.\nПробуем ещё раз или заканчиваем (quit)?"); continue;            }

            exitFlag = true;
        } while (!exitFlag);

        return di;
    }

    //======================

    public static bool clearFolder(DirectoryInfo dir)
    {
        bool result = true;
        //снесём файлы
        FileInfo[] fls = dir.GetFiles();
        foreach (var ff in fls)
        {   try { ff.Delete(); }
            catch (Exception e)
            { result = false; Console.WriteLine("\t не могу удалить файл {0}, ошибка {1}", ff.FullName, e.Message); }
        }
        //снесём папки
        DirectoryInfo[] drs = dir.GetDirectories();
        foreach (var dd in drs)
        {
            try { clearFolder(dd); dd.Delete(); }
            catch (Exception e)
            { result = false; Console.WriteLine("\t не могу удалить папку {0}, ошибка {1}", dd.FullName, e.Message); }
        }

        return result;
    }
    //====================== 
}

