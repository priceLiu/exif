// <copyright file="ExifSaturationPropertyFormatter.cs" company="Nish Sivakumar">
// Copyright (c) Nish Sivakumar. All rights reserved.
// </copyright>

namespace ExifReader.PropertyFormatters
{
    using System.Collections.Generic;

    /// <summary>
    /// An IExifPropertyFormatter specific to the Saturation property
    /// </summary>
    internal class ExifSaturationPropertyFormatter : GenericDictionaryPropertyFormatter<ushort>
    {
        /// <summary>
        /// Map of SceneCaptureType names to their unsigned short representations
        /// </summary>
        private Dictionary<ushort, string> saturationMap = new Dictionary<ushort, string>()
        {
            { 0, "Normal" },
            { 1, "Low saturation" },
            { 2, "High saturation" }           
        };

        /// <summary>
        /// Gets a display name for this property
        /// </summary>
        public override string DisplayName
        {
            get
            {
                return "Saturation";
            }
        }

        /// <summary>
        /// Gets a dictionary that maps values to named strings
        /// </summary>
        /// <returns>The mapping dictionary</returns>
        protected override Dictionary<ushort, string> GetNameMap()
        {
            return this.saturationMap;
        }
    }
}
