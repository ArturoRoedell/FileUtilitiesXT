![image info](./icon/FileUtilitiesXT_icon.png)
[![NuGet](https://img.shields.io/nuget/vpre/ArturoRoedell.Util.FileUtilitiesXT.svg)](https://www.nuget.org/packages/downloader)

>## Read and write JSON files and text files with ease Version 1.0.0.1 Targets net6.0 & net472
>## Simplest way to save game save, save configs, or save high scores in c# there is!

### Description

This is a small simple c# library to make working with JSON files a  bit easier to work with. Also reduces the lines of code to read and write custom files text csv etc.

>#### Getting Started Example:

    //Instantiate
	FileUtilitiesXT fileUtilitiesXt = new FileUtilitiesXT();  
	string fileContent = "I am  a string serving as content for an example";

    //This method creates and writes to DefaultSaveFile.sav file located in the executable folder
	fileUtilitiesXt.FastCreateWriteFile(fileContent); 

    //This method reads from DefaultSaveFile.sav file located in the executable folder
	string contentReadFromFile = fileUtilitiesXt.FastReadFile();

	System.Console.WriteLine(contentReadFromFile);




`FastCreateWriteFile(filecontent)` which I could have named DefaultSavefileSystem as well, and all that is needed is to add the file Content
Also `FastReadFile()` returns text that is saved in the default file. The default directory is relative and the filename is stored in the code

It is now simpler to serialize and save files with fewer lines of code using `CustomJsonFile` object,
`LoadFileToListThenSortAndCap` method and the `CreateFileSortWriteToJson` method as seen in the demo file.


FileUtilitiesXT needs to be instantiated to use the methods like this `FileUtilitiesXT fileUtilitiesXt = new FileUtilitiesXT();`. The following is the current list of methods that can be used.



>### Important:
>>It is convenient to serialize from a list before you write to JSON file to avoid the issues with
JSON comma separations and bracket beginning and ending. In short, do not use `AppendToFile` for JSON files.
To append to JSON file here are my suggested steps: read the file, deserialize,
add data to the list, serialize, and finally overwrite the file.
***


### The demo project below shows a great example below to save JSON files with custom types
***
~~~~

>### List of Methods for FileUtilitiesXT

`LoadFileToListThenSortAndCap<T>(CustomJsonFile<T> myJsonFile, Func<T, IComparable> getProp, int capLimit = 500)`

`SortScore<T>(CustomJsonFile<T> myJsonFile,Func<T, IComparable> getProp )`

`CreateFileSortWriteToJson<T>(CustomJsonFile<T> myJsonFile, Func<T, IComparable> getProp, int capLimit = 500)`

`List<T> DeserializeJsonStringReturnList<T>(string fileContent)`

`string SerializeJsonDataReturnString<T>(List<T> listData)`

`string PromptForRelativeDirectory(string pathReplace = null, string repeatString = "Would You like to use this relative directory folder shown above?")`

`ErraseOverflow <T>(List<T> listData, int totalCap)`

`ConcatPathFileNameAndSuffix(string path, string name, string suffix)`

`CreateFile(string filePath, string name)`

`TestPathAndCreateFolder(string dirpath)`

`ReadFromFile(string filepath)`

`CheckIfFileExistsThenCreateIt(string filepath)`

`WriteToFile(string filePath, string jsonString)`

`AppendToFile(string filePath, string contents)`

`List<T> AppendToAndRetunList<T>(List<T> listDataOriginal, List<T> listDataToAppend)`

`string FastFilePath(String fileName = defaultfileName, string fileDirectory = " ")`

`FastCreateWriteFile(string fileContent, string filepath = " ")`

`string FastReadFile()`

>### List of Methods for LittleHelpersLibrary

`using LittleHelpersLibrary;`

`static bool YesNoSelection(string ChoiceAsk)`

Example

`play = selectionTools.YesNoSelection("\n\nDo You Want To Continue Playing?");`

`static bool FileCompare(string file1, string file2)`
Example
`bool passed = Comparison.FileCompare(ExpectedFilepath, ActualFilepath);`

>### Nuget Package is also available version 1.0.0 in nuget packet manager search for: FileUtilitiesXT

>### Reason for this project
>#### I initially started this library to incorporate into c# console games. I thought it would be very helpful for simple file save systems for c# games. Then I saw how it was helpful so I do not have to re-write my code when I am reading and writing JSON files

Link to this repository on GitHub:
https://github.com/ArturoRoedell/FileUtilitiesXT/tree/FileUtilitiesXT.LibraryV1.0.0.1


