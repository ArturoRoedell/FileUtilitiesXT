using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Mime;
using System.Net.Sockets;
using System.Text.Json;
using System.Text.Json.Serialization;
using static Types;
using static FileUtilitiesBasic;

/* TASKS:
MAINTASKONGOING: Continue with TDD, Test Driven Development, philosphy of having all Tests pass before adding more Fetures
MAINTASK: Add Unit Tests ...(Inprogress)...
SECONDARYTASK - Rename file and namespace for unit test correctly
SECONDARYTASK - Create three more Unit tests
SECONDARYTASK - Add Data from Debug to start of Unit test to rull all tests
TODO - PIPEDREAM: Create a Demo Project to go along with Dll called JsonUtilities003Demo
TODO - FEATURE: Add feature to change csv file to json file if Value names are given. example SCORE, NAME...
todo - ...Or tke Form Header information.
TODO - FEATURE: add a fancy input system with that asks you questions which can be used for any program
*/

/*NOTES ON USAGE:
// It is convenient serialize from a list before you write to json file to avoid the issues with 
// json comma separations and bracket begining and ending. In short dont use file append. Read file, deserialize, then
// add data to list then, serialize, write file. 
*/

	public class FileUtilitiesBasic
	{
		public  void LoadFileToListThenSortAndCap<T>(CustomJsonFile<T> myJsonFile, Func<T, IComparable> getProp, int capLimit = 500)
		{
			string fileContent = ReadFromFile(myJsonFile.PathFileNameAndSuffix);
			List<T> tempTransferList = new List<T>();
			tempTransferList = DeserializeJsonStringReturnList<T>(fileContent);
			AppendTolist<T>(myJsonFile.ListData,tempTransferList);
			SortScore(myJsonFile,getProp);
			ErraseOverflow<T>(myJsonFile.ListData, capLimit);
		}

		public  void SortScore<T>(CustomJsonFile<T> myJsonFile,Func<T, IComparable> getProp )
		{
			List<T> transferList =  new List<T>(myJsonFile.ListData.OrderByDescending(set => getProp(set)));
			myJsonFile.ListData = transferList;
		}
		

		public  void CreateFileSortWriteToJson<T>(CustomJsonFile<T> myJsonFile, Func<T, IComparable> getProp)
		{
			TestPathAndCreateFolder(myJsonFile.PathFileNameAndSuffix);
			CheckIfFileExistsThenCreateIt(myJsonFile.PathFileNameAndSuffix);
			SortScore(myJsonFile,getProp);
			WriteToFile(myJsonFile.PathFileNameAndSuffix, SerializeJsonDataReturnString(myJsonFile.ListData));
		}
		
		public  List<T> DeserializeJsonStringReturnList<T>(string fileContent)
		{
			List<T> FileDataList = null;
			//string fileContent = File.ReadAllText(filePath);
			try
			{
				FileDataList = JsonSerializer.Deserialize<List<T>>(fileContent);
			}
			catch (Exception e)
			{
				Console.WriteLine("Not A json file");
			}
			return FileDataList;
		}

		public  string SerializeJsonDataReturnString<T>(List<T> listData)
		{
			string jsonString = JsonSerializer.Serialize
			(
				listData, new JsonSerializerOptions() { WriteIndented = true }
			);

			return jsonString;
		}
		
		public  string PromptForRelativeDirectory
			(string pathReplace = null, string repeatString = "Would You like to use this folder?")
		{
			string dir = Directory.GetCurrentDirectory();
			bool Yes = selectionTools.YesNoSelection(repeatString);
			dir = Yes ? dir : pathReplace;
			return dir;
		}
		
		public   void ErraseOverflow <T>(List<T> listData, int totalCap)
		{
			int listCount = listData.Count;
			int remove = listCount - totalCap;
			if (remove > 1)
			{
				listData.RemoveRange(totalCap + 1, remove);
			}
		}
		
		public  string ConcatPathFileNameAndSuffix(string path, string name, string suffix)
		{
			string concatString;
			return concatString = path + @"\" + name + suffix;
		}

		public  void CreateFile(string filePath, string name)
		{
			string fullpathAndName = ConcatPathFileNameAndSuffix(filePath, name, Suffix.json);
			FileStream fileStream = File.Create(fullpathAndName);
		}

		public  void TestPathAndCreateFolder(string dirpath)
		{
			try
			{
				Directory.CreateDirectory(Path.GetDirectoryName(dirpath));
			}finally {}
		}
		
		public  string ReadFromFile(string filepath)
		{
			string contents = "";
			var fileInfo = new FileInfo(filepath);
			if (fileInfo.Length == 0)
			{
				Console.WriteLine("Error Empty File");
			}
			else
			{
				contents = File.ReadAllText(filepath);
			}

			return contents;
		}
		
		public  void CheckIfFileExistsThenCreateIt(string filepath)
		{
			string dir = Path.GetDirectoryName(filepath);
			if (!(File.Exists(filepath)))
			{
				Directory.Exists(dir);
				TestPathAndCreateFolder(dir);
				FileStream fileStream = File.Create(filepath);
			}
		}
		
		public  void WriteToFile(string filePath, string jsonString)
		{
			using (StreamWriter outputFile = new StreamWriter(filePath))
			{
				outputFile.WriteLine(jsonString);
			}
		}

		public  void AppendToFile(string filePath, string contents)
		{
			using(StreamWriter appenFile = File.AppendText(filePath))
			{
				appenFile.WriteLine(contents);
			}
			
		}

		public  void AppendTolist<T>(List<T> listDataOriginal, List<T> listDataToAppend)
		{
			for (int i = 0; i < listDataToAppend.Count; i++)
			{
				listDataOriginal.Add(listDataToAppend[i]);
			}
		}
		
	}

public class Types
{ 
	public class NameAndScoreSet
	{
		public string Name { get; set; }
		public int Score { get; set; }

		public NameAndScoreSet(string name, int score)
		{
			this.Name = name;
			this.Score = score;
		}
		public override string ToString()
		{
			return "Name: " + Name + "   Score: " + Score;
		}
		
	}

	public static class Suffix
	{
		public static string json = ".json";
		public static string txt = ".txt";
	}

	public class CustomJsonFile<T>
	{
		private FileUtilitiesBasic _fileUtilitiesBasic = new FileUtilitiesBasic();
		public string FileName { get; set; }
		public string DirPath { get; set; }
		public List<T> ListData { get; set; }
		private string jsonFormat;
		public string JsonFormat
		{
			get { return _fileUtilitiesBasic.SerializeJsonDataReturnString<T>(this.ListData); }
		}
		private string pathFileNameAndSuffix;
		public string PathFileNameAndSuffix
		{
			get { return this.DirPath + @"\" + this.FileName + Suffix.json; }
		}
	}
}
