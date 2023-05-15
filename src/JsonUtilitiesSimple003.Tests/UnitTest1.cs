using FileUtilities;
using FileUtilities.Types;


namespace JsonUtilitiesSimple003;

public class ConcatPathFileNameAndSuffixTests
{
	[Fact]
	public void Should_PutPathTotgether()
	{
		string path = @"C:\MyFolder";
		string filename = "HighSocres";
		string expected = @"C:\MyFolder\HighSocres.json";
		string actual = FileUtilitiesBasic.ConcatPathFileNameAndSuffix(path, filename, ".json");
		Assert.Equal(expected, actual);
	}
}

public class FileUtilitiesBasicTesting
{
	[Fact]
	public void Should_CheckIfFIleExistsThenCreateFile()
	{
		string FilePath =
			@"C:\Users\ARTURO 001\source\repos\JsonUtilitiesSimple003\src\JsonUtilitiesSimple003.Tests\" + 
			@"TestCreationFile\UnicornSecrets.txt";
		FileUtilitiesBasic.CheckIfFileExistsThenCreateIt(FilePath);
		Assert.True(File.Exists(FilePath));
		File.Delete(FilePath);
	}
}

// string NonExistingFolder = 
// 	@"C:\Users\ARTURO 001\source\repos\JsonUtilitiesSimple003\src\JsonUtilitiesSimple003.Tests\"+
// 	@"TestCreationFile\IdontExist\UnicornSecrets2.txt";
// Directory.Exists(NonExistingFolder);

