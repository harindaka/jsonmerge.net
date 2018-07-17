using CommandLine;
using System;
using System.Collections.Generic;

namespace jsonmerge.net
{
    public class CommandLineOptions
    {
        [Option('b', "base-json", Required = false, HelpText = "The base JSON to be overridden.")]
        public string BaseJson { get; set; }

        [Option("base-json-file", Required = false, Default = null, HelpText = "Path to the file where the base JSON to be overridden is stored.")]
        public string BaseJsonFile { get; set; }

        [Option('r', "override-json", Required = false, HelpText = "The JSON to be used as the override.")]
        public string OverrideJson { get; set; }

        [Option("override-json-file", Required = false, Default = null, HelpText = "Path to the file where the JSON to be used as the override is stored.")]
        public string OverrideJsonFile { get; set; }

        [Option("output-json-file", Required = false, Default = null, HelpText = "Path to the output file. If file exists it will be overwritten. If this option is not specified output will be written to standard output.")]
        public string OutputJsonFile { get; set; }

        [Option("comments-ignore", Required = false, Default = true, HelpText = "Ignore comments when parsing.")]
        public bool CommentsIgnore { get; set; }
        
        [Option("line-info-ignore", Required = false, Default = true, HelpText = "Ignore line info  when parsing.")]
        public bool LineInfoIgnore { get; set; }
        
        [Option("array-concat", Required = false, Default = false, HelpText = "Concat arrays when merging.")]
        public bool ArrayConcat { get; set; }

        [Option("array-replace", Required = false, Default = true, HelpText = "Replace arrays when merging.")]
        public bool ArrayReplace { get; set; }
        
        [Option("array-union", Required = false, Default = false, HelpText = "Union arrays (remove duplicates) when merging.")]
        public bool ArrayUnion { get; set; }
        
        [Option("array-merge", Required = false, Default = false, HelpText = "Merge arrays when merging.")]
        public bool ArrayMerge { get; set; }
        
        [Option("null-ignore", Required = false, Default = false, HelpText = "Ignore null fields when merging.")]
        public bool NullIgnore { get; set; }
        
        [Option("indent", Required = false, Default = false, HelpText = "Output indented JSON.")]
        public bool Indent { get; set; }
        
    }
}
