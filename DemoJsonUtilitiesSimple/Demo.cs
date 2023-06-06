using System;
using System.Collections.Generic;
using System.IO;
using static FileUtilitiesXT.Types;
using static BoringStuff;
using LittleHelpersLibrary;

class Start
{
	public static void Main()
	{
		CustomJsonFile<NameAndScoreSet> myJsonFile = new CustomJsonFile<NameAndScoreSet>();
		FileUtilitiesXT fileUtilitiesXt = new FileUtilitiesXT();
		myJsonFile.FileName = "Dice Game Scores";
		myJsonFile.DirPath = Directory.GetCurrentDirectory() + @"\HighScoresFolder";
		fileUtilitiesXt.LoadFileToListThenSortAndCap(myJsonFile, x => x.Score);
		bool play = true;
		while (play)
		{
			// "### Three One-Hundred sided dice Game ###"
			Game game = new Game();
			int score = game.Begin();
			myJsonFile.ListData = SudoGUI_HighScore(myJsonFile.ListData, score);
			fileUtilitiesXt.CreateFileSortWriteToJson(myJsonFile, x => x.Score);
			play = selectionTools.YesNoSelection("\n\nDo You Want To Continue Playing?");
		}
		bool clearHighScores = selectionTools.YesNoSelection("Do You Want To Clear High Scores?");
		if (clearHighScores)
		{
			myJsonFile.ListData.Clear();
			fileUtilitiesXt.CreateFileSortWriteToJson(myJsonFile, x => x.Score);
			Console.WriteLine("High Scores cleared!");
			Console.WriteLine("Press Any Key to exit");
			Console.ReadKey(true);
		}
	}
}

class Game
{
	Random rand = new Random(Guid.NewGuid().GetHashCode());
	public int Begin()
	{
		int score = 0;
		Console.WriteLine("\n### Three One-Hundred sided dice Game ###\n");
		bool roll = true;
		while (roll)
		{
			int scoreOne = rand.Next(101);
			int scoreTwo = rand.Next(101);
			int scoreThree = rand.Next(101);
			Console.WriteLine("Your roll die One: " + scoreOne);
			Console.WriteLine("Your roll die Two: " + scoreTwo);
			Console.WriteLine("Your roll die Three: " + scoreThree);
			score = scoreOne + scoreTwo + scoreThree;
			Console.WriteLine("Your Current Score: " + score);
			roll = selectionTools.YesNoSelection("Do You Want To Re-Roll?");
			Console.Clear();
		}
		return score;
	}
}

class BoringStuff
{
	public static List<NameAndScoreSet> SudoGUI_HighScore(List<NameAndScoreSet> HighScoreList, int score)
	{
		Console.Write("\n\nEnter Player Name: ");
		Console.CursorVisible = true;
		string playerName = Console.ReadLine();
		Console.CursorVisible = false;
		Console.Clear();
		AddNamesAndScoresToList(playerName, score, HighScoreList);
		Console.WriteLine("###### TOP SCORES ########");
		string name;
		for (int i = 0; i < 7; i++)
		{
			name = HighScoreList[i].Name;
			Console.WriteLine($"#{i+1}: {name} \nScore:  {HighScoreList[i].Score} \n");
			if (i+1 >= HighScoreList.Count)
			{
				break;
			}
		}
		return HighScoreList;
	}
	
	public static void AddNamesAndScoresToList(String name, int score, List<NameAndScoreSet> list)
	{
		list.Add(new NameAndScoreSet(name, score));
	}
}