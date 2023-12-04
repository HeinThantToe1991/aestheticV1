namespace ACE.OmniChannel.FileSystem.Core
{
    public interface IFileSystem
    {
        bool FileExists(string filePath);
        bool DirectoryExists(string directoryPath);
        void CreateDirectoryIfNotExists(string directoryPath);
        void DeleteFileIfExists(string filePath);
        void DeleteDirectoryIfExists(string directoryPath);
        void CopyDirectory(string localPath,string ftpPath,string sourcePath, string destinationPath);
        void MoveDirectory(string localPath, string ftpPath, string sourcePath, string destinationPath);

        string[] GetNameListing(string folderPath);
    }
}
