// <copyright file="ExifShutterSpeedPropertyFormatter.cs" company="Nish Sivakumar">
// Copyright (c) Nish Sivakumar. All rights reserved.
// </copyright>

namespace ExifReader.PropertyFormatters
{
    using System;
    using System.ComponentModel;
    using System.Linq;

    /// <summary>
    /// An IExifPropertyFormatter specific to the Exif shutter-speed property
    /// </summary>
    internal class ExifShutterSpeedPropertyFormatter : IExifPropertyFormatter
    {
        /// <summary>
        /// Gets a display name for this property
        /// </summary>
        public string DisplayName
        {
            get
            {
                return "Shutter Speed";
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

            if (values.Count() == 0)
            {
                return String.Empty;
            }

            double apexValue = (double)values.First();
            double shutterSpeed = 1 / Math.Pow(2, apexValue);

            return shutterSpeed > 1 ?
                String.Format("{0} sec.", (int)Math.Round(shutterSpeed)) :
                String.Format("{0}/{1} sec.", 1, (int)Math.Round(1 / shutterSpeed));            
        }
    }
}
