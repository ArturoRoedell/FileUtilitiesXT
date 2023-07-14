using System;
using System.IO;

namespace FileUtilitiesXTUtil.LittleHelpersLibrary
{
	public class selectionTools
	{
		public static string CaseSelect(string repeatString)
		{
			string ChoseDemoTest;
			string select = "0";
			bool loopMe = true;
			do
			{
				Console.WriteLine(repeatString);
				ChoseDemoTest = null;
				while (true)
				{
					var key = System.Console.ReadKey(false);
					if (key.Key == ConsoleKey.Enter)
						break;
					ChoseDemoTest += key.KeyChar;
				}

				if (ChoseDemoTest == "y" || ChoseDemoTest == "n" || ChoseDemoTest == "Y" || ChoseDemoTest == "N")
				{
					select = ChoseDemoTest;
					loopMe = false;
				}
			} while (loopMe);
			return select;
		}

		public static bool YesNoSelection(string ChoiceAsk)
		{
			bool loopme = true;
			bool BoolYes = true;
			do
			{
				string repeatString = ChoiceAsk + "\n Y /  N";
				string choice = CaseSelect(repeatString);
				switch (choice)
				{
					case "y":
						loopme = false;
						break;
					case "Y":
						loopme = false;
						break;
					case "n":
						BoolYes = false;
						loopme = false;
						break;
					case "N":
						BoolYes = false;
						break;
					default:
						break;
				}

			} while (loopme);

			return BoolYes;
		}
	}

	public class Comparison
	{
		public static bool FileCompare(string file1, string file2)
		{
			int file1byte;
			int file2byte;
			FileStream fs1;
			FileStream fs2;

			if (file1 == file2)
			{
				return true;
			}

			fs1 = new FileStream(file1, FileMode.Open);
			fs2 = new FileStream(file2, FileMode.Open);
			if (fs1.Length != fs2.Length)
			{
				// Close the file
				fs1.Close();
				fs2.Close();
				return false;
			}
			do
			{
				file1byte = fs1.ReadByte();
				file2byte = fs2.ReadByte();
			} while ((file1byte == file2byte) && (file1byte != -1));
			fs1.Close();
			fs2.Close();
			return ((file1byte - file2byte) == 0);
		}
	}
}
