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

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog
            {
                InitialDirectory = @"A:",
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
            original = new Bitmap(900,900);
            for(int i=0;i<100;i++) for (int j = 0; j < 100; j++) original.SetPixel(i, j, Color.Black);
            for (int i = 100; i < 200; i++) for (int j = 100; j < 200; j++) original.SetPixel(i, j, Color.Black);
            original.SetPixel(2, 2, Color.Black);
            original.SetPixel(0, 1, Color.White);
            original.SetPixel(0, 2, Color.White);
            original.SetPixel(1, 0, Color.White);
            original.SetPixel(2, 0, Color.White);
            original.SetPixel(1, 2, Color.White);
            original.SetPixel(2, 1, Color.White);

            output1 = new Bitmap(original.Width * 2, original.Height * 2);
            output2 = new Bitmap(original.Width * 2, original.Height * 2);
            if (!string.IsNullOrWhiteSpace(textBox1.Text))
            {
                Color color;
                for (int i = 0; i < original.Width; i++)
                    for (int j = 0; j < original.Height; j++)
                    {
                        color = original.GetPixel(i, j);
                        Random random = new Random();
                        for (int z = 0; z < 4; z++) choice[z] = random.Next(1, 6);

                        output1.SetPixel(i * 2, j * 2, patterns[choice[0]][0]);
                        output1.SetPixel(i * 2 + 1, j * 2, patterns[choice[1]][1]);
                        output1.SetPixel(i * 2, j * 2 + 1, patterns[choice[2]][2]);
                        output1.SetPixel(i * 2 + 1, j * 2 + 1, patterns[choice[3]][3]);

                        if(color.Equals(Color.Black))
                        //if (color.GetBrightness() < 0.01) // If it's black
                        {
                            output2.SetPixel(i * 2, j * 2, patterns[Math.Abs(choice[0]-5)][0]);
                            output2.SetPixel(i * 2 + 1, j * 2, patterns[Math.Abs(choice[1]-5)][1]);
                            output2.SetPixel(i * 2, j * 2 + 1, patterns[Math.Abs(choice[2]-5)][2]);
                            output2.SetPixel(i * 2 + 1, j * 2 + 1, patterns[Math.Abs(choice[3]-5)][3]);
                        }
                        else
                        //else if (color.GetBrightness() > 0.99) // If it's white
                        {
                            output2.SetPixel(i * 2, j * 2, patterns[choice[0]][0]);
                            output2.SetPixel(i * 2 + 1, j * 2, patterns[choice[1]][1]);
                            output2.SetPixel(i * 2, j * 2 + 1, patterns[choice[2]][2]);
                            output2.SetPixel(i * 2 + 1, j * 2 + 1, patterns[choice[3]][3]);
                        }
                    }
            }
            output1.Save("output1.jpg");
            output2.Save("output2.jpg");
        }
    }
}
