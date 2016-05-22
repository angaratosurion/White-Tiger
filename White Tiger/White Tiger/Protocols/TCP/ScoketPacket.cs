using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace White_Tiger.Protocols.TCP
{
   
    public class SocketPacket
    {
        public System.Net.Sockets.Socket m_currentSocket;
        public byte[] dataBuffer = new byte[1];
    }
}
