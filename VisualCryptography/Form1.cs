using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VisualCryptography
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        Bitmap original;
        Bitmap output1;
        Bitmap output2;

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog
            {
                InitialDirectory = @"C:\Users\inovgorodtsev\Desktop\Przechwytywanie.png",
                Title = "Browse Image Files",
                CheckFileExists = true,
                CheckPathExists = true,
                RestoreDirectory = true,
            };

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = openFileDialog1.FileName;
                original = new Bitmap(textBox1.Text);
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            output1 = new Bitmap(original.Width * 2, original.Height * 2);
            output2 = new Bitmap(original.Width * 2, original.Height * 2);
            if (!string.IsNullOrWhiteSpace(textBox1.Text))
            {
                Color color;
                int p = 0;
                for (int i = 0; i < original.Width; i++)
                    for (int j = 0; j < original.Height; j++)
                    {
                        color = original.GetPixel(i, j);
                        if (color.GetBrightness() < 0.01) // If it's black
                        {
                            p++;
                        }
                        if (color.GetBrightness() > 0.99) // If it's white
                        {
                            p++;
                        }
                    }
                MessageBox.Show(p.ToString());
            }
        }
    }
}
