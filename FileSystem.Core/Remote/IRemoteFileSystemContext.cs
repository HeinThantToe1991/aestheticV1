using System;

namespace ACE.OmniChannel.FileSystem.Core.Remote
{
    public interface IRemoteFileSystemContext : IFileSystem, IDisposable
    {
        bool IsConnected();
        void Connect();
        void Disconnect();

        void SetWorkingDirectory(string path);
        void SetRootAsWorkingDirectory();

        void UploadFile(string localFilePath, string remoteFilePath);
        void DownloadFile(string localFilePath, string remoteFilePath);

        string ServerDetails();
    }
}