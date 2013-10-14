// <copyright file="Rational32.cs" company="Nish Sivakumar">
// Copyright (c) Nish Sivakumar. All rights reserved.
// </copyright>

namespace ExifReader
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Struct that represents a rational number
    /// </summary>
    public struct Rational32 : IComparable, IComparable<Rational32>, IEquatable<Rational32>
    {
        /// <summary>
        /// Separator character for the string representation
        /// </summary>
        private const char SEPARATOR = '/';

        /// <summary>
        /// The numerator
        /// </summary>
        private CommonInt32 numerator;
        
        /// <summary>
        /// The denominator
        /// </summary>
        private CommonInt32 denominator;        

        /// <summary>
        /// Initializes a new instance of the Rational32 struct for signed use
        /// </summary>
        /// <param name="numerator">The numerator</param>
        /// <param name="denominator">The denominator</param>
        public Rational32(int numerator, int denominator)
            : this()
        {
            int gcd = Rational32.EuclidGCD(numerator, denominator);

            this.numerator = new CommonInt32(numerator / gcd);
            this.denominator = new CommonInt32(denominator / gcd);
        }

        /// <summary>
        /// Initializes a new instance of the Rational32 struct for unsigned use
        /// </summary>
        /// <param name="numerator">The numerator</param>
        /// <param name="denominator">The denominator</param>
        public Rational32(uint numerator, uint denominator)
            : this()
        {
            uint gcd = Rational32.EuclidGCD(numerator, denominator);

            this.numerator = new CommonInt32(numerator / gcd);
            this.denominator = new CommonInt32(denominator / gcd);
        }

        /// <summary>
        /// Gets the numerator
        /// </summary>
        public CommonInt32 Numerator
        {
            get { return this.numerator; }
        }

        /// <summary>
        /// Gets the denominator
        /// </summary>
        public CommonInt32 Denominator
        {
            get { return this.denominator; }
        }

        /// <summary>
        /// Explicit conversion operator to double
        /// </summary>
        /// <param name="rational">The source object</param>
        /// <returns>A double value representing the source object</returns>
        public static explicit operator double(Rational32 rational)
        {
            return rational.denominator.IsSigned ?
                (int)rational.denominator == 0 ? 0.0 : (double)(int)rational.numerator / (double)(int)rational.denominator :
                (uint)rational.denominator == 0 ? 0.0 : (double)(uint)rational.numerator / (double)(uint)rational.denominator;
        }

        /// <summary>
        /// Operator overload for GreaterThan
        /// </summary>
        /// <param name="x">Left value</param>
        /// <param name="y">Right value</param>
        /// <returns>True if the left instance is greater than the right instance</returns>
        public static bool operator >(Rational32 x, Rational32 y)
        {
            return (double)x > (double)y;
        }

        /// <summary>
        /// Operator overload for GreaterThanOrEqual
        /// </summary>
        /// <param name="x">Left value</param>
        /// <param name="y">Right value</param>
        /// <returns>True if the left instance is greater than or equal to the right instance</returns>
        public static bool operator >=(Rational32 x, Rational32 y)
        {
            return (double)x >= (double)y;
        }

        /// <summary>
        /// Operator overload for LesserThan
        /// </summary>
        /// <param name="x">Left value</param>
        /// <param name="y">Right value</param>
        /// <returns>True if the left instance is lesser than the right instance</returns>
        public static bool operator <(Rational32 x, Rational32 y)
        {
            return (double)x < (double)y;
        }

        /// <summary>
        /// Operator overload for LesserThanOrEqual
        /// </summary>
        /// <param name="x">Left value</param>
        /// <param name="y">Right value</param>
        /// <returns>True if the left instance is lesser than or equal to the right instance</returns>
        public static bool operator <=(Rational32 x, Rational32 y)
        {
            return (double)x <= (double)y;
        }

        /// <summary>
        /// Override for ToString
        /// </summary>
        /// <returns>The string representation</returns>
        public override string ToString()
        {
            return this.denominator.IsSigned ?
                String.Format("{0} {1} {2}", (int)this.numerator, Rational32.SEPARATOR, (int)this.denominator) :
                String.Format("{0} {1} {2}", (uint)this.numerator, Rational32.SEPARATOR, (uint)this.denominator);
        }

        /// <summary>
        /// Override for Equals
        /// </summary>
        /// <param name="obj">Object to check equality with</param>
        /// <returns>True if they are equal</returns>
        public override bool Equals(object obj)
        {
            return obj is Rational32 && this.Equals((Rational32)obj);
        }

        /// <summary>
        /// Tests if this instance is equal to another
        /// </summary>
        /// <param name="other">The other instance</param>
        /// <returns>True if they are equal</returns>
        public bool Equals(Rational32 other)
        {
            return this.numerator.Equals(other.numerator) && this.denominator.Equals(other.denominator);
        }

        /// <summary>
        /// Override for GetHashCode
        /// </summary>
        /// <returns>Returns a hash code for this object</returns>
        public override int GetHashCode()
        {
            int primeSeed = 29;
            return unchecked((this.numerator.GetHashCode() + primeSeed) * this.denominator.GetHashCode());
        }

        /// <summary>
        /// Compares this instance with an object
        /// </summary>
        /// <param name="obj">An object to compare with this instance</param>
        /// <returns>Zero of equal, 1 if greater than, and -1 if less than the compared to object</returns>
        public int CompareTo(object obj)
        {
            if (obj == null)
            {
                return 1;
            }

            if (!(obj is Rational32))
            {
                throw new ArgumentException("Rational32 expected");
            }

            return this.CompareTo((Rational32)obj);
        }

        /// <summary>
        /// Compares this instance with another
        /// </summary>
        /// <param name="other">A Rational32 object to compare with this instance</param>
        /// <returns>Zero of equal, 1 if greater than, and -1 if less than the compared to object</returns>
        public int CompareTo(Rational32 other)
        {
            if (this.Equals(other))
            {
                return 0;
            }

            return ((double)this).CompareTo((double)other);
        }

        /// <summary>
        /// Calculates the GCD for two signed ints
        /// </summary>
        /// <param name="x">First signed int</param>
        /// <param name="y">Second signed int</param>
        /// <returns>The GCD for the two numbers</returns>
        private static int EuclidGCD(int x, int y)
        {
            return y == 0 ? x : EuclidGCD(y, x % y);
        }

        /// <summary>
        /// Calculates the GCD for two unsigned ints
        /// </summary>
        /// <param name="x">First unsigned int</param>
        /// <param name="y">Second unsigned int</param>
        /// <returns>The GCD for the two numbers</returns>
        private static uint EuclidGCD(uint x, uint y)
        {
            return y == 0 ? x : EuclidGCD(y, x % y);
        }
    }
}
