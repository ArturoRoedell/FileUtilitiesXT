#### Read write Json files and text files with ease

This is a small simple c# library to make to make working wih json files a  bit easier to work with. Also reduces the lines of code to read and write custom files text csv etc.


'FastCreateWriteFile(filecontent)' which I could have named DefaultSavefileSystem as well, and all that is needed is to add the file Content
Also `FastReadFile()` returns text that is saved in the default file. The default directory is relative and the filename is stored in code

Also for Json Files it is now simpler to serialize and save files with fewer lines of code using `CustomJsonFile` object,
`LoadFileToListThenSortAndCap` method and the `CreateFileSortWriteToJson` method as seen in the demo file.

### List of Methods
FileUtilitiesXT needs to be instantiated to use the methods like this `FileUtilitiesXT fileUtilitiesXt = new FileUtilitiesXT();`. The following is the current list of methods that can be used:

LoadFileToListThenSortAndCap<T>(CustomJsonFile<T> myJsonFile, Func<T, IComparable> getProp, int capLimit = 500)

SortScore<T>(CustomJsonFile<T> myJsonFile,Func<T, IComparable> getProp )

CreateFileSortWriteToJson<T>(CustomJsonFile<T> myJsonFile, Func<T, IComparable> getProp, int capLimit = 500)

List<T> DeserializeJsonStringReturnList<T>(string fileContent)

string SerializeJsonDataReturnString<T>(List<T> listData)

string PromptForRelativeDirectory(string pathReplace = null, string repeatString = "Would You like to use this relative directory folder shown above?")

ErraseOverflow <T>(List<T> listData, int totalCap)

ConcatPathFileNameAndSuffix(string path, string name, string suffix)

CreateFile(string filePath, string name)

TestPathAndCreateFolder(string dirpath)

ReadFromFile(string filepath)

CheckIfFileExistsThenCreateIt(string filepath)

WriteToFile(string filePath, string jsonString)

AppendToFile(string filePath, string contents)

List<T> AppendToAndRetunList<T>(List<T> listDataOriginal, List<T> listDataToAppend)

string FastFilePath(String fileName = defaultfileName, string fileDirectory = " ")

FastCreateWriteFile(string fileContent, string filepath = " ")

string FastReadFile()