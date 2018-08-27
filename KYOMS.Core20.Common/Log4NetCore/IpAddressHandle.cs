using System;
using System.Net;
using System.Net.Sockets;

namespace KYOMS.Core20.Common.Log4NetCore
{
    public static class IpAddressHandle
    {
        public static string GetLocalIp()
        {
            try
            {
                var hostName = Dns.GetHostName(); //得到主机名
                IPHostEntry ipEntry = Dns.GetHostEntry(hostName);
                for (int i = 0; i < ipEntry.AddressList.Length; i++)
                {
                    //从IP地址列表中筛选出IPv4类型的IP地址
                    //AddressFamily.InterNetwork表示此IP为IPv4,
                    //AddressFamily.InterNetworkV6表示此地址为IPv6类型
                    if (ipEntry.AddressList[i].AddressFamily == AddressFamily.InterNetwork)
                    {
                        return ipEntry.AddressList[i].ToString();
                    }
                }
                return "";
            }
            catch (Exception)
            {
                return "";
            }
        }
    }
}
