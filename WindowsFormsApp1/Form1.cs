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
using System.Threading;
using System.Media;
using System.Diagnostics;

namespace WindowsFormsApp1
{
    

    public partial class Form1 : Form
    {
        public Form1()
        {
            //Initializing properties of the samplChart to allow zooming/selection
            InitializeComponent();
            samplChart.ChartAreas[0].CursorX.AutoScroll = true;
            samplChart.ChartAreas[0].AxisX.ScaleView.Zoomable = false;
            samplChart.ChartAreas[0].AxisX.ScaleView.SizeType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Number;
            samplChart.ChartAreas[0].CursorX.IsUserEnabled = true;
            samplChart.ChartAreas[0].CursorX.IsUserSelectionEnabled = true;
            //samplChart.ChartAreas[0].AxisX.ScaleView.Zoom(xposition, xSize);
        }


        double xposition = 0;
        double xSize = 100;

        double[] samples;
        Fourier fourier = new Fourier();
        wavFile wave = new wavFile();

        /// <summary>
        /// Function for opening a file by prompting the user to select a file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void openBtn_Click(object sender, EventArgs e)
        {
            


            if(openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                /*
                System.IO.StreamReader sr = new
                    System.IO.StreamReader(openFileDialog1.FileName);
                MessageBox.Show(sr.ReadToEnd());
                */

                clearSamplChart();
                clearFreqChart();

                FileStream fs = File.OpenRead(openFileDialog1.FileName);
                readChunk(fs);//riff
                readChunk(fs);//fmt
                readChunk(fs);//data

                fs.Close();

                //sr.Close();
                richTextBox1.Text += "RIFF:" + wave.RIFF + " \n";
                richTextBox1.Text += "filesize_minus_4:" + wave.filesize_minus_4 + " \n";
                richTextBox1.Text += "WAVE:" + wave.WAVE + " \n\n";

                richTextBox1.Text += "fmt:" + wave.fmt + " \n";
                richTextBox1.Text += "fmt_size:" + wave.fmt_size + " \n";
                richTextBox1.Text += "format_tag:" + wave.format_tag + " \n";
                richTextBox1.Text += "nchannels:" + wave.nchannels + " \n";
                richTextBox1.Text += "samples_per_sec:" + wave.samples_per_sec + " \n";
                richTextBox1.Text += "avg_bytes_per_sec:" + wave.avg_bytes_per_sec + " \n";
                richTextBox1.Text += "nblock_align:" + wave.nblock_align + " \n";
                richTextBox1.Text += "bits_per_sample:" + wave.bits_per_sample + " \n\n";

                richTextBox1.Text += "data:" + wave.data + " \n";
                richTextBox1.Text += "data_size:" + wave.data_size + " \n";

                //Copy wave data to the double samples array
                copyArrToDouble();
                
                for (int i = 0; i < wave.data.Length; i++)
                {
                    samplChart.Series["Series1"].Points.AddXY(i, wave.data[i]);
                }
                
                //Copy data to the double samples
                
                //fourier.FourierTransInt(wave.data, wave.data.Length, freqChart);
            }

        }

        //Function to read a chunk given a filestream of a wav file
        //Used to read the file in openBtn_Click()
        //Filestream fs a filestream of a .wav file
        /// <summary>
        /// Function used to read a chunk given a filestream of a wav file
        /// It is used to read a wav file in openBtn_Click()
        /// </summary>
        /// <param name="fs">a filestream of a wav file</param>
        private void readChunk(FileStream fs)
        {
            ASCIIEncoding encoder = new ASCIIEncoding();
            byte[] bChunkID = new byte[4];

            fs.Read(bChunkID, 0, 4);
            string sChunkID = encoder.GetString(bChunkID);

            byte[] ChunkSize = new byte[4];

            if (sChunkID.Equals("RIFF"))
            {
                wave.RIFF = BitConverter.ToInt32(bChunkID, 0);

                fs.Read(ChunkSize, 0, 4);
                wave.filesize_minus_4 = BitConverter.ToInt32(ChunkSize, 0);

                fs.Read(ChunkSize, 0, 4);
                wave.WAVE = BitConverter.ToInt32(ChunkSize, 0);
            }
            else if(sChunkID.Equals("fmt "))
            {
                wave.fmt = BitConverter.ToInt32(bChunkID, 0);

                fs.Read(ChunkSize, 0, 4);
                wave.fmt_size = BitConverter.ToInt32(ChunkSize, 0);

                fs.Read(ChunkSize, 0, 2);
                wave.format_tag = BitConverter.ToInt16(ChunkSize, 0);

                fs.Read(ChunkSize, 0, 2);
                wave.nchannels = BitConverter.ToInt16(ChunkSize, 0);

                fs.Read(ChunkSize, 0, 4);
                wave.samples_per_sec = BitConverter.ToInt32(ChunkSize, 0);

                fs.Read(ChunkSize, 0, 4);
                wave.avg_bytes_per_sec = BitConverter.ToInt32(ChunkSize, 0);

                fs.Read(ChunkSize, 0, 2);
                wave.nblock_align = BitConverter.ToInt16(ChunkSize, 0);

                fs.Read(ChunkSize, 0, 2);
                wave.bits_per_sample = BitConverter.ToInt16(ChunkSize, 0);
            }
            else if(sChunkID.Equals("data"))
            {
                fs.Read(ChunkSize, 0, 4);
                wave.data_size = BitConverter.ToInt32(ChunkSize, 0);
                
                byte[] dataPart = new byte[wave.bits_per_sample / 8]; //if it is not 8 bit need to -128

                int numSamples = wave.data_size / wave.nchannels / wave.bits_per_sample * 8;
                wave.data = new int[numSamples];

                for(int i = 0; i<wave.data.Length; i++)
                {
                    fs.Read(dataPart, 0, wave.bits_per_sample / 8);
                    wave.data[i] = BitConverter.ToInt16(dataPart, 0);
                }
            }
        }

        //startX is wherever the click starts
        int startX = 0;

        //endX is wherever the click ends
        int endX = 0;
        
        /// <summary>
        /// Used to get where the first position of a click is made on the 
        /// samplChart and save the position in startX
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void samplChart_MouseDown(object sender, EventArgs e)
        {
            MouseEventArgs clickCoord = (MouseEventArgs)e;
            startX = (int)samplChart.ChartAreas[0].AxisX.PixelPositionToValue(clickCoord.X);
        }

        /// <summary>
        /// Used to get where the second position a click is made on samplChart
        /// and save the position in endX
        /// It will swap startX and endX if startX is bigger than endX to maintain order
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
            
            //Flips startX and endX if startX is bigger than endX
            if(startX > endX)
            {
                int temp = startX;
                startX = endX;
                endX = temp;
            }

            if(startX < 0)
            {
                startX = 0;
            }

            richTextBox1.Text = "Start: " + startX + "\n";
            richTextBox1.Text += "End: " + endX + "\n";
            richTextBox1.Text += "Number of selected samples: " + (endX - startX);
            //richTextBox1.Text += 
        }
        
        /// <summary>
        /// Used for deleting selected samples between startX and endX
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void deleteBtn_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = "Deleting selected samples\n";

            copySamples = new double[endX-startX];

            for (int i2 = 0; i2 < endX - startX; i2++)
            {
                copySamples[i2] = samples[startX + i2];
            }

            //int difference = endX - startX; //use to find out how many samples are being deleted
            int size = startX + samples.Length - endX;

            //i is the index of where to overwrite
            int i = startX;
            
            //overwrite old samples starting at startX with samples starting from endX
            for(int i2 = endX; i2 < samples.Length; i2++, i++)
            {
                
                samples[i] = samples[i2];
            }

            //Resize the array
            Array.Resize<double>(ref samples, size);

            //Clearing both sampleChart of data
            clearSamplChart();

            richTextBox1.Text += "Deleted " + (endX-startX) + " samples.";

            //
            for (int i2 = 0; i2 < samples.Length; i2++)
            {
                samplChart.Series["Series1"].Points.AddXY(i2, samples[i2]);
            }

        }

        /// <summary>
        /// assist function to copy the wave.data into samples
        /// </summary>
        private void copyArrToDouble()
        {
            int size = wave.data.Length;
            samples = new double[size];
            for(int i = 0; i < size; i++)
            {
                samples[i] = wave.data[i];
            }
        }

        /// <summary>
        /// Clears samplChart by removing it and adding it back again
        /// </summary>
        private void clearSamplChart()
        {
            richTextBox1.Text = "";
            /* this version is inefficient
            foreach (var series in samplChart.Series)
            {
                series.Points.Clear();
            }
            */
            samplChart.Series.Clear();
            samplChart.Series.Add("Series1");
        }

        /// <summary>
        /// Clears freqChart by removing it and adding it back again
        /// </summary>
        private void clearFreqChart()
        {
            richTextBox1.Text = "";
            /* this version is inefficient
            foreach (var series in freqChart.Series)
            {
                series.Points.Clear();
            }
            */
            freqChart.Series.Clear();
            freqChart.Series.Add("freqChart");
        }

        /// <summary>
        /// Button for enabling selecting or zooming while changing color/text depending
        ///on the mode it currently is on
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            if(samplChart.ChartAreas[0].AxisX.ScaleView.Zoomable == false)
            {
                samplChart.ChartAreas[0].AxisX.ScaleView.Zoomable = true;
                button2.Text = "Zoom";
                button2.BackColor = Color.Yellow;
            }
            else
            {
                samplChart.ChartAreas[0].AxisX.ScaleView.Zoomable = false;
                button2.Text = "Selection";
                button2.BackColor = Color.LightSkyBlue;
            }

        }

        /// <summary>
        /// Button for performing fourier within the selected startX and endX area
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 
        Complex[] complex;
        double[] fMagnitude;
        //int[] frequencies;
        //double[] frequencies;
        private void button1_Click(object sender, EventArgs e)
        {
            clearFreqChart();
            fMagnitude = new double[endX - startX];
            complex = new Complex[endX - startX];
            //frequencies = new int[endX - startX];

            double[] windowSamples = new double[endX - startX];
            windowing(samples, windowSamples, startX, endX);

            Stopwatch sw = Stopwatch.StartNew();
            Thread fourierThread = new Thread(() => fourier.FourierTransArea(windowSamples, 0, windowSamples.Length, fMagnitude, complex));
            //Thread fourierThread = new Thread(()=>fourier.FourierTransArea(samples,startX,endX, fMagnitude, complex));
            fourierThread.Start();

            fourierThread.Join();
            sw.Stop();
            richTextBox1.Text = ("Time elapsed" + sw.ElapsedMilliseconds.ToString());

            double diff = (double)wave.samples_per_sec / fMagnitude.Length;

            for(int i = 0; i < fMagnitude.Length; i++)
            {
                //frequencies[i] = (int)(i * diff);
                freqChart.Series["freqChart"].Points.AddXY((int)(i*diff), fMagnitude[i]);
            }
            
            //fourier.FourierTransArea(samples, startX, endX, freqChart);

        }

        /// <summary>
        /// Saves the samples as a wav file after the user selects
        /// a location and name to save as
        /// Asks user for a file location to save to
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveBtn_Click(object sender, EventArgs e)
        {
            string path;
            SaveFileDialog saveDlg = new SaveFileDialog();
            saveDlg.Filter = "Wave File|*.wav";
            saveDlg.Title = "Save a Wav File";
   
            if(saveDlg.ShowDialog() == DialogResult.OK)
            {
                path = saveDlg.FileName;
                richTextBox1.Text = path;
                save(path);
            }
        }

        /// <summary>
        /// Function to do the saving portion into a file
        /// </summary>
        /// <param name="filePath">The filepath a user has selected</param>
        public void save(string filePath)
        { 

            FileStream fileStream = new FileStream(filePath, FileMode.Create);
            BinaryWriter writer = new BinaryWriter(fileStream);


            wavFile newWav = new wavFile();

            newWav.RIFF = wave.RIFF;
            
            newWav.WAVE = wave.WAVE;

            newWav.fmt = wave.fmt;
            newWav.fmt_size = wave.fmt_size;
            newWav.format_tag = wave.format_tag;
            newWav.nchannels = wave.nchannels;
            newWav.samples_per_sec = wave.samples_per_sec;
            newWav.avg_bytes_per_sec = wave.avg_bytes_per_sec;
            newWav.nblock_align = wave.nblock_align;
            newWav.bits_per_sample = wave.bits_per_sample;

            newWav.data_size = samples.Length * newWav.nchannels * newWav.bits_per_sample / 8;


            newWav.filesize_minus_4 = newWav.data_size/2+36;

            //newWav.data = sSamples;

            richTextBox1.Text += "Done converting iSamples";

            //Writing
            writer.Write('R');
            writer.Write('I');
            writer.Write('F');
            writer.Write('F');
            writer.Write(newWav.filesize_minus_4);
            writer.Write(newWav.WAVE);

            writer.Write('f');
            writer.Write('m');
            writer.Write('t');
            writer.Write(' ');
            writer.Write(newWav.fmt_size);
            writer.Write(newWav.format_tag);
            writer.Write(newWav.nchannels);
            writer.Write(newWav.samples_per_sec);
            writer.Write(newWav.avg_bytes_per_sec);
            writer.Write(newWav.nblock_align);
            writer.Write(newWav.bits_per_sample);

            string data = "data";
            writer.Write(data.ToCharArray());
            writer.Write(newWav.data_size);


            //Needs to convert to short if it is 2bytes long
            short[] sSamples = new short[samples.Length];
            for (int i = 0; i < sSamples.Length; i++)
            {
                sSamples[i] = (short)samples[i];
            }
            byte[] byteStream = new byte[sSamples.Length * 2];
            Buffer.BlockCopy(sSamples, 0, byteStream, 0, byteStream.Length);

            //Writing the data bytes
            foreach (byte dataPoint in byteStream)
            {
                writer.Write(dataPoint);
            }
            
            //writer.Seek(4, SeekOrigin.Begin);
            //uint filesize = (uint)writer.BaseStream.Length;
            //writer.Write(filesize - 8);

            writer.Close();
            fileStream.Close();

            richTextBox1.Text += "Completed saving file.";
        }

        double[] copySamples;
        /// <summary>
        /// Copies samples into copySamples in the selection between
        /// startX and endX
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void copyBtn_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = "Copying samples \n";
            int numSamp = endX - startX;
            copySamples = new double[numSamp];

            for (int i = 0; i < numSamp; i++)
            {
                copySamples[i] = samples[startX + i];
            }
            richTextBox1.Text = "Copied from " + startX + " to " + endX;
        }

        /// <summary>
        /// Pastes the copySamples into samples and updates the chart
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pasteBtn_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = "Setting up pasting samples. \n";
            int numOldSamples = samples.Length - startX;
            double[] samplOld = new double[numOldSamples];
            double[] newSamples = new double[samples.Length + copySamples.Length];

            int counter = 0;
            //copy old up to startX
            for(;counter < startX; counter++)
            {
                newSamples[counter] = samples[counter];
            }
            
            //copy the copy from startX to endX
            for(int i = 0; i < copySamples.Length; i++, counter++)
            {
                newSamples[counter] = copySamples[i];
            }
            
            //Copying from old startX to end
            for(int i = startX; i < samples.Length; i++, counter++)
            {
                newSamples[counter] = samples[i];
            }

            samples = newSamples;

            clearSamplChart();
            for (int i = 0; i < samples.Length; i++)
            {
                samplChart.Series["Series1"].Points.AddXY(i, samples[i]);
            }

            richTextBox1.Text += "Completed pasting";
        }

        public void placeOnChartThread(double[] points)
        {
            for(int i = 0; i < points.Length; i++)
            {
                samplChart.Invoke(new Action(()=>samplChart.Series["Series1"].Points.AddXY(i, points[i])));
            }
        }

        SoundPlayer player = new SoundPlayer();
        MemoryStream ms;
        private void playBtn_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = "Playing audio \n";
            wavFile newWav = new wavFile();
            
            newWav.RIFF = wave.RIFF;
            
            newWav.WAVE = wave.WAVE;
            
            newWav.fmt = wave.fmt;
            newWav.fmt_size = wave.fmt_size;
            newWav.format_tag = wave.format_tag;
            newWav.nchannels = wave.nchannels;
            newWav.samples_per_sec = wave.samples_per_sec;
            newWav.avg_bytes_per_sec = wave.avg_bytes_per_sec;
            newWav.nblock_align = wave.nblock_align;
            newWav.bits_per_sample = wave.bits_per_sample;
            
            newWav.data_size = samples.Length * newWav.nchannels * newWav.bits_per_sample / 8;
            newWav.filesize_minus_4 = newWav.data_size/2 + 36;
            
            byte[] riff = BitConverter.GetBytes(newWav.RIFF);
            byte[] minus4 = BitConverter.GetBytes(newWav.filesize_minus_4);
            byte[] bWave = BitConverter.GetBytes(newWav.WAVE);
            
            byte[] fmt = BitConverter.GetBytes(newWav.fmt);
            byte[] fmt_size = BitConverter.GetBytes(newWav.fmt_size);
            byte[] format_tag = BitConverter.GetBytes(newWav.format_tag);
            byte[] nchannels = BitConverter.GetBytes(newWav.nchannels);
            byte[] samplerate = BitConverter.GetBytes(newWav.samples_per_sec);
            byte[] byterate = BitConverter.GetBytes(newWav.avg_bytes_per_sec);
            byte[] nblock = BitConverter.GetBytes(newWav.nblock_align);
            byte[] bitspersamp = BitConverter.GetBytes(newWav.bits_per_sample);
            
            byte[] dataid = Encoding.ASCII.GetBytes("data");
            byte[] subch2 = BitConverter.GetBytes(newWav.data_size);

            //turn data into bytes
            short[] shortArr = new short[samples.Length];
            for(int i = 0; i<samples.Length; i++)
            {
                shortArr[i] = (short)samples[i];
            }
            
            byte[] byteStream = new byte[shortArr.Length * sizeof(short)];
            Buffer.BlockCopy(shortArr, 0, byteStream, 0, samples.Length*sizeof(short));
            ////byte[] byteStream = BitConverter.GetBytes(shortArr*);
            //
            ////Get total amount of bytes
            int length = riff.Length + minus4.Length + bWave.Length +
            
                        fmt.Length + fmt_size.Length +
                        format_tag.Length + nchannels.Length +
                        samplerate.Length + byterate.Length + 
                        nblock.Length + bitspersamp.Length + 
                        
                        dataid.Length +subch2.Length+ byteStream.Length;

            byte[] audioStream = new byte[length];
            //
            Buffer.BlockCopy(riff, 0, audioStream,0, 4);
            Buffer.BlockCopy(minus4, 0, audioStream,4, 4);
            Buffer.BlockCopy(bWave, 0, audioStream,8, 4);
            
            Buffer.BlockCopy(fmt, 0, audioStream,12, 4);
            Buffer.BlockCopy(fmt_size, 0, audioStream,16, 4);
            Buffer.BlockCopy(format_tag, 0, audioStream,20, 2);
            Buffer.BlockCopy(nchannels, 0, audioStream,22, 2);
            Buffer.BlockCopy(samplerate, 0, audioStream,24, 4);
            Buffer.BlockCopy(byterate, 0, audioStream,28, 4);
            Buffer.BlockCopy(nblock, 0, audioStream,32, 2);
            Buffer.BlockCopy(bitspersamp, 0, audioStream,34, 2);
            
            Buffer.BlockCopy(dataid, 0, audioStream,36,4);
            Buffer.BlockCopy(subch2, 0, audioStream,40,4);
            Buffer.BlockCopy(byteStream, 0, audioStream, 44, byteStream.Length);

            richTextBox1.Text += "Created byte audio \n";




            using (MemoryStream ms = new MemoryStream(audioStream))
            {
                richTextBox1.Text += "Starting audio now";
                player = new SoundPlayer();
                player.Stream = ms;
                player.Load();
                player.PlaySync();
                
            }
        }

        private void invFourierBtn_Click(object sender, EventArgs e)
        {
            double[] returnSamp = new double[complex.Length];
            richTextBox1.Text = returnSamp.Length + "\n";
            richTextBox1.Text += complex.Length;
            fourier.invFourier(complex, returnSamp);
            
            
            clearSamplChart();
            for(int i = 0; i < returnSamp.Length; i++)
            {
                samplChart.Series["Series1"].Points.AddXY(i, returnSamp[i]);
            }
            samples = returnSamp;
        }

        /// <summary>
        /// Creates a filter to convolve the selected area with
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        
        private void filterBtn_Click(object sender, EventArgs e)
        {
            makeFilter();
            richTextBox1.Text += "Performing filtering \n";
            performFilter();
            richTextBox1.Text += "Completed Filtering \n";
            richTextBox1.Text += idftTime;
        }

        double[] idftFilter;
        String filterString;
        private void makeFilter()
        {
            Complex[] filter;
            filter = new Complex[endX - startX];
            idftFilter = new double[filter.Length];

            //Initialize complexes
            for (int i = 0; i < filter.Length; i++)
            {
                filter[i] = new Complex();
            }
            //filter[0] = 1;
            filter[0].setReal(1);
            filter[0].setImag(1);

            //calculate cutoff
            double freqCutoff = ((double)filterValue.Value) * (double)filter.Length / (double)wave.samples_per_sec;
            filterString = "\nRawCut"+ freqCutoff;
            if (lpRadio.Checked)
            {
                richTextBox1.Text = "Low Pass Filtering\n" + freqCutoff + " " + filter.Length + " " + wave.samples_per_sec + "\n";
                //cut everything lower than freqCutoff (round up)
                //freqCutoff = Math.Ceiling(freqCutoff);
                freqCutoff = Math.Floor(freqCutoff);
                filterString += " Low Pass Filtering\n" + freqCutoff + " " + filter.Length + " " + wave.samples_per_sec;

                //Set to account for nyquist
                if (freqCutoff > filter.Length)
                {
                    freqCutoff -= filter.Length / 2;
                }

                //Apply to both sides
                for (int i1 = 1, i2 = filter.Length - 1; i1 < freqCutoff; i1++, i2--)
                {
                    //filter[i1] = 1;
                    //filter[i2] = 1;
                    filter[i1].setReal(1);
                    filter[i2].setReal(1);
                    filter[i1].setImag(1);
                    filter[i2].setImag(1);
                }
                //for (int i = 0; i < filter.Length; i++)
                //{
                //    Debug.Write(filter[i].GetReal());
                //}
            }


            else if (hpRadio.Checked)
            {
                //richTextBox1.Text = "Hi Pass Filtering\n" + freqCutoff + " " + filter.Length + " " + wave.samples_per_sec + "\n";
                //filterString = "Hi Pass Filtering\n" + freqCutoff + " " + filter.Length + " " + wave.samples_per_sec;
                //cut everythihg higher than freqCutoff(round down)
                //freqCutoff = Math.Floor(freqCutoff);
                freqCutoff = Math.Ceiling(freqCutoff);
                filterString += "Hi Pass Filtering\n" + freqCutoff + " " + filter.Length + " " + wave.samples_per_sec;


                //Account for nyquist
                if (freqCutoff > filter.Length)
                {
                    freqCutoff -= filter.Length / 2;
                }

                //Preset to 1s
                for (int i = 0; i < filter.Length; i++)
                {
                    //filter[i] = 1;
                    filter[i] = new Complex(1, 1);
                }

                //Apply to both sides
                for (int i1 = 1, i2 = filter.Length - 1; i1 < freqCutoff; i1++, i2--)
                {
                    //filter[i1] = 0;
                    //filter[i2] = 0;
                    filter[i1].setReal(0);
                    filter[i2].setReal(0);
                    filter[i1].setImag(0);
                    filter[i2].setImag(0);

                }
                for(int i = 0; i < filter.Length; i++)
                {
                    Debug.Write(filter[i].GetReal());
                }
            }
            richTextBox1.Text += "Done constructing base filter\nPerforming IDFT on filter\n";

            //Can thread
            Stopwatch sw = Stopwatch.StartNew();
            fourier.invFourier(filter, idftFilter);
            sw.Stop();
            idftTime = "Elapsed time" + sw.ElapsedMilliseconds.ToString();
        }

        public void performFilter()
        {
            //Apply windowing
            double[] windowedFilter = new double[idftFilter.Length];

            if (aplWinCheck.Checked)
            {
                windowing(idftFilter, windowedFilter, 0, idftFilter.Length);
                idftFilter = windowedFilter;
            }

            double[] convolSamples = new double[idftFilter.Length];
            double[] copy2Samples = new double[idftFilter.Length + idftFilter.Length-1];

            //idftFilter = windowedFilter;

            //Copy the samples over
            for (int i = 0; i < idftFilter.Length; i++)
            {
                copy2Samples[i] = samples[i + startX];
            }

            //
            for(int i1 = 0; i1 < endX-startX; i1++)
            {
                double sum = 0;
                for (int i2 = 0; i2 < endX - startX; i2++)
                {
                    sum += copy2Samples[i1+i2] * idftFilter[i2];
                }
                convolSamples[i1] = sum;
                //Debug.WriteLine(i1 + " " + convolSamples[i1]);
            }

            //clearSamplChart();

            //Thread.Sleep(5000);

            //Cut out old samples and replace with filtered
            for(int i1 = startX, i2 = 0; i1 < endX; i1++, i2++)
            {
                //Debug.WriteLine(samples[i1] + " "+ convolSamples[i2]);
                samples[i1] = convolSamples[i2];
                
                //samplChart.Series["Series1"].Points.AddXY(i1, convolSamples[i2]);
            }

            clearSamplChart();
            for (int i = 0; i < samples.Length; i++)
            {
                //Debug.WriteLine("Removing" + i);
                //samplChart.Series["Series1"].Points.RemoveAt(startX);
                samplChart.Series["Series1"].Points.AddXY(i, samples[i]);
                //samplChart.Series["Series1"].Points.AddXY(i, 0);

            }

            Debug.WriteLine(filterString);
        }
        
        public void windowing(double[] input, double[] output, int startX2, int endX2)
        {
            
            for(int i1 = 0, i2=startX2; i2 < endX2; i1++,i2++)
            {
                output[i1] = input[i2];
            }

            if (radioRect.Checked)
            {
                //do nothing
            }
            else if (triRadio.Checked)
            {
                triagWindow(output);
            }
            else if (hamRadio.Checked)
            {
                hamWindow(output);
            }
        }

        /// <summary>
        /// Triangular window
        /// </summary>
        /// <param name="output"></param> the output array of windowed samples
        public void triagWindow(double[] output)
        {
            double[] window = new double[output.Length];
            double N = window.Length;
            for (int n = 0; n < N; n++)
            {
                double inner = Math.Abs(n - ((N - 1) / 2));
                window[n] = (2 / N) *
                            ((N / 2) - inner);
            }

            for (int i = 0; i < output.Length; i++)
            {
                output[i] = output[i] * window[i];
            }
        }

        /// <summary>
        /// Hamming window
        /// </summary>
        /// <param name="output"></param> the output array of windowed samples
        public void hamWindow(double[] output)
        {
            double[] window = new double[output.Length];
            double N = window.Length;
            for(int n = 0; n < N; n++)
            {
                double cosEqua = Math.Cos(2 * Math.PI * n / N);
                window[n] = 0.54 - 0.46 * cosEqua;
            }

            for(int i = 0; i < output.Length; i++)
            {
                output[i] = output[i] * window[i];
            }
        }

        string idftTime = "";
        //Threaded fourier
        private void thrdFourierBtn_Click(object sender, EventArgs e)
        {
            
            clearFreqChart();
            fMagnitude = new double[endX - startX];
            complex = new Complex[endX - startX];
            //frequencies = new int[endX - startX];

            double[] windowSamples = new double[endX - startX];
            windowing(samples, windowSamples, startX, endX);

            Stopwatch sw = Stopwatch.StartNew();
            fourier.threadedFourierTransArea(windowSamples, 0, windowSamples.Length, fMagnitude, complex);
            sw.Stop();
            richTextBox1.Text = ("Time elapsed" + sw.ElapsedMilliseconds.ToString());
            //Thread fourierThread = new Thread(()=>fourier.FourierTransArea(samples,startX,endX, fMagnitude, complex));
            //fourierThread.Start();

            //fourierThread.Join();

            double diff = (double)wave.samples_per_sec / fMagnitude.Length;

            for (int i = 0; i < fMagnitude.Length; i++)
            {
                //frequencies[i] = (int)(i * diff);
                freqChart.Series["freqChart"].Points.AddXY((int)(i * diff), fMagnitude[i]);
            }
        }

        private void thrdFilterBtn_Click(object sender, EventArgs e)
        {
            threadedMakeFilter();
            richTextBox1.Text += "Performing filtering \n";
            performFilter();
            richTextBox1.Text += "Completed Filtering \n";
            richTextBox1.Text += idftTime;
        }

        private void threadedMakeFilter()
        {
            Complex[] filter;
            filter = new Complex[endX - startX];
            idftFilter = new double[filter.Length];

            //Initialize complexes
            for (int i = 0; i < filter.Length; i++)
            {
                filter[i] = new Complex();
            }
            //filter[0] = 1;
            filter[0].setReal(1);
            filter[0].setImag(1);

            //calculate cutoff
            double freqCutoff = ((double)filterValue.Value) * (double)filter.Length / (double)wave.samples_per_sec;
            filterString = "\nRawCut" + freqCutoff;
            if (lpRadio.Checked)
            {
                richTextBox1.Text = "Low Pass Filtering\n" + freqCutoff + " " + filter.Length + " " + wave.samples_per_sec + "\n";
                //cut everything lower than freqCutoff (round up)
                //freqCutoff = Math.Ceiling(freqCutoff);
                freqCutoff = Math.Floor(freqCutoff);
                filterString += " Low Pass Filtering\n" + freqCutoff + " " + filter.Length + " " + wave.samples_per_sec;

                //Set to account for nyquist
                if (freqCutoff > filter.Length)
                {
                    freqCutoff -= filter.Length / 2;
                }

                //Apply to both sides
                for (int i1 = 1, i2 = filter.Length - 1; i1 < freqCutoff; i1++, i2--)
                {
                    //filter[i1] = 1;
                    //filter[i2] = 1;
                    filter[i1].setReal(1);
                    filter[i2].setReal(1);
                    filter[i1].setImag(1);
                    filter[i2].setImag(1);
                }
                //for (int i = 0; i < filter.Length; i++)
                //{
                //    Debug.Write(filter[i].GetReal());
                //}
            }


            else if (hpRadio.Checked)
            {
                //richTextBox1.Text = "Hi Pass Filtering\n" + freqCutoff + " " + filter.Length + " " + wave.samples_per_sec + "\n";
                //filterString = "Hi Pass Filtering\n" + freqCutoff + " " + filter.Length + " " + wave.samples_per_sec;
                //cut everythihg higher than freqCutoff(round down)
                //freqCutoff = Math.Floor(freqCutoff);
                freqCutoff = Math.Ceiling(freqCutoff);
                filterString += "Hi Pass Filtering\n" + freqCutoff + " " + filter.Length + " " + wave.samples_per_sec;


                //Account for nyquist
                if (freqCutoff > filter.Length)
                {
                    freqCutoff -= filter.Length / 2;
                }

                //Preset to 1s
                for (int i = 0; i < filter.Length; i++)
                {
                    //filter[i] = 1;
                    filter[i] = new Complex(1, 1);
                }

                //Apply to both sides
                for (int i1 = 1, i2 = filter.Length - 1; i1 < freqCutoff; i1++, i2--)
                {
                    //filter[i1] = 0;
                    //filter[i2] = 0;
                    filter[i1].setReal(0);
                    filter[i2].setReal(0);
                    filter[i1].setImag(0);
                    filter[i2].setImag(0);

                }
                for (int i = 0; i < filter.Length; i++)
                {
                    Debug.Write(filter[i].GetReal());
                }
            }
            richTextBox1.Text += "Done constructing base filter\nPerforming IDFT on filter\n";

            //Can thread
            Stopwatch sw = Stopwatch.StartNew();
            fourier.threadedInvFourier(filter, idftFilter);
            sw.Stop();
            idftTime = "Elapsed time"+sw.ElapsedMilliseconds.ToString();
        }

    }
}
