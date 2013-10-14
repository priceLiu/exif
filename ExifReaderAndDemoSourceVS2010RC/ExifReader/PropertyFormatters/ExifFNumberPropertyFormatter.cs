// <copyright file="ExifFNumberPropertyFormatter.cs" company="Nish Sivakumar">
// Copyright (c) Nish Sivakumar. All rights reserved.
// </copyright>

namespace ExifReader.PropertyFormatters
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// An IExifPropertyFormatter specific to the F-Stop property 
    /// </summary>
    internal class ExifFNumberPropertyFormatter : SimpleExifPropertyFormatter
    {
        /// <summary>
        /// Initializes a new instance of the ExifFNumberPropertyFormatter class.
        /// </summary>
        /// <param name="tagId">The associated PropertyTagId</param>
        public ExifFNumberPropertyFormatter(PropertyTagId tagId)
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
            return values.Count() == 0 ? String.Empty : String.Format("f/{0:g3}", (double)values.First());
        }
    }
}
