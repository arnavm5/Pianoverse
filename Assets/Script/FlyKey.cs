using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyKey : MonoBehaviour
{
    private float speed = 0.025f;

    // Update is called once per frame
    void Update()
    {
        flyKey();
    }

    public void flyKey() {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
}

