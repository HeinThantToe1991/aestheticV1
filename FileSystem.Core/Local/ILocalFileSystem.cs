namespace ACE.OmniChannel.FileSystem.Core.Local
{
    public interface ILocalFileSystem 
    {
        string RootDirectoryPath { get; }
        bool Exists();
        void CreateIfNotExists();
    }
}
