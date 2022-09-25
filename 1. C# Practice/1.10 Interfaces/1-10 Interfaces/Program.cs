public class Program
{
    public static void Main(string[] args)
    {
       
        Calc1 C = new Calc1();

        double d1 = 0;
        double d2 = 0;
        byte n = 1;

        while (true)
        {
            Console.WriteLine("\n\nВведите два числа которые нужно сложить.\n");
            n = 1;
            while (n < 3)
            {
                try
                {
                    d2 = ((Calculator)C).getNumber(n);
                    if (n == 1) d1 = d2;
                    n += 1;
                }
                catch (Exception ex)
                { Console.WriteLine(ex.Message + "\n" + ex.StackTrace); }

                if (n == 3)
                {
                    ((Calculator)C).ToSum(d1, d2);

                    C.showResult();
                }
            }
        
        }

    }

    public class Calc1 : Calculator
    {
        private double sum;

        public Calc1()
        {
            sum = 0; 
        }

        void Calculator.ToSum(double d1, double d2)
        {
            sum = d1 + d2;
            //return sum;
        }
        double Calculator.getNumber(byte n)
        {
            double res=0;
            Console.Write("Введите {0}-ое число: ",n);
            string s = Console.ReadLine();
            try
            { res = double.Parse(s); }
            catch (Exception)
            { throw; }
            return res;
        }

        public void showResult()
        {
            Console.WriteLine("Результат сложения: {0}", sum);
        }

    }


    public interface Calculator
    {
        public void ToSum(double d1, double d2);
        
        public double getNumber(byte n);

        public void showResult();

    }

}
