namespace ACE.OmniChannel.FileSystem.Core.Local
{
    public class LocalFileSystem : ILocalFileSystem
    {
        public string RootDirectoryPath
        {
            get;
            private set;
        }

        public LocalFileSystem(string rootDirectoryPath)
        {
            RootDirectoryPath = rootDirectoryPath;
        }

        public bool Exists()
        {
            return DirectoryExists(RootDirectoryPath);
        }

        public void CreateIfNotExists()
        {
            CreateDirectoryIfNotExists(RootDirectoryPath);
        }

        public void CreateDirectoryIfNotExists(string directoryPath)
        {
            if (!DirectoryExists(directoryPath))
            {
                System.IO.Directory.CreateDirectory(directoryPath);
            }
        }
        public void DeleteDirectoryIfExists(string directoryPath)
        {
            if (!DirectoryExists(directoryPath))
            {
                System.IO.Directory.Delete(directoryPath);
            }
        }
        public void DeleteFileIfExists(string filePath)
        {
            if (FileExists(filePath))
            {
                System.IO.File.Delete(filePath);
            }
        }
        public void MoveDirectory(string sourcePath, string destinationPath)
        {
            if (FileExists(destinationPath))
            {
                System.IO.File.Move(sourcePath, destinationPath);
            }
        }
        public bool DirectoryExists(string directoryPath)
        {
            return System.IO.Directory.Exists(directoryPath);
        }

        public bool FileExists(string filePath)
        {
            return System.IO.File.Exists(filePath);
        }

        
    }
}
