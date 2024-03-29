﻿// <copyright file="ResolutionUnitPropertyFormatter.cs" company="Nish Sivakumar">
// Copyright (c) Nish Sivakumar. All rights reserved.
// </copyright>

namespace ExifReader.PropertyFormatters
{
    using System;
    using System.ComponentModel;
    using System.Linq;

    /// <summary>
    /// An IExifPropertyFormatter specific to the Resolution Unit property
    /// </summary>
    internal class ResolutionUnitPropertyFormatter : IExifPropertyFormatter
    {
        /// <summary>
        /// Gets a display name for this property
        /// </summary>
        public virtual string DisplayName
        {
            get { return "Resolution Unit"; }
        }

        /// <summary>
        /// Gets a formatted string for a given Exif value
        /// </summary>
        /// <param name="exifValue">The source Exif value</param>
        /// <returns>The formatted string</returns>
        public virtual string GetFormattedString(IExifValue exifValue)
        {
            var values = exifValue.Values.Cast<ushort>();
            string formattedString = String.Empty;

            switch (values.FirstOrDefault())
            {
                case 2:
                    formattedString = "Inches";
                    break;

                case 3:
                    formattedString = "Centimeters";
                    break;

                default:
                    formattedString = "Reserved";
                    break;
            }

            return formattedString;
        }
    }
}
