using System;
using System.IO;
using System.Linq;

class mainClass
{ 

    public static void Main(string[] args)
    {
        Console.WriteLine("Какое упражнение будем делать (1-4)?");
        string unitN = Console.ReadLine();
        //string unitN = "4";
        DirectoryInfo mainDir = null;

        long[] filesSize=new long[2];
        long[] filesCounter = new long[2];

        switch (unitN)
        {
            case "1":
                mainDir= Unit_1_8.MainFunction1();
                break;
            case "2":
                Console.WriteLine("\n\nВызвано упражнение 2\n\n");
                mainDir = Unit_1_8.MainFunction2();
                break;
            case "3":
                Console.WriteLine("\n\nВызвано упражнение 3\n\n");
                mainDir = Unit_1_8.MainFunction2();
                filesSize[0] = Math.Abs(Unit_1_8.DirSize(mainDir, false));
                filesCounter[0] = Math.Abs(Unit_1_8.filesCounter(mainDir, false));
                mainDir = Unit_1_8.MainFunction1(mainDir);
                mainDir = Unit_1_8.MainFunction2(mainDir);
                filesSize[1] = Math.Abs(Unit_1_8.DirSize(mainDir, false));
                filesCounter[1] = Math.Abs(Unit_1_8.filesCounter(mainDir, false));
                
                Console.WriteLine("\nИТОГИ:\nДо чистки папка весила {0} байт и содержала {1} файлов", filesSize[0], filesCounter[0]);
                if (filesSize[1]>0||filesCounter[1]>0)                
                    Console.WriteLine("После чистки папка стала весить {0} байт и содержать {1} файлов", filesSize[1], filesCounter[1]);
                Console.WriteLine("Удалено {0} файлов", filesCounter[0]- filesCounter[1]);
                Console.WriteLine("Освобождено {0} байт", filesSize[0] - filesSize[1]);
                break;
            default:
                Unit_1_8.MainFunction4();
                break;
        }


        Console.ReadKey();
    }
}

//======================
//======================
//======================
//======================

public class Unit_1_8
{
    public static DirectoryInfo MainFunction1(params DirectoryInfo[] dirIn)
    {
        byte timeLag = 0;
        bool deleted;

        DirectoryInfo mainD;
        try
        { mainD = dirIn[0]; }
        catch
        {
            Console.WriteLine("\n\nВызвано упражнение 1\n\n");
            mainD = getFolder();
        }

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

        return mainD;
    }

    public static DirectoryInfo MainFunction2(params DirectoryInfo[] dirIn)
    {
        DirectoryInfo mainD;
        try
        { mainD = dirIn[0]; }
        catch
        {   //Console.WriteLine("Вызвано упражнение 2\n\n");
            mainD = getFolder(); }

        try
        { Console.WriteLine("Имя папки успешно задано: " + mainD.FullName); }
        catch
        {   //Console.WriteLine(e.Message);
            Console.WriteLine("Завершаем работу приложения");
            Console.ReadKey();
            Environment.Exit(0);
        }

        long result = DirSize(mainD, true);

        if (result > 0)
        { Console.WriteLine("\nВес папки {0} составляет {1} байт", mainD, result); }
        else
        { Console.WriteLine("\nВес папки {0} составляет не менее {1} байт", mainD, -result);
            Console.WriteLine("Папка пуста (если после сообщения об успешном задании имени папки не было сообщений об ошибках), либо в процессе подсчёта встретились какие-то проблемы и тогда подсчёт может быть некорректен");
        }
        return mainD;
    }

    public static void MainFunction4(params string[] args)
    {
        Console.WriteLine("\n\nВызвано упражнение 4\n\n");

        //выбор файла для загрузки
        Console.WriteLine("Выбор папки в которой лежит исходный файл.");
        DirectoryInfo mainD = getFolder();

        try
        { Console.WriteLine("Имя папки успешно задано: " + mainD.FullName); }
        catch
        { Console.WriteLine("Завершаем работу приложения");
            Console.ReadKey();
            Environment.Exit(0); }

        string fileName = mainD.FullName + "//Students.dat";
        if (File.Exists(fileName))
        { Console.WriteLine("Файл с данными найден"); }
        else { Console.WriteLine("Файл с данными не найден. \nЗавершаем работу приложения"); Console.ReadKey(); Environment.Exit(0); }

        //создаём папку вывода
        string mainPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + "//students";
        DirectoryInfo outDir = new DirectoryInfo(mainPath);
        if (!outDir.Exists) outDir.Create();

        //список групп
        var groups = new List<string>();

        //поехали читать исходный файл

        using (BinaryReader reader = new BinaryReader(File.Open(fileName, FileMode.Open)))
        {   /// задача конечно примитивна, но почему не получается скачать предложенный файл по схеме - я не понимаю
            /// очень интересно было бы узнать, что это блин за ребус, почему не читается
            /// или тут имелось в виду, что нужно сообразить собрать себе свой двоичный файл по предложенной схеме, а не пользоваться готовым с сайта?
        }

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

    public static long DirSize(DirectoryInfo dr,bool showMessages)
    {   long size = 0;
        bool okFlag=true;

        if (size < 0) {okFlag = false; size = -size; }
        
        FileInfo[] fs = dr.GetFiles();
        foreach (var ff in fs)
        {   try {size += ff.Length;}
            catch (Exception e)
            {   okFlag = false;
                if (showMessages) Console.WriteLine("Не получается посчитать вес файла {0}, код ошибки {1}", ff.FullName, e.Message);}
        }

        DirectoryInfo[] ds = dr.GetDirectories();
        foreach (var dd in ds)
        {   try { size += DirSize(dd, showMessages); }
            catch (Exception e)
            {   okFlag = false;
                if (showMessages) Console.WriteLine("Ошибка запроса веса папки {0}, код ошибки {1}", dd.FullName, e.Message);            }
        }
        if (!okFlag) { size = -size; }
        return size;
    }

    public static long filesCounter(DirectoryInfo dr, bool showMessages)
    {
        long counter = 0;
        bool okFlag = true;

        if (counter < 0) { okFlag = false; counter = -counter; }

        FileInfo[] fs = dr.GetFiles();
        foreach (var ff in fs)
        {   counter += 1; }

        DirectoryInfo[] ds = dr.GetDirectories();
        foreach (var dd in ds)
        {   try { counter += filesCounter(dd, showMessages); }
            catch (Exception e)
            {   okFlag = false;
                if (showMessages) Console.WriteLine("Ошибка запроса количества файлов в папке {0}, код ошибки {1}", dd.FullName, e.Message);
            }
        }
        if (!okFlag) { counter = -counter; }
        return counter;
    }
}

