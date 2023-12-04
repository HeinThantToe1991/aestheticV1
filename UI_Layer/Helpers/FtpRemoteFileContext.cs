using ACE.OmniChannel.FileSystem.Core.Remote;
using FluentFTP;
using UI_Layer.Models;

namespace ACE.OmniChannel.Retail.UI
{
    class FtpRemoteFileSystem : FtpContext
    {
        private string _serverDetails;

        public FtpRemoteFileSystem(RemoteSystemSetting setting)
        {
            _serverDetails = FtpHelper.ServerDetails(setting.Host, setting.Port.ToString(), setting.UserName, setting.Type);
            FtpClient = new FtpClient(setting.Host);
            FtpClient.Credentials = new System.Net.NetworkCredential(setting.UserName, setting.Password);
            FtpClient.Port = setting.Port;
        }

        public override string ServerDetails()
        {
            return _serverDetails;
        }
    }
}
