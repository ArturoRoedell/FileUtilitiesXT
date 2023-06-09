using System;
using System.Collections.Generic;
using System.IO;
using static FileUtilitiesXT.Types;
using static OnScreen;
using LittleHelpersLibrary;

class Start
{
	public static void Main()
	{
		//Setup
		CustomJsonFile<NameAndScoreSet> myJsonFile = new CustomJsonFile<NameAndScoreSet>();
		FileUtilitiesXT fileUtilitiesXt = new FileUtilitiesXT();
		myJsonFile.FileName = "Dice Game Scores";
		myJsonFile.DirPath = Directory.GetCurrentDirectory() + @"\HighScoresFolder";
		fileUtilitiesXt.LoadFileToListThenSortAndCap(myJsonFile, x => x.Score);// Key Feature for DLL
		
		bool simple = selectionTools.YesNoSelection("\n\nDo You Want to enter numbers quickly. Press N to play dice game?");
		
		bool play = true;
		while (play) // Main Loop
		{
			int score;
			//bool simple = selectionTools.YesNoSelection("\n\nDo You Want to enter numbers quickly. Press N to play dice game?");
			if (simple)
			{
				SimpleGame simpleGame = new SimpleGame();
				score = simpleGame.Begin();
			}
			else
			{
				DiceGame diceGame = new DiceGame();
				score = diceGame.Begin(); // KeyFeature ;Starts Game Then Returns Score from Game
			}
			
			myJsonFile.ListData = SudoGUI_HighScore(myJsonFile.ListData, score); //Key Feature
			fileUtilitiesXt.CreateFileSortWriteToJson(myJsonFile, x => x.Score); // Key Feature for DLL
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

class DiceGame
{
	Random rand = new Random(Guid.NewGuid().GetHashCode());
	public int Begin()
	{
		Console.Clear();
		int score = 0;
		bool roll = true;
		while (roll)
		{
			Console.WriteLine("\n### Three One-Hundred sided dice Game ###\n");
			int scoreOne = rand.Next(101);
			int scoreTwo = rand.Next(101);
			int scoreThree = rand.Next(101);
			Console.WriteLine("Your roll die One: " + scoreOne);
			Console.WriteLine("Your roll die Two: " + scoreTwo);
			Console.WriteLine("Your roll die Three: " + scoreThree);
			score = scoreOne + scoreTwo + scoreThree;
			Console.WriteLine("\nYour Current Score: " + score);
			roll = selectionTools.YesNoSelection("Do You Want To Re-Roll?");
			Console.Clear();
		}
		return score;
	}
}

class SimpleGame
{
	public int Begin()
	{
		Console.WriteLine("Enter a score below: ");
		int score = Convert.ToInt32(Console.ReadLine());
		return score;
	}
}

class OnScreen
{
	public static List<NameAndScoreSet> SudoGUI_HighScore(List<NameAndScoreSet> HighScoreList, int score)
	{
		Console.WriteLine("Your Score: " + score);
		Console.Write("\nEnter Player Name: ");
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