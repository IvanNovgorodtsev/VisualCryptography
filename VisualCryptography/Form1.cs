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

        private Bitmap original;
        private Bitmap output1;
        private Bitmap output2;
        private List<List<Color>> patterns = new List<List<Color>>
        {
            new List<Color> { Color.White, Color.White, Color.Black, Color.Black }, // 1,1,0,0 // 0
            new List<Color> { Color.White, Color.Black, Color.White, Color.Black }, // 1,0,1,0 // 1
            new List<Color> { Color.White, Color.Black, Color.Black, Color.White }, // 1,0,0,1 // 2
            new List<Color> { Color.Black, Color.White, Color.White, Color.Black }, // 0,1,1,0 // 3
            new List<Color> { Color.Black, Color.White, Color.Black, Color.White }, // 0,1,0,1 // 4
            new List<Color> { Color.Black, Color.Black, Color.White, Color.White }, // 0,0,1,1 // 5
        };
        private static int[] choice= new int[4];
        private static int choice2 = new int();
        Random random = new Random();

        private void button1_Click(object sender, EventArgs e)
        {

            OpenFileDialog openFileDialog1 = new OpenFileDialog
            {
                InitialDirectory = @"C:",
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
                for (int i = 0; i < original.Width; i++)
                for (int j = 0; j < original.Height; j++)
                    {
                        color = original.GetPixel(i, j);
                        for (int x = 0; x < 4; x++) choice[x] = random.Next(0, 5);

                        output1.SetPixel(i * 2, j * 2, patterns[choice[0]][0]);
                        output1.SetPixel(i * 2 + 1, j * 2, patterns[choice[1]][1]);
                        output1.SetPixel(i * 2, j * 2 + 1, patterns[choice[2]][2]);
                        output1.SetPixel(i * 2 + 1, j * 2 + 1, patterns[choice[3]][3]);

                        if (color.GetBrightness()!=0) // If it's black
                        {
                            output2.SetPixel(i * 2, j * 2, patterns[Math.Abs(choice[0] - 5)][0]);
                            output2.SetPixel(i * 2 + 1, j * 2, patterns[Math.Abs(choice[1] - 5)][1]);
                            output2.SetPixel(i * 2, j * 2 + 1, patterns[Math.Abs(choice[2] - 5)][2]);
                            output2.SetPixel(i * 2 + 1, j * 2 + 1, patterns[Math.Abs(choice[3] - 5)][3]);
                        }
                        else
                        {
                            output2.SetPixel(i * 2, j * 2, patterns[choice[0]][0]);
                            output2.SetPixel(i * 2 + 1, j * 2, patterns[choice[1]][1]);
                            output2.SetPixel(i * 2, j * 2 + 1, patterns[choice[2]][2]);
                            output2.SetPixel(i * 2 + 1, j * 2 + 1, patterns[choice[3]][3]);
                        }                       
                    }
            }
            output1.Save("output1.png");
            output2.Save("output2.png");
        }
    }
}
