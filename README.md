# grename
C# windows Console App (.NET 4.6.1)\
grename is used to rename files to a new name+index/sequence.\
\
Useful if you have a bunch of files ordered correctly, but not named in a sequence, for instance with dates, like "file20220101", "file20220102", et.c.\

# Usage
grename [-test] [searchpattern] [newname] [string indexformat] [int startindex]\
\
grename [searchpattern] -> TEST searchpattern only: outputs list of this searchpattern in the order grename will process it.\
\
grename -test : insert ' -test ' anywhere in argument list to do a dry run, showing each file and potential new name without actually renaming\
\
[newname] : e.g newname?.png (- ? will be replaced with sequence)\

# Examples
grename *.png newname?.png 000 1 -> rename all files found by *.png (ordered alphabetically) to 'newname001.png, 002, etc\
\
grename *.jpg new?file.jpg 000 1 -> rename all files found by *.png (ordered alphabetically) to 'newname001, 002, etc\
\
grename *.jpg new?.jpeg -> rename *.jpg to new?.jpeg using default format ('0') and default startindex (1)\

# Install
Download release, put grename.exe in e.g c:\grename\
Edit system environment variables, and add c:\grename to your PATH.\

# Order of files
grename uses Directoy.GetFiles() to retrieve list of files, which does not guarantee which order you get the files, but usually gets them in alphabetical order.\
Use grename *.something to check which order your files are returned in.\
You're welcome to fork the repo and add LINQ style OrderBy() on the list, if you have specific needs, such as getting the files in order of date or some property.\
See https://stackoverflow.com/questions/4765789/getting-files-by-creation-date-in-net\

