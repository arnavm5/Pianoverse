using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartGame : MonoBehaviour
{
    public float speed = 0.05f;
    private GameObject[] flyKeys;

    // Start is called before the first frame update
    void Start()
    {
        // Button button = GetComponent<Button>();
        // button.onClick.AddListener(MoveKey);
    }

    // Update is called once per frame
    void Update()
    {
        if (flyKeys == null)
            flyKeys = GameObject.GetComponent<Button>();

        Debug.Log(flyKeys);

        // foreach (GameObject flyKey in flyKeys) {
        //     transform.Translate(Vector3.forward * speed * Time.deltaTime);
        // }
    }

    // public void MoveKey() {
    //     Debug.Log("aaa");

    //     foreach (GameObject flyKey in flyKeys) {
    //         transform.Translate(Vector3.forward * speed * Time.deltaTime);
    //     }
    // }
}

