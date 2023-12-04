using Renci.SshNet;
using System.IO;

namespace ACE.OmniChannel.FileSystem.Core.Remote
{
    /*Renci.SshNet example : https://github.com/sshnet/SSH.NET
     * Local Directory:             @"C:\Files\Temp\file.csv"
     *                              @"C:\Files\Temp"
     * Ftp Directory(FluentFTP):    @"/Files/Temp/file.csv"
     *                              @"/Files/Temp/
     */
    public abstract class SftpContext
        //: IRemoteFileSystemContext
    {
        protected SftpClient SftpClient { get; set; }


        public void Connect()
        {
            SftpClient.Connect();
        }

        public void Disconnect()
        {
            SftpClient.Disconnect();
        }

        public void Dispose()
        {
            if (SftpClient != null)
            {
                SftpClient.Dispose();
            }
        }

        /*actions*/
        public bool FileExists(string filePath)
        {
            return SftpClient.Exists(filePath);
        }

        public void DeleteFileIfExists(string filePath)
        {
            if (!FileExists(filePath))
            {
                SftpClient.DeleteFile(filePath);
            }
        }

        public void UploadFile(string localFilePath, string remoteFilePath)
        {
            var fileStream = new FileStream(localFilePath, FileMode.Open);
            SftpClient.UploadFile(fileStream, remoteFilePath);
        }


        public bool DirectoryExists(string directoryPath)
        {
            return SftpClient.Exists(directoryPath);
        }

        public void CreateDirectoryIfNotExists(string directoryPath)
        {
            if (!DirectoryExists(directoryPath))
            {
                SftpClient.CreateDirectory(directoryPath);
            }
        }
        public void DeleteDirectoryIfExists(string folderPath)
        {
            if (DirectoryExists(folderPath))
            {
                SftpClient.DeleteDirectory(folderPath);
            }
        }

        public void MoveDirectory(string sourcePath, string destinationPath)
        {
            
        }
        public void DownloadFile(string localFilePath, string remoteFilePath)
        {
            using (Stream fileStream = File.Create(localFilePath))
            {
                SftpClient.DownloadFile(remoteFilePath, fileStream);
            }
        }

        public bool IsConnected()
        {
            return SftpClient.IsConnected;
        }

        public void SetWorkingDirectory(string directoryPath)
        {
            SftpClient.ChangeDirectory(directoryPath);
        }

        public void SetRootAsWorkingDirectory()
        {
            SetWorkingDirectory("");
        }
        public string[] GetNameListing(string folderPath)
        {
            return null;
        }
        public abstract string ServerDetails();
    }
}
