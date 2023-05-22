 using System;
 using static FileUtilitiesXT.Types;
 using static FileUtilitiesXT;

/*NOTES: Scratch code when debuging
 Nothing should be referencing this area of the project.
 It is a mess and I don't mind if it stays a mess
*/
namespace DebugTests;

public class Bebug01
{
	public static void Begin()
	{



		//Directory.CreateDirectory(@"C:\Users\ARTURO 001\source\repos\JsonUtilitiesSimpleTest002");


		List<NameAndScoreSet> HighScoreList = new List<NameAndScoreSet>();
		AddNamesAndScoresToList("Arty", 481, HighScoreList);
		AddNamesAndScoresToList("Cherry", 3454, HighScoreList);
		AddNamesAndScoresToList("Jessica", 462, HighScoreList);
		AddNamesAndScoresToList("Arty", 865, HighScoreList);
		CustomJsonFile<NameAndScoreSet> myJsonFile = new CustomJsonFile<NameAndScoreSet>();
		myJsonFile.FileName = "artysdfsfFile";
		myJsonFile.DirPath = @"C:\Users\ARTURO 001\source\repos\JsonUtilitiesSimpleTest001";
		myJsonFile.ListData = HighScoreList;
		FileUtilitiesXT fileUtilitiesXt = new FileUtilitiesXT();
		
		
		
		
		fileUtilitiesXt.CreateFileSortWriteToJson(myJsonFile, x => x.Score); //KEY: .Begin<{type}>(myJsonFile, x => x.{property}) 
		
		
		Console.WriteLine(myJsonFile.JsonFormat);
		

		Console.WriteLine("Deleting The list Shold Be empty");
		myJsonFile.ListData.Clear();
		Console.WriteLine(myJsonFile.JsonFormat);
		Console.WriteLine("IsThere Something Aboveme");
		Console.WriteLine("Repopulating The List Then Printing it");
		fileUtilitiesXt.LoadFileToListThenSortAndCap(myJsonFile, x => x.Score); 
		
		Console.WriteLine(myJsonFile.JsonFormat);
		
		
		return;
		
		
		
		myJsonFile.FileName = "Helicopter Scores";
		string CurrentDir = Directory.GetCurrentDirectory();
		myJsonFile.DirPath = CurrentDir + @"\HighScoresFolder";
		Console.WriteLine(Directory.Exists(myJsonFile.DirPath));
		Console.WriteLine(myJsonFile.DirPath);
		fileUtilitiesXt.TestPathAndCreateFolder(myJsonFile.DirPath);
		Console.WriteLine(Directory.Exists(myJsonFile.DirPath));
		
		
		
		
		return;
		
		
		
		
		
		Console.WriteLine("Append String");
		
		string input = @"example string\"; // Replace with your input string

		if (!input.EndsWith("\\"))
		{
			input += "\\";
		}

		Console.WriteLine("Modified string: " + input);
		
		
		//string getthisDirectory = fileUtilitiesXt.PromptForRelativeDirectory(@"C:Pretend/Folder\\notTrue");
		
		//fileUtilitiesXt.TestPathAndCreateFolder(@"C:\Users\ARTURO 001\source\repos\JsonUtilitiesSimple003\AnotherTest\");
		return;
	
		

	
		fileUtilitiesXt.CreateFileSortWriteToJson<NameAndScoreSet>(myJsonFile,x => x.Score);
		
		Console.WriteLine("\nBefore Clear\n");

		for (int i = 0; i < myJsonFile.ListData.Count(); i++)
		{
			Console.WriteLine(myJsonFile.ListData[i]);
		}

		//myJsonFile.ListData.Clear();

		Console.WriteLine("\nAfter Clear\n");
		
		for (int i = 0; i < myJsonFile.ListData.Count(); i++)
		{
			Console.WriteLine(myJsonFile.ListData[i]);
		}

		Console.WriteLine("writeThis");
		fileUtilitiesXt.LoadFileToListThenSortAndCap<NameAndScoreSet>(myJsonFile, x => x.Score);
		
		Console.WriteLine("\nLoad From File\n");
		for (int i = 0; i < myJsonFile.ListData.Count(); i++)
		{
			Console.WriteLine(myJsonFile.ListData[i]);
		}

		
		return;


		
		
		//HighScoreList.Clear();
		
		for (int i = 0; i < HighScoreList.Count(); i++)
		{
			Console.WriteLine(HighScoreList[i]);
		}
		
		List<NameAndScoreSet> BetterScores = new List<NameAndScoreSet>();
		AddNamesAndScoresToList("duro", 4721, BetterScores);
		AddNamesAndScoresToList("Marco", 7554, BetterScores);
		
		BetterScores.Clear();
		Console.WriteLine("\n Better Scores\n");
		for (int i = 0; i < BetterScores.Count(); i++)
		{
			Console.WriteLine(BetterScores[i]);
			
		}

		Console.WriteLine("\n ScoresTogether In one\n");
		
		fileUtilitiesXt.AppendToAndRetunList(HighScoreList, BetterScores);
		
		for (int i = 0; i < HighScoreList.Count(); i++)
		{
			Console.WriteLine(HighScoreList[i]);
		}
		return;
		//-----------------------
		
		
		for (int i = 0; i < HighScoreList.Count(); i++)
		{
			Console.WriteLine(HighScoreList[i]);
		}
		Console.WriteLine("\n Reduce Items Below\n");
		
		fileUtilitiesXt.ErraseOverflow(HighScoreList, 2 );
		
		for (int i = 0; i < HighScoreList.Count(); i++)
		{
			Console.WriteLine(HighScoreList[i]);
		}

		return;
		//-----------------------
		
		Console.WriteLine("List Unsorted");
		Console.WriteLine(myJsonFile.JsonFormat);
		
		Console.WriteLine("\n List Sorted");

		
		
		fileUtilitiesXt.CreateFileSortWriteToJson<NameAndScoreSet>(myJsonFile, x => x.Score); //KEY: .Begin<{type}>(myJsonFile, x => x.{property}) 
		
		
		Console.WriteLine(myJsonFile.JsonFormat);
		

		Console.WriteLine("Deleting The list Shold Be empty");
		myJsonFile.ListData.Clear();
		Console.WriteLine(myJsonFile.JsonFormat);
		Console.WriteLine("IsThere Something Aboveme");
		Console.WriteLine("Repopulating The List Then Printing it");
		fileUtilitiesXt.LoadFileToListThenSortAndCap<NameAndScoreSet>(myJsonFile, x => x.Score); // KEY: .Begin<{type}>(myJsonFile, x => x.{property}) 
		
		
		fileUtilitiesXt.LoadFileToListThenSortAndCap<NameAndScoreSet>(myJsonFile, x => x.Score);
		fileUtilitiesXt.LoadFileToListThenSortAndCap<NameAndScoreSet>(myJsonFile, x => x.Score);
		fileUtilitiesXt.LoadFileToListThenSortAndCap<NameAndScoreSet>(myJsonFile, x => x.Score);
		Console.WriteLine(myJsonFile.JsonFormat);
		

		return;
		
		for (int i = 0; i < myJsonFile.ListData.Count(); i++)
		{
			Console.WriteLine(myJsonFile.ListData[i]);
		}
		//foreach (var e in myJsonFile.ListData)
		//{
		//Console.WriteLine("Something Should Be here");
		//Console.WriteLine(e.Name + " " + e.Score);
		//}
		//Console.WriteLine(myJsonFile.JsonFormat);
		List<NameAndScoreSet> newList = new List<NameAndScoreSet>(myJsonFile.ListData.OrderByDescending(set => set.Score));
		Console.WriteLine("Are they in Order?");

		
		//Console.WriteLine(HighScoreList[1]);
		//Console.WriteLine(myJsonFile.ListData[1]);
				
		for (int i = 0; i < newList.Count(); i++)
		{
			Console.WriteLine(newList[i]);
		}
		// foreach (var e in myJsonFile.ListData)
		// {
		// 	//Console.WriteLine("Something Should Be here");
		// 	Console.WriteLine(e.Name + " " + e.Score);
		// }

		myJsonFile.ListData = newList;

		Console.WriteLine("\n Is Json String in order now??\n");
		Console.WriteLine(myJsonFile.JsonFormat);

		return;
		//fileUtilitiesXt.CheckIfFileExistsThenCreateIt(myJsonFile.PathFileNameAndSuffix);

		
		//FileUtilities.Prefabs.CreateFileSortWriteToJson.Begin<NameAndScoreSet>(myJsonFile, x => x.Score); // TODO - This works Great Need To Create a unit Test. except not sorted
		
		fileUtilitiesXt.SerializeJsonDataReturnString<NameAndScoreSet>(myJsonFile.ListData);
		
		
		Console.WriteLine("Deleting The list Shold Be empty");
		myJsonFile.ListData.Clear();
		Console.WriteLine(myJsonFile.JsonFormat);
		Console.WriteLine("IsThere Something Aboveme");
		
		
		Console.WriteLine("Repopulating The List Then Printing it");
		fileUtilitiesXt.LoadFileToListThenSortAndCap<NameAndScoreSet>(myJsonFile, x => x.Score);
		
		Console.WriteLine(myJsonFile.JsonFormat);
		foreach (var e in myJsonFile.ListData)
		{
			Console.WriteLine("Something Should Be here");
			Console.WriteLine(e.Name + " " + e.Score);
		}
		return;
		//------------------------------------
		Console.WriteLine(myJsonFile.JsonFormat);
		
		foreach (NameAndScoreSet set in myJsonFile.ListData)
		{
			Console.WriteLine($"Name: {set.Name} Score: {set.Score}");
		}

		//myJsonFile.ListData.Clear();

		Console.WriteLine("Is there Something Here?\n");

		myJsonFile.ListData =
			fileUtilitiesXt.DeserializeJsonStringReturnList<NameAndScoreSet>(
				myJsonFile.PathFileNameAndSuffix);
		

		
		PrintToScreenMyJsonFile.Begin<NameAndScoreSet>(myJsonFile);

		myJsonFile.ListData.Clear();


		foreach (var e in myJsonFile.ListData)
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
	public static void Begin<T>(CustomJsonFile<NameAndScoreSet> myJsonFile)
	{
		Console.WriteLine("The Path " + myJsonFile.DirPath + " The FileName" + myJsonFile.FileName);
		Console.WriteLine("The Complete File Path: " + myJsonFile.PathFileNameAndSuffix);

		foreach (NameAndScoreSet set in myJsonFile.ListData)
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

		FileUtilitiesXT fileUtilitiesXt = new FileUtilitiesXT();
		
		List<NameAndScoreSet> actualList =
			fileUtilitiesXt.DeserializeJsonStringReturnList<NameAndScoreSet>(JsonRawData);
		

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