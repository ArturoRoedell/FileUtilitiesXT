using System;
using System.Collections.Generic;
using System.IO;
using static FileUtilitiesXTUtil.FileUtilitiesXT.Types;
using FileUtilitiesXTUtil;
using static Demo472.OnScreen;
using FileUtilitiesXTUtil.LittleHelpersLibrary;

namespace Demo472
{
	class Start476
	
	{
		public static void Main(string[] args)
		{
			CustomJsonFile<NameAndScoreSet> myJsonFile = new CustomJsonFile<NameAndScoreSet>(); // Key Feature
			FileUtilitiesXT fileUtilitiesXt = new FileUtilitiesXT(); // Key Feature45
			myJsonFile.FileName = "Dice Game Scores";
			myJsonFile.DirPath = Directory.GetCurrentDirectory() + @"\HighScoresFolder";
			fileUtilitiesXt.LoadFileToListThenSortAndCap(myJsonFile, nameAndScoreSet => nameAndScoreSet.Score); // Key Feature
			bool play = true;
			while (play) // Main Loop
			{
				//Start simple game
				Console.WriteLine("Enter a score below: ");
				int score = Convert.ToInt32(Console.ReadLine());
				Console.WriteLine("Your Score: " + score + "\nEnter Player Name: ");
				string playerName = Console.ReadLine();
				Console.Clear();

				//Add score to List
				myJsonFile.ListData.Add(new NameAndScoreSet(playerName, score));

				//Then sort score. Sorting scores at this point is only necessary because
				// we chose CreateFileSortWriteToJson after we SudoGUI_HighScore that shows us the high scores
				fileUtilitiesXt.SortScore(myJsonFile, nameAndScoreSet => nameAndScoreSet.Score);

				//Show score on screen
				SudoGUI_HighScore(myJsonFile.ListData);

				//Write file to disk
				fileUtilitiesXt.CreateFileSortWriteToJson(myJsonFile, x => x.Score); // Key Feature

				//Do you want to keep playing user input
				play = selectionTools.YesNoSelection(
					"\nYour scores were sorted then immediately saved to the file." +
					" only the top seven scores will show.\nDo You Want To Continue Playing?");
			}
			bool clearHighScores = selectionTools.YesNoSelection("Do You Want To Clear High Scores?");
			if (clearHighScores)
			{
				//Clearing HighScores
				myJsonFile.ListData.Clear();
				fileUtilitiesXt.CreateFileSortWriteToJson(myJsonFile, x => x.Score); // Key Feature
				Console.WriteLine("High Scores cleared!");
			}
		}
	}

	class OnScreen
	{
		public static void SudoGUI_HighScore(List<NameAndScoreSet> HighScoreList)
		{
			Console.WriteLine("###### TOP SCORES ########");
			string name;
			for (int i = 0; i < 7; i++)
			{
				name = HighScoreList[i].Name;
				Console.WriteLine($"#{i + 1}: {name} Score:  {HighScoreList[i].Score} ");
				if (i + 1 >= HighScoreList.Count)
				{
					break;
				}
			}
		}
	}
}