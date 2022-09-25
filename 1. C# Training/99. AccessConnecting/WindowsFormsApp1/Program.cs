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


}


public class MsAccessWorking
{
    private OleDbConnection BaseConnection;
    private OleDbDataReader Reader;

    private string ConnectionString(string fn)
    {
        string Result = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=[dataFileName];Persist Security Info=False;";
        if (fn == "") throw new ArgumentNullException("Пустое имя файла");
        if (!File.Exists(fn)) throw new FileNotFoundException("База данных не найдена");
        Result = Result.Replace("[dataFileName]", fn);
        return Result;
    }

    public MsAccessWorking(string fn)
    {
        try
        {
            BaseConnection = new OleDbConnection(ConnectionString(fn));
            BaseConnection.Open();
        }
        catch (Exception)
        { throw; }
    }

    public void CloseConnection()
    { BaseConnection.Close(); }

    public List<string[]> getDataFromBase(string sql)  //загрузка не глядя на форматы
    {
        if (sql == "") throw new ArgumentNullException("Пустая инструкция");

        try
        {
            // создаем объект OleDbCommand для выполнения запроса к БД MS Access
            OleDbCommand Command = new OleDbCommand(sql, BaseConnection);
            // получаем объект OleDbDataReader для чтения табличного результата запроса SELECT
            Reader = Command.ExecuteReader();
        }
        catch (Exception)
        { throw; }

        int fldAm = Reader.FieldCount;

        var Res = new List<string[]>();

        while (Reader.Read())
        {
            Res.Add(new string[fldAm]);
            for (int j = 0; j < fldAm; j++)
            {
                Res[Res.Count - 1][j] = Reader[j].ToString();
            }
        }


        Reader.Close();
        return Res;
    }

    public List<int[]> getIntDataFromBase(string sql)  //загрузка числовых полей
    {
        if (sql == "") throw new ArgumentNullException("Пустая инструкция");

        try
        {
            // создаем объект OleDbCommand для выполнения запроса к БД MS Access
            OleDbCommand Command = new OleDbCommand(sql, BaseConnection);
            // получаем объект OleDbDataReader для чтения табличного результата запроса SELECT
            Reader = Command.ExecuteReader();
        }
        catch (Exception)
        { throw; }

        int fldAm = Reader.FieldCount;

        var Res = new List<int[]>();

        while (Reader.Read())
        {
            Res.Add(new int[fldAm]);
            for (int j = 0; j < fldAm; j++)
            {
                Res[Res.Count - 1][j] = Reader.GetInt32(j);
            }
        }

        Reader.Close();
        return Res;
    }

    public int updateData(string sqlLine)
    {
        int Res = 0;
        if (sqlLine == "") throw new ArgumentNullException("Пустая инструкция");
        try
        { Res = ExecuteCommand(sqlLine); }
        catch 
        { throw; }
        return Res;
    }

    private int ExecuteCommand(string sqlLine)
    {
        int Res = 0;
        try
        {
            OleDbCommand command = new OleDbCommand(sqlLine, BaseConnection);
            // выполняем запрос к MS Access
            Res = command.ExecuteNonQuery();
        }
        catch (Exception ex)
        { throw ex; }

        return Res;
    }
}