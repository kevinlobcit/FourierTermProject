using System;

public class Complex
{
    class Complex
    {
        double r;
        double i;

        public Complex(double real, double imaginary)
        {
            r = real;
            i = imaginary;
        }

        //Adding
        public double addReal(Complex complex1, Complex complex2)
        {
            return complex1.r + complex2.r;
        }

        public double addImaginary(Complex complex1, Complex complex2)
        {
            return complex1.i + complex2.i;
        }

        //Subtraction
        public double subReal(Complex complex1, Complex complex2)
        {
            return complex1.r - complex2.r;
        }

        public double subImaginary(Complex complex1, Complex complex2)
        {
            return complex1.r - complex2.r;
        }

        //Multiplication
        public double multReal(Complex complex1, Complex complex2)
        {
            return (complex1.r * complex2.r) - (complex1.i * complex2.i);
        }

        public double multImaginary(Complex complex1, Complex complex2)
        {
            return (complex1.r * complex2.i) + (complex1.i * complex2.r);
        }

    }
}

