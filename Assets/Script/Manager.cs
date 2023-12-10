using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Manager : MonoBehaviour
{
    InputController inputController;
    OutputController outputController;
    bool waitState = true;
    // public GameObject cube;
    //  public TextMeshProUGUI numberText;
     bool stop = true;

    void Start()
    {
        //This will do the network stuff
        // inputController = new InputController();
        // inputController.Begin("192.168.137.29", 5005);
    }

    void Update()
    {
        // if (inputController.CurrentValue == 0 && waitState)
        // {
        //     waitState = false;
        //     Signal();
        // }
        // int num = int.Parse(numberText.text);
        // if (num == 2 && stop == true){
        //     outputController = new OutputController();
        //     outputController.Begin2("192.168.137.29", 5005);
        //     stop = false;
        // }

    }

    // public void Signal()
    // {
    //     Debug.Log("192.168.100.13");
    //     if (cube.activeInHierarchy)
    //         cube.SetActive(false);
    //     else
    //         cube.SetActive(true);

    //     StartCoroutine(Wait());
    // }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(2);
        waitState = true;
    }
}
