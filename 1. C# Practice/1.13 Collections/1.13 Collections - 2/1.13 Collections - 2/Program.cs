using System.Diagnostics;
using System.Threading;
using System.Collections.Generic;
using System.Collections;


public class Program
{
    //Наша задача — сравнить производительность вставки в List<T> и LinkedList<T>.Для этого используйте уже знакомый вам StopWatch.
    //На примере этого текста, выясните, какие будут различия между этими коллекциями.

    static void Main(string[] args)
    {

        string fn = "";
        string[] fContent = null;

        while (fn == "")
        {
            try
            { fn = GetFileName(); }
            catch (Exception ex)
            {
                if (ex is ArgumentNullException) Console.WriteLine("Пустое имя файла");
                if (ex is FileNotFoundException) Console.WriteLine("Файл не найден");
            }
        }
        Console.WriteLine("\nНачинаю обработку...");


        fContent = getFileContent(fn);
        var l1 = new List<string>();
        var l2 = new LinkedList<string>();
        var lRes = new List<double>();

        for (int n = 1; n <= 3; n++)
        {
            lRes.Clear();
            Console.Write("\nВычисляю  ");

            for (int rep = 1; rep < 1000; rep++)
            {
                if (rep % 100 == 0) Console.Write(".");
                l1.Clear();
                l2.Clear();
                var stopWatch = Stopwatch.StartNew();
                foreach (var v in fContent)
                {
                    if (n == 1) l1.Add(v);
                    if (n == 2) l2.AddFirst(v);
                    if (n == 3) l2.AddLast(v);
                }
                //Console.WriteLine(stopWatch.Elapsed.TotalMilliseconds);
                lRes.Add(stopWatch.Elapsed.TotalMilliseconds);
            }
            lRes.Sort();

            if (n == 1) Console.WriteLine("\nПростой список");
            if (n == 2) Console.WriteLine("\nСвязанный список (добавление в начало списка)");
            if (n == 3) Console.WriteLine("\nСвязанный список (добавление в конец списка)");

            Console.WriteLine("Минимальное время наполнения {0}", lRes[0]);
            Console.WriteLine("Максимальное время наполнения {0}", lRes[lRes.Count - 1]);
            Console.WriteLine("Среднее время наполнения {0}", (double)((int)(lRes.Average() * 10000)) / 10000);
            if (n == 1) Console.WriteLine("Размер списка {0}", l1.Count());
            if (n >= 2) Console.WriteLine("Размер списка {0}", l2.Count());
        }

        Console.ReadKey();
    }

    static string GetFileName()
    {
        string s = "";
        Console.WriteLine("Введите путь к файлу");
        s = Console.ReadLine();
        if (s.Trim() == "") throw new ArgumentNullException();
        if (!File.Exists(s)) throw new FileNotFoundException();
        return s;
    }

    static string[] getFileContent(string fn)
    {
        string text = File.ReadAllText(fn);
        char[] delimiters = new char[] { ' ', '\r', '\n' };
        // разбиваем нашу строку текста, используя ранее перечисленные символы-разделители
        var words = text.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
        // выводим количество
        Console.WriteLine("Количество слов в тексте: {0}", words.Length);
        return words;
    }

}