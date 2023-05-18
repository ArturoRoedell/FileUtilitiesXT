using System;
using static FileUtilitiesXT.Types;
using static FileUtilitiesXT;

/* ## MY TASKS
* 
*/
public struct TestData
{
	public CustomJsonFile<NameScoreDifficulty> SetDataJsonFile()
	{
		List<NameScoreDifficulty> HighScoreListex = new List<NameScoreDifficulty>();
		AddNameScoreDifficultyToList("Arty", 481, _difficulty.Easy, HighScoreListex);
		AddNameScoreDifficultyToList("Cherry", 3454, _difficulty.Medium, HighScoreListex);
		AddNameScoreDifficultyToList("Jessica", 462, _difficulty.Medium, HighScoreListex);
		AddNameScoreDifficultyToList("Arty", 865, _difficulty.Hard, HighScoreListex);
		CustomJsonFile<NameScoreDifficulty> myJsonFile = new CustomJsonFile<NameScoreDifficulty>();
		myJsonFile.FileName = "MyTestHighScoreFile";
		string CurrentDir = Directory.GetCurrentDirectory();
		myJsonFile.DirPath = CurrentDir + @"\HighScoresFolderUnitTest";
		myJsonFile.ListData = HighScoreListex;
		return myJsonFile;
	}
	
	public static void AddNameScoreDifficultyToList(String name, int score, _difficulty Difficulty, List<NameScoreDifficulty> list)
	{
		list.Add(new NameScoreDifficulty(name, score, Difficulty));
	}
}

public enum _difficulty
{
	Easy,
	Medium,
	Hard
}

public class NameScoreDifficulty
{
	public _difficulty Difficulty { get; set; }
	public string Name { get; set; }
	public int Score { get; set; }
	public NameScoreDifficulty(string name, int score, _difficulty difficulty)
	{
		this.Name = name;
		this.Score = score;
		this.Difficulty = difficulty;
	}

	public override string ToString()
	{
		return "Name: " + Name + "   Score: " + Score + "   Difficulty: " + Difficulty;
	}
}

class Program
{
	static void Main()
	{
		DebugTests.Bebug01.Begin();
		//Should_LoadFileToListThenSortAndCap();

	}
	
	public static void Should_LoadFileToListThenSortAndCap()
	{
		//Arrange
		Types.CustomJsonFile<NameScoreDifficulty> myJsonFile = new Types.CustomJsonFile<NameScoreDifficulty>();
		FileUtilitiesXT fileUtilitiesXt = new FileUtilitiesXT();
		TestData testData = new TestData();
		myJsonFile = testData.SetDataJsonFile();

		string jsonString = 
			@"[
  {
    ""Difficulty"": 1,
    ""Name"": ""Cherry"",
    ""Score"": 3454
  },
  {
    ""Difficulty"": 2,
    ""Name"": ""Arty"",
    ""Score"": 865
  },
  {
    ""Difficulty"": 0,
    ""Name"": ""Arty"",
    ""Score"": 481
  }
]
";
		string testDirectory = Directory.GetCurrentDirectory() + "LoadFileToListThenSortAndCapTest";
		string fileName = "Should_LoadFileToListThenSortAndCapTestFile";
		myJsonFile.FileName = fileName;
		myJsonFile.DirPath = testDirectory;
		string filePath = testDirectory + "/" + fileName + ".json";
		Directory.CreateDirectory(testDirectory);
		FileStream fileStream = File.Create(filePath);
		fileStream.Close();

		using (StreamWriter outputFile = new StreamWriter(filePath))
		{
			outputFile.WriteLine(jsonString);
			outputFile.Close();
		}
		
		//Act
		fileUtilitiesXt.LoadFileToListThenSortAndCap<NameScoreDifficulty>(myJsonFile, x => x.Score,25);
		
		Console.WriteLine(myJsonFile.JsonFormat);
		//Assert
		//Assert.True(true);
		//Assert.True(false);
	}
	
	
}


	
	