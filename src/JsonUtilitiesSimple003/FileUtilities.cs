using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Linq;
using System.Net.Mime;
using System.Net.Sockets;
using System.Text.Json;
using System.Text.Json.Serialization;
using FileUtilities.Types;
using static FileUtilities.FileUtilitiesBasic;
using static FileUtilities.JsonUtilityBasics;

/* TASKS:
//TODO - Continue with TDD, Test Driven Development, philosphy of having all Tests pass before adding more Fetures
//TODO - FEATURE: Append to List Class in filebasics
//TODO - FEATURE: SORT List Class
*/

namespace FileUtilities
{
	public class JsonUtilityBasics
	{
		public static List<T> JsonDeserializeDataReturnList<T>(CustomJsonFile<T> cjf)
		{
			List<T> FileData = null;
			string content = File.ReadAllText(cjf.PathFileNameAndSuffix);
			try
			{
				FileData = JsonSerializer.Deserialize<List<T>>(content);
			}
			catch (Exception e)
			{
				Console.WriteLine("Not A json file");
			}
			return FileData;
		}

		public static string JsonSerializeDataReturnString<T>(List<T> listData)
		{
			string jsonString = JsonSerializer.Serialize
			(
				listData, new JsonSerializerOptions() { WriteIndented = true }
			);

			return jsonString;
		}
	}
	
	public class FileUtilitiesBasic
	{
		public static string PromptForRelativeDirectory
			(string pathReplace = null, string repeatString = "Would You like to use this folder?")
		{
			string dir = Directory.GetCurrentDirectory();
			bool Yes = selectionTools.YesNoSelection(repeatString);
			dir = Yes ? dir : pathReplace;
			return dir;
		}
		
		public static  void ListEntryCap <T>(List<T> myJsonFile, int totalCap)
		{
			int listCount = myJsonFile.Count;
			int remove = listCount - totalCap;
			if (remove > 1)
			{
				myJsonFile.RemoveRange(totalCap + 1, remove);
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
				get { return JsonSerializeDataReturnString<T>(this.ListData); }
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
		class ReadFileSortScoresSaveToList
		{
			public void Begin<T>(CustomJsonFile<NameAndScoreSet> myJsonFile)
			{
				FileUtilitiesBasic.CheckIfFileExistsThenCreateIt(myJsonFile.PathFileNameAndSuffix);
				myJsonFile.ListData.OrderByDescending(set => set.Score);
				FileUtilitiesBasic.ListEntryCap(myJsonFile.ListData, 500);
			}
		}
		
		class CreateFileAndWriteToJson
		{
			public static void Begin<T>(CustomJsonFile<NameAndScoreSet> myJsonFile)
			{
				TestPathAndCreateFolder(myJsonFile.PathFileNameAndSuffix);
				CheckIfFileExistsThenCreateIt(myJsonFile.PathFileNameAndSuffix);
				WriteToFile(myJsonFile.PathFileNameAndSuffix, JsonSerializeDataReturnString(myJsonFile.ListData));
			}
		}
		
	}
}

