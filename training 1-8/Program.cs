using System;
using System.IO;
using System.Linq;
using training_1_8;


 class mainClass
{
    public static void Main(string[] args)
    {
        Console.WriteLine("Hello");

        
        /*Folder F = new Folder();
        F.Files.Add("пеРвый".ToLower());
        F.Files.Add("Второй".ToLower());
        F.Files.Add("пеРвый".ToLower());
        if (F.Files.Contains("Первый".ToLower()))
        { Console.WriteLine("да!"); };
        Console.WriteLine(F.Files[1]);
        */

        /*
        // получим системные диски
        DriveInfo[] drives = DriveInfo.GetDrives();
        // Пробежимся по дискам и выведем их свойства
        foreach (DriveInfo drive in drives.Where((DriveInfo dd) => dd.DriveType == DriveType.Fixed))
        {
            WriteDriveInfo(drive);
            DirectoryInfo rootF = drive.RootDirectory;
            Console.WriteLine("Корень: "+rootF.FullName);
            var folders  = rootF.GetDirectories();

            foreach (DirectoryInfo d in folders)
            {WriteFolderInfo(d);}

            Console.WriteLine("");
        }
        */

        //GetCatalogs();
        //CreatAndMoveTraining();

        /*
        // упражнения с файлами
        FileInfo f1 = new FileInfo("C:\\Users\\Юрий\\Desktop\\TEST\\111.txt");
        Console.WriteLine(f1.Length);
        Console.WriteLine(f1.Name);
        Console.WriteLine(f1.FullName);
        Console.WriteLine(f1.Extension);
        Console.WriteLine(f1.DirectoryName);
        Console.WriteLine("");

        string f2n = f1.FullName.Replace("111", "222");
        Console.WriteLine(f2n);
        FileInfo f2 = new FileInfo(f2n);

        if (f2.Exists)
        {f2.Delete();}
        f2.Create();
        */

        /*string tf = Path.GetTempFileName();
        Console.WriteLine(tf);

        DateTime d;
        d = DateTime.Now;
        DateTime d1 =d.AddYears(10);
        Console.WriteLine(d.Year);
        Console.WriteLine(d1.Year);
        int i = d1.Year - d.Year;
        Console.WriteLine(i);

        //и опять про файл, вернёмся f1

        //DirectoryInfo di = new DirectoryInfo(f1.DirectoryName);
        //long dSize = DirectoryExtension.DirSize(di);
        //Console.WriteLine("Размер папки:{0}",dSize);

        //Console.WriteLine("\n\nБинарная шняжка");
        //experiment.WriteValues(f1.FullName);
        //experiment.ReadValues(f1.FullName);
        */




        Console.ReadKey();
    }


    public static void WriteDriveInfo(DriveInfo drive)
    {   
        Console.WriteLine($"Название: {drive.Name}");
        Console.WriteLine($"Тип: {drive.DriveType}");
        if (drive.IsReady)
        {
          Console.WriteLine($"Объем: {drive.TotalSize}");
          Console.WriteLine($"Свободно: {drive.TotalFreeSpace}");
          Console.WriteLine($"Метка: {drive.VolumeLabel}");
        }
    }

    public static void WriteFolderInfo(DirectoryInfo folder)
    {
        Console.WriteLine($"Папка: {folder.FullName}");
        Console.WriteLine($"корень: {folder.Root}");
    }


    public static void CreatAndMoveTraining()
    {
        //создать и тут же удалить
        try
        {
            DirectoryInfo dirInfo = new DirectoryInfo("C:\\Users\\Юрий\\Desktop\\TEST");
            if (!dirInfo.Exists) dirInfo.Create();
            dirInfo.CreateSubdirectory("NewFolder");

            DirectoryInfo dirForDel = new DirectoryInfo("C:\\Users\\Юрий\\Desktop\\test\\newfolder");
            //string trashPath = "C:\\Users\\Юрий\\$RECYCLE.BIN";
            string trashPath = "C:\\Users\\Юрий\\Desktop\\арх\\newfolder";

            dirForDel.MoveTo(trashPath);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }

    }


    static void GetCatalogs()
    {
        string dirName = "C:\\"; // Прописываем путь к корневой директории MacOS (для Windows скорее всего тут будет "C:\\")
        Console.WriteLine(dirName);
        //Console.ReadKey();
        
        if (Directory.Exists(dirName)) // Проверим, что директория существует
        {
            Console.WriteLine("Папки:");
            string[] dirs = Directory.GetDirectories(dirName);  // Получим все директории корневого каталога

            foreach (string d in dirs) // Выведем их все
                Console.WriteLine(d);

            Console.WriteLine();
            Console.WriteLine("Файлы:");
            string[] files = Directory.GetFiles(dirName);// Получим все файлы корневого каталога

            foreach (string s in files)   // Выведем их все
                Console.WriteLine(s);
        }
    }


    public class Drive
    {
        private string name;
        public Drive(string name, long totalSpace, long freeSpace)
        {   Name = name;
            TotalSpace = totalSpace;
            FreeSpace = freeSpace;        }

        public string Name 
        { get { return name; } 
          set { this.name = value; }        }
        public long TotalSpace { get; }
        public long FreeSpace { get; }
    }

    public class Folder
    {
        public List<string> Files { get; set; } = new List<string>();
    }

}
