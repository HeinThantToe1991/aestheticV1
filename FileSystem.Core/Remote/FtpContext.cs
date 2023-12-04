using FluentFTP;
using System.Collections.Generic;

namespace ACE.OmniChannel.FileSystem.Core.Remote
{
    /*FluentFTP example : https://github.com/robinrodricks/FluentFTP
     * Local Directory:             @"C:\Files\Temp\file.csv"
     *                              @"C:\Files\Temp"
     * Ftp Directory(FluentFTP):    @"/Files/Temp/file.csv"
     *                              @"/Files/Temp/
     */
    public abstract class FtpContext : IRemoteFileSystemContext
    {
        protected IFtpClient FtpClient { get; set; }

        
        
        public void Connect()
        {
            FtpClient.Connect();
        }

        public void Disconnect()
        {
            FtpClient.Disconnect();
        }

        public void Dispose()
        {
            if (FtpClient != null && !FtpClient.IsDisposed)
            {
                FtpClient.Dispose();
            }
        }

        /*actions*/
        public bool FileExists(string filePath)
        {
            return FtpClient.FileExists(filePath);
        }

        public void DeleteFileIfExists(string filePath)
        {
            if (FileExists(filePath))
            {
                FtpClient.DeleteFile(filePath);
            }
        }

        public void UploadFile(string localFilePath, string remoteFilePath)
        {
            FtpClient.UploadFile(localFilePath, remoteFilePath);
        }


        public bool DirectoryExists(string directoryPath)
        {
            return FtpClient.DirectoryExists(directoryPath);
        }

        public void CreateDirectoryIfNotExists(string directoryPath)
        {
            if (!DirectoryExists(directoryPath))
            {
                FtpClient.CreateDirectory(directoryPath);
            }
        }

        public void DownloadFile(string localFilePath, string remoteFilePath)
        {
            FtpClient.DownloadFile(localFilePath, remoteFilePath);
        }

        public bool IsConnected()
        {
            return FtpClient.IsConnected;
        }
        
        public void SetWorkingDirectory(string directoryPath)
        {
            FtpClient.SetWorkingDirectory(directoryPath);
        }

        public void SetRootAsWorkingDirectory()
        {
            SetWorkingDirectory("");
        }

        public abstract string ServerDetails();

        public void DeleteDirectoryIfExists(string folderPath)
        {
            if (DirectoryExists(folderPath))
            {
                FtpClient.DeleteDirectory(folderPath);
            }
        }
        public void CopyDirectory(string localPath, string ftpPath, string sourcePath, string destinationPath)
        {

            if (DirectoryExists(sourcePath))
            {
                var fileNames = FtpClient.GetListing(sourcePath, FtpListOption.Auto);

                foreach (var file in fileNames)
                {
                    string fileName = file.FullName.ToString();
                    string localFileName = localPath + "\\" + file.Name;
                    string destinationFileName = destinationPath + "/" + file.Name;
                    FtpClient.DownloadFile(localFileName, fileName, true);
                    FtpClient.UploadFile(localFileName, destinationFileName, FtpExists.Overwrite);
                    //FtpClient.DeleteFile(fileName);
                    System.IO.File.Delete(localFileName);

                }
            }
        }

        public void MoveDirectory(string localPath,string ftpPath,string sourcePath, string destinationPath)
        {

            if (DirectoryExists(sourcePath))
            {
                var fileNames = FtpClient.GetListing(sourcePath, FtpListOption.Auto);

                foreach (var file in fileNames)
                {
                    string fileName = file.FullName.ToString();
                    string localFileName = localPath + "\\" + file.Name;
                    string destinationFileName = destinationPath + "/" + file.Name;
                    FtpClient.DownloadFile(localFileName, fileName,true);
                    FtpClient.UploadFile(localFileName, destinationFileName,FtpExists.Overwrite);
                    FtpClient.DeleteFile(fileName);
                    System.IO.File.Delete(localFileName);

                }
            }
        }

        public string[] GetNameListing(string folderPath)
        {
            List<string> resultList = new List<string>();

            try
            {
                var nameListing = FtpClient.GetListing(folderPath, FtpListOption.Auto);
                foreach (var item in nameListing)
                {
                    resultList.Add(item.FullName);
                }
            }
            catch(FtpException ex)
            {
                return resultList.ToArray(); 
            }
            
            return resultList.ToArray();
        }
    }
}
