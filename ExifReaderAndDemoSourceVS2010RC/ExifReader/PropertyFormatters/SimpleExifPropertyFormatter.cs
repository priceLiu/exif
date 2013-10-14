// <copyright file="SimpleExifPropertyFormatter.cs" company="Nish Sivakumar">
// Copyright (c) Nish Sivakumar. All rights reserved.
// </copyright>

namespace ExifReader.PropertyFormatters
{
    using System;
    using System.ComponentModel;
    using System.Linq;
    using System.Text.RegularExpressions;

    /// <summary>
    /// A very simple implementation of IExifPropertyFormatter that's used by default
    /// if a more specialized implementation is not available.
    /// </summary>
    internal class SimpleExifPropertyFormatter : IExifPropertyFormatter
    {
        /// <summary>
        /// The associated PropertyTagId
        /// </summary>
        private PropertyTagId tagId;

        /// <summary>
        /// The display name attribute if one's available
        /// </summary>
        private EnumDisplayNameAttribute displayNameAttribute;

        /// <summary>
        /// Initializes a new instance of the SimpleExifPropertyFormatter class.
        /// </summary>
        /// <param name="tagId">The associated PropertyTagId</param>
        public SimpleExifPropertyFormatter(PropertyTagId tagId)
        {
            this.tagId = tagId;
            this.displayNameAttribute = CachedAttributeExtractor<PropertyTagId, EnumDisplayNameAttribute>.Instance.GetAttributeForField(this.tagId.ToString());
        }

        /// <summary>
        /// Gets a display name for this property. This default implementation checks to
        /// see if a display name is provided, and if one is not, then it attempts a rather 
        /// crude enhancement and separates out words heuristically by spliting them 
        /// up based on an uppercase letter following a lowercase one.
        /// </summary>
        public virtual string DisplayName
        {
            get
            {
                return this.displayNameAttribute != null ?
                    this.displayNameAttribute.DisplayName :
                    Regex.Replace(this.tagId.ToString(), @"([a-z])([A-Z])", @"$1 $2", RegexOptions.None);
            }
        }

        /// <summary>
        /// Gets a formatted string for a given Exif value
        /// </summary>
        /// <param name="exifValue">The source Exif value</param>
        /// <returns>The formatted string</returns>
        public virtual string GetFormattedString(IExifValue exifValue)
        {
            string firstValue = String.Empty;

            foreach (var item in exifValue.Values)
            {
                firstValue = item.ToString();
                break;
            }

            return firstValue;
        }
    }        
}
