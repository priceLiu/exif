// <copyright file="GpsLatitudeLongitudePropertyFormatter.cs" company="Nish Sivakumar">
// Copyright (c) Nish Sivakumar. All rights reserved.
// </copyright>

namespace ExifReader.PropertyFormatters
{
    using System;
    using System.ComponentModel;
    using System.Linq;

    /// <summary>
    /// An IExifPropertyFormatter specific to the Gps Latitude and Longitude properties
    /// </summary>
    internal class GpsLatitudeLongitudePropertyFormatter : SimpleExifPropertyFormatter
    {
        /// <summary>
        /// Initializes a new instance of the GpsLatitudeLongitudePropertyFormatter class.
        /// </summary>
        /// <param name="tagId">The associated PropertyTagId</param>
        public GpsLatitudeLongitudePropertyFormatter(PropertyTagId tagId)
            : base(tagId)
        {            
        }

        /// <summary>
        /// Gets a formatted string for a given Exif value
        /// </summary>
        /// <param name="exifValue">The source Exif value</param>
        /// <returns>The formatted string</returns>
        public override string GetFormattedString(IExifValue exifValue)
        {
            var values = exifValue.Values.Cast<Rational32>();
            if (values.Count() != 3)
            {
                return String.Empty;
            }

            return String.Format("{0}; {1}; {2}", (double)values.ElementAt(0), (double)values.ElementAt(1), (double)values.ElementAt(2));
        }
    }
}
