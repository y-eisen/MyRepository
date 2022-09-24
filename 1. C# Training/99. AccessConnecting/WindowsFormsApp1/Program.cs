using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace WindowsFormsApp1
{

    internal class Program
    {
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]

        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }

    /*public class ConnectionClass
    {
        //public static string connectString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=D:\\Program\\Repositories\\MyRepository\\1. C# Training\\99. AccessConnecting\\f-main.accdb;";
        public static string connectString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\\Program\\Repositories\\MyRepository\\1. C# Training\\99. AccessConnecting\\f-main.accdb;Persist Security Info=False;";
        // вариант 2
        //public static string connectString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=Workers.mdb;";
    }
    */
}


public class MsAccessWorking
{
    private OleDbConnection BaseConnection;
    private OleDbCommand Command;
    private OleDbDataReader Reader;

    private static string ConnectionString(string fn)
    {
        string Result = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=[dataFileName];Persist Security Info=False;";
        if (fn == "") throw new ArgumentNullException("Пустое имя файла");
        if (!File.Exists(fn)) throw new FileNotFoundException("База данных не найдена");
        Result = Result.Replace("[dataFileName]", fn);
        return Result;
    }

    public MsAccessWorking(string fn)
    { BaseConnection = new OleDbConnection(ConnectionString(fn)); }
    public void CloseConnection()
    { BaseConnection.Close(); }

    public List<string[]> getDateFromBase(string sql)
    {
        if (sql == "") throw new ArgumentNullException("Пустая инструкция");

        try
        {
            // создаем объект OleDbCommand для выполнения запроса к БД MS Access
            Command = new OleDbCommand(sql, BaseConnection);
            // получаем объект OleDbDataReader для чтения табличного результата запроса SELECT
            Reader = Command.ExecuteReader();
        }
        catch (Exception ex)
        { throw; }

        int fldAm = Reader.FieldCount;

        var Res = new List<string[]>();

        return Res;
    }

}