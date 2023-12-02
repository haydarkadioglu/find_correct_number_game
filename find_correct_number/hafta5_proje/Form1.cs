using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace hafta5_proje
{
    public partial class Form1 : Form
    {

        Timer timer = new Timer();
        int seconds = 30;


        public Form1()
        {
            InitializeComponent();
            
            textBox1.PasswordChar = '*';
            textBox1.MaxLength = 9;
            button2.Enabled = false;
            numericUpDown1.Value = 5;
            numericUpDown2.Value = seconds;
            timer.Interval = 1000;
            timer.Tick += timer1_Tick;
            textBox1.KeyPress += textBox1_KeyPress;
            textBox2.KeyPress += textBox2_KeyPress;

        }

        private void down_num()
        {
            numericUpDown1.Value--;
            if (numericUpDown1.Value == 0)
            {
                timer.Stop();
                MessageBox.Show("Tahmin hakkınız dolmuştur.");
                clear_everything();
                set_again();


            }
        }
        private void clear_everything()
        {
            List<TextBox> listbox = new List<TextBox> { textBox3, textBox4, textBox5, textBox6, textBox7, textBox8, textBox9, textBox10, textBox11 };
            List<Label> listlabel1 = new List<Label> { label6, label7, label8, label9, label10, label11, label12, label13, label14 };
            List<Label> listlabel2 = new List<Label> { label15, label16, label17, label18, label19, label20, label21, label22, label23 };

            int len = textBox1.Text.Length;
            textBox1.Clear();
            textBox2.Clear();
            for (int i = 0; i < len; i++)
            {
                listbox[i].Clear();
                listlabel2[i].BackColor = Color.Transparent;
                deactivate_tools(listbox[i]);
                deactivate_tools(listlabel1[i]);

            }

            
        }
        private void set_again() {
            numericUpDown1.Value = 5;
            numericUpDown2.Value = 30;
        }

        private void deactivate_tools(dynamic temp)
        {
            temp.Visible = false;
            
        }

        private void activate_tools(dynamic temp) {
            temp.Visible = true;
        }

        
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }


        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; 
            }
        }
        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; 
            }
        }

        private bool control_num()
        {
            string text = textBox1.Text;

            for (int i = 0; i < text.Length - 1; i++)
            {
                for (int j = i + 1; j < text.Length; j++)
                {
                    if (text[i] == text[j])
                    {
                        return true;
                    }
                }
            }

            return false;
        }


        private void button1_Click(object sender, EventArgs e)
        {
            bool check_num = control_num();
            if (check_num)
            {
                MessageBox.Show("Aynı sayıyı iki kez yazamazsınız");
                clear_everything();
                return;
            }

            List<TextBox> listbox = new List<TextBox> { textBox3, textBox4, textBox5, textBox6, textBox7, textBox8, textBox9, textBox10, textBox11 };
            List<Label> listlabel1 = new List<Label> { label6, label7, label8, label9, label10, label11, label12,label13, label14 };
            List<Label> listlabel2 = new List<Label> { label15, label16, label17,label18, label19, label20, label21, label22, label23};
            int len = textBox1.Text.Length;
            textBox2.MaxLength = len;
            button2.Enabled = true;
            for (int i = 0; i<len; i++)
            {
                activate_tools(listbox[i]);
                activate_tools(listlabel1[i]);
                
            }
            label1.Text = $"Basamak Sayısı = {len}";
            timer.Start();
            seconds = (int)numericUpDown2.Value;
            numericUpDown1.Value = numericUpDown1.Value;

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            
            clear_everything();
            

        }




        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "") 
            {
                MessageBox.Show("kutuları boş bıraktınız");
                return;
            }

            int len = textBox1.Text.Length;

            List<TextBox> listbox = new List<TextBox> { textBox3, textBox4, textBox5, textBox6, textBox7, textBox8, textBox9, textBox10, textBox11 };
            List<Label> listlabel2 = new List<Label> { label15, label16, label17, label18, label19, label20, label21, label22, label23 };

            List<char> list = textBox1.Text.ToCharArray().ToList(); 
            List<char> list2 = textBox2.Text.ToCharArray().ToList();

            if (list.Count != list2.Count)
            {
                MessageBox.Show("Girdi uzunlukları eşleşmiyor.");
                return;
            }

            for (int i = 0; i < len; i++)
            {
                listbox[i].Text = list2[i].ToString();
                if (list[i] == list2[i])
                {
                    listlabel2[i].BackColor = Color.Blue;
                    
                }
                else if (list.Contains(list2[i]))
                {
                    listlabel2[i].BackColor = Color.Red;
                    
                }
                else
                {
                    listlabel2[i].BackColor = Color.White;
                    
                }
            }
            down_num();
            if (list.SequenceEqual(list2))
            {
                timer.Stop();
                MessageBox.Show("tebrikler doğru bildiniz");
                listBox1.Items.Add($"bilinen sayı :{textBox1.Text} ({textBox1.Text.Length*10} Puan)");
                
                string dosyaAdi = "dosya.txt";
                using (StreamWriter writer = new StreamWriter(dosyaAdi, true))
                {
                    writer.WriteLine($"bilinen sayı :{textBox1.Text} ({textBox1.Text.Length * 10} Puan)");
                }
                set_again();
                clear_everything();
            }

            


        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (seconds > 0)
            {
                seconds--;
                numericUpDown2.Value = seconds;
            }
            else
            {
                down_num();
                numericUpDown2.Value = seconds = 30;
                MessageBox.Show("1 hakkınız tamamlandı!");

            }
        }

        
    }
}
