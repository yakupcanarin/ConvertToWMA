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
using NAudio;
using NAudio.Lame;
using NAudio.Wave;

namespace ConvertToWMA
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "MP3|*.mp3";
            ofd.Multiselect = true;
            ofd.ShowDialog();


            foreach (var item in ofd.FileNames)
            {
                comboBox1.Items.Add(item);
            }
            comboBox1.SelectedIndex = 0;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (var item in comboBox1.Items)
                {
                    using (Mp3FileReader reader = new Mp3FileReader(item.ToString()))
                    {
                        string data = Path.GetDirectoryName(item.ToString());
                        string fileName = Path.GetFileNameWithoutExtension(item.ToString());
                        if (!File.Exists(data+"\\"+fileName+".wma"))
                        {
                            WaveFileWriter.CreateWaveFile(data + "\\" + fileName + ".wma", reader);
                        }
                        else
                        {
                            File.Delete(data + "\\" + fileName + ".wma");
                            WaveFileWriter.CreateWaveFile(data + "\\" + fileName + ".wma", reader);
                        }
                    }
                }
                MessageBox.Show(Parent, "Başarılı", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "HATA!", MessageBoxButtons.OK);
            }
        }
    }
}
