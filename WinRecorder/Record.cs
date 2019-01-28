using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Runtime.InteropServices;


namespace WindowsFormsApplication1
{
    public unsafe partial class RecordDialog : Form
    {
        
        uint INP_BUFFER_SIZE= 16384;
     static int        dwDataLength, dwRepetitions = 1 ;
     static int count = 0;
     const int CALLBACK_FUNCTION = 0x0030000;
     const int CALLBACK_WINDOW = 0x0010000;
        bool endRecording  = false;
      public static WAVEHDR pWaveHdr1, pWaveHdr2;
     static String    szOpenError = "Error opening waveform audio!";
     static String        szMemError = "Error allocating memory!" ;
     static WAVEFORMAT waveform;
     public delegate void AudioRecordingDelegate(IntPtr deviceHandle, uint message, IntPtr instance, ref WAVEHDR wavehdr, IntPtr reserved2);
     private AudioRecordingDelegate wavein;
     //callbackWaveIn(IntPtr deviceHandle, uint message, IntPtr instance, ref WAVEHDR wavehdr, IntPtr reserved2)
     [DllImport("winmm.dll")]
     public static extern int waveInAddBuffer(IntPtr hWaveIn, ref WAVEHDR lpWaveHdr, uint cWaveHdrSize);
     [DllImport("winmm.dll")]
     public static extern int waveInPrepareHeader(IntPtr hWaveIn, ref WAVEHDR lpWaveHdr, uint Size);
     [DllImport("winmm.dll")]
     public static extern int waveInStart(IntPtr hWaveIn);
     [DllImport("winmm.dll", EntryPoint = "waveInOpen", SetLastError = true)]
     public static extern int waveInOpen(ref IntPtr t, uint id, ref WAVEFORMAT pwfx, IntPtr dwCallback, int dwInstance, int fdwOpen);
     [DllImport("winmm.dll", EntryPoint = "waveInUnprepareHeader", SetLastError = true)]
     public static extern int waveInUnprepareHeader(IntPtr hwi,ref WAVEHDR pwh, uint cbwh);
        [DllImport("winmm.dll",EntryPoint="waveInClose",SetLastError=true)]
        public static extern uint waveInClose(IntPtr hwnd);
        [DllImport("winmm.dll",EntryPoint="waveInReset",SetLastError=true)]
        static extern uint waveInReset(IntPtr hwi);
        [DllImport("winmm.dll", EntryPoint = "waveInStop", SetLastError = true)]
        static extern uint waveInStop(IntPtr hwi);
        private static uint sampleLength = 0;
        private byte[] samples = new byte[0];
        private AudioRecordingDelegate waveIn;
        public static IntPtr handle;
        private WAVEHDR header;
        private GCHandle headerPin;
        private GCHandle bufferPin;
 //      private GCHandle bufferPin2;
        private byte[] buffer;
//        private byte[] buffer2;
        private uint bufferLength;
        private Form1 form;

        private void setupBuffer()
        {

            pWaveHdr1 = new WAVEHDR();
            pWaveHdr1.lpData = bufferPin.AddrOfPinnedObject();
            pWaveHdr1.dwBufferLength = INP_BUFFER_SIZE;
            pWaveHdr1.dwBytesRecorded = 0;
            pWaveHdr1.dwUser = IntPtr.Zero;
            pWaveHdr1.dwFlags = 0;
            pWaveHdr1.dwLoops = 1;
            pWaveHdr1.lpNext = IntPtr.Zero;
            pWaveHdr1.reserved = (System.IntPtr)null;

            int i = waveInPrepareHeader(handle, ref pWaveHdr1, Convert.ToUInt32(Marshal.SizeOf(pWaveHdr1)));
            if (i != 0)
            {
                this.Text = "Error: waveInPrepare " + i.ToString();
                return;
            }
 
             i = waveInAddBuffer(handle, ref pWaveHdr1, Convert.ToUInt32(Marshal.SizeOf(pWaveHdr1)));
            if (i != 0)
            {
                this.Text = "Error: waveInAddrBuffer";
                return;
            }

        }

        private void setupWaveIn()
        {
            waveIn = this.callbackWaveIn;
            handle = new IntPtr();
            WAVEFORMAT format;
            format.wFormatTag = 1;
            format.nChannels = 1;
            format.nSamplesPerSec = 11025;
            format.wBitsPerSample = 8;
            format.nBlockAlign = 1;// Convert.ToUInt16(format.nChannels * format.wBitsPerSample);
            format.nAvgBytesPerSec = 11025;//format.nSamplesPerSec * format.nBlockAlign;
            bufferLength = 16384;// format.nAvgBytesPerSec / 800;
            format.cbSize = 0;

            buffer = new byte[bufferLength];
            bufferPin = GCHandle.Alloc(buffer, GCHandleType.Pinned);
            
           
            //4294967295 = WAVE_MAPPER aka unsigned int (-1)
          //  int i = waveInOpen(ref handle, ((unchecked uint)-1), ref format, Marshal.GetFunctionPointerForDelegate(waveIn), 0, CALLBACK_FUNCTION);           
            uint wavemapper = (unchecked((uint)-1));
            int i = waveInOpen(ref handle, wavemapper, ref format, Marshal.GetFunctionPointerForDelegate(waveIn), 0, CALLBACK_FUNCTION);           
            if (i != 0)
            {
                this.Text = "Error: waveInOpen";
                return;
            }

            setupBuffer();
          //  setupBuffer();//2nd buffer
            i = waveInStart(handle);
            if (i != 0)
            {
                this.Text = "Error: waveInStart" + i;
            }

        }
 
        public void callbackWaveIn(IntPtr deviceHandle, uint message, IntPtr instance, ref WAVEHDR wavehdr, IntPtr reserved2)
        {
            int i = 0;
            uint ii = 0;
     
            if (message == 0x3BF)
            {//WIM_CLOSE
           //     i = waveInUnprepareHeader(deviceHandle, ref wavehdr, Convert.ToUInt32(Marshal.SizeOf(wavehdr)));
           //     if (i != 0)
           //     {
           //         this.Text = "Error: waveInUnprepareHeader " + i;
           //     }
            }else
            if (message == 958)
            {//WIM_OPEN

 
                return;
            }else
            if (message == 0x3C0){//WIM_DATA
                //if (this.InvokeRequired)
                //    this.Invoke(waveIn, deviceHandle, message, instance, wavehdr, reserved2);
                //else
               {
                    if (wavehdr.dwBytesRecorded > 0)
                    {
                        byte[] temp = new byte[sampleLength + wavehdr.dwBytesRecorded];
                        Array.Copy(samples, temp, sampleLength);
                       
                        Array.Copy(buffer, 0, temp, sampleLength,wavehdr.dwBytesRecorded);
                        sampleLength += wavehdr.dwBytesRecorded;
                        samples = temp;
                   //     foreach (byte buf in buffer)
                    //    {

                                    //               form.addData(buffer);
                            // do something cool with your byte stream
                     //   }
                        i = waveInUnprepareHeader(deviceHandle, ref wavehdr, Convert.ToUInt32(Marshal.SizeOf(wavehdr)));
                        if (i != 0)
                        {
                            this.Text = "Error: waveInUnprepareHeader " + i;
                        }
                        setupBuffer();
                        
                    }
                    if (endRecording)
                    {
                        
                       
                    
   
                        
                        return;
                    }

    

                    }
            }else//SHOULD NOT GET HERE
                endRecording = true;
          
        }

        public RecordDialog(Form1 f)
        {
            InitializeComponent();
            form = f;
        }

        private void RecordDialog_Load(object sender, EventArgs e)
        {

        }
         
 
        unsafe void recordBtn_Click(object sender, EventArgs e)
        {


            setupWaveIn();
 

        }
 
        private void endRecordBtn_Click(object sender, EventArgs e)
        {
           
            uint ii = waveInStop(handle);
            endRecording = true;
            System.Threading.Thread.Sleep(200);
             ii = waveInReset(handle);
             ii = waveInClose(handle);

             form.addData(samples);
       
        }
    }
}
