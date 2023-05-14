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

/* TASKS:
*/

namespace FileUtilities
{
	public class JsonUtilityBasics
	{
		public static void DeserializeScoreDataFromFileAndSaveToList<T>(CustomJsonFile<T> cjf)
		{
			string content = File.ReadAllText(cjf.PathFileNameAndSuffix);
			try
			{
				cjf.ListData = JsonSerializer.Deserialize<List<T>>(content);
			}
			catch (Exception e)
			{
				Console.WriteLine("Not A json file");
			}
		}

		public static void SerializeDataAndWriteToFile<T>(CustomJsonFile<T> cjf)
		{
			string jsonString = JsonSerializer.Serialize
			(
				cjf.ListData, new JsonSerializerOptions() { WriteIndented = true }
			);
			File.WriteAllText(cjf.PathFileNameAndSuffix, jsonString);
		}
	}
	
	public class FileUtilitiesBasic
	{
		public static string PromptForSaveFileDirectory(string pathReplace = null)
		{
			string dir = Directory.GetCurrentDirectory();
			string repeatString = "Would You like to use this folder to save your file";
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

		public static void CreateFile(string path, string name)
		{
			string fullpathAndName = ConcatPathFileNameAndSuffix(path, name, Suffix.json);
			FileStream fileStream = File.Create(fullpathAndName);
		}

		public static void TestPathAndCreateFolder(string dirpath)
		{
			try
			{
				Directory.CreateDirectory(Path.GetDirectoryName(dirpath));
			}
			finally
			{
			}
			
		}
		
		public static string ReadFromCustomFile(string filepath)
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
			private string pathFileNameAndSuffix;
			public string PathFileNameAndSuffix
			{
				get { return this.DirPath + @"\" + this.FileName + Suffix.json; } // get method
				set { pathFileNameAndSuffix = value; }
			}
		}
	}

	namespace Prefabs
	{
		class PrintToScreenMyJsonFile
		{
			public static void Begin<T>(CustomJsonFile<NameAndScoreSet> cjf)
			{
				Console.WriteLine("The Path " + cjf.DirPath + " The FileName" + cjf.FileName);
				Console.WriteLine("The Complete File Path: " + cjf.PathFileNameAndSuffix);

				foreach (NameAndScoreSet set in cjf.ListData)
				{
					Console.WriteLine($"Name: {set.Name} Score: {set.Score}");
				}
				
			}
		} //TODO -Move this somewhere else. Class For Games and Helicopter Game specific

		class ReadFileSortScoresSaveToList
		{
			public void Begin<T>(CustomJsonFile<NameAndScoreSet> myJsonFile)
			{
				FileUtilitiesBasic.CheckIfFileExistsThenCreateIt(myJsonFile.PathFileNameAndSuffix);
				JsonUtilityBasics.DeserializeScoreDataFromFileAndSaveToList(myJsonFile);
				myJsonFile.ListData.OrderByDescending(set => set.Score);
				FileUtilitiesBasic.ListEntryCap(myJsonFile.ListData, 500);
			}
		}
		
		class CreateFileAndWriteToJson
		{
			public static void Begin<T>(CustomJsonFile<NameAndScoreSet> myJsonFile)
			{
				FileUtilitiesBasic.TestPathAndCreateFolder(myJsonFile.PathFileNameAndSuffix);
				FileUtilitiesBasic.CheckIfFileExistsThenCreateIt(myJsonFile.PathFileNameAndSuffix);
				JsonUtilityBasics.SerializeDataAndWriteToFile(myJsonFile);
			}
		}
		
	}
}