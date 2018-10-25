// <copyright file="MastercamPaths.cs" company="Mick George @Osoy">
// Copyright (c) Mick George @Osoy. All rights reserved.
// </copyright>

namespace SHOP
{
    using Mastercam.IO;
    using System.IO;

    public static class MastercamPaths
    {
        /// <summary> The solid holes preset file location.  </summary>
        public static string SolidHolesPresetFileLocation => Path.Combine(SettingsManager.SharedDirectory, "common\\FBM");

        /// <summary> Path to the solid holes preset file xml. </summary>
        public static string SolidHolesPresetFile => Path.Combine(SolidHolesPresetFileLocation, "SolidsHoleOperationPresets.xml");

        /// <summary> The solid holes preset file backup. </summary>
        public static string SolidHolesPresetFileBackup => Path.Combine(SolidHolesPresetFileLocation, "SolidsHoleOperationPresets.xml.bak");

        /// <summary> Path to the solid holes preset schema. </summary>
        public static string SolidHolesPresetSchema => Path.Combine(SolidHolesPresetFileLocation, "SolidsHoleOperationPresets.xsd");

        /// <summary> The solid holes preset schema backup. </summary>
        public static string SolidHolesPresetSchemaBackup => Path.Combine(SolidHolesPresetFileLocation, "SolidsHoleOperationPresets.xsd.bak");
    }
}