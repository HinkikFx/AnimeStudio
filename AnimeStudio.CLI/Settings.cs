using System;
using System.ComponentModel;
using System.Configuration;

namespace AnimeStudio.CLI.Properties {
    public static class AppSettings
    {
        public static string Get(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }

        public static TValue Get<TValue>(string key, TValue defaultValue)
        {
            try
            {
                var value = Get(key);

                if (string.IsNullOrEmpty(value)) 
                    return defaultValue;

                return (TValue)TypeDescriptor.GetConverter(typeof(TValue)).ConvertFromInvariantString(value);
            }
            catch (Exception)
            {
                Console.WriteLine($"Invalid value at \"{key}\", switching to default value [{defaultValue}] !!");
                return defaultValue;
            }
            
        }
    }

    public class Settings
    {
        private static Settings defaultInstance = new Settings();

        public static Settings Default => defaultInstance;

        private bool? _convertTexture;
        private bool? _convertAudio;
        private ImageFormat? _convertType;
        private bool? _eulerFilter;
        private decimal? _filterPrecision;
        private bool? _exportAllNodes;
        private bool? _exportSkins;
        private bool? _exportMaterials;
        private bool? _collectAnimations;
        private bool? _exportAnimations;
        private decimal? _boneSize;
        private int? _fbxVersion;
        private int? _fbxFormat;
        private decimal? _scaleFactor;
        private bool? _exportBlendShape;
        private bool? _castToBone;
        private bool? _restoreExtensionName;
        private bool? _enableFileLogging;
        private bool? _minimalAssetMap;
        private bool? _allowDuplicates;
        private string _types;
        private string _texs;
        private string _uvs;

        public bool convertTexture => _convertTexture ?? AppSettings.Get("convertTexture", true);
        public bool convertAudio => _convertAudio ?? AppSettings.Get("convertAudio", true);
        public ImageFormat convertType => _convertType ?? AppSettings.Get("convertType", ImageFormat.Png);
        public bool eulerFilter => _eulerFilter ?? AppSettings.Get("eulerFilter", true);
        public decimal filterPrecision => _filterPrecision ?? AppSettings.Get("filterPrecision", (decimal)0.25);
        public bool exportAllNodes => _exportAllNodes ?? AppSettings.Get("exportAllNodes", true);
        public bool exportSkins => _exportSkins ?? AppSettings.Get("exportSkins", true);
        public bool exportMaterials => _exportMaterials ?? AppSettings.Get("exportMaterials", false);
        public bool collectAnimations => _collectAnimations ?? AppSettings.Get("collectAnimations", true);
        public bool exportAnimations => _exportAnimations ?? AppSettings.Get("exportAnimations", true);
        public decimal boneSize => _boneSize ?? AppSettings.Get("boneSize", (decimal)10);
        public int fbxVersion => _fbxVersion ?? AppSettings.Get("fbxVersion", 3);
        public int fbxFormat => _fbxFormat ?? AppSettings.Get("fbxFormat", 0);
        public decimal scaleFactor => _scaleFactor ?? AppSettings.Get("scaleFactor", (decimal)1);
        public bool exportBlendShape => _exportBlendShape ?? AppSettings.Get("exportBlendShape", true);
        public bool castToBone => _castToBone ?? AppSettings.Get("castToBone", false);
        public bool restoreExtensionName => _restoreExtensionName ?? AppSettings.Get("restoreExtensionName", true);
        public bool enableFileLogging => _enableFileLogging ?? AppSettings.Get("enableFileLogging", false);
        public bool minimalAssetMap => _minimalAssetMap ?? AppSettings.Get("minimalAssetMap", true);
        public bool allowDuplicates => _allowDuplicates ?? AppSettings.Get("allowDuplicates", false);
        public string types => _types ?? AppSettings.Get("types", string.Empty);
        public string texs => _texs ?? AppSettings.Get("texs", string.Empty);
        public string uvs => _uvs ?? AppSettings.Get("uvs", string.Empty);

        public void ApplyCommandLineOptions(
            bool? convertTexture = null,
            bool? convertAudio = null,
            ImageFormat? convertType = null,
            bool? eulerFilter = null,
            decimal? filterPrecision = null,
            bool? exportAllNodes = null,
            bool? exportSkins = null,
            bool? exportMaterials = null,
            bool? collectAnimations = null,
            bool? exportAnimations = null,
            decimal? boneSize = null,
            int? fbxVersion = null,
            int? fbxFormat = null,
            decimal? scaleFactor = null,
            bool? exportBlendShape = null,
            bool? castToBone = null,
            bool? restoreExtensionName = null,
            bool? enableFileLogging = null,
            bool? minimalAssetMap = null,
            bool? allowDuplicates = null,
            string types = null,
            string texs = null,
            string uvs = null)
        {
            if (convertTexture.HasValue) _convertTexture = convertTexture;
            if (convertAudio.HasValue) _convertAudio = convertAudio;
            if (convertType.HasValue) _convertType = convertType;
            if (eulerFilter.HasValue) _eulerFilter = eulerFilter;
            if (filterPrecision.HasValue) _filterPrecision = filterPrecision;
            if (exportAllNodes.HasValue) _exportAllNodes = exportAllNodes;
            if (exportSkins.HasValue) _exportSkins = exportSkins;
            if (exportMaterials.HasValue) _exportMaterials = exportMaterials;
            if (collectAnimations.HasValue) _collectAnimations = collectAnimations;
            if (exportAnimations.HasValue) _exportAnimations = exportAnimations;
            if (boneSize.HasValue) _boneSize = boneSize;
            if (fbxVersion.HasValue) _fbxVersion = fbxVersion;
            if (fbxFormat.HasValue) _fbxFormat = fbxFormat;
            if (scaleFactor.HasValue) _scaleFactor = scaleFactor;
            if (exportBlendShape.HasValue) _exportBlendShape = exportBlendShape;
            if (castToBone.HasValue) _castToBone = castToBone;
            if (restoreExtensionName.HasValue) _restoreExtensionName = restoreExtensionName;
            if (enableFileLogging.HasValue) _enableFileLogging = enableFileLogging;
            if (minimalAssetMap.HasValue) _minimalAssetMap = minimalAssetMap;
            if (allowDuplicates.HasValue) _allowDuplicates = allowDuplicates;
            if (!string.IsNullOrEmpty(types)) _types = types;
            if (!string.IsNullOrEmpty(texs)) _texs = texs;
            if (!string.IsNullOrEmpty(uvs)) _uvs = uvs;
        }
    }
}
