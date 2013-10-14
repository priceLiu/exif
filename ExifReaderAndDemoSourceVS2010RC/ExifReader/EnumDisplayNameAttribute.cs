// <copyright file="EnumDisplayNameAttribute.cs" company="Nish Sivakumar">
// Copyright (c) Nish Sivakumar. All rights reserved.
// </copyright>

namespace ExifReader
{
    using System;
    using System.ComponentModel;
    using System.Linq;

    /// <summary>
    /// An attribute used to specify display names for enum values
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    internal class EnumDisplayNameAttribute : DisplayNameAttribute
    {
        /// <summary>
        /// Initializes a new instance of the EnumDisplayNameAttribute class.
        /// </summary>
        public EnumDisplayNameAttribute()
        {
        }

        /// <summary>
        /// Initializes a new instance of the EnumDisplayNameAttribute class using the display name.
        /// </summary>
        /// <param name="displayName">The display Name</param>
        public EnumDisplayNameAttribute(string displayName)
            : base(displayName)
        {
        }
    }
}
