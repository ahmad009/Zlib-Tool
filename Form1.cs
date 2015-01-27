using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Ionic.Zlib;
using System.IO;

namespace ZlibTool
{
    public partial class Form1 : Form
    {
        string inputpath;
        string inputfile;

        OpenFileDialog openFileDialog1 = new OpenFileDialog();
        SaveFileDialog saveFileDialog1 = new SaveFileDialog();

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "All Files Compressed With Zlib (*.*)|*.*";

            openFileDialog1.FilterIndex = 1;

            openFileDialog1.Multiselect = false;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                button2.Enabled = true;
                button3.Enabled = true;

                inputpath = Path.GetDirectoryName(openFileDialog1.FileName);
                inputfile = Path.GetFileName(openFileDialog1.FileName);
                
                comboBox1.Text = inputfile;
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "All Files (*.*)|*.*";
            saveFileDialog1.ShowDialog();

            if (saveFileDialog1.FileName != null)
            {
                byte[] bytes = System.IO.File.ReadAllBytes(openFileDialog1.FileName);
                var compressedBlob = ZlibStream.CompressBuffer(bytes);

                try
                {
                    System.IO.FileStream _FileStream = (System.IO.FileStream)saveFileDialog1.OpenFile();
                    _FileStream.Write(compressedBlob, 0, compressedBlob.Length);
                    _FileStream.Close();
                }
                catch
                { }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "All Files (*.*)|*.*";
            saveFileDialog1.ShowDialog();

            if (saveFileDialog1.FileName != null)
            {
                byte[] bytes = System.IO.File.ReadAllBytes(openFileDialog1.FileName);
                var decompressedBlob = ZlibStream.UncompressBuffer(bytes);

                try
                {
                    System.IO.FileStream _FileStream = (System.IO.FileStream)saveFileDialog1.OpenFile();
                    _FileStream.Write(decompressedBlob, 0, decompressedBlob.Length);
                    _FileStream.Close();
                }
                catch
                { }
            }
        }
    }
}
