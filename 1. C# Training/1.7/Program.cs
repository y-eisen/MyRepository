// See https://aka.ms/new-console-template for more information
public class mainClass
{ 
    public static void Main(string[] args)
    {
        Console.WriteLine("Hello");

        
        MainSpace.UPD upd1 = new MainSpace.UPD();

        //upd1.Date = 111;

        Console.WriteLine(upd1.Date);

        Console.ReadKey();
    }

    


}

namespace MainSpace
{
     class UPD
    {
        private DateTime date;
        private string numb;
        private string contract;

        public DateTime Date
        {get
            {return date;}
         set
            {date = value;}}

        public UPD()
            {
            date = DateTime.Today;
            numb="001";
            contract= date.ToString();
            }

    }
}
