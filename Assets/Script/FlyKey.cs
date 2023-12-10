using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyKey : MonoBehaviour
{
    private float speed = 0.04f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Click();
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    // private void OnTriggerEnter (Collider other) {
    //     GameObject.Destroy(this.gameObject);
    // }

    // public void Click() {
    //     if (gameObject.CompareTag("FlyKey")) {
    //         transform.Translate(Vector3.forward * speed * Time.deltaTime);
    //     }
    // }
}
