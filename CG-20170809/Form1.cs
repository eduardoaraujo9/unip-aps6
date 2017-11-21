using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        Bitmap foto;
        int[] h;
        int[] h2;
        int[] h3;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
            openFileDialog1.Filter = "Fotos|*.png;*.jpg;*.jpeg;*.gif;*.bmp";
            openFileDialog1.Title = "Selecione a digital";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                this.foto = new Bitmap(openFileDialog1.FileName);

                // Perfil de imagem
                int[] f = new int[this.foto.Width];
                int[] f2 = new int[this.foto.Width];
                int[] f3 = new int[this.foto.Width];
                int linha = this.foto.Height / 4;
                int linha2 = this.foto.Height / 2;
                int linha3 = linha + linha2;
                for (int x = 0; x < this.foto.Width; x++)
                {
                    f[x] = foto.GetPixel(x, linha).R;
                    f2[x] = foto.GetPixel(x, linha2).R;
                    f3[x] = foto.GetPixel(x, linha3).R;
                }
                this.h = f;
                this.h2 = f2;
                this.h3 = f3;

                int x0 = 0;
                int y0 = 255 - f[0];
                int y2 = 255 - f[1];
                int y3 = 255 - f[2];
                for (int x = 0; x < this.foto.Width; x++)
                {
                    x0 = x;
                    y0 = 255 - f[x];
                    y2 = 255 - f2[x];
                    y3 = 255 - f3[x];
                }

                // amostragem do perfil
                String t = ""; String t2 = ""; String t3 = "";
                int l = 9; int[] v = new int[this.h.Count()]; int[] v2 = new int[this.h.Count()]; int[] v3 = new int[this.h.Count()];
                int partes = this.h.Count() / 32; int acum = 0; int acum2 = 0; int acum3 = 0;
                for (int x = 0; x < this.h.Count(); x++)
                {
                    for (int j = 0; j < partes; j++)
                    {
                        if (x + j >= this.h.Count()) { break; }
                        // acumula e tira média
                        acum += this.h[x + j] * l / this.foto.Height;
                        acum2 += this.h2[x + j] * l / this.foto.Height;
                        acum3 += this.h3[x + j] * l / this.foto.Height;
                    }
                    v[x] = acum / partes; acum = 0;
                    v2[x] = acum2 / partes; acum2 = 0;
                    v3[x] = acum3 / partes; acum3 = 0;
                    t += v[x].ToString();
                    t2 += v2[x].ToString();
                    t3 += v3[x].ToString();

                    x += partes;

                }
                t += t2 + t3;

                // validação de login
                Boolean login = false;
                try
                {
                    using (StreamReader sr = new StreamReader("cadastro.txt"))
                    {
                        String line;
                        string[] stringSeparators = new string[] { "," };
                        string[] id;
                        while ( ! sr.EndOfStream) {
                            line = sr.ReadLine();
                            id = line.Split(stringSeparators, StringSplitOptions.None);
                            if (id[0] == t)
                            {
                                label3.Text = id[1].ToString();
                                login = true;
                                break;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Houve um erro consultando a base de dados, não foi possível prosseguir com a autenticação. " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                if (login)
                {
                    label1.Visible = true;
                    label3.Visible = true;
                    pictureBox1.Visible = true;
                    //MessageBox.Show("Aprovado", "My Application", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
                else
                {
                    MessageBox.Show("Acesso não autorizado.", "Login", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
            pictureBox1.Visible = false;
            label1.Visible = false;
            label3.Visible = false;
        }
    }
}
