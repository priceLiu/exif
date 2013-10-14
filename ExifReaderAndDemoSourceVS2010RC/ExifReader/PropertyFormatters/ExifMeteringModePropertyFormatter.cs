// <copyright file="ExifMeteringModePropertyFormatter.cs" company="Nish Sivakumar">
// Copyright (c) Nish Sivakumar. All rights reserved.
// </copyright>

namespace ExifReader.PropertyFormatters
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;

    /// <summary>
    /// An IExifPropertyFormatter specific to the Metering Mode property
    /// </summary>
    internal class ExifMeteringModePropertyFormatter : GenericDictionaryPropertyFormatter<ushort>
    {
        /// <summary>
        /// Map of metering modes to their unsigned short representations
        /// </summary>
        private Dictionary<ushort, string> meteringModeMap = new Dictionary<ushort, string>()
        {
            { 0, "Unknown" },
            { 1, "Average" },
            { 2, "Center-Weighted" },
            { 3, "Spot" },
            { 4, "Multi-Spot" },
            { 5, "Pattern" },
            { 6, "Partial" },
            { 255, "Other" }
        };

        /// <summary>
        /// Gets a display name for this property
        /// </summary>
        public override string DisplayName
        {
            get { return "Metering Mode"; }
        }

        /// <summary>
        /// Gets a dictionary that maps values to named strings
        /// </summary>
        /// <returns>The mapping dictionary</returns>
        protected override Dictionary<ushort, string> GetNameMap()
        {
            return this.meteringModeMap;
        }
    }
}
