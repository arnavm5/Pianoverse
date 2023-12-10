using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class PlayButton : MonoBehaviour
{

    public GameObject button;
    public UnityEvent onPress;
    public UnityEvent onRelease;
    GameObject presser;
    Vector3 originalPosition;
    bool isPressed;

    void Start()
    {
        isPressed = false;
        originalPosition = button.transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isPressed)
        {
            Vector3 newPosition = originalPosition;
            newPosition.y -= 0.02f;
            button.transform.position = newPosition;
            presser = other.gameObject;
            onPress.Invoke();
            isPressed = true;
            }
        }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == presser)
        {
            button.transform.position = originalPosition;
            onRelease.Invoke();
            isPressed = false;
        }
    }

    public void MoveToScene(int sceneID)
    {
        SceneManager.LoadScene(sceneID);
    }
}