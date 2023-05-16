﻿using System;
using FileUtilities;
using FileUtilities.Types;
using FileUtilities.Prefabs;

namespace DebugTests;

public class Bebug01
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

		
		Console.WriteLine("List Unsorted");
		Console.WriteLine(customJsonFile.JsonFormat);
		
		Console.WriteLine("\n List Sorted");
		CreateFileSortWriteToJson.Begin<NameAndScoreSet>(customJsonFile, x => x.Score); //KEY: .Begin<{type}>(customJsonFile, x => x.{property}) 
		
		Console.WriteLine(customJsonFile.JsonFormat);
		

		Console.WriteLine("Deleting The list Shold Be empty");
		customJsonFile.ListData.Clear();
		Console.WriteLine(customJsonFile.JsonFormat);
		Console.WriteLine("IsThere Something Aboveme");
		Console.WriteLine("Repopulating The List Then Printing it");
		LoadFileToListThenSortAndCap.Begin<NameAndScoreSet>(customJsonFile, x => x.Score); // KEY: .Begin<{type}>(customJsonFile, x => x.{property}) 
		
		
		LoadFileToListThenSortAndCap.Begin<NameAndScoreSet>(customJsonFile, x => x.Score);
		LoadFileToListThenSortAndCap.Begin<NameAndScoreSet>(customJsonFile, x => x.Score);
		LoadFileToListThenSortAndCap.Begin<NameAndScoreSet>(customJsonFile, x => x.Score);
		Console.WriteLine(customJsonFile.JsonFormat);
		

		return;
		
		for (int i = 0; i < customJsonFile.ListData.Count(); i++)
		{
			Console.WriteLine(customJsonFile.ListData[i]);
		}
		//foreach (var e in customJsonFile.ListData)
		//{
			//Console.WriteLine("Something Should Be here");
			//Console.WriteLine(e.Name + " " + e.Score);
		//}
		//Console.WriteLine(customJsonFile.JsonFormat);
		List<NameAndScoreSet> newList = new List<NameAndScoreSet>(customJsonFile.ListData.OrderByDescending(set => set.Score));
		Console.WriteLine("Are they in Order?");

		
		//Console.WriteLine(HighScoreList[1]);
		//Console.WriteLine(customJsonFile.ListData[1]);
				
		for (int i = 0; i < newList.Count(); i++)
		{
			Console.WriteLine(newList[i]);
		}
		// foreach (var e in customJsonFile.ListData)
		// {
		// 	//Console.WriteLine("Something Should Be here");
		// 	Console.WriteLine(e.Name + " " + e.Score);
		// }

		customJsonFile.ListData = newList;

		Console.WriteLine("\n Is Json String in order now??\n");
		Console.WriteLine(customJsonFile.JsonFormat);

		return;
		//FileUtilitiesBasic.CheckIfFileExistsThenCreateIt(customJsonFile.PathFileNameAndSuffix);

		
		//FileUtilities.Prefabs.CreateFileSortWriteToJson.Begin<NameAndScoreSet>(customJsonFile, x => x.Score); // TODO - This works Great Need To Create a unit Test. except not sorted
		
		FileUtilities.FileUtilitiesBasic.SerializeJsonDataReturnString<NameAndScoreSet>(customJsonFile.ListData);
		
		
		Console.WriteLine("Deleting The list Shold Be empty");
		customJsonFile.ListData.Clear();
		Console.WriteLine(customJsonFile.JsonFormat);
		Console.WriteLine("IsThere Something Aboveme");
		
		
		Console.WriteLine("Repopulating The List Then Printing it");
		FileUtilities.Prefabs.LoadFileToListThenSortAndCap.Begin<NameAndScoreSet>(customJsonFile, x => x.Score);
		
		Console.WriteLine(customJsonFile.JsonFormat);
		foreach (var e in customJsonFile.ListData)
		{
			Console.WriteLine("Something Should Be here");
			Console.WriteLine(e.Name + " " + e.Score);
		}
		return;
		//------------------------------------
		Console.WriteLine(customJsonFile.JsonFormat);
		
		foreach (NameAndScoreSet set in customJsonFile.ListData)
		{
			Console.WriteLine($"Name: {set.Name} Score: {set.Score}");
		}

		//customJsonFile.ListData.Clear();

		Console.WriteLine("Is there Something Here?\n");

		customJsonFile.ListData =
			FileUtilitiesBasic.DeserializeJsonStringReturnList<NameAndScoreSet>(
				customJsonFile.PathFileNameAndSuffix);
		

		
		PrintToScreenMyJsonFile.Begin<NameAndScoreSet>(customJsonFile);

		customJsonFile.ListData.Clear();


		foreach (var e in customJsonFile.ListData)
		{
			Console.WriteLine("Something Should Be here");
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

		List<NameAndScoreSet> actualList =
			FileUtilitiesBasic.DeserializeJsonStringReturnList<NameAndScoreSet>(JsonRawData);

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
