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

    void Start()
    {
        sound = GetComponent<AudioSource>();
        isPressed = false;
        originalRotation = button.transform.rotation;
        noteSequence = GameObject.FindGameObjectWithTag("Note").GetComponent<NoteSequence>();
    }

    void Update() {
        expectedNoteName = noteSequence.GetExpectedNoteName();
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

            if (gameObject.name != expectedNoteName) {
                button.GetComponent<Renderer>().material.color = Color.red;
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
