using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class KeyPress : MonoBehaviour
{
    public GameObject button;
    public UnityEvent onPress;
    public UnityEvent onRelease;
    GameObject presser;
    AudioSource sound;
    bool isPressed;
    Quaternion originalRotation;
    NoteSequence noteSequence;
    private string expectedNoteName;
    Connection myWebSocket;

    void Start()
    {
        sound = GetComponent<AudioSource>();
        isPressed = false;
        originalRotation = button.transform.rotation;
        noteSequence = GameObject.FindGameObjectWithTag("Note").GetComponent<NoteSequence>();
        myWebSocket = GameObject.FindGameObjectWithTag("connectionController").GetComponent<Connection>();
    }

    void Update() {
        // expectedNoteName = noteSequence.GetExpectedNoteName();
        HighlightNote();
    }

    public void HighlightNote() {
        if (gameObject.name == expectedNoteName) {
            button.GetComponent<Renderer>().material.color = Color.yellow;
        }
        else {
            button.GetComponent<Renderer>().material.color = Color.white;
        }
    }

    private async void SendWebSocketMessage2() {
        myWebSocket.SendWebSocketMessage();
    }

    IEnumerator MySendMessage(string param1)
     {
        float delayTime = 0;
        yield return new WaitForSeconds(delayTime);
        myWebSocket.SendWebSocketMessageParameter(param1);
     }

    private void OnTriggerEnter(Collider other)
    {
        if (!isPressed)
        {
            button.transform.Rotate(new Vector3(5.0f, 0, 0));
            presser = other.gameObject;
            onPress.Invoke();
            sound.Play();
            isPressed = true;
            button.GetComponent<Renderer>().material.color = Color.green;
            expectedNoteName = noteSequence.currNote;

            // myWebSocket.toSend = this.gameObject.name;
            // Invoke("SendWebSocketMessage2", 0.00f);
            // myWebSocket.toSend = expectedNoteName;
            // Invoke("SendWebSocketMessage2", 0.00f);
            StartCoroutine(MySendMessage(this.gameObject.name));
            StartCoroutine(MySendMessage(expectedNoteName));

            if (this.gameObject.name != expectedNoteName) {
                button.GetComponent<Renderer>().material.color = Color.red;
                // string curr_key = this.gameObject.name;
                StartCoroutine(MySendMessage("wrong"));
                // myWebSocket.toSend = "wrong";
                // Invoke("SendWebSocketMessage2", 0.00f);
            }
            if (this.gameObject.name == expectedNoteName){
                noteSequence.sentinel = true;
                myWebSocket.toSend = "right";
                Invoke("SendWebSocketMessage2", 0.00f);
            }
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

        button.GetComponent<Renderer>().material.color = Color.white;
    }
}
