using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using White_Tiger.Commands;
using System.Net;
using System.Net.Sockets;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using White_Tiger.DataContracts;
namespace White_Tiger.Protocols.TCP
{
   public  class TCPServer
    {
       List<ICommand> commands = new List<ICommand>();
       public const int defaultport = 49152;
       Socket mainSocket;
       SocketPermission permision;
       IPAddress ipAddress;
       IPHostEntry ipHost;
       IPEndPoint ipEndpoint;
       public AsyncCallback pfnWorkerCallBack;
       private int clientCount = 0;
 
       private Socket[] m_workerSocket = new Socket[200];
       
       public TCPServer()
       { 
           ICommand [] cmd = {new LoadTable(), new SaveTable(),new CreateTable(), new CreateDatabase(),
           new EncryptTable(),new DecryptTable(),new GetColumns(), new isTableEncrypted(),new ListTables(),new FindCommand(),
                             new ListDatabases(),new GetVersion()};
           commands.AddRange(cmd);
       }
       public void StartServer()
       {
           try
           {
               permision = new SocketPermission(NetworkAccess.Accept, TransportType.Tcp, "", SocketPermission.AllPorts);
               permision.Demand();
               ipHost = Dns.GetHostEntry(Environment.MachineName);
               ipAddress = ipHost.AddressList[0];
               ipEndpoint =new IPEndPoint(ipAddress, defaultport);
               mainSocket = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
               mainSocket.Bind(ipEndpoint);
               mainSocket.Listen(200000);
            
               mainSocket.BeginAccept(new AsyncCallback(OnClientConnect), null);

           }
           catch (Exception ex)
           {
               program.errorreport(ex);
               
           }

       }
       /// <summary>
       /// This event occures when the client will connect to the server
       /// </summary>
       /// <param name="asyn"></param>
       public void OnClientConnect(IAsyncResult asyn)
       {
           try
           {
               
               m_workerSocket[clientCount] = mainSocket.EndAccept(asyn);
               
               WaitForData(m_workerSocket[clientCount]);

              
               ++clientCount;
             //  Console.WriteLine(m_workerSocket[clientCount].AddressFamily.ToString());
               mainSocket.BeginAccept(new AsyncCallback(OnClientConnect), null);
           }
           catch (ObjectDisposedException)
           {
               System.Diagnostics.Debugger.Log(0, "1", "\n OnClientConnection: Socket has been closed\n");
           }
           catch (Exception ex)
           {
               program.errorreport(ex);
           }

       }
       /// <summary>
       /// Start waiting for data from the client
       /// </summary>
       /// <param name="soc"></param>
       public void WaitForData(System.Net.Sockets.Socket soc)
       {
           try
           {
               if (pfnWorkerCallBack == null)
               {
                   // Specify the call back function which is to be 
                   // invoked when there is any write activity by the 
                   // connected client
                   pfnWorkerCallBack = new AsyncCallback(OnDataReceived);
               }
               SocketPacket theSocPkt = new SocketPacket();
               theSocPkt.m_currentSocket = soc;
               // Start receiving any data written by the connected client
               // asynchronously
               soc.BeginReceive(theSocPkt.dataBuffer, 0,
                                  theSocPkt.dataBuffer.Length,
                                  SocketFlags.None,
                                  pfnWorkerCallBack,
                                  theSocPkt);
           }
           catch (Exception ex)
           {
               program.errorreport(ex);
           }

       }
      /// <summary>
      /// This call back will be executed when the server a client start transmiting data
      /// </summary>
      /// <param name="asyn"></param>
       public void OnDataReceived(IAsyncResult asyn)
       {
           try
           {
               SocketPacket socketData = (SocketPacket)asyn.AsyncState;

               int iRx = 0;
               
               iRx = socketData.m_currentSocket.EndReceive(asyn);
               char[] chars = new char[iRx + 1];
               System.Text.Decoder d = System.Text.Encoding.UTF8.GetDecoder();
               int charLen = d.GetChars(socketData.dataBuffer,
                                        0, iRx, chars, 0);
               System.String szData = new System.String(chars);
           //    if (szData.Contains("ENDOFMESSAGE"))
               {
                 //  szData.Replace("ENDOFMESSAGE", "");
                 
                   
                   string res = this.ExecuteCommand(szData);


                  
                   Object objData = res + "ENDOFMESSAGE";
                   byte[] byData = System.Text.Encoding.UTF8.GetBytes(objData.ToString());
                   if (socketData.m_currentSocket != null)
                   {
                       socketData.m_currentSocket.Send(byData);

                       for (int i = 0; i < clientCount; i++)
                       {
                           if (m_workerSocket[i].RemoteEndPoint == socketData.m_currentSocket.RemoteEndPoint)
                           {
                               if (m_workerSocket[i] != null)
                               {
                                   if (m_workerSocket[i].Connected)
                                   {
                                       m_workerSocket[i].Send(byData);
                                   }
                               }
                           }
                       }
                   }

               }
               // Continue the waiting for data on the Socket
               WaitForData(socketData.m_currentSocket);
           }
           catch (ObjectDisposedException)
           {
               System.Diagnostics.Debugger.Log(0, "1", "\nOnDataReceived: Socket has been closed\n");
           }
           catch (Exception ex)
           {
               program.errorreport(ex);
           }
       }
       /// <summary>
       /// Searches the command sent from the client and executes it
       /// </summary>
       /// <param name="comman"></param>
       /// <returns></returns>
       public string ExecuteCommand(String comman)
       {

           try
           {
               string ap = "";
               if(comman !=null)
               {
                   CommandModel cms = JsonConvert.DeserializeObject<CommandModel>(comman);
                   foreach ( ICommand cmd in commands)
                   {
                        /* args[0] root
                            args[1] record tag
                            * args[2] username
                            * args[3] password
                            * args[4] db name
                            * args[5] tablename
                           */
                       string[] args = new string[10];
                       args[0] = cms.root;
                       args[1] = cms.recordtag;
                       args[2] = cms.username;
                       args[3] = cms.password;
                       args[4] = cms.dbname;
                       args[5] = cms.tablename;
                       args[6] = cms.data;
                       args[7] = cms.algorith;
                       args[8] = cms.hashalg;
                       args[9] = cms.passphrase;
                     //  if( comman.StartsWith(cms.commandname))
                       {
                          // string arg = comman.Substring(comman.IndexOf(cmd.Name()) + 1);
                         //  string [] args= arg.Split('&');
                           ap=cmd.Execute(args);


                       }
                   }
               }
               return ap;
           }
           catch (Exception ex)
           {
               program.errorreport(ex);
               CommandModel cmd = new CommandModel();
               cmd.friendlyerrormsg = ex.Message;
               cmd.exception = ex.ToString();

               return JsonConvert.SerializeObject(cmd);
           }

       }
       public void StopServer()
       {
             try
           {
               foreach( Socket soc in this.m_workerSocket)
               {
                  // soc.Disconnect();
                   soc.Close();
               }
              
           }
           catch (Exception ex)
           {
               program.errorreport(ex);
               

       }

       }
		

      
    }
}
