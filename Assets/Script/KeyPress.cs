// using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using NativeWebSocket;
// using System.IO.Ports;
// using System.Threading;

public class KeyPress : MonoBehaviour
{
    public GameObject button;
    public UnityEvent onPress;
    public UnityEvent onRelease;
    GameObject presser;
    AudioSource sound;
    bool isPressed;
    Quaternion originalRotation;
    Connection myWebSocket;
    
    // GameObject go = GameObject.Find ("Controlaaaaa");
    // Connection connection = go.GetComponent <Connection> ();
    // WebSocket myWebSocket = connection.websocket;
    // OutputController outputController;
    // public static SerialPort sp = new SerialPort("COM4", 115200);

    void Start()
    {
        sound = GetComponent<AudioSource>();
        isPressed = false;
        originalRotation = button.transform.rotation;
        // myWebSocket = GameObject.FindGameObjectWithTag("connectionController").GetComponent<Connection>();
        // myWebSocket.SendWebSocketMessage();
    }

    private async void OnTriggerEnter(Collider other)
    {
        if (!isPressed)
        {
            button.transform.Rotate(new Vector3(5.0f, 0, 0));
            presser = other.gameObject;
            onPress.Invoke();
            sound.Play();
            isPressed = true;
            string curr_key = this.gameObject.name;
            // InvokeRepeating("SendWebSocketMessage2", 0.0f, 2.0f);
            // myWebSocket.SendWebSocketMessage("pressed: " + curr_key);
            // await websocket.SendText("pressed: " + curr_key);
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == presser)
        {
            button.transform.rotation = originalRotation;
            onRelease.Invoke();
            isPressed = false;
        }
    }

    private async void SendWebSocketMessage2() {
        myWebSocket.SendWebSocketMessage();
    }
    // public void OpenConnection()
	// {
	// 	if (sp != null)
	// 	{
	// 		if (sp.IsOpen)
	// 		{
	// 			sp.Close();
	// 			print("Closing port, because it was already open!");
	// 		}
	// 		else
	// 		{
	// 			sp.Open();  // opens the connection
	// 			sp.ReadTimeout = 16;  // sets the timeout value before reporting error
	// 			print("Port Opened!");
				
	// 		}
	// 	}
	// 	else
	// 	{
	// 		if (sp.IsOpen)
	// 		{
	// 			print("Port is already open");
	// 		}
	// 		else
	// 		{
	// 			print("Port == null");
	// 		}
	// 	}
	// }

	// void OnApplicationQuit()
	// {
	// 	sp.Close();
	// }
}
