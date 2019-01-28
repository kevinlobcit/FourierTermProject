using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace WindowsFormsApplication1
{

    //    [StructLayout(LayoutKind.Sequential)]
    //unsafe public class WaveFormatEx
    //{
    //        public System.UInt16 wFormatTag;
    //        public System.UInt16 nChannels;
    //        public System.UInt32 nSamplesPerSec;
    //        public System.UInt32 nAvgBytesPerSec;
    //        public System.UInt16 nBlockAlign;
    //        public System.UInt16 wBitsPerSample;
    //        public System.UInt16 cbSize;
    //}
        [StructLayout(LayoutKind.Sequential)]
        public struct WAVEFORMAT
        {
            public ushort wFormatTag;
            public ushort nChannels;
            public uint nSamplesPerSec;
            public uint nAvgBytesPerSec;
            public ushort nBlockAlign;
            public ushort wBitsPerSample;
            public ushort cbSize;
        }
}
