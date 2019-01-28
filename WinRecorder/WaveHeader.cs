using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;


namespace WindowsFormsApplication1
{
    //[StructLayout(LayoutKind.Sequential)]
    //unsafe public class WaveHeader
    //{
    //    public System.Byte[] lpData;
    //    public System.UInt32 dwBufferLength;
    //    public System.UInt32 dwBytesRecorded;
    //    public System.UInt32* dwUser;
    //    public System.UInt32 dwFlags;
    //    public System.UInt32 dwLoops;
    //    public WaveHeader  lpNext;
    //    public System.UInt32* reserved;
    //}
    [StructLayout(LayoutKind.Sequential)]
    public struct WAVEHDR
    {
        public IntPtr lpData;
        public uint dwBufferLength;
        public uint dwBytesRecorded;
        public IntPtr dwUser;
        public uint dwFlags;
        public uint dwLoops;
        public IntPtr lpNext;
        public IntPtr reserved;
    }

}
