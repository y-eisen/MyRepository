using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace WindowsFormsApp1
{
    public partial class MainForm : Form
    {
       // private OleDbConnection MyConnection;
        private MsAccessWorking CurrentDB = null;

        public MainForm()
        {
            InitializeComponent();

            // создаем экземпляр класса OleDbConnection
            /*MyConnection = new OleDbConnection(ConnectionClass.connectString);

            // открываем соединение с БД
            try
            {
                MyConnection.Open();
            }
            catch (Exception ex)
            {
                textBox1.Text = ex.Message;
            }
            */
                      

            try
            {
                CurrentDB = new MsAccessWorking("D:\\Program\\Repositories\\MyRepository\\1. C# Training\\99. AccessConnecting\\f-main.accdb");
            }
            catch (Exception ex)
            { }



        }

        private void button1_Click(object sender, EventArgs e)
        {
            /*string query = "SELECT fieldb FROM table1";

            // создаем объект OleDbCommand для выполнения запроса к БД MS Access
            OleDbCommand command = new OleDbCommand(query, MyConnection);

            // выполняем запрос и выводим результат в textBox1
            var l = command.ExecuteScalar();

            textBox1.Text = l.ToString();
            */
        }


        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            //MyConnection.Close();
            CurrentDB.CloseConnection();
        }

        private void button2_Click(object sender, EventArgs e)
        {/*
            string sql = "select fieldS,fieldL,fieldDt,fieldB,fieldDbl from table1";
            sql = "select номер_тн, номер_гд, дата_тн, статус, сумма_тн from _портфель";

            // создаем объект OleDbCommand для выполнения запроса к БД MS Access
            OleDbCommand command = new OleDbCommand(sql, MyConnection);
            // получаем объект OleDbDataReader для чтения табличного результата запроса SELECT
            OleDbDataReader reader = command.ExecuteReader();

            textBox3.Text = reader.FieldCount.ToString();
            textBox4.Text = reader.Depth.ToString();

            // очищаем listBox1
            listBox1.Items.Clear();
            int counter = 0;

            // в цикле построчно читаем ответ от БД
            while (reader.Read())
            {
                // выводим данные столбцов текущей строки в listBox1
                //listBox1.Items.Add(reader[0].ToString() + " " + reader[1].ToString() + " " + reader[2].ToString() + " " + reader[3].ToString() + " " + reader[4].ToString() + " ");
                if (counter < 1)
                {
                    listBox1.Items.Add(reader.GetFieldType(0) + "   " + reader.GetFieldType(1) + "   " + reader.GetFieldType(2) + "   " + reader.GetFieldType(3) + "   " + reader.GetFieldType(4) + " ");
                    listBox1.Items.Add(reader.GetName(0) + "   " + reader.GetName(1) + "   " + reader.GetName(2) + "   " + reader.GetName(3) + "   " + reader.GetName(4) + " ");
                }
                counter += 1;
            }
            int counter2 = 0;
            while (reader.Read())
            {
                counter2 += 1;
            }

            textBox1.Text = "ok";
            textBox2.Text = counter.ToString();

            // закрываем OleDbDataReader
            reader.Close();
            */
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }
    }
}
