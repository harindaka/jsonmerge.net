using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
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
            var jsonList = new List<string>();

            if (opts.JsonFiles != null)
            {
                foreach (var file in opts.JsonFiles)
                {
                    var fileContent = File.ReadAllText(file);
                    jsonList.Add(fileContent);
                }
            }

            if (opts.JsonStrings != null)
            {
                foreach (var jstring in opts.JsonStrings)
                {
                    jsonList.Add(jstring);
                }
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

            string mergedJson = MergeJson(jsonList, loadSettings, mergeSettings, formatting);

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
            IEnumerable<string> jstrings,
            JsonLoadSettings loadSettings,
            JsonMergeSettings mergeSettings,
            Formatting formatting
            )
        {
            if(jstrings == null || !jstrings.Any())
            {
                return String.Empty;
            }

            bool hasJson = false;
            var mergedObject = new JObject();
            foreach (var jstring in jstrings)
            {
                if(!String.IsNullOrWhiteSpace(jstring))
                {
                    var overrideObject = JObject.Parse(jstring, loadSettings);
                    mergedObject.Merge(overrideObject, mergeSettings);
                    hasJson = true;
                }
            }

            if (hasJson)
            {
                return mergedObject.ToString(formatting);
            }
            else
            {
                return string.Empty;
            }
        }
    }
}
