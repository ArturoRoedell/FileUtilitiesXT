public class  selectionTools
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
		do{
			string repeatString = ChoiceAsk + "\n Y /  N";
			//int choice = CaseSelect(repeatString);
			string choice = CaseSelect(repeatString);
			switch (choice)
			{
				case "y": loopme = false; break;
				case "Y" : loopme = false; break;
				case "n": BoolYes = false; loopme = false; break;
				case "N" : BoolYes = false; break;
				default:
					break;
			}
        
		} while (loopme);

		return BoolYes;
	}
}