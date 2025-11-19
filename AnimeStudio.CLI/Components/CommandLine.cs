using System;
using System.IO;
using System.Linq;
using System.CommandLine;
using System.CommandLine.Binding;
using System.CommandLine.Parsing;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using AnimeStudio.CLI.Properties;

namespace AnimeStudio.CLI
{
    public static class CommandLine
    {
        public static void Init(string[] args)
        {
            var rootCommand = RegisterOptions();
            rootCommand.Invoke(args);
        }
        public static RootCommand RegisterOptions()
        {
            var optionsBinder = new OptionsBinder();
            var rootCommand = new RootCommand()
            {
                optionsBinder.Silent,
                optionsBinder.LoggerFlags,
                optionsBinder.TypeFilter,
                optionsBinder.NameFilter,
                optionsBinder.ContainerFilter,
                optionsBinder.GameName,
                optionsBinder.MapOp,
                optionsBinder.MapType,
                optionsBinder.MapName,
                optionsBinder.UnityVersion,
                optionsBinder.GroupAssetsType,
                optionsBinder.AssetExportType,
                optionsBinder.Key,
                optionsBinder.AIFile,
                optionsBinder.DummyDllFolder,
                optionsBinder.Input,
                optionsBinder.Output,
                optionsBinder.ConvertTexture,
                optionsBinder.ConvertAudio,
                optionsBinder.ConvertType,
                optionsBinder.EulerFilter,
                optionsBinder.FilterPrecision,
                optionsBinder.ExportAllNodes,
                optionsBinder.ExportSkins,
                optionsBinder.ExportMaterials,
                optionsBinder.CollectAnimations,
                optionsBinder.ExportAnimations,
                optionsBinder.BoneSize,
                optionsBinder.FbxVersion,
                optionsBinder.FbxFormat,
                optionsBinder.ScaleFactor,
                optionsBinder.ExportBlendShape,
                optionsBinder.CastToBone,
                optionsBinder.RestoreExtensionName,
                optionsBinder.EnableFileLogging,
                optionsBinder.MinimalAssetMap,
                optionsBinder.AllowDuplicates,
                optionsBinder.Types,
                optionsBinder.Texs,
                optionsBinder.Uvs
            };

            rootCommand.SetHandler(Program.Run, optionsBinder);

            return rootCommand;
        }
    }
    public class Options
    {
        public bool Silent { get; set; }
        public LoggerEvent[] LoggerFlags { get; set; }
        public string[] TypeFilter { get; set; }
        public Regex[] NameFilter { get; set; }
        public Regex[] ContainerFilter { get; set; }
        public string GameName { get; set; }
        public MapOpType MapOp { get; set; }
        public ExportListType MapType { get; set; }
        public string MapName { get; set; }
        public string UnityVersion { get; set; }
        public AssetGroupOption GroupAssetsType { get; set; }
        public ExportType AssetExportType { get; set; }
        public byte Key { get; set; }
        public FileInfo AIFile { get; set; }
        public DirectoryInfo DummyDllFolder { get; set; }
        public FileInfo Input { get; set; }
        public DirectoryInfo Output { get; set; }
        public bool? ConvertTexture { get; set; }
        public bool? ConvertAudio { get; set; }
        public ImageFormat? ConvertType { get; set; }
        public bool? EulerFilter { get; set; }
        public decimal? FilterPrecision { get; set; }
        public bool? ExportAllNodes { get; set; }
        public bool? ExportSkins { get; set; }
        public bool? ExportMaterials { get; set; }
        public bool? CollectAnimations { get; set; }
        public bool? ExportAnimations { get; set; }
        public decimal? BoneSize { get; set; }
        public int? FbxVersion { get; set; }
        public int? FbxFormat { get; set; }
        public decimal? ScaleFactor { get; set; }
        public bool? ExportBlendShape { get; set; }
        public bool? CastToBone { get; set; }
        public bool? RestoreExtensionName { get; set; }
        public bool? EnableFileLogging { get; set; }
        public bool? MinimalAssetMap { get; set; }
        public bool? AllowDuplicates { get; set; }
        public string Types { get; set; }
        public string Texs { get; set; }
        public string Uvs { get; set; }
    }

    public class OptionsBinder : BinderBase<Options>
    {
        public readonly Option<bool> Silent;
        public readonly Option<LoggerEvent[]> LoggerFlags;
        public readonly Option<string[]> TypeFilter;
        public readonly Option<Regex[]> NameFilter;
        public readonly Option<Regex[]> ContainerFilter;
        public readonly Option<string> GameName;
        public readonly Option<MapOpType> MapOp;
        public readonly Option<ExportListType> MapType;
        public readonly Option<string> MapName;
        public readonly Option<string> UnityVersion;
        public readonly Option<AssetGroupOption> GroupAssetsType;
        public readonly Option<ExportType> AssetExportType;
        public readonly Option<byte> Key;
        public readonly Option<FileInfo> AIFile;
        public readonly Option<DirectoryInfo> DummyDllFolder;
        public readonly Argument<FileInfo> Input;
        public readonly Argument<DirectoryInfo> Output;
        public readonly Option<bool> ConvertTexture;
        public readonly Option<bool> ConvertAudio;
        public readonly Option<ImageFormat> ConvertType;
        public readonly Option<bool> EulerFilter;
        public readonly Option<decimal> FilterPrecision;
        public readonly Option<bool> ExportAllNodes;
        public readonly Option<bool> ExportSkins;
        public readonly Option<bool> ExportMaterials;
        public readonly Option<bool> CollectAnimations;
        public readonly Option<bool> ExportAnimations;
        public readonly Option<decimal> BoneSize;
        public readonly Option<int> FbxVersion;
        public readonly Option<int> FbxFormat;
        public readonly Option<decimal> ScaleFactor;
        public readonly Option<bool> ExportBlendShape;
        public readonly Option<bool> CastToBone;
        public readonly Option<bool> RestoreExtensionName;
        public readonly Option<bool> EnableFileLogging;
        public readonly Option<bool> MinimalAssetMap;
        public readonly Option<bool> AllowDuplicates;
        public readonly Option<string> Types;
        public readonly Option<string> Texs;
        public readonly Option<string> Uvs;

        public OptionsBinder()
        {
            Silent = new Option<bool>("--silent", "Hide log messages.");
            LoggerFlags = new Option<LoggerEvent[]>("--logger_flags", "Flags to control toggle log events.") { AllowMultipleArgumentsPerToken = true, ArgumentHelpName = "Verbose|Debug|Info|etc.." };
            TypeFilter = new Option<string[]>("--types", "Specify unity class type(s)") { AllowMultipleArgumentsPerToken = true, ArgumentHelpName = "Texture2D|Shader:Parse|Sprite:Both|etc.." };
            NameFilter = new Option<Regex[]>("--names", result => 
            {
                var items = new List<Regex>();
                var value = result.Tokens.Single().Value;
                if (File.Exists(value))
                {
                    var lines = File.ReadLines(value);
                    foreach (var line in lines)
                    {
                        if (string.IsNullOrWhiteSpace(line))
                        {
                            continue;
                        }

                        try
                        {
                            items.Add(new Regex(line, RegexOptions.IgnoreCase));
                        }
                        catch (ArgumentException e)
                        {
                            continue;
                        }
                    }
                }
                else
                {
                    items.AddRange(result.Tokens.Select(x => new Regex(x.Value, RegexOptions.IgnoreCase)).ToArray());
                }

                return items.ToArray();
            }, false, "Specify name regex filter(s).") { AllowMultipleArgumentsPerToken = true };
            ContainerFilter = new Option<Regex[]>("--containers", result =>
            {
                var items = new List<Regex>();
                var value = result.Tokens.Single().Value;
                if (File.Exists(value))
                {
                    var lines = File.ReadLines(value);
                    foreach(var line in lines)
                    {
                        if (string.IsNullOrWhiteSpace(line))
                        {
                            continue;
                        }

                        try
                        {
                            items.Add(new Regex(line, RegexOptions.IgnoreCase));
                        }
                        catch (ArgumentException e)
                        {
                            continue;
                        }
                    }
                }
                else
                {
                    items.AddRange(result.Tokens.Select(x => new Regex(x.Value, RegexOptions.IgnoreCase)).ToArray());
                }

                return items.ToArray();
            }, false, "Specify container regex filter(s).") { AllowMultipleArgumentsPerToken = true };
            GameName = new Option<string>("--game", $"Specify Game.") { IsRequired = true };
            MapOp = new Option<MapOpType>("--map_op", "Specify which map to build.");
            MapType = new Option<ExportListType>("--map_type", "AssetMap output type.");
            MapName = new Option<string>("--map_name", () => "assets_map", "Specify AssetMap file name.");
            UnityVersion = new Option<string>("--unity_version", "Specify Unity version.");
            GroupAssetsType = new Option<AssetGroupOption>("--group_assets", "Specify how exported assets should be grouped.");
            AssetExportType = new Option<ExportType>("--export_type", "Specify how assets should be exported.");
            AIFile = new Option<FileInfo>("--ai_file", "Specify asset_index json file path (to recover GI containers).").LegalFilePathsOnly();
            DummyDllFolder = new Option<DirectoryInfo>("--dummy_dlls", "Specify DummyDll path.").LegalFilePathsOnly();
            Input = new Argument<FileInfo>("input_path", "Input file/folder.").LegalFilePathsOnly();
            Output = new Argument<DirectoryInfo>("output_path", "Output folder.").LegalFilePathsOnly();

            Key = new Option<byte>("--key", result =>
            {
                return ParseKey(result.Tokens.Single().Value);
            }, false, "XOR key to decrypt MiHoYoBinData.");

            LoggerFlags.AddValidator(FilterValidator);
            TypeFilter.AddValidator(FilterValidator);
            NameFilter.AddValidator(FilterValidator);
            ContainerFilter.AddValidator(FilterValidator);
            Key.AddValidator(result =>
            {
                var value = result.Tokens.Single().Value;
                try
                {
                    ParseKey(value);
                }
                catch (Exception e)
                {
                    result.ErrorMessage = "Invalid byte value.\n" + e.Message;
                }
            });

            GameName.FromAmong(GameManager.GetGameNames());

            ConvertTexture = new Option<bool>("--convert_texture", "Convert texture assets.");
            ConvertAudio = new Option<bool>("--convert_audio", "Convert audio assets.");
            ConvertType = new Option<ImageFormat>("--convert_type", "Image format for texture conversion.");
            EulerFilter = new Option<bool>("--euler_filter", "Apply Euler filter to rotations.");
            FilterPrecision = new Option<decimal>("--filter_precision", "Precision for Euler filter.");
            ExportAllNodes = new Option<bool>("--export_all_nodes", "Export all nodes.");
            ExportSkins = new Option<bool>("--export_skins", "Export skin data.");
            ExportMaterials = new Option<bool>("--export_materials", "Export materials.");
            CollectAnimations = new Option<bool>("--collect_animations", "Collect animations.");
            ExportAnimations = new Option<bool>("--export_animations", "Export animations.");
            BoneSize = new Option<decimal>("--bone_size", "Size of bones in export.");
            FbxVersion = new Option<int>("--fbx_version", "FBX version to export.");
            FbxFormat = new Option<int>("--fbx_format", "FBX format (0=binary, 1=ascii).");
            ScaleFactor = new Option<decimal>("--scale_factor", "Scale factor for export.");
            ExportBlendShape = new Option<bool>("--export_blend_shape", "Export blend shapes.");
            CastToBone = new Option<bool>("--cast_to_bone", "Cast to bone.");
            RestoreExtensionName = new Option<bool>("--restore_extension_name", "Restore extension names.");
            EnableFileLogging = new Option<bool>("--enable_file_logging", "Enable file logging.");
            MinimalAssetMap = new Option<bool>("--minimal_asset_map", "Create minimal asset map.");
            AllowDuplicates = new Option<bool>("--allow_duplicates", "Allow duplicate assets.");
            Types = new Option<string>("--settings_types", "Types setting value.");
            Texs = new Option<string>("--settings_texs", "Texs setting value.");
            Uvs = new Option<string>("--settings_uvs", "Uvs setting value.");

            LoggerFlags.SetDefaultValue(new LoggerEvent[] { LoggerEvent.Debug, LoggerEvent.Info, LoggerEvent.Warning, LoggerEvent.Error });
            GroupAssetsType.SetDefaultValue(AssetGroupOption.ByType);
            AssetExportType.SetDefaultValue(ExportType.Convert);
            MapOp.SetDefaultValue(MapOpType.None);
            MapType.SetDefaultValue(ExportListType.XML);
        }
        
        public byte ParseKey(string value)
        {
            if (value.StartsWith("0x"))
            {
                value = value[2..];
                return Convert.ToByte(value, 0x10);
            }
            else
            {
                return byte.Parse(value);
            }
        }

        public void FilterValidator(OptionResult result)
        {
            var values = result.Tokens.Select(x => x.Value).ToArray();
            foreach (var val in values)
            {
                if (string.IsNullOrWhiteSpace(val))
                {
                    result.ErrorMessage = "Empty string.";
                    return;
                }

                try
                {
                    Regex.Match("", val, RegexOptions.IgnoreCase);
                }
                catch (ArgumentException e)
                {
                    result.ErrorMessage = "Invalid Regex.\n" + e.Message;
                    return;
                }
            }
        }

        protected override Options GetBoundValue(BindingContext bindingContext) =>
        new()
        {
            Silent = bindingContext.ParseResult.GetValueForOption(Silent),
            LoggerFlags = bindingContext.ParseResult.GetValueForOption(LoggerFlags),
            TypeFilter = bindingContext.ParseResult.GetValueForOption(TypeFilter),
            NameFilter = bindingContext.ParseResult.GetValueForOption(NameFilter),
            ContainerFilter = bindingContext.ParseResult.GetValueForOption(ContainerFilter),
            GameName = bindingContext.ParseResult.GetValueForOption(GameName),
            MapOp = bindingContext.ParseResult.GetValueForOption(MapOp),
            MapType = bindingContext.ParseResult.GetValueForOption(MapType),
            MapName = bindingContext.ParseResult.GetValueForOption(MapName),
            UnityVersion = bindingContext.ParseResult.GetValueForOption(UnityVersion),
            GroupAssetsType = bindingContext.ParseResult.GetValueForOption(GroupAssetsType),
            AssetExportType = bindingContext.ParseResult.GetValueForOption(AssetExportType),
            Key = bindingContext.ParseResult.GetValueForOption(Key),
            AIFile = bindingContext.ParseResult.GetValueForOption(AIFile),
            DummyDllFolder = bindingContext.ParseResult.GetValueForOption(DummyDllFolder),
            Input = bindingContext.ParseResult.GetValueForArgument(Input),
            Output = bindingContext.ParseResult.GetValueForArgument(Output),
            ConvertTexture = bindingContext.ParseResult.GetValueForOption(ConvertTexture),
            ConvertAudio = bindingContext.ParseResult.GetValueForOption(ConvertAudio),
            ConvertType = bindingContext.ParseResult.GetValueForOption(ConvertType),
            EulerFilter = bindingContext.ParseResult.GetValueForOption(EulerFilter),
            FilterPrecision = bindingContext.ParseResult.GetValueForOption(FilterPrecision),
            ExportAllNodes = bindingContext.ParseResult.GetValueForOption(ExportAllNodes),
            ExportSkins = bindingContext.ParseResult.GetValueForOption(ExportSkins),
            ExportMaterials = bindingContext.ParseResult.GetValueForOption(ExportMaterials),
            CollectAnimations = bindingContext.ParseResult.GetValueForOption(CollectAnimations),
            ExportAnimations = bindingContext.ParseResult.GetValueForOption(ExportAnimations),
            BoneSize = bindingContext.ParseResult.GetValueForOption(BoneSize),
            FbxVersion = bindingContext.ParseResult.GetValueForOption(FbxVersion),
            FbxFormat = bindingContext.ParseResult.GetValueForOption(FbxFormat),
            ScaleFactor = bindingContext.ParseResult.GetValueForOption(ScaleFactor),
            ExportBlendShape = bindingContext.ParseResult.GetValueForOption(ExportBlendShape),
            CastToBone = bindingContext.ParseResult.GetValueForOption(CastToBone),
            RestoreExtensionName = bindingContext.ParseResult.GetValueForOption(RestoreExtensionName),
            EnableFileLogging = bindingContext.ParseResult.GetValueForOption(EnableFileLogging),
            MinimalAssetMap = bindingContext.ParseResult.GetValueForOption(MinimalAssetMap),
            AllowDuplicates = bindingContext.ParseResult.GetValueForOption(AllowDuplicates),
            Types = bindingContext.ParseResult.GetValueForOption(Types),
            Texs = bindingContext.ParseResult.GetValueForOption(Texs),
            Uvs = bindingContext.ParseResult.GetValueForOption(Uvs)
        };
    }
}
