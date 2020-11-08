using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Osu_XZTraining
{
    public partial class Form1 : Form
    {
        bool IsNextX = true;
        int TotalChars = 0;
        bool enable = true;
        Stopwatch Watch = new Stopwatch();
        Stopwatch KeysWatch = new Stopwatch();
        long LastKeyMillis = 0;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (!enable) return;
            if (textBox1.Text == "") return;
            if (TotalChars == 0)
            {
                Watch.Start();
                KeysWatch.Start();
            }

            if (IsNextX)
            {
                if (textBox1.Text.ToLower().ToCharArray().Last() == 'x')
                {
                    KeysWatch.Stop();
                    LastKeyMillis = KeysWatch.ElapsedMilliseconds;
                    KeysWatch.Restart();
                }
                else
                {
                    MessageBox.Show("no\nTotal: " + TotalChars);
                    listBox1.Items.Add("Failed! Combo: " + TotalChars);
                    textBox1.Text = "";
                    TotalChars = 0;
                    IsNextX = true;
                    Watch.Stop();
                    Watch.Reset();
                    KeysWatch.Stop();
                    KeysWatch.Reset();
                    return;
                }
            }
            else
            {
                if (textBox1.Text.ToLower().ToCharArray().Last() == 'z')
                {
                    KeysWatch.Stop();
                    LastKeyMillis = KeysWatch.ElapsedMilliseconds;
                    KeysWatch.Restart();
                }
                else
                {
                    MessageBox.Show("no\nTotal: " + TotalChars);
                    listBox1.Items.Add("Failed! Combo: " + TotalChars);
                    textBox1.Text = "";
                    TotalChars = 0;
                    IsNextX = true;
                    Watch.Stop();
                    Watch.Reset();
                    KeysWatch.Stop();
                    KeysWatch.Reset();
                    return;
                }
            }
            TotalChars++;
            IsNextX = !IsNextX;
            if (textBox1.Text.Length > 20)
            {
                enable = false;
                textBox1.Text = "";
                enable = true;
            }

            UpdateStatistics();
        }

        private void UpdateStatistics()
        {
            label1.Text = $"Statistics:\nCombo: {TotalChars}\nTime between keys: {LastKeyMillis} ms\nKeys per second: {TotalChars / (Watch.ElapsedMilliseconds/ 1000f)}";
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            TotalChars = 0;
            IsNextX = true;
            Watch.Stop();
            Watch.Reset();
            KeysWatch.Stop();
            KeysWatch.Reset();
        }
    }
}
