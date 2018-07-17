using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using CommandLine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace jsonmerge.net
{
    class Program
    {
        static void Main(string[] args)
        {                                   
            var parser = new Parser(p =>
            {
                p.CaseSensitive = true;
                p.EnableDashDash = true;
                p.HelpWriter = Console.Error;
                p.ParsingCulture = CultureInfo.InvariantCulture;
            });

            parser.ParseArguments<CommandLineOptions>(args)
                .WithParsed<CommandLineOptions>(opts => ExecuteCommand(opts))
                .WithNotParsed<CommandLineOptions>((errs) => HandleParseError(errs));
                        
        }

        private static void ExecuteCommand(CommandLineOptions opts)
        {
            string baseJson = null;
            string overrideJson = null;

            if (String.IsNullOrWhiteSpace(opts.BaseJsonFile))
            {
                baseJson = opts.BaseJson;
            }
            else
            {
                baseJson = File.ReadAllText(opts.BaseJsonFile);
            }

            if (String.IsNullOrWhiteSpace(opts.OverrideJsonFile))
            {
                overrideJson = opts.OverrideJson;
            }
            else
            {
                overrideJson = File.ReadAllText(opts.OverrideJsonFile);
            }
                        
            var loadSettings = new JsonLoadSettings();
            if(opts.CommentsIgnore)
            {
                loadSettings.CommentHandling = CommentHandling.Ignore;
            }
            else
            {
                loadSettings.CommentHandling = CommentHandling.Load;
            }

            if (opts.LineInfoIgnore)
            {
                loadSettings.LineInfoHandling = LineInfoHandling.Ignore;
            }
            else
            {
                loadSettings.LineInfoHandling = LineInfoHandling.Load;
            }

            var mergeSettings = new JsonMergeSettings();
            if (opts.ArrayConcat)
            {
                mergeSettings.MergeArrayHandling = MergeArrayHandling.Concat;
            }
            else if (opts.ArrayMerge)
            {
                mergeSettings.MergeArrayHandling = MergeArrayHandling.Merge;
            }
            else if (opts.ArrayReplace)
            {
                mergeSettings.MergeArrayHandling = MergeArrayHandling.Replace;
            }
            else if (opts.ArrayUnion)
            {
                mergeSettings.MergeArrayHandling = MergeArrayHandling.Union;
            }

            if (opts.NullIgnore)
            {
                mergeSettings.MergeNullValueHandling = MergeNullValueHandling.Ignore;
            }
            else
            {
                mergeSettings.MergeNullValueHandling = MergeNullValueHandling.Merge;
            }

            Formatting formatting = Formatting.None;
            if(opts.Indent)
            {
                formatting = Formatting.Indented;
            }

            var mergedJson = MergeJson(baseJson, overrideJson, loadSettings, mergeSettings, formatting);

            if(String.IsNullOrEmpty(opts.OutputJsonFile))
            {
                Console.Write(mergedJson);
            }
            else
            {
                File.WriteAllText(opts.OutputJsonFile, mergedJson);
            }

        }

        private static void HandleParseError(IEnumerable<Error> errors)
        {
            foreach(var error in errors)
            {
                Console.WriteLine(error.ToString());                
            }
        }

        private static string MergeJson(
            string baseJson, 
            string overrideJson, 
            JsonLoadSettings loadSettings,
            JsonMergeSettings mergeSettings,
            Formatting formatting
            )
        {
            if (String.IsNullOrWhiteSpace(baseJson) && String.IsNullOrWhiteSpace(overrideJson))
            {
                return String.Empty;
            }

            if(String.IsNullOrWhiteSpace(baseJson))
            {
                baseJson = "{}";
            }
            else if (String.IsNullOrWhiteSpace(overrideJson))
            {
                overrideJson = "{}";
            }

            var baseObject = JObject.Parse(baseJson, loadSettings);
            var overrideObject = JObject.Parse(overrideJson, loadSettings);

            baseObject.Merge(overrideObject, mergeSettings);

            return baseObject.ToString(formatting);
        }
    }
}
