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


            string fn = "D:\\Program\\Repositories\\MyRepository\\1. C# Training\\99. AccessConnecting\\test.accdb";

            try
            {
                CurrentDB = new MsAccessWorking(fn);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

           


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
        {
            string sqlLine = textBox1.Text;
            try
            {  CurrentDB.updateData(sqlLine); }
            catch (Exception ex)
            {  MessageBox.Show(ex.StackTrace.ToString());}
        }

        private void button3_Click(object sender, EventArgs e)
        {
            List<int[]> v;
            List<string[]> v1;

            try
            {   
                string sql = "select [0].уин, [1].имя, [1].пол, [2].название, [0].количество, [2].цена, [0].дата_заказа " +
                    "from ([0] left outer join [1] on [0].клиент=[1].уин) left outer join [2] on [0].товар=[2].уин";
                sql = sql.Replace("[0]", "[заказы]").Replace("[1]", "[клиенты]").Replace("[2]", "[товары]");
                v1 = CurrentDB.getDataFromBase(sql);

                sql = "select [0].уин, [0].клиент, [0].товар " +
                    "from ([0] left outer join [1] on [0].клиент=[1].уин) left outer join [2] on [0].товар=[2].уин";
                sql = sql.Replace("[0]", "[заказы]").Replace("[1]", "[клиенты]").Replace("[2]", "[товары]");
                v=CurrentDB.getIntDataFromBase(sql);
                //MessageBox.Show(v[0].Count().ToString());


                string newS;
                for (int i = 0; i < v1.Count(); i++)
                {
                    newS = "";
                    for (int j = 0; j < v1[0].Count(); j++)
                    {
                        newS = newS + v1[i][j].ToString() + "     ";
                    }
                    listBox1.Items.Add(newS);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
