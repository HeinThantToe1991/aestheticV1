using System;

namespace ACE.OmniChannel.Retail.UI
{
    public class LocalMachineHelper
    {
        /*
            https://stackoverflow.com/questions/1768198/how-do-i-get-the-computer-name-in-net
            https://stackoverflow.com/questions/1233217/difference-between-systeminformation-computername-environment-machinename-and
         */
        public static string ServerDetails()
        {
            string machineName = String.Empty;
            string hostName = String.Empty;
            string computerName = String.Empty;
            try
            {
                machineName = Environment.MachineName;
                hostName = System.Net.Dns.GetHostName();
                computerName = Environment.GetEnvironmentVariable("COMPUTERNAME");
            }
            catch (Exception ex)
            {

            }

            string details = String.Format("MachineName:'{0}' HostName:'{1}' ComputerName:'{2}'", machineName, hostName, computerName);
            return details;
        }
    }
}
