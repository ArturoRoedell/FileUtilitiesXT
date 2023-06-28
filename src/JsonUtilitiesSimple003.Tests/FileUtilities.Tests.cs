using Xunit.Abstractions;
using Xunit;
using static FileUtilitiesXT.Types;

namespace JsonUtilitiesSimple003.Tests;

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

public class FileUtilitiesBasicTest
{
	private readonly ITestOutputHelper output; //Boiler Plate / *  This Line Lets me output to the unit test window to debug the the test

	public FileUtilitiesBasicTest(ITestOutputHelper output)
	{
		this.output = output; //Bp: This Line Lets me output to the unit test window to debug the the test
	}

	[Fact]
	public void Should_ConcatPathFileNameAndSuffix()
	{
		//Arrange
		FileUtilitiesXT fileUtilitiesXt = new FileUtilitiesXT();
		string path = @"C:\MyFolder";
		string filename = "HighSocres";
		string expected = @"C:\MyFolder\HighSocres.json";

		//Act
		string actual = fileUtilitiesXt.ConcatPathFileNameAndSuffix(path, filename, ".json");

		//Assert
		Xunit.Assert.Equal(expected, actual);
		bool passed = expected == actual;
		output.WriteLine("Should_ConcatPathFileNameAndSuffix passed:" + passed.ToString());
	}

	[Fact]
	public void Should_CheckIfFIleExistsThenCreateFile()
	{
		//Arrange
		FileUtilitiesXT fileUtilitiesXt = new FileUtilitiesXT();
		string FilePath =
			@"C:\Users\ARTURO 001\source\repos\JsonUtilitiesSimple003\src\JsonUtilitiesSimple003.Tests\" +
			@"TestCreationFile\UnicornSecrets.txt";
		File.Delete(FilePath);

		//Act
		fileUtilitiesXt.CheckIfFileExistsThenCreateIt(FilePath);

		//Assert
		Xunit.Assert.True(File.Exists(FilePath));
	}

	[Fact]
	public void Should_DeserializeJsonStringReturnList()
	{
		//Arrange
		FileUtilitiesXT fileUtilitiesXt = new FileUtilitiesXT();
		List<NameScoreDifficulty> expected = new List<NameScoreDifficulty>();
		expected.Add(new NameScoreDifficulty("Arty", 481, _difficulty.Easy));
		expected.Add(new NameScoreDifficulty("Jessica", 3454, _difficulty.Medium));
		string JsonRawData =
			@"[
  {
    ""Name"": ""Arty"",
    ""Score"": 481,
    ""Difficulty"": 0
  },
  {
    ""Name"": ""Jessica"",
    ""Score"": 3454,
    ""Difficulty"": 1
  }
]
";

		//Act
		List<NameScoreDifficulty> actual = fileUtilitiesXt.DeserializeJsonStringReturnList<NameScoreDifficulty>(JsonRawData);

		//Assert
		bool passed = true;
		for (int i = 0; i < actual.Count; i++)
		{
			if (!(actual[i].Name == expected[i].Name))
			{
				passed = false;
			}
			if (!(actual[i].Score == expected[i].Score))
			{
				passed = false;
			}
			if (!(actual[i].Difficulty == expected[i].Difficulty))
			{
				passed = false;
			}
		}
		Xunit.Assert.True(passed);
	}

	[Fact]
	public void Should_SerializeJsonDataReturnString()
	{
		//Arrange
		CustomJsonFile<NameScoreDifficulty> myJsonFile = new CustomJsonFile<NameScoreDifficulty>();
		FileUtilitiesXT fileUtilitiesXt = new FileUtilitiesXT();
		TestData testData = new TestData();
		myJsonFile = testData.SetDataJsonFile();
		List<NameScoreDifficulty> listData = new List<NameScoreDifficulty>();
		listData.Add(new NameScoreDifficulty("Arty", 481, _difficulty.Medium));
		listData.Add(new NameScoreDifficulty("Jessica", 3454, _difficulty.Hard));
		string expectedJsonData =
			@"[
  {
    ""Name"": ""Arty"",
    ""Score"": 481
  },
  {
    ""Name"": ""Jessica"",
    ""Score"": 3454
  }
]
";
		//Act
		string actualJsonData = fileUtilitiesXt.SerializeJsonDataReturnString(listData);

		//Assert
		Xunit.Assert.Matches(expectedJsonData, actualJsonData);
	}

	[Fact]
	public void Should_CreateFileSortWriteToJson()
	{
		//Arrange
		CustomJsonFile<NameScoreDifficulty> myJsonFile = new CustomJsonFile<NameScoreDifficulty>();
		FileUtilitiesXT fileUtilitiesXt = new FileUtilitiesXT();
		TestData testData = new TestData();
		myJsonFile = testData.SetDataJsonFile();
		string expected =
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
  },
  {
    ""Difficulty"": 1,
    ""Name"": ""Jessica"",
    ""Score"": 462
  }
]" + "\n";

		//Act
		fileUtilitiesXt.CreateFileSortWriteToJson<NameScoreDifficulty>(myJsonFile, x => x.Score);
		string actual = fileUtilitiesXt.ReadFromFile(myJsonFile.PathFileNameAndSuffix);

		//Assert

		output.WriteLine("Start Actual:\n" + actual);
		output.WriteLine("Start Expected:\n" + expected);

		bool passed = expected == actual;
		output.WriteLine("Should_CreateFileSortWriteToJson passed:" + passed.ToString());

		Xunit.Assert.Equal(expected, actual);
	}

	[Fact]
	public void Should_LoadFileToListThenSortAndCap()
	{
		//Arrange
		CustomJsonFile<NameScoreDifficulty> myJsonFile = new CustomJsonFile<NameScoreDifficulty>();
		FileUtilitiesXT fileUtilitiesXt = new FileUtilitiesXT();
		TestData testData = new TestData();
		myJsonFile = testData.SetDataJsonFile();
		string testDirectory = Directory.GetCurrentDirectory() + @"\" + "LoadFileToListThenSortAndCapTest";
		string fileName = "Should_LoadFileToListThenSortAndCapTestFile";
		myJsonFile.FileName = fileName;
		myJsonFile.DirPath = testDirectory;
		string filePath = testDirectory + "/" + fileName + ".json";
		Directory.CreateDirectory(testDirectory);
		FileStream fileStream = File.Create(filePath);
		fileStream.Close();
		using (StreamWriter outputFile = new StreamWriter(filePath))
		{
			outputFile.WriteLine(myJsonFile.JsonFormat);
			outputFile.Close();
		}
		List<NameScoreDifficulty> actual = new List<NameScoreDifficulty>();
		actual.Add(new NameScoreDifficulty("Cherry", 3454, _difficulty.Medium));
		actual.Add(new NameScoreDifficulty("Arty", 865, _difficulty.Hard));
		actual.Add(new NameScoreDifficulty("Arty", 481, _difficulty.Easy));

		//Act
		myJsonFile.ListData.Clear();
		fileUtilitiesXt.LoadFileToListThenSortAndCap<NameScoreDifficulty>(myJsonFile, x => x.Score, 3);

		//Assert
		List<NameScoreDifficulty> expected = myJsonFile.ListData;
		bool passed = true;
		for (int i = 0; i < actual.Count; i++)
		{
			if (!(actual[i].Name == expected[i].Name))
			{
				passed = false;
			}
			if (!(actual[i].Score == expected[i].Score))
			{
				passed = false;
			}
			if (!(actual[i].Difficulty == expected[i].Difficulty))
			{
				passed = false;
			}

		}
		Xunit.Assert.True(passed);
	}

	[Fact]
	public void Should_SortScore()
	{
		//Arrange
		CustomJsonFile<NameScoreDifficulty> myJsonFile = new CustomJsonFile<NameScoreDifficulty>();
		FileUtilitiesXT fileUtilitiesXt = new FileUtilitiesXT();
		TestData testData = new TestData();
		myJsonFile = testData.SetDataJsonFile();

		//Act
		fileUtilitiesXt.SortScore<NameScoreDifficulty>(myJsonFile, x => x.Score);
		List<NameScoreDifficulty> actual = new List<NameScoreDifficulty>();
		actual.Add(new NameScoreDifficulty("Cherry", 3454, _difficulty.Medium));
		actual.Add(new NameScoreDifficulty("Arty", 865, _difficulty.Hard));
		actual.Add(new NameScoreDifficulty("Arty", 481, _difficulty.Easy));
		List<NameScoreDifficulty> expected = myJsonFile.ListData;

		//Assert
		bool passed = true;
		for (int i = 0; i < actual.Count; i++)
		{
			if (!(actual[i].Score == expected[i].Score))
			{
				passed = false;
			}
		}
		Xunit.Assert.True(passed);
	}

	[Fact]
	public void Should_ErraseOverflow()
	{
		//Arrange
		FileUtilitiesXT fileUtilitiesXt = new FileUtilitiesXT();
		List<char> ExpectedList = new List<char>();
		ExpectedList.Add('a');
		ExpectedList.Add('b');
		ExpectedList.Add('c');
		ExpectedList.Add('d');
		List<char> ActualList = new List<char>();
		ActualList.Add('a');
		ActualList.Add('b');
		//Act
		fileUtilitiesXt.ErraseOverflow(ExpectedList, 2);
		//Assert
		Assert.Equal(ExpectedList.Count, ActualList.Count);
	}

	[Fact]
	public void Should_AppendToFile()
	{
		//Arrange
		FileUtilitiesXT fileUtilitiesXt = new FileUtilitiesXT();

		string original = "1 2 3 4 ", appendThis = "5 6";
		string ActualFilepath = Directory.GetCurrentDirectory() + "\\TestAppendtoFileActual.txt";
		File.Delete(ActualFilepath);
		fileUtilitiesXt.FastCreateWriteFile(original, ActualFilepath);

		string expected = "1 2 3 4 5 6";
		string ExpectedFilepath = Directory.GetCurrentDirectory() + "\\TestAppendtoFileExpected.txt";
		fileUtilitiesXt.FastCreateWriteFile(expected, ExpectedFilepath);

		//Act
		fileUtilitiesXt.AppendToFile(ActualFilepath, appendThis);

		//Assert
		bool passed = LittleHelpersLibrary.Comparison.FileCompare(ExpectedFilepath, ActualFilepath);
		Assert.True(passed);
	}
	
	[Fact]
	public void Should_FastCreateWriteFile()
	{
		//Arrange
		FileUtilitiesXT fileUtilitiesXt = new FileUtilitiesXT();  // Key Feature

		string expectedContent = "I am  a string serving as content for an example";
		
		//Act
		fileUtilitiesXt.FastCreateWriteFile(expectedContent);

		//Assert
		string actualContent = fileUtilitiesXt.FastReadFile();
		Assert.Equal(expectedContent,actualContent);
	}
	
	
}
