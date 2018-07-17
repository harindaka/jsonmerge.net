# jsonmerge.net
Cross platform .net command-line tool for merging json strings and/or files. Merge those json configuration files in your CI pipeline now!

# Prerequisites
Requires the [.Net Runtime](https://www.microsoft.com/net/download) to be pre-installed.

# Download
Check [Releases](https://github.com/harindaka/jsonmerge.net/releases) for the latest version. Extract the zip archive once downloaded.

# Usage

## Windows
`jsonmerge.bat (options)`

## MacOS or Linux
`./jsonmerge (options)`

### Options 

```
  -j, --json-strings    The JSON strings to be merged. Multiple JSON strings 
                        can be specified seperated by a space. They are merged 
                        in the order specified. JSON strings always override 
                        any file based content.

  -f, --json-files      The files containing JSON to be merged. Multiple files 
                        can be specified seperated by a space. They are merged 
                        in the order specified. JSON strings always override 
                        any file based content.

  -o, --output-file     Path to the output file. If file exists it will be 
                        overwritten. If this option is not specified output 
                        will be written to standard output.

  --comments-ignore     (Default: true) Ignore comments when parsing.

  --line-info-ignore    (Default: true) Ignore line info when parsing.

  --array-concat        (Default: false) Concat arrays when merging.

  --array-replace       (Default: true) Replace arrays when merging.

  --array-union         (Default: false) Union arrays (remove duplicates) when 
                        merging.

  --array-merge         (Default: false) Merge arrays when merging.

  --null-ignore         (Default: false) Ignore null fields when merging.

  --indent              (Default: false) Output indented JSON.

  --help                Display this help screen.

  --version             Display version information.

```

## Examples

### Merge files and output to a file with indented formatting
`./jsonmerge -f "/path/to/file1.json" "/path/to/file2.json" "/path/to/file3.json" -o "/path/to/merged.json" --indented`

### Merge strings and output to stdout
`./jsonmerge -j "{ \"key1\": \"value1\" }" "{ \"key2\": \"value2\" }"`

### Merge strings and files and output to stdout
`./jsonmerge -j "{ \"key1\": \"value1\" }" "{ \"key2\": \"value2\" }" -f "/path/to/file1.json"`