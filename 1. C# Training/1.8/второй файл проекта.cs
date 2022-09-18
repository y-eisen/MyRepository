using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace training_1_8
{
    public static class DirectoryExtension
    {
        public static long DirSize(DirectoryInfo d)
        {
            long size = 0;

            FileInfo[] fs = d.GetFiles();
            foreach (var f in fs)
            { size += f.Length; }

            DirectoryInfo[] ds = d.GetDirectories();
            foreach (var dd in ds)
            { size += DirSize(dd); }

            return size;
        }
    }

    public static class experiment
    {
        //const string SettingsFileName = "Settings.cfg";

        public static void WriteValues(string SettingsFileName)
        {
            // Создаем объект BinaryWriter и указываем, куда будет направлен поток данных
            using (BinaryWriter writer = new BinaryWriter(File.Open(SettingsFileName, FileMode.Create)))
            {
                // записываем данные в разном формате
                writer.Write(20.666F);
                writer.Write(@"Тестовая строка");
                writer.Write(55);
                writer.Write(false);
            }

        }

        public static void ReadValues(string SettingsFileName)
        {
            float FloatValue;
            string StringValue;
            int IntValue;
            bool BooleanValue;

            if (File.Exists(SettingsFileName))
            {
                // Создаем объект BinaryReader и инициализируем его возвратом метода File.Open.
                using (BinaryReader reader = new BinaryReader(File.Open(SettingsFileName, FileMode.Open)))
                {
                    // Применяем специализированные методы Read для считывания соответствующего типа данных.
                    FloatValue = reader.ReadSingle();
                    StringValue = reader.ReadString();
                    IntValue = reader.ReadInt32();
                    BooleanValue = reader.ReadBoolean();
                }

                Console.WriteLine("Из файла считано:");

                Console.WriteLine("Дробь: " + FloatValue);
                Console.WriteLine("Строка: " + StringValue);
                Console.WriteLine("Целое: " + IntValue);
                Console.WriteLine("Булево значение " + BooleanValue);
            }

        }

    }
}
