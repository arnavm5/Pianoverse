using Sngty;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Communicator : MonoBehaviour
{
    public SingularityManager mySingularityManager;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(waiter());
    }

    IEnumerator waiter()
    {   
        yield return new WaitForSeconds(2);
        Debug.Log("Here is where to look");
        Debug.Log(mySingularityManager);
        List<DeviceSignature> pairedDevices = mySingularityManager.GetPairedDevices();
        DeviceSignature myDevice = new DeviceSignature();
        for (int i = 0; i < pairedDevices.Count; i++) {
            Debug.Log(pairedDevices[i].name);
            if ("RitESP".Equals(pairedDevices[i].name)) {
                myDevice = pairedDevices[i];
                break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
