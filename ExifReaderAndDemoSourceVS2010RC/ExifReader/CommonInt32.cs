﻿// <copyright file="CommonInt32.cs" company="Nish Sivakumar">
// Copyright (c) Nish Sivakumar. All rights reserved.
// </copyright>

namespace ExifReader
{
    using System;
    using System.Runtime.InteropServices;

    /// <summary>
    /// A struct that can efficiently represent either an int or an uint
    /// </summary>
    [StructLayout(LayoutKind.Explicit)]
    public struct CommonInt32 : IEquatable<CommonInt32>
    {
        /// <summary>
        /// Integer value
        /// </summary>
        [FieldOffset(0)]
        private int integer;

        /// <summary>
        /// Unsigned integer value
        /// </summary>
        [FieldOffset(0)]
        private uint uinteger;

        /// <summary>
        /// True if this is a signed value
        /// </summary>
        [FieldOffset(4)]
        private bool isSigned;

        /// <summary>
        /// Initializes a new instance of the CommonInt32 struct
        /// </summary>
        /// <param name="integer">The int value</param>
        public CommonInt32(int integer)
            : this()
        {
            this.integer = integer;
            this.isSigned = true;
        }

        /// <summary>
        /// Initializes a new instance of the CommonInt32 struct
        /// </summary>
        /// <param name="uinteger">The uint value</param>
        public CommonInt32(uint uinteger)
            : this()
        {
            this.uinteger = uinteger;
            this.isSigned = false;
        }

        /// <summary>
        /// Gets a value indicating whether the value of this struct is signed or not
        /// </summary>
        public bool IsSigned
        {
            get
            {
                return this.isSigned;
            }
        }

        /// <summary>
        /// Explicit operator overload to int
        /// </summary>
        /// <param name="commonInt">Source object to convert</param>
        /// <returns>The int value</returns>
        public static explicit operator int(CommonInt32 commonInt)
        {
            return commonInt.integer;
        }

        /// <summary>
        /// Explicit operator overload to uint
        /// </summary>
        /// <param name="commonInt">Source object to convert</param>
        /// <returns>The uint value</returns>
        public static explicit operator uint(CommonInt32 commonInt)
        {
            return commonInt.uinteger;
        }

        /// <summary>
        /// Override for Equals
        /// </summary>
        /// <param name="obj">Object to check equality with</param>
        /// <returns>True if they are equal</returns>
        public override bool Equals(object obj)
        {
            return obj is CommonInt32 && this.Equals((CommonInt32)obj);
        }

        /// <summary>
        /// Tests if this instance is equal to another
        /// </summary>
        /// <param name="other">The other instance</param>
        /// <returns>True if they are equal</returns>
        public bool Equals(CommonInt32 other)
        {
            if (this.isSigned != other.isSigned)
            {
                return false;
            }

            return this.isSigned ?
                this.integer.Equals(other.integer) :
                this.uinteger.Equals(other.uinteger);
        }

        /// <summary>
        /// Override for GetHashCode
        /// </summary>
        /// <returns>Returns a hash code for this object</returns>
        public override int GetHashCode()
        {
            return this.isSigned ? this.integer.GetHashCode() : this.uinteger.GetHashCode();
        }
    }
}
