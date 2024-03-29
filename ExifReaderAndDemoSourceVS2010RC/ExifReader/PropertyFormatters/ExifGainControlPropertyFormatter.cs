﻿// <copyright file="ExifGainControlPropertyFormatter.cs" company="Nish Sivakumar">
// Copyright (c) Nish Sivakumar. All rights reserved.
// </copyright>

namespace ExifReader.PropertyFormatters
{
    using System.Collections.Generic;

    /// <summary>
    /// An IExifPropertyFormatter specific to the Gain Control property
    /// </summary>
    internal class ExifGainControlPropertyFormatter : GenericDictionaryPropertyFormatter<ushort>
    {
        /// <summary>
        /// Map of SceneCaptureType names to their unsigned short representations
        /// </summary>
        private Dictionary<ushort, string> gainControlMap = new Dictionary<ushort, string>()
        {
            { 0, "None" },
            { 1, "Low gain up" },
            { 2, "High gain up" },
            { 3, "Low gain down" },
            { 4, "High gain down" }
        };

        /// <summary>
        /// Gets a display name for this property
        /// </summary>
        public override string DisplayName
        {
            get
            {
                return "Gain Control";
            }
        }

        /// <summary>
        /// Gets a dictionary that maps values to named strings
        /// </summary>
        /// <returns>The mapping dictionary</returns>
        protected override Dictionary<ushort, string> GetNameMap()
        {
            return this.gainControlMap;
        }
    }
}
