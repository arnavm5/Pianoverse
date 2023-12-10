using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class KeyPressStart : MonoBehaviour
{
    public GameObject button;
    public UnityEvent onPress;
    public UnityEvent onRelease;
    GameObject presser;
    AudioSource sound;
    bool isPressed;
    Quaternion originalRotation;

    void Start()
    {
        sound = GetComponent<AudioSource>();
        isPressed = false;
        originalRotation = button.transform.rotation;
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
}
