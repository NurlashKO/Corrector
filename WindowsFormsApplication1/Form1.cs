using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Controls;
using System.Windows.Documents;

namespace WindowsFormsApplication1
{
    public partial class Main : Form
    {
        public static Dictionary dictionary = new Dictionary();
        bool disable = false, check = true, getList = true;
        public Main()
        {
            InitializeComponent();
            listBox1.Items.Clear();
            dictionary.load("words.txt");
            dictionary.process();
        }

        public void loadItem(string word)
        {
            
            listBox1.Items.Clear();
            txb.Clear();
            //hgWord(rtb.SelectionStart);
            
            if (word == "" || dictionary.isAvailable(word) == true)
                return;
            List<string> identical = new List<string>();
            identical = dictionary.findIdentical(word);
            foreach (string s in identical)
                listBox1.Items.Add(s);
            listBox1.SelectedItem = listBox1.Items[0];
        }

        public void hgWord(int pos)
        {
            disable = true;
            string buf = rtb.Text;
            string word = getWordAtPos(pos);
            System.Drawing.Color color;
            if (word == "" || dictionary.isAvailable(word) == true)
                color = System.Drawing.Color.Black;
            else
                color = System.Drawing.Color.Red;

            int Left = pos - 1, Right = pos;
            while (Left >= 0 && Char.IsLetter(buf[Left]))
            {
                rtb.SelectionStart = Left;
                rtb.SelectionLength = 1;
                rtb.SelectionColor = color;
                Left--;
            }

            while (Right < buf.Length && Char.IsLetter(buf[Right]))
            {
                rtb.SelectionStart = Right;
                rtb.SelectionLength = 1;
                rtb.SelectionColor = color;
                Right++;
            }
            rtb.SelectionStart = pos;
            rtb.SelectionLength = 0;
            disable = false;
        }

        public string getWordAtPos(int pos)
        {
            string res = "", buf = rtb.Text;
            if (pos < 0 || pos > rtb.Text.Length)
                return "";
            int Left = pos - 1, Right = pos;
            while (Left >= 0 && Char.IsLetter(buf[Left]))
            {
                res = buf[Left] + res;
                Left--;
            }
            while (Right < buf.Length && Char.IsLetter(buf[Right]))
            {
                res += buf[Right];
                Right++;
            }
            res = res.ToLower();

            return res;
        }

        private void rtb_SelectionChanged(object sender, EventArgs e)
        {
            if (disable)
                return;
            loadItem(getWordAtPos(rtb.SelectionStart));
            /*int pos = rtb.SelectionStart;
            disable = true;
            for (int i = 0; i < rtb.Text.Length; i++)
            {
                if (Char.IsLetter(rtb.Text[i]) == false)
                {
                    rtb.SelectionStart = i;
                    rtb.SelectionLength = 1;
                    rtb.SelectionColor = System.Drawing.Color.Black;     
                }
            }
            hgWord(Math.Max(pos - 1, 0));
            hgWord(Math.Max(pos - 2, 0));
            disable = true;
            rtb.SelectionStart = pos;
            disable = false;
             */
        }

        private void btn1_Click(object sender, EventArgs e)
        {
            this.Close();   
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            txb.Text = listBox1.SelectedItem.ToString();
        }

        private void btn2_Click(object sender, EventArgs e)
        {
            string word = getWordAtPos(rtb.SelectionStart);
            if (word == "" || rtb.TextLength == 0)
                return;
            rtb.Text = rtb.Text.Replace(word, txb.Text);
            //for (int i = 0; i < rtb.Text.Length; i++)
            //    hgWord(i);
        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            btn2_Click(this, EventArgs.Empty);
        }
    }
}
    