using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace projekt54
{
    public partial class Form1 : Form
    {
        private char operacja = 'a';

        public Form1()
        {
            InitializeComponent();

            radioButton1.CheckedChanged += new EventHandler(radioButton1_CheckedChanged);

            radioButton3.CheckedChanged += new EventHandler(radioButton_CheckedChanged);
            radioButton4.CheckedChanged += new EventHandler(radioButton_CheckedChanged);
            radioButton5.CheckedChanged += new EventHandler(radioButton_CheckedChanged);
            radioButton6.CheckedChanged += new EventHandler(radioButton_CheckedChanged);
            radioButton7.CheckedChanged += new EventHandler(radioButton_CheckedChanged);
            radioButton8.CheckedChanged += new EventHandler(radioButton_CheckedChanged);

            radioButton9.CheckedChanged += new EventHandler(radioButton9_CheckedChanged);

            textBox1.TextChanged += new EventHandler(textBox_TextChanged);
            textBox2.TextChanged += new EventHandler(textBox_TextChanged);

            textBox1.KeyPress += new KeyPressEventHandler(textBox_KeyPress);
            textBox2.KeyPress += new KeyPressEventHandler(textBox_KeyPress);

            textBox1.Leave += new EventHandler(textBox_Leave);
            textBox2.Leave += new EventHandler(textBox_Leave);

            ToolTip toolTip3 = new ToolTip();
            ToolTip toolTip4 = new ToolTip();
            ToolTip toolTip5 = new ToolTip();
            ToolTip toolTip6 = new ToolTip();
            ToolTip toolTip7 = new ToolTip();
            ToolTip toolTip8 = new ToolTip();

            toolTip3.SetToolTip(radioButton3, "C = A & B");
            toolTip4.SetToolTip(radioButton4, "C = A | B");
            toolTip5.SetToolTip(radioButton5, "C = A ^ B");
            toolTip6.SetToolTip(radioButton6, "C = ~A\nD = ~B");
            toolTip7.SetToolTip(radioButton7, "C = A << B");
            toolTip8.SetToolTip(radioButton8, "C = A >> B");
        }

        private void FormatujDane()
        {
            if (textBox1.Text.Length != textBox2.Text.Length)
            {
                textBox1.Text = textBox1.Text.TrimStart('0');
                textBox2.Text = textBox2.Text.TrimStart('0');

                if (textBox1.Text.Length > textBox2.Text.Length)
                {
                    textBox2.Text = textBox2.Text.PadLeft(textBox1.Text.Length, '0');
                }
                else if (textBox1.Text.Length < textBox2.Text.Length)
                {
                    textBox1.Text = textBox1.Text.PadLeft(textBox2.Text.Length, '0');
                }
            }
        }

        private void textBox_Leave(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
            {
                FormatujDane();
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            textBox1.TextChanged -= new EventHandler(textBox_TextChanged);
            textBox2.TextChanged -= new EventHandler(textBox_TextChanged);

            if (radioButton1.Checked)
            {
                try
                {
                    textBox1.Text = Convert.ToInt64(textBox1.Text, 2).ToString();
                    textBox2.Text = Convert.ToInt64(textBox2.Text, 2).ToString();
                }
                catch
                {

                }
            }
            else
            {
                try
                {
                    textBox1.Text = Convert.ToString(long.Parse(textBox1.Text), 2);
                    textBox2.Text = Convert.ToString(long.Parse(textBox2.Text), 2);
                    FormatujDane();
                }
                catch
                {

                }
            }

            textBox1.TextChanged += new EventHandler(textBox_TextChanged);
            textBox2.TextChanged += new EventHandler(textBox_TextChanged);
        }

        private void radioButton9_CheckedChanged(object sender, EventArgs e)
        {
            WyswietlWyniki();
        }

        private void WyswietlWyniki()
        {
            long l1 = 0, l2 = 0, w1 = 0, w2 = 0;
            ushort dlugosc = 0;
            try
            {
                if (radioButton1.Checked)
                {
                    if (long.Parse(textBox1.Text) > long.Parse(textBox2.Text) || operacja == '<' || operacja == '>')
                    {
                        dlugosc = (ushort)Convert.ToString(long.Parse(textBox1.Text), 2).Length;
                    }
                    else
                    {
                        dlugosc = (ushort)Convert.ToString(long.Parse(textBox2.Text), 2).Length;
                    }

                    l1 = long.Parse(textBox1.Text);
                    l2 = long.Parse(textBox2.Text);
                }
                else
                {
                    if (textBox1.Text.Length > textBox2.Text.Length || operacja == '<' || operacja == '>')
                    {
                        dlugosc = (ushort)textBox1.Text.Length;
                    }
                    else
                    {
                        dlugosc = (ushort)textBox2.Text.Length;
                    }

                    l1 = Convert.ToInt64(textBox1.Text, 2);
                    l2 = Convert.ToInt64(textBox2.Text, 2);
                }
            }
            catch
            {
                if (richTextBox1.Text.Length != 0)
                {
                    richTextBox1.Clear();
                }
                if (richTextBox2.Text.Length != 0)
                {
                    richTextBox2.Clear();
                }
                return;
            }

            if (richTextBox1.Text.Length != 0)
            {
                richTextBox1.SelectionStart = 0;
                richTextBox1.SelectionLength = 64;
                richTextBox1.SelectionColor = SystemColors.WindowText;
            }

            if (operacja != 'n' && richTextBox2.Text.Length != 0)
            {
                richTextBox2.Clear();

                richTextBox2.SelectionStart = 0;
                richTextBox2.SelectionLength = 64;
                richTextBox2.SelectionColor = SystemColors.WindowText;
            }

            if (operacja == 'a')
            {
                w1 = l1 & l2;
            }
            else if (operacja == 'o')
            {
                w1 = l1 | l2;
            }
            else if (operacja == 'x')
            {
                w1 = l1 ^ l2;
            }
            else if (operacja == 'n')
            {
                w1 = ~l1;
                w2 = ~l2;
            }
            else if (operacja == '<')
            {
                w1 = (long)l1 << (int)l2;
            }
            else
            {
                w1 = (long)l1 >> (int)l2;
            }

            if (radioButton9.Checked)
            {
                richTextBox1.Text = w1.ToString();
                if (operacja == 'n')
                {
                    richTextBox2.Text = w2.ToString();
                }
            }
            else
            {
                richTextBox1.Text = Convert.ToString(w1, 2).PadLeft(64, '0');
                if (operacja == 'n')
                {
                    richTextBox2.Text = Convert.ToString(w2, 2).PadLeft(64, '0');
                }

                if (radioButton3.Checked || radioButton4.Checked || radioButton5.Checked || radioButton6.Checked)
                {
                    richTextBox1.SelectionStart = 64 - dlugosc;
                    richTextBox1.SelectionLength = dlugosc;
                    richTextBox1.SelectionColor = Color.Red;
                    if (operacja == 'n')
                    {
                        richTextBox2.SelectionStart = 64 - dlugosc;
                        richTextBox2.SelectionLength = dlugosc;
                        richTextBox2.SelectionColor = Color.Red;
                    }
                }
                else if (radioButton7.Checked || radioButton8.Checked)
                {
                    try
                    {
                        if (radioButton1.Checked)
                        {
                            richTextBox1.SelectionStart = richTextBox1.Text.IndexOf(Convert.ToString(long.Parse(textBox1.Text), 2));
                        }
                        else
                        {
                            richTextBox1.SelectionStart = richTextBox1.Text.IndexOf(textBox1.Text);
                        }
                        richTextBox1.SelectionLength = dlugosc;
                        richTextBox1.SelectionColor = Color.Red;
                    }
                    catch
                    {

                    }
                }
            }
        }

        private void radioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton3.Checked)
            {
                operacja = 'a';
            }
            else if (radioButton4.Checked)
            {
                operacja = 'o';
            }
            else if (radioButton5.Checked)
            {
                operacja = 'x';
            }
            else if (radioButton6.Checked)
            {
                operacja = 'n';
            }
            else if (radioButton7.Checked)
            {
                operacja = '<';
            }
            else
            {
                operacja = '>';
            }

            WyswietlWyniki();
        }

        private void textBox_TextChanged(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            if (radioButton1.Checked && textBox.Text.Length >= 19)
            {
                try
                {
                    long.Parse(textBox.Text);
                }
                catch
                {
                    if (textBox.Text.StartsWith("-"))
                    {
                        textBox.Text = "-9223372036854775808";
                    }
                    else
                    {
                        textBox.Text = "9223372036854775807";
                    }
                    SendKeys.Send("{END}");
                }
            }

            WyswietlWyniki();
        }

        private bool SprawdzWprowadzaneDane(TextBox textBox, char e)
        {
            if (radioButton1.Checked)
            {
                if (textBox.SelectionStart == 0 && e == 45)
                {
                    return true;
                }
                else if (char.IsNumber(e))
                {
                    return true;
                }
                else if (textBox.Text.Length != 0 && e == 8)
                {
                    return true;
                }
            }
            else
            {
                if (e == 48 || e == 49)
                {
                    return true;
                }
                else if (textBox.Text.Length != 0 && e == 8)
                {
                    return true;
                }
            }
            return false;
        }

        private void textBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
            if (SprawdzWprowadzaneDane((TextBox)sender, e.KeyChar))
            {
                e.Handled = false;
            }
        }
    }
}