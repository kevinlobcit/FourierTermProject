using System;

public class Fourier
{
	public Fourier()
	{
	}


    public double[] FourierTrans(double frequency, double N)
    {
        double[] samples = new double[N];
        for (int t = 0; t < N; t++)
        {
            samples[t] = Math.Cos(2 * Math.PI * frequency * t / N);
            //Console.Write(samples[t]);
        }
        Complex[] complex = new Complex[N];

        double[] amplitude = new double[N];
        double st = 0;

        return samples;

    }
}
