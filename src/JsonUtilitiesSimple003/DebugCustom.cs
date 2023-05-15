using System;
using FileUtilities;
using FileUtilities.Types;

namespace DebugCustom
{

/* ## MY TASKS
* TODO - Add Unit Tests ...(Inprogress)...
*/


public class TestinArea
{
	public static void SeeTestData()
	{
		string JsonRawData =
			@"[
  {
    ""Name"": ""a"",
    ""Score"": 1
  },
  {
    ""Name"": ""b"",
    ""Score"": 2
  }
]
";
		List<NameAndScoreSet> expectedList = new List<NameAndScoreSet>();
		expectedList.Add(new NameAndScoreSet("a", 1));
		expectedList.Add(new NameAndScoreSet("b", 2));
		
		List<NameAndScoreSet> actualList = FileUtilitiesBasic.DeserializeJsonStringReturnList<NameAndScoreSet>(JsonRawData);

		Boolean passed = expectedList == actualList ? true : false;
		Console.WriteLine("Did it pass?" + passed);
		foreach (NameAndScoreSet set in actualList)
		{
			Console.WriteLine(set.Name + set.Score + set.GetType());
		}
		Console.WriteLine(actualList.Count + expectedList.Capacity);
		
		foreach (NameAndScoreSet set in expectedList)
		{
			Console.WriteLine(set.Name + set.Score + set.GetType());
		}

		Console.WriteLine(expectedList.Count + expectedList.Capacity);

		

		Console.WriteLine(actualList.ToString());
		Console.WriteLine(expectedList.ToString());
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
				//FileUtilities.JsonUtilityBasics.SerializeJsonDataReturnString(customJsonFile);

				FileUtilities.Prefabs.LoadFileToListThenSortAndCap.Begin<NameAndScoreSet>(customJsonFile, x => x.Score);
				
				Console.WriteLine(customJsonFile.JsonFormat);
				return;	

				foreach (NameAndScoreSet set in customJsonFile.ListData)
				{
					Console.WriteLine($"Name: {set.Name} Score: {set.Score}");
				}

				customJsonFile.ListData.Clear();

				Console.WriteLine("Is there Something Here?\n");
				
				customJsonFile.ListData = FileUtilitiesBasic.DeserializeJsonStringReturnList<NameAndScoreSet>(customJsonFile.PathFileNameAndSuffix);
				
				foreach (NameAndScoreSet set in customJsonFile.ListData)
				{
					Console.WriteLine($"Name: {set.Name} Score: {set.Score}");
				}
				Console.WriteLine("now There is");

				
				//-----------------------------	

				PrintToScreenMyJsonFile.Begin<NameAndScoreSet>(customJsonFile);

				//FileUtilities.Prefabs.CreateFileSortWriteToJson.Begin<NameAndScoreSet>(customJsonFile);
				
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
}