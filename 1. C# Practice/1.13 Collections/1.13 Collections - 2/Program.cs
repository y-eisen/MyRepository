using System.Diagnostics;
using System.Threading;
using System.Collections.Generic;
using System.Collections;
using System.Linq;


public class Program
{
    //Ваша задача — написать программу ,которая позволит понять, какие 10 слов чаще всего встречаются в тексте.

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
        
        List<string> list = new List<string>();
        list.AddRange(fContent);
        HashSet<string> hash = new HashSet<string>();
        foreach (string s in list)
            hash.Add(s);
        Console.WriteLine("Уникальных слов {0}",hash.Count());
       
        List<Words> counterList = new List<Words>();

        int k = 0;
        foreach (string s in hash)
        {
            k++;
            if (k % 1000 == 0) Console.Write("{0}% ... ",(int)(100*k/hash.Count()));
            counterList.Add(new Words(s, list.Count(v => v == s)));
        }

        Console.WriteLine("Самые частые слова:");
        var sorted=counterList.OrderByDescending(s => s.Amount);

        k = 0;
        foreach (var s in sorted)
        {
            k++;
            Console.WriteLine("{0} - {1} шт",s.Word,s.Amount);
            if (k == 10) break;
        }
        Console.WriteLine("Закончили");

        Console.ReadKey();
    }

    class Words
    {
        public Words(string word, int amoint)
        {
            Word = word;
            Amount = amoint;
        }
        public string Word { get; set; }
        public int Amount { get; set; }
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
        string text = File.ReadAllText(fn).ToLower();

        var noPunctuationText = new string(text.Where(c => !char.IsPunctuation(c)).ToArray());

        char[] delimiters = new char[] { ' ', '\r', '\n' };
        // разбиваем нашу строку текста, используя ранее перечисленные символы-разделители
        var words = noPunctuationText.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
        // выводим количество
        Console.WriteLine("Количество слов в тексте: {0}", words.Length);
        return words;
    }

}