

public struct wavFile
{
    public int RIFF;
    public int filesize_minus_4;
    public int WAVE;

    public int fmt;
    public int fmt_size;
    public short format_tag;
    public short nchannels;
    public int samples_per_sec;
    public int avg_bytes_per_sec;
    public short nblock_align;
    public short bits_per_sample;

    public int data_size;
    public int[] data;
    
};





