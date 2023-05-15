using System;
using FileUtilities;
using FileUtilities.Types;
//using xUnitJsonUtilitiesUnitTest;

/* ## MY TASKS
* TODO - Add Unit Tests ...(Inprogress)...
*/

class Program
{
	static void Main()
	{
		HelicopterExample.HighScore.Begin();
	}
}

namespace HelicopterExample
{
	public class HighScore
	{
		public static void Begin()
		{
			List<NameAndScoreSet> HighScoreList = new List<NameAndScoreSet>();
			AddNamesAndScoresToList("Arty", 481, HighScoreList);
			AddNamesAndScoresToList("Arty", 3454, HighScoreList);
			AddNamesAndScoresToList("Jessica", 462, HighScoreList);
			AddNamesAndScoresToList("Arty", 865, HighScoreList);

			CustomJsonFile<NameAndScoreSet> customJsonFile = new CustomJsonFile<NameAndScoreSet>();
			
			
			customJsonFile.FileName = "artysdfsfFile";
			customJsonFile.DirPath = @"C:\Users\ARTURO 001\source\repos\JsonUtilitiesSimpleTest001";
			customJsonFile.ListData = HighScoreList;
			
			//customJsonFile.DirPath = FileUtilitiesBasic.PromptForRelativeDirectory(customJsonFile.DirPath);
			//FileUtilitiesBasic.CheckIfFileExistsThenCreateIt(customJsonFile.PathFileNameAndSuffix);
			//FileUtilities.JsonUtilityBasics.JsonSerializeDataReturnString(customJsonFile);
			
			Console.WriteLine("Serious Not Cheating" + customJsonFile.JsonFormat + "I Am Not Cheating");
			return;	

			foreach (NameAndScoreSet set in customJsonFile.ListData)
			{
				Console.WriteLine($"Name: {set.Name} Score: {set.Score}");
			}

			customJsonFile.ListData.Clear();

			Console.WriteLine("Is there Something Here?\n");
			
			customJsonFile.ListData = FileUtilities.JsonUtilityBasics.JsonDeserializeDataReturnList(customJsonFile);
			
			foreach (NameAndScoreSet set in customJsonFile.ListData)
			{
				Console.WriteLine($"Name: {set.Name} Score: {set.Score}");
			}
			Console.WriteLine("now There is");

			
			//-----------------------------	

			PrintToScreenMyJsonFile.Begin<NameAndScoreSet>(customJsonFile);

			//FileUtilities.Prefabs.CreateFileAndWriteToJson.Begin<NameAndScoreSet>(customJsonFile);
			
			//customJsonFile.ListData.Clear();
			foreach (var e in customJsonFile.ListData)
			{
				Console.WriteLine(e.Name + " " + e.Score);
			}
			
			
		}
		
		public static void AddNamesAndScoresToList(String name, int score, List<NameAndScoreSet> list)
		{
			list.Add(new NameAndScoreSet(name, score));
		}
	}
	
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
	}
}