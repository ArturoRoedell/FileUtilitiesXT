namespace FileUtilitiesXT.Tests;

public class Practice
{
	public static void PracticeStart()
	{
		// var relativePath = Path.GetRelativePath(
		// 	@"C:\Program Files\Dummy Folder\MyProgram",
		// 	@"C:\Program Files\Dummy Folder\MyProgram\Data\datafile1.dat");
		
		//			@"C:\Users\ARTURO 001\source\repos\JsonUtilitiesSimple003\src\FileUtilitiesXT.Tests\" + //TODO - MAKE MODULAR MAINTASK!
		//@"TestCreationFile\UnicornSecrets.txt";
		
		
		string sCurrentDirectory = AppDomain.CurrentDomain.BaseDirectory;
		Console.WriteLine(sCurrentDirectory);
		string sFile = System.IO.Path.Combine(sCurrentDirectory, @"..\..\..\Data\Orders\Test.xml");  
		string sFilePath = Path.GetFullPath(sFile);
		Console.WriteLine(sFilePath);

		string relative009 = $@"..\..\..\TestCreationFile\UnicornSecrets.txt";
		string Test99999 = Path.GetFullPath(relative009);
		Console.WriteLine("GetFull: " + Test99999);
		
		string transfer700 = Directory.GetCurrentDirectory();
		
		string newpath005 = Path.Combine(transfer700,relative009);
		Console.WriteLine("newpath005 " + newpath005);

		return;
		string relative = $@"TestCreationFile\UnicornSecrets.txt";
		string relative01 = $@"TestCreationFile\UnicornSecrets.txt";
		string relative02 = $@"TestCreationFile\UnicornSecrets.txt";
		string relative03 = $@"TestCreationFile\UnicornSecrets.txt";
		//string FilePath = Path.GetFullPath(relative);
		string FilePath = Path.GetFullPath(relative);
		Console.WriteLine("Relative to Fill path: " + FilePath);
		return;

		string CurrentTestDir = Directory.GetCurrentDirectory();
		
		var relativePath = Path.GetRelativePath(CurrentTestDir,
				@"C:\Users\ARTURO 001\source\repos\JsonUtilitiesSimple003\src\FileUtilitiesXT.Tests\TestCreationFile"
			);
		Console.WriteLine("Current: "+ CurrentTestDir);
		Console.WriteLine("Relatite TestCreation FIle: " + relativePath);


		string NewString = MakeRelativePath( CurrentTestDir,
			@"C:\Users\ARTURO 001\source\repos\JsonUtilitiesSimple003\src\FileUtilitiesXT.Tests\TestCreationFile"
		);
		
		Console.WriteLine("Third: " + NewString);
	}
	
	public static String MakeRelativePath(String fromPath, String toPath)
	{
		if (String.IsNullOrEmpty(fromPath)) throw new ArgumentNullException("fromPath");
		if (String.IsNullOrEmpty(toPath))   throw new ArgumentNullException("toPath");

		Uri fromUri = new Uri(fromPath);
		Uri toUri = new Uri(toPath);

		if (fromUri.Scheme != toUri.Scheme) { return toPath; } // path can't be made relative.

		Uri relativeUri = fromUri.MakeRelativeUri(toUri);
		String relativePath = Uri.UnescapeDataString(relativeUri.ToString());

		if (toUri.Scheme.Equals("file", StringComparison.InvariantCultureIgnoreCase))
		{
			relativePath = relativePath.Replace(Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar);
		}

		return relativePath;
	}
	
	
}