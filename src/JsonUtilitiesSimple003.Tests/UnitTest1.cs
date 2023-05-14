using FileUtilities;
using FileUtilities.Types;


namespace JsonUtilitiesSimple003;

public class ConcatPathFileNameAndSuffixTests
{
	public ConcatPathFileNameAndSuffixTests()
	{
		
	}
	
	[Fact]
	public void PutPathTotgether()
	{
		string path = @"C:\Users\ARTURO 001\source\repos\001ScratchCode";
		string filename = "HighSocres";
		string expected = @"C:\Users\ARTURO 001\source\repos\001ScratchCode\HighSocres.json";
		string actual = FileUtilitiesBasic.ConcatPathFileNameAndSuffix(path, filename, ".json");
		Assert.Equal(expected, actual);
	}
}


// public class FileUtilitiesBasic
// {
// 	public void ShouldCheckIfFIleExistsThenCreateFile()
// 	{
// 		//FileUtilitiesBasic
// 	}
// }