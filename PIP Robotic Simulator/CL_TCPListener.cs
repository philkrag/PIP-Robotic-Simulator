using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace PIP_Robotic_Simulator
{
    class CL_TCPListener
    {

        public static void Run_Listener()
        {
            //int Device_ID = Convert.ToInt32(Address);



                Int32 Port_Address = 60001;
                IPAddress IP_Address = IPAddress.Parse("127.0.0.1");

                TcpListener server = null;
                try
                {
                    
                    server = new TcpListener(IP_Address, Port_Address);

                    server.Start();

                    Byte[] bytes = new Byte[256];
                    String data = null;

                    while (true)
                    {
                    TcpClient client = server.AcceptTcpClient();
                    data = null;

                        NetworkStream stream = client.GetStream();

                        int i;

                        while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
                        {
                            data = System.Text.Encoding.ASCII.GetString(bytes, 0, i);

                            CL_Global_Variables.Received_Message = data;

                            data = data.ToUpper();

                            byte[] msg = System.Text.Encoding.ASCII.GetBytes(data);

                            stream.Write(msg, 0, msg.Length);

                        }

                        client.Close();
                    }
                }
                catch (SocketException e)
                {
                }

                finally
                {
                    // Stop listening for new clients.
                    server.Stop();
                }

            
        }






    }
}
