using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _exceptions
{
    public class TestSolution
    {
        public static void GoProgramm(string[] args)
        {
            NameException NE = new NameException("Неверное имя");

            Console.WriteLine(NE.Message);


        }
    }
    public class NameException: ArgumentException
    {
        public NameException(string _exceptionMessage) : base(_exceptionMessage) 
        {
            HelpLink = "google.com";
            Data.Add("Дата исключения: ", DateTime.Now);
        }
    }




    
}

public class ExceptionHandler : ActionFilterAttribute, IExceptionFilter
{
    readonly Error _error = new Error();
    public void OnException(ExceptionContext context)
    {
        _error.Write(context.Exception.ToString());
        string errorMessage = "Произошла непредвиденная ошибка в приложении. Администрация сайта уже бежит на помощь.";
        if (context.Exception is HumanException) errorMessage = context.Exception.Message;
        context.Result = new BadRequestObjectResult(errorMessage);
    }
}
