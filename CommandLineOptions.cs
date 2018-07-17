using CommandLine;
using System;
using System.Collections.Generic;

namespace jsonmerge.net
{
    public class CommandLineOptions
    {
        [Option('b', "base-json", Required = false, Default = "", HelpText = "(Mandatory) The base JSON to be overridden.")]
        public string BaseJson { get; set; }

        [Option("base-json-file", Required = false, Default = null, HelpText = "(Mandatory) Path to the file where the base JSON to be overridden is stored.")]
        public string BaseJsonFile { get; set; }

        [Option('r', "override-json", Required = false, Default = "", HelpText = "(Mandatory) The JSON to be used as the override.")]
        public string OverrideJson { get; set; }

        [Option("override-json-file", Required = false, Default = null, HelpText = "(Mandatory) Path to the file where the JSON to be used as the overridde is stored.")]
        public string OverrideJsonFile { get; set; }

        [Option("output-json-file", Required = false, Default = null, HelpText = "(Optional) Path to the output file. If file exists it will be overridden. If this option is not specified output will be written to standard output.")]
        public string OutputJsonFile { get; set; }

        [Option("comments-ignore", Required = false, Default = false, HelpText = "(Optional) Ignore comments in JSON. Default: false")]
        public bool CommentsIgnore { get; set; }
        
        [Option("line-info-ignore", Required = false, Default = false, HelpText = "(Optional) Ignore line info in JSON. Default: false")]
        public bool LineInfoIgnore { get; set; }
        
        [Option("array-concat", SetName = "array-handling", Required = false, Default = false, HelpText = "(Optional) Concats arrays when merging JSON. Default: false")]
        public bool ArrayConcat { get; set; }

        [Option("array-replace", SetName = "array-handling", Required = false, Default = true, HelpText = "(Optional) Replaces arrays when merging JSON. Default: true")]
        public bool ArrayReplace { get; set; }
        
        [Option("array-union", SetName = "array-handling", Required = false, Default = false, HelpText = "(Optional) Unions arrays when merging JSON (remove duplicates). Default: false")]
        public bool ArrayUnion { get; set; }
        
        [Option("array-merge", SetName = "array-handling", Required = false, Default = false, HelpText = "(Optional) Merge arrays when merging JSON. Default: false")]
        public bool ArrayMerge { get; set; }
        
        [Option("null-ignore", Required = false, Default = false, HelpText = "(Optional) Ignore null fields when merging JSON. Default: false")]
        public bool NullIgnore { get; set; }
        
        [Option("indent", Required = false, Default = false, HelpText = "(Optional) Format output as indented JSON. Default: false")]
        public bool Indent { get; set; }
        
    }
}
