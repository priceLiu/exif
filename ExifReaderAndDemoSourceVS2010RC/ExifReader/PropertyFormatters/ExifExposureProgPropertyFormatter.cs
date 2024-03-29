﻿// <copyright file="ExifExposureProgPropertyFormatter.cs" company="Nish Sivakumar">
// Copyright (c) Nish Sivakumar. All rights reserved.
// </copyright>

namespace ExifReader.PropertyFormatters
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// An IExifPropertyFormatter specific to the Exposure Program property
    /// </summary>
    internal class ExifExposureProgPropertyFormatter : GenericDictionaryPropertyFormatter<ushort>
    {
        /// <summary>
        /// Map of exposure value names to their unsigned short representations
        /// </summary>
        private Dictionary<ushort, string> exposureProgramNameMap = new Dictionary<ushort, string>()
        {
            { 0, "Not defined" },
            { 1, "Manual" },
            { 2, "Normal program" },
            { 3, "Aperture priority" },
            { 4, "Shutter priority" },
            { 5, "Creative program" },
            { 6, "Action program" },
            { 7, "Portrait mode" },
            { 8, "Landscape mode" }
        };

        /// <summary>
        /// Gets a display name for this property
        /// </summary>
        public override string DisplayName
        {
            get
            {
                return "Exposure program";
            }
        }

        /// <summary>
        /// Gets a dictionary that maps values to named strings
        /// </summary>
        /// <returns>The mapping dictionary</returns>
        protected override Dictionary<ushort, string> GetNameMap()
        {
            return this.exposureProgramNameMap;
        }
    }
}
