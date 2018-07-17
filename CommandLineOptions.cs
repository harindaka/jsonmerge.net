using CommandLine;
using System;
using System.Collections.Generic;

namespace jsonmerge.net
{
    public class CommandLineOptions
    {
        [Option('j', "json-strings", Required = false, HelpText = "The JSON strings to be merged. Multiple JSON strings can be specified seperated by a space. They are merged in the order specified. JSON strings always override any file based content.")]
        public IEnumerable<string> JsonStrings { get; set; }

        [Option('f', "json-files", Required = false, HelpText = "The files containing JSON to be merged. Multiple files can be specified seperated by a space. They are merged in the order specified. JSON strings always override any file based content.")]
        public IEnumerable<string> JsonFiles { get; set; }
                
        [Option('o', "output-file", Required = false, Default = null, HelpText = "Path to the output file. If file exists it will be overwritten. If this option is not specified output will be written to standard output.")]
        public string OutputJsonFile { get; set; }

        [Option("comments-ignore", Required = false, Default = true, HelpText = "Ignore comments when parsing.")]
        public bool CommentsIgnore { get; set; }
        
        [Option("line-info-ignore", Required = false, Default = true, HelpText = "Ignore line info when parsing.")]
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
