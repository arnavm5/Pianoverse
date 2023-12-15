using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using NativeWebSocket;

public class Connection : MonoBehaviour
{
  public WebSocket websocket;
  private string _toSend = "quack";
  public string toSend {
      get
      {
          return _toSend;
      }
      set
      {
          _toSend = value;
      }
    }

  // Start is called before the first frame update
  async void Start()
  {
    websocket = new WebSocket("ws://192.168.137.66:81");

    websocket.OnOpen += () =>
    {
      Debug.Log("Connection open!");
    };

    websocket.OnError += (e) =>
    {
      Debug.Log("Error! " + e);
    };

    websocket.OnClose += (e) =>
    {
      Debug.Log("Connection closed!");
    };

    websocket.OnMessage += (bytes) =>
    {
    //   Debug.Log("OnMessage!");
    //   Debug.Log(bytes);

      // getting the message as a string
      var message = System.Text.Encoding.UTF8.GetString(bytes);
      Debug.Log("OnMessage! " + message);
    };

    // Keep sending messages at every 2.0s
    // InvokeRepeating("SendWebSocketMessage", 0.0f, 2.0f);

    // waiting for messages
    // for (int i = 0; i < 10; i++){
    //   SendWebSocketMessage();
    // }
    // await websocket.SendText("pressed1");
    // SendWebSocketMessage();
    // Invoke("SendWebSocketMessage", 2.0f);
    await websocket.Connect();
  }

  void Update()
  {
    #if !UNITY_WEBGL || UNITY_EDITOR
      websocket.DispatchMessageQueue();
    #endif
  }

  public async void SendWebSocketMessage()
  {
    if (websocket.State == WebSocketState.Open)
    {
      // Sending bytes
      // await websocket.Send(new byte[] { 10, 20, 30 });

      // Sending plain text
      await websocket.SendText(toSend);
      Debug.Log(websocket.State);
      // await websocket.SendText(toSend);
    }
  }

  public async void SendWebSocketMessageParameter(string param)
  {
    if (websocket.State == WebSocketState.Open)
    {
      // Sending bytes
      // await websocket.Send(new byte[] { 10, 20, 30 });

      // Sending plain text
      await websocket.SendText(param);
      Debug.Log(websocket.State);
      // await websocket.SendText(toSend);
    }
  }

  private async void OnApplicationQuit()
  {
    await websocket.Close();
  }

}