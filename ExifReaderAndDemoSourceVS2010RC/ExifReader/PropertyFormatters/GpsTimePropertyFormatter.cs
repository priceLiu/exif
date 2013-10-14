// <copyright file="GpsTimePropertyFormatter.cs" company="Nish Sivakumar">
// Copyright (c) Nish Sivakumar. All rights reserved.
// </copyright>

namespace ExifReader.PropertyFormatters
{
    using System;
    using System.ComponentModel;
    using System.Linq;

    /// <summary>
    /// An IExifPropertyFormatter specific to the Gps Time property
    /// </summary>
    internal class GpsTimePropertyFormatter : IExifPropertyFormatter
    {
        /// <summary>
        /// Gets a display name for this property
        /// </summary>
        public string DisplayName
        {
            get
            {
                return "GPS Time";
            }
        }

        /// <summary>
        /// Gets a formatted string for a given Exif value
        /// </summary>
        /// <param name="exifValue">The source Exif value</param>
        /// <returns>The formatted string</returns>
        public string GetFormattedString(IExifValue exifValue)
        {
            var values = exifValue.Values.Cast<Rational32>();
            if (values.Count() != 3)
            {
                return String.Empty;
            }

            return String.Format("{0}:{1}:{2}", (int)(double)values.ElementAt(0), (int)(double)values.ElementAt(1), (int)(double)values.ElementAt(2));
        }
    }
}
