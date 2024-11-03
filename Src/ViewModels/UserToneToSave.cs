namespace Integra7AuralAlchemist.ViewModels;

public class UserToneToSave(string newName, int zeroBasedMemoryId)
{
    public int ZeroBasedMemoryId => zeroBasedMemoryId;
    public string NewName => newName;
}