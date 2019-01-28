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

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        double[] samples;
        Fourier fourier = new Fourier();




        private void button4_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = "";
            foreach (var series in samplChart.Series)
            {
                series.Points.Clear();
            }
            foreach (var series in freqChart.Series)
            {
                series.Points.Clear();
            }
            /*
            double frequency = 5;
            int samples = 1000000;
            for (int t = 0; t < samples; t++)
            {
                double sample = fourier.SampleGenerate(frequency, samples, t);
                samplChart.Series["samplChart"].Points.AddXY(t, sample);
            }
            */
            
            samples = fourier.GenerateSamples(5, 2000 , 10);
            for(int i = 0; i < samples.Length; i++)
            {
                samplChart.Series["Series1"].Points.AddXY(i, samples[i]);
            }

            fourier.FourierTrans(samples, 2000, freqChart);
            richTextBox1.Text += "dadsa";
        }

        //For opening a wav file
        private void openBtn_Click(object sender, EventArgs e)
        {


            if(openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                System.IO.StreamReader sr = new
                    System.IO.StreamReader(openFileDialog1.FileName);
                MessageBox.Show(sr.ReadToEnd());
                sr.Close();
            }
        }

        private void readChunk(FileStream fs)
        {
            ASCIIEncoding encoder = new ASCIIEncoding();
            byte[] bChunkID = new byte[4];

            fs.Read(bChunkID, 0, 4);
            string sChunkID = encoder.GetString(bChunkID);

            byte[] ChunkSize = new byte[4];

            fs.Read(ChunkSize, 0, 4);

            if (sChunkID.Equals("RIFF"))
            {

            }
        }


        //Methods below are used to find  where the samplGraph was clicked
        int startX = 0;
        int endX = 0; 
        private void samplChart_MouseDown(object sender, EventArgs e)
        {
            MouseEventArgs clickCoord = (MouseEventArgs)e;
            startX = (int)samplChart.ChartAreas[0].AxisX.PixelPositionToValue(clickCoord.X);
        }
        private void samplChart_MouseUp(object sender, EventArgs e)
        {
            MouseEventArgs clickCoord = (MouseEventArgs)e;
            try
            {
                endX = (int)samplChart.ChartAreas[0].AxisX.PixelPositionToValue(clickCoord.X);
                
            }
            catch
            {
                int endX = startX;
            }

            if(endX > samplChart.Series[0].Points.Count)
            {
                endX = samplChart.Series[0].Points.Count;
            }
            
            if(startX > endX)
            {
                int temp = startX;
                startX = endX;
                endX = temp;
            }

            richTextBox1.Text = startX + "\n";
            richTextBox1.Text += endX + "\n";
            richTextBox1.Text += samplChart.Series[0].Points.Count;
        }

        private void deleteBtn_Click(object sender, EventArgs e)
        {
            int difference = endX - startX; //use to find out how many samples are being deleted
            double[] tempSampl = new double[startX + samples.Length-endX]; ///Deal with bug of trying to delete when there is nothing inside

            //Making temp array with the new array
            int i = 0;
            for (; i <= startX; i++)
            {
                tempSampl[i] = samples[i];
            }
            i--;
            for(int i2 = endX; i2 < samples.Length; i2++, i++)
            {
                tempSampl[i] = samples[i2];
            }

            samples = tempSampl; //Replace old array with new array
            
            //Clearing both Charts of data
            foreach (var series in samplChart.Series)
            {
                series.Points.Clear();
            }

            //Fully clearing freqChart
            foreach (var series in freqChart.Series)
            {
                series.Points.Clear();
            }

            

            richTextBox1.Text += "\n" + tempSampl.Length + " ";

            for (int i2 = 0; i2 < tempSampl.Length; i2++)
            {
                samplChart.Series["Series1"].Points.AddXY(i2, tempSampl[i2]);
            }

            fourier.FourierTrans(samples, samples.Length, freqChart);

            //richTextBox1.Text = "Delete Failed" + startX + " " + endX;
            //richTextBox1.Text = "Delete Success" + startX + " " + endX;
            //richTextBox1.Text = samples.Length + " " + tempSampl.Length;

        }
    }
}
