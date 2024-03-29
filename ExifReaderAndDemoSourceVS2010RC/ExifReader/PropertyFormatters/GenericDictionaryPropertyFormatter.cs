﻿// <copyright file="GenericDictionaryPropertyFormatter.cs" company="Nish Sivakumar">
// Copyright (c) Nish Sivakumar. All rights reserved.
// </copyright>

namespace ExifReader.PropertyFormatters
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// An IExifPropertyFormatter base implementation for formatters that use a basic dictionary to map values to names
    /// </summary>
    /// <typeparam name="VTYPE">The type of the value that maps to the string</typeparam>
    internal abstract class GenericDictionaryPropertyFormatter<VTYPE> : IExifPropertyFormatter
    {
        /// <summary>
        /// Gets a display name for this property
        /// </summary>
        public abstract string DisplayName { get; }

        /// <summary>
        /// Gets a formatted string for a given Exif value
        /// </summary>
        /// <param name="exifValue">The source Exif value</param>
        /// <returns>The formatted string</returns>
        public string GetFormattedString(IExifValue exifValue)
        {
            var values = exifValue.Values.Cast<VTYPE>();

            return this.GetStringValueInternal(values.FirstOrDefault());
        }

        /// <summary>
        /// Gets a dictionary that maps values to named strings
        /// </summary>
        /// <returns>The mapping dictionary</returns>
        protected abstract Dictionary<VTYPE, string> GetNameMap();

        /// <summary>
        /// Gets the reserved string for values not in the dictionary
        /// </summary>
        /// <returns>The reserved string</returns>
        protected virtual string GetReservedValue()
        {
            return "Reserved";
        }

        /// <summary>
        /// Returns an Exif Light Source from a VTYPE value
        /// </summary>
        /// <param name="value">The VTYPE value</param>
        /// <returns>The string value</returns>
        private string GetStringValueInternal(VTYPE value)
        {
            string stringValue;

            if (!this.GetNameMap().TryGetValue(value, out stringValue))
            {
                stringValue = this.GetReservedValue();
            }

            return stringValue;
        }
    }
}
