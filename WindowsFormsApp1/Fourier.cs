using System;
using System.Threading;

public class Fourier
{
    public Fourier()
    {
    }

    public double[] GenerateSamples(int frequency, int N, double amplitude)
    {

        //Generates Samples
        double[] samples = new double[N];
        for (int t = 0; t < N; t++)
        {
            //Generate the sample
            samples[t] = amplitude * Math.Sin(2 * Math.PI * frequency * t / N) + amplitude* Math.Sin(4 * Math.PI * frequency * t / N);
        }

        

        return samples;

    }



    public Complex[] FourierTrans(double[] samples, int N, System.Windows.Forms.DataVisualization.Charting.Chart chart)
    {
        int numSamp = samples.Length;
        Complex[] amplitude = new Complex[numSamp];

        for (int f = 0; f < numSamp; f++)
        {
            amplitude[f] = new Complex();
            for (int t = 0; t < numSamp; t++)
            {
                amplitude[f].AddReal(samples[t] * Math.Cos(2 * Math.PI * t * f / numSamp));
                amplitude[f].SubImaginary(samples[t] * Math.Sin(2 * Math.PI * t * f / numSamp));
            }
            double magnitude = Math.Sqrt(amplitude[f].GetReal() * amplitude[f].GetReal()
                + amplitude[f].GetImaginary() * amplitude[f].GetImaginary());
            double angle = Math.Atan2(amplitude[f].GetReal(), amplitude[f].GetImaginary());

            chart.Series["freqChart"].Points.AddXY(f, magnitude);
        }


        return amplitude;

        //chart.Series["freqChart"].Points.AddXY(1, 5);
    }

    //Select a start and end point for where the samples to take will be
    
    public void FourierTransArea(double[] samples, int start, int end,double[] aMagnitude,Complex[] amplitude)
    {
        int numSamp = end-start;
        //Complex[] amplitude = new Complex[numSamp];
        //double[] aMagnitude = new double[numSamp];

        for(int f = 0; f < numSamp; f++)
        {
            amplitude[f] = new Complex();
            for(int t = 0; t < numSamp; t++)
            {
                double baseExpr = Math.PI * 2 * t * f/ numSamp;
                amplitude[f].AddReal(samples[t + start]*Math.Cos(baseExpr));
                amplitude[f].SubImaginary(samples[t + start]*Math.Sin(baseExpr));
            }
            double pythag = amplitude[f].GetReal() * amplitude[f].GetReal();
            pythag += amplitude[f].GetImaginary() * amplitude[f].GetImaginary();
            pythag = Math.Sqrt(pythag);
        
            aMagnitude[f] = pythag;
        }
    }

    //Main of the threaded fourier which creates all the threads
    public void threadedFourierTransArea(double[] samples, int start, int end, double[] aMagnitude, Complex[] amplitude)
    {
        int numSamp = end - start;
        int offset = numSamp / 4;

        int startingF1 = 0;
        int endingF1 = offset;

        int startingF2 = offset;
        int endingF2 = offset*2;

        int startingF3 = offset*2;
        int endingF3 = offset * 3;

        int startingF4 = offset*3;
        int endingF4 = numSamp;

        //Console.WriteLine(numSamp);

        //for(int i = 0; i < numSamp; i++)
        //{
        //    amplitude[i] = new Complex();
        //}

        Thread fourierThread1 = new Thread(() => innerFourierTrans(samples, start, numSamp, aMagnitude, amplitude, startingF1, endingF1));
        fourierThread1.Start();

        Thread fourierThread2 = new Thread(() => innerFourierTrans(samples, start, numSamp, aMagnitude, amplitude, startingF2, endingF2));
        fourierThread2.Start();


        Thread fourierThread3 = new Thread(() => innerFourierTrans(samples, start, numSamp, aMagnitude, amplitude, startingF3, endingF3));
        fourierThread3.Start();

        
        Thread fourierThread4 = new Thread(() => innerFourierTrans(samples, start, numSamp, aMagnitude, amplitude, startingF4, endingF4));
        fourierThread4.Start();

        fourierThread1.Join();
        fourierThread2.Join();
        fourierThread3.Join();
        fourierThread4.Join();
    }

    public void innerFourierTrans(double[] samples, int start, int numSamp, double[] aMagnitude, Complex[] amplitude, int f2, int fEnd)
    {
        //int numSamp = end - start;
        //Complex[] amplitude = new Complex[numSamp];
        //double[] aMagnitude = new double[numSamp];
        for (int f = f2; f < numSamp && f < fEnd; f++)
        {
            //Console.WriteLine("Thread doing " + f);
            //amplitude[f] = new Complex();
            amplitude[f] = new Complex();
            for (int t = 0; t < numSamp; t++)
            {
                double baseExpr = Math.PI * 2 * t * f / numSamp;
                amplitude[f].AddReal(samples[t + start] * Math.Cos(baseExpr));
                amplitude[f].SubImaginary(samples[t + start] * Math.Sin(baseExpr));
            }
            double pythag = amplitude[f].GetReal() * amplitude[f].GetReal();
            pythag += amplitude[f].GetImaginary() * amplitude[f].GetImaginary();
            pythag = Math.Sqrt(pythag);

            aMagnitude[f] = pythag;
        }
    }
    
    //The part being run in fourier threads
    public void invFourier(Complex[] complex, double[] sample)
    {
        int f = complex.Length;
        //double[] sample = new double[complx.Length];

        for (int t = 0; t < f; t++)
        {
            Complex sum = new Complex();
            for(int freq = 0; freq < f; freq++)
            {
                double baseExpr = (double)2 * Math.PI * t * freq / f;
                sample[t] += complex[freq].GetReal() * Math.Cos(baseExpr) - complex[freq].GetImaginary() * Math.Sin(baseExpr);
                
            }
            sample[t] = sample[t]/f;
        }

    }

    //Threaded version of idft
    public void threadedInvFourier(Complex[] complex, double[] sample)
    {
        int f = complex.Length;

        int offset = f / 4;

        int startingF1 = 0;
        int endingF1 = offset;

        int startingF2 = offset;
        int endingF2 = offset * 2;

        int startingF3 = offset * 2;
        int endingF3 = offset * 3;

        int startingF4 = offset * 3;
        int endingF4 = f;

        //Console.WriteLine(numSamp);

        //for(int i = 0; i < numSamp; i++)
        //{
        //    amplitude[i] = new Complex();
        //}

        Thread ifourierThread1 = new Thread(() => innerInvFourier(complex, sample, startingF1, endingF1, f));
        ifourierThread1.Start();

        Thread ifourierThread2 = new Thread(() => innerInvFourier(complex, sample, startingF2, endingF2, f));
        ifourierThread2.Start();


        Thread ifourierThread3 = new Thread(() => innerInvFourier(complex, sample, startingF3, endingF3, f));
        ifourierThread3.Start();


        Thread ifourierThread4 = new Thread(() => innerInvFourier(complex, sample, startingF4, endingF4, f));
        ifourierThread4.Start();

        ifourierThread1.Join();
        ifourierThread2.Join();
        ifourierThread3.Join();
        ifourierThread4.Join();

        //double[] sample = new double[complx.Length];

        //for (int t = 0; t < f; t++)
        //{
        //    Complex sum = new Complex();
        //    for (int freq = 0; freq < f; freq++)
        //    {
        //        double baseExpr = (double)2 * Math.PI * t * freq / f;
        //        sample[t] += complex[freq].GetReal() * Math.Cos(baseExpr) - complex[freq].GetImaginary() * Math.Sin(baseExpr);
        //
        //    }
        //    sample[t] = sample[t] / f;
        //}

    }

    //The part being run by idft threads
    public void innerInvFourier(Complex[] complex, double[] sample, int start, int end, int f)
    {
        //int f = complex.Length;
        //double[] sample = new double[complx.Length];

        for (int t = start; t < f && t < end; t++)
        {
            Complex sum = new Complex();
            for (int freq = 0; freq < f; freq++)
            {
                double baseExpr = (double)2 * Math.PI * t * freq / f;
                sample[t] += complex[freq].GetReal() * Math.Cos(baseExpr) - complex[freq].GetImaginary() * Math.Sin(baseExpr);

            }
            sample[t] = sample[t] / f;
        }

    }

    //public void FourierTransInt(int[] samples, int N, System.Windows.Forms.DataVisualization.Charting.Chart chart)
    //{
    //    Complex[] amplitude = new Complex[N];
    //    for (int f = 0; f < N; f++)
    //    {
    //        amplitude[f] = new Complex();
    //        for (int t = 0; t < N; t++)
    //        {
    //            amplitude[f].AddReal((double)samples[t] * Math.Cos(2 * Math.PI * t * f / N));
    //            amplitude[f].SubImaginary((double)samples[t] * Math.Sin(2 * Math.PI * t * f / N));
    //        }
    //        double magnitude = Math.Sqrt(amplitude[f].GetReal() * amplitude[f].GetReal()
    //            + amplitude[f].GetImaginary() * amplitude[f].GetImaginary());
    //        double angle = Math.Atan2(amplitude[f].GetReal(), amplitude[f].GetImaginary());
    //
    //        chart.Series["freqChart"].Points.AddXY(f, magnitude);
    //    }
    //
    //
    //    //return amplitude;
    //
    //    //chart.Series["freqChart"].Points.AddXY(1, 5);
    //}


}
