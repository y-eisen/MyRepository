using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace collections
{
    public partial class MainForm : Form
    {

        ArrayTraining AT;

        public MainForm()
        {
            InitializeComponent();
            AT = new ArrayTraining();
            AT.AL.Add(150);
            AT.AL.Add("sdf");
            AT.AL.Add(true);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            AT.intArray[0] = AT.intArray[0]+1;
            AT.AL[0] = (int)AT.AL[0] + 1;
            textBox1.Text = AT.AL[AT.intArray[0] % 3].ToString();

            AT.s.Add("q");
            AT.s.Add("w");
            AT.s.Add("e");
            AT.s.Add("r");
            AT.s.Add("t");

            AT.s.Sort((x, y) => string.Compare(x, y, StringComparison.Ordinal));

            string res="";
            foreach (string el in AT.s)
            {
                res = res + el;
            }

            textBox1.Text = res;

            // Сохраняем предложение в строку
            var sentence =
                "Подсчитайте, сколько уникальных символов в этом предложении, используя HashSet<T>, учитывая знаки препинания, но не учитывая пробелы в начале и в конце предложения.";

            // сохраняем в массив char
            var characters = sentence.ToCharArray();

            var symbols = new HashSet<char>();

            // добавляем во множество. Сохраняются только неповторяющиеся символы
            foreach (var symbol in characters)
                symbols.Add(symbol);

            // Выводим результат
            textBox1.Text = symbols.Count.ToString();
        }

        private void MainFormLoad(object sender, EventArgs e)
        {

        }

        public class ArrayTraining
        {
            public int[] intArray;
            public ArrayList AL;
            public List<string> s;

            public  ArrayTraining()
            {
                intArray = new int[10];
                AL = new ArrayList();
                s = new List<string>();
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
