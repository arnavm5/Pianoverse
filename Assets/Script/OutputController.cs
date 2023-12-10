using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Sockets;
using System.IO;
using System.Text;
using System.Threading;

public class OutputController: MonoBehaviour
{
    public float CurrentValue = 2;
    public bool StateClient;
    public bool First = true;

    public void Begin2(string ipAddress, int port)
    {
        //Give the network stuff its own special thread
        var thread = new Thread(() =>
        {
            //This class makes it super easy to do network stuff
            var client = new TcpClient();

            //Change this to your real device address
            client.Connect(ipAddress, port);

            var stream = new StreamReader(client.GetStream());
            StreamWriter wr = new StreamWriter(client.GetStream());

            //We'll read values and buffer them up in here
            var buffer = new List<byte>();

            StateClient = client.Connected;
            Debug.Log("Right before the loop");
            while (client.Connected)
            {
                if (CurrentValue == 2 && First == true){
                    wr.WriteLine("a");
                    First = false;
                }
                
                // Debug.Log("Right before the loop");
            }
        });

        thread.Start();
    }
}
