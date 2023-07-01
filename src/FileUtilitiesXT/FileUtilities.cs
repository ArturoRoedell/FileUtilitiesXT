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
using FileUtilitiesXTUtil.LittleHelpersLibrary;
using static FileUtilitiesXTUtil.FileUtilitiesXT.Types;
using static FileUtilitiesXTUtil.FileUtilitiesXT;

namespace FileUtilitiesXTUtil;

public class FileUtilitiesXT
{
	const string  defaultfileName = "DefaultSaveFile.sav";

	public  void LoadFileToListThenSortAndCap<T>(Types.CustomJsonFile<T> myJsonFile, Func<T, IComparable> getProp, int capLimit = 500)
	{
		TestPathAndCreateFolder(myJsonFile.DirPath);
		CheckIfFileExistsThenCreateIt(myJsonFile.PathFileNameAndSuffix);
		string fileContent = ReadFromFile(myJsonFile.PathFileNameAndSuffix);
		List<T> tempTransferList = new List<T>();
		string checknull = fileContent; // Debug Erase Whole Line;
		tempTransferList = DeserializeJsonStringReturnList<T>(fileContent);
		if (!(tempTransferList == null))
		{
			myJsonFile.ListData = AppendToAndRetunList<T>(myJsonFile.ListData,tempTransferList);
		}
		SortScore(myJsonFile,getProp);
		ErraseOverflow<T>(myJsonFile.ListData, capLimit);
	}//TestExixsts

	public  void SortScore<T>(Types.CustomJsonFile<T> myJsonFile,Func<T, IComparable> getProp )
	{
		List<T> transferList =  new List<T>(myJsonFile.ListData.OrderByDescending(set => getProp(set)));
		myJsonFile.ListData = transferList;
	}//TestExixsts

	public  void CreateFileSortWriteToJson<T>(Types.CustomJsonFile<T> myJsonFile, Func<T, IComparable> getProp, int capLimit = 500)
	{
		TestPathAndCreateFolder(myJsonFile.DirPath);
		CheckIfFileExistsThenCreateIt(myJsonFile.PathFileNameAndSuffix);
		SortScore(myJsonFile,getProp);
		ErraseOverflow<T>(myJsonFile.ListData, capLimit);
		WriteToFile(myJsonFile.PathFileNameAndSuffix, SerializeJsonDataReturnString(myJsonFile.ListData));
	}//TestExixsts
		
	public  List<T> DeserializeJsonStringReturnList<T>(string fileContent)
	{
		List<T> FileDataList = new List<T>();
		try
		{
			FileDataList = JsonSerializer.Deserialize<List<T>>(fileContent);
		}
		catch{}

		return FileDataList;
	}//TestExixsts

	public  string SerializeJsonDataReturnString<T>(List<T> listData)
	{
		string jsonString = JsonSerializer.Serialize
		(
			listData, new JsonSerializerOptions() { WriteIndented = true }
		);

		return jsonString + "\n";
	}//TestExixsts
		
	public  string PromptForRelativeDirectory
		(string pathReplace = null, string repeatString = "Would You like to use this relative directory folder shown above?")
	{
		string dir = Directory.GetCurrentDirectory();
		Console.WriteLine(dir);
		bool Yes = selectionTools.YesNoSelection(repeatString);
		dir = Yes ? dir : pathReplace;
		Console.WriteLine("Directory below will be used:\n"+ dir);
		return dir;
	}
		
	public  void ErraseOverflow <T>(List<T> listData, int totalCap)
	{
		int listCount = listData.Count;
		int remove = listCount - totalCap;
		if (remove > 0)
		{
			listData.RemoveRange(totalCap, remove);
		}
	}
		
	public  string ConcatPathFileNameAndSuffix(string path, string name, string suffix)
	{
		string concatString;
		return concatString = path + @"\" + name + suffix;
	}//TestExixsts

	public  void CreateFile(string filePath, string name)
	{
		string fullpathAndName = ConcatPathFileNameAndSuffix(filePath, name, Types.Suffix.json);
		FileStream fileStream = File.Create(fullpathAndName);
		fileStream.Close();
	}

	public  void TestPathAndCreateFolder(string dirpath)
	{
		if (!(Directory.Exists(dirpath)))
		{
			Directory.CreateDirectory(dirpath);
		}
	}
		
	public  string ReadFromFile(string filepath)
	{
		if(!(File.Exists(filepath)))
		{
			return null;
		};
		string contents = null;
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
			fileStream.Close();
		}
	}//TestExixsts
		
	public  void WriteToFile(string filePath, string jsonString)
	{
		using (StreamWriter streamWriter = new StreamWriter(filePath))
		{
			streamWriter.Write(jsonString);
			streamWriter.Close();
		}
	}

	public  void AppendToFile(string filePath, string contents)
	{
		using(StreamWriter streamWriter = File.AppendText(filePath))
		{
			streamWriter.Write(contents);
			streamWriter.Close();
		}
			
	}

	public List<T> AppendToAndRetunList<T>(List<T> listDataOriginal, List<T> listDataToAppend)
	{
		List<T> newList = new List<T>();
		if (listDataOriginal == null)
		{
			listDataOriginal = newList;
		}

		if (listDataOriginal.Count == 0)
		{
			listDataOriginal = listDataToAppend;
		}
		else
		{
			for (int i = 0; i < listDataToAppend.Count; i++)
			{
				listDataOriginal.Add(listDataToAppend[i]);
			}
		}
		return listDataOriginal;
	}

	public string FastFilePath(String fileName = defaultfileName, string fileDirectory = " ")
	{
		string filePath;
		if (fileDirectory == " ")
		{
			fileDirectory = Directory.GetCurrentDirectory();
		}
		return filePath = fileDirectory + @"\" + fileName;
	}
	
	public void FastCreateWriteFile(string fileContent, string filepath = " ")
	{
		if (filepath == " ")
		{
			filepath = FastFilePath();
		}
		TestPathAndCreateFolder(Path.GetDirectoryName(filepath));
		CheckIfFileExistsThenCreateIt(filepath);
		WriteToFile(filepath, fileContent);
	}

	public string FastReadFile() //AKA DefaultFileSaveSystem
	{
		string filePath = Directory.GetCurrentDirectory() + @"\" + defaultfileName;
		string fileContent = ReadFromFile(filePath);
		return fileContent;
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
			private FileUtilitiesXT _fileUtilitiesXt = new FileUtilitiesXT();
			public string FileName { get; set; }
			public string DirPath { get; set; }
			private Type listType;
			public Type ListType
			{
				get { return typeof(T); }
			}
			public List<T> ListData { get; set; }
			private string jsonFormat;
			public string JsonFormat
			{
				get { return _fileUtilitiesXt.SerializeJsonDataReturnString<T>(this.ListData); }
			}
			private string pathFileNameAndSuffix;
			public string PathFileNameAndSuffix
			{
				get { return this.DirPath + @"\" + this.FileName + Suffix.json; }
			}
		}
	}
}

