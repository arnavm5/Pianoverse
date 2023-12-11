using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NoteSequence : MonoBehaviour
{
    public GameObject[] notes; 
    public float[] waitTimes; 
    public GameObject countdownText;

    private int currentNoteIndex = 0;
    Connection myWebSocket;
    public bool sentinel;
    public string currNote;

    void Start()
    {
        myWebSocket = GameObject.FindGameObjectWithTag("connectionController").GetComponent<Connection>();
        Debug.Log(myWebSocket);
        sentinel=false;
        // Invoke("SendWebSocketMessage2", 0.00f);
        // StartCoroutine(MySendMessage("The note sequence"));
        StartCoroutine(Countdown(3));
    }

     private async void SendWebSocketMessage2() {
        myWebSocket.SendWebSocketMessage();
    }

    IEnumerator Countdown(int seconds)
    {
        int count = seconds;
       
        while (count > 0) {
            countdownText.GetComponent<TMPro.TextMeshProUGUI>().text = count.ToString();   
            yield return new WaitForSeconds(1);
            count --;
        }

        countdownText.GetComponent<TMPro.TextMeshProUGUI>().text = "START!";

        StartCoroutine(PlayNoteSequence());

        countdownText.GetComponent<TMPro.TextMeshProUGUI>().text = " ";
    }

    IEnumerator MySendMessage(string param1)
    {
        float delayTime = 0;
        yield return new WaitForSeconds(delayTime);
        myWebSocket.SendWebSocketMessageParameter(param1);
    }

    IEnumerator PlayNoteSequence()
    {
        Debug.Log("start note sequence");

        while (currentNoteIndex < notes.Length)
        {
            GetExpectedNoteName();
            // sentinel = false;
            yield return new WaitForSeconds(waitTimes[currentNoteIndex]);
            // yield return new WaitUntil(() => sentinel == true);
            currentNoteIndex++;
        }
        yield return new WaitForSeconds(5);
        ChangeScene();
    }

    public string GetExpectedNoteName() {
        if (currentNoteIndex < notes.Length) {
            string curr_key = notes[currentNoteIndex].name;
            Debug.Log(curr_key);
            myWebSocket.toSend = curr_key;
            Invoke("SendWebSocketMessage2", 0.00f);
            currNote = notes[currentNoteIndex].name;
            return notes[currentNoteIndex].name;
            
        }
        return string.Empty;
    }

    private void ChangeScene() {
        SceneManager.LoadScene(0);
    }
}
