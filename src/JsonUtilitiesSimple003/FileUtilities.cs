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
using FileUtilities.Types;
using static FileUtilities.FileUtilitiesBasic;

/* TASKS:
//TODO - Continue with TDD, Test Driven Development, philosphy of having all Tests pass before adding more Fetures
//TODO - FEATURE: Append to List Class in filebasics
//TODO - FEATURE: Append to File
// FEATURE: SORT List Class?? Maybe I dont Need Sort?
*/

/*NOTES ON USAGE:
// It is convenient serialize from a list before you write to json file to avoid the issues with 
// json comma separations and bracket begining and ending. In short dont use file append. Read file, deserialize, then
// add data to list then, serialize, write file. 
*/

namespace FileUtilities
{
	public class FileUtilitiesBasic
	{
		public static List<T> DeserializeJsonStringReturnList<T>(string fileContent)
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

		public static string SerializeJsonDataReturnString<T>(List<T> listData)
		{
			string jsonString = JsonSerializer.Serialize
			(
				listData, new JsonSerializerOptions() { WriteIndented = true }
			);

			return jsonString;
		}
		
		public static string PromptForRelativeDirectory
			(string pathReplace = null, string repeatString = "Would You like to use this folder?")
		{
			string dir = Directory.GetCurrentDirectory();
			bool Yes = selectionTools.YesNoSelection(repeatString);
			dir = Yes ? dir : pathReplace;
			return dir;
		}
		
		public static  void ErraseOverflow <T>(List<T> listData, int totalCap)
		{
			int listCount = listData.Count;
			int remove = listCount - totalCap;
			if (remove > 1)
			{
				listData.RemoveRange(totalCap + 1, remove);
			}
		}
		
		public static string ConcatPathFileNameAndSuffix(string path, string name, string suffix)
		{
			string concatString;
			return concatString = path + @"\" + name + suffix;
		}

		public static void CreateFile(string filePath, string name)
		{
			string fullpathAndName = ConcatPathFileNameAndSuffix(filePath, name, Suffix.json);
			FileStream fileStream = File.Create(fullpathAndName);
		}

		public static void TestPathAndCreateFolder(string dirpath)
		{
			try
			{
				Directory.CreateDirectory(Path.GetDirectoryName(dirpath));
			}finally {}
		}
		
		public static string ReadFromFile(string filepath)
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
		
		public static void CheckIfFileExistsThenCreateIt(string filepath)
		{
			string dir = Path.GetDirectoryName(filepath);
			if (!(File.Exists(filepath)))
			{
				Directory.Exists(dir);
				TestPathAndCreateFolder(dir);
				FileStream fileStream = File.Create(filepath);
			}
		}
		
		public static void WriteToFile(string filePath, string jsonString)
		{
			using (StreamWriter outputFile = new StreamWriter(filePath))
			{
				outputFile.WriteLine(jsonString);
			}
		}

		public static void AppendToFile(string filePath, string contents)
		{
			using(StreamWriter appenFile = File.AppendText(filePath))
			{
				appenFile.WriteLine(contents);
			}
			
		}

		public static void AppendTolist<T>(List<T> listDataOriginal, List<T> listDataToAppend)
		{
			for (int i = 0; i < listDataToAppend.Count; i++)
			{
				listDataOriginal.Add(listDataToAppend[i]);
			}
		}
	}

	namespace Types
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
	}

		static class Suffix
		{
			public static string json = ".json";
			public static string txt = ".txt";
		}

		public class CustomJsonFile<T>
		{
			public string FileName { get; set; }
			public string DirPath { get; set; }
			public List<T> ListData { get; set; }
			private string jsonFormat;
			public string JsonFormat
			{
				get { return SerializeJsonDataReturnString<T>(this.ListData); }
			}
			private string pathFileNameAndSuffix;
			public string PathFileNameAndSuffix
			{
				get { return this.DirPath + @"\" + this.FileName + Suffix.json; }
			}
		}
	}

	namespace Prefabs
	{
		class LoadFileToListThenSortAndCap
		{
			public static void Begin<T>(CustomJsonFile<T> myJsonFile, Func<T, IComparable> getProp, int capLimit = 500)
			{
				string fileContent = ReadFromFile(myJsonFile.PathFileNameAndSuffix);
				List<T> tempTransferList = new List<T>();
				tempTransferList = DeserializeJsonStringReturnList<T>(fileContent);
				AppendTolist<T>(myJsonFile.ListData,tempTransferList);
				SortScore.Begin(myJsonFile,getProp);
				ErraseOverflow<T>(myJsonFile.ListData, capLimit);
			}
		}
		
		class SortScore
		{
			public static void Begin<T>(CustomJsonFile<T> myJsonFile,Func<T, IComparable> getProp )
			{
				myJsonFile.ListData.OrderByDescending(set => getProp(set));
			}
		}
		
		class CreateFileSortWriteToJson
		{
			public static void Begin<T>(CustomJsonFile<NameAndScoreSet> myJsonFile)
			{
				TestPathAndCreateFolder(myJsonFile.PathFileNameAndSuffix);
				CheckIfFileExistsThenCreateIt(myJsonFile.PathFileNameAndSuffix);
				myJsonFile.ListData.OrderByDescending(set => set.Score);
				WriteToFile(myJsonFile.PathFileNameAndSuffix, SerializeJsonDataReturnString(myJsonFile.ListData));
			}
		}
	}
}

