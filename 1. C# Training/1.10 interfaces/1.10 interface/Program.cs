using System;


class mainClass
{
    public static void Main(string[] args)
    {
        Console.WriteLine("new func");

        IMessenger<Phone> v = new Viber<Phone>();
        Phone ph = v.DeviceInfo();
       
        Console.WriteLine(ph.s);

        IMessenger<IPhone> v1 = new Viber<IPhone>();
        Phone ph1 = v1.DeviceInfo();

        Console.WriteLine(ph1.s);

        Console.ReadKey();
    }

    public class Viber <T> : IMessenger<T> where T:Phone, new()
    {
        public T DeviceInfo()
        {
            T device = new T();
            Console.WriteLine(device);
            return new T();
        }

    }

    public interface IMessenger<out T>
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

    public class IPhone:Phone
    {
        public string s { get; set; }
        public IPhone()
        {
            s = "Iphone string";
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







}
