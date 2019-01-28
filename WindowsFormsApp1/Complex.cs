using System;

public class Complex
{
    double r;
    double i;

    public Complex()
    {
        r = 0;
        i = 0;
    }

    public Complex(double real, double imaginary)
    {
        r = real;
        i = imaginary;
    }

    public void setReal(double real)
    {
        r = real;
    }

    public void setImag(double imaginary)
    {
        i = imaginary;
    }

    public void AddReal(double toAdd)
    {
        r += toAdd;
    }

    public void AddImaginary(double toAdd)
    {
        i += toAdd;
    }

    public void SubImaginary(double toSub)
    {
        i -= toSub;
    }

    public double GetReal()
    {
        return r;
    }

    public double GetImaginary()
    {
        return i;
    }

    //Adding
    public double AddReal(Complex complex1, Complex complex2)
    {
        return complex1.r + complex2.r;
    }

    public double AddImaginary(Complex complex1, Complex complex2)
    {
        return complex1.i + complex2.i;
    }

    //Subtraction
    public double SubReal(Complex complex1, Complex complex2)
    {
        return complex1.r - complex2.r;
    }

    public double SubImaginary(Complex complex1, Complex complex2)
    {
        return complex1.r - complex2.r;
    }

    //Multiplication
    public double MultReal(Complex complex1, Complex complex2)
    {
        return (complex1.r * complex2.r) - (complex1.i * complex2.i);
    }

    public double MultImaginary(Complex complex1, Complex complex2)
    {
        return (complex1.r * complex2.i) + (complex1.i * complex2.r);
    }
}

