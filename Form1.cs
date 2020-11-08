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
        Scoreboard TheScoreboard = new Scoreboard();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (System.IO.File.Exists("scoreboard.json"))
            {
                TheScoreboard = Scoreboard.FromFile("scoreboard.json");
            }
            else
            {
                TheScoreboard.Save("scoreboard.json");
            }
            foreach(KeyValuePair<DateTime, int> pair in TheScoreboard.Scores)
            {
                listBox1.Items.Add(pair.Key + " - " + "Failed! Combo: " + pair.Value);
            }
            if (TheScoreboard.TopScore.Key == null)
            {
                
            }
            else
            {
                label2.Text = "Top Score: " + TheScoreboard.TopScore.Value + " - " + DateTime.Now.Subtract(TheScoreboard.TopScore.Key).ToString() + " ago";
            }
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
                    DateTime now = DateTime.Now;
                    listBox1.Items.Add(now + " - " + "Failed! Combo: " + TotalChars);
                    TheScoreboard.Scores.Add(now, TotalChars);
                    if (TotalChars > TheScoreboard.TopScore.Value)
                    {
                        TheScoreboard.TopScore = new KeyValuePair<DateTime, int>(now, TotalChars);
                    }
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
                    DateTime now = DateTime.Now;
                    listBox1.Items.Add(now + " - " + "Failed! Combo: " + TotalChars);
                    TheScoreboard.Scores.Add(now, TotalChars);
                    if (TotalChars > TheScoreboard.TopScore.Value)
                    {
                        TheScoreboard.TopScore = new KeyValuePair<DateTime, int>(now, TotalChars);
                    }
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
            label2.Text = "Top Score: " + TheScoreboard.TopScore.Value + " - " + DateTime.Now.Subtract(TheScoreboard.TopScore.Key).ToString() + " ago";
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

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            TheScoreboard.Save("scoreboard.json");
        }
    }
}
