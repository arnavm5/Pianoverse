using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NoteSequence : MonoBehaviour
{
    public GameObject[] notes; 
    public float[] waitTimes; 
    public GameObject countdownText;
    FlyKey flyKey;

    private int currentNoteIndex = 0;

    void Start()
    {
        StartCoroutine(PlayNoteSequence());
    }

    // IEnumerator Countdown(int seconds)
    // {
    //     int count = seconds;
       
    //     while (count > 0) {
    //         countdownText.GetComponent<TMPro.TextMeshProUGUI>().text = count.ToString();   
    //         yield return new WaitForSeconds(1);
    //         count --;
    //     }

    //     countdownText.GetComponent<TMPro.TextMeshProUGUI>().text = "START!";
    //     // yield return new WaitForSeconds(2);

    //     StartCoroutine(PlayNoteSequence());
    //     // GetExpectedNoteName();
    //     countdownText.GetComponent<TMPro.TextMeshProUGUI>().text = " ";
    // }

    IEnumerator PlayNoteSequence()
    {
        GetExpectedNoteName();

        while (currentNoteIndex < notes.Length)
        {
            // Wait for the specified duration
            yield return new WaitForSeconds(waitTimes[currentNoteIndex]);

            // Move to the next note in the sequence
            currentNoteIndex++;
        }
        yield return new WaitForSeconds(5);
        ChangeScene();
    }

    public string GetExpectedNoteName() {
        if (currentNoteIndex < notes.Length) {
            return notes[currentNoteIndex].name;
        }
        return string.Empty;
    }

    private void ChangeScene() {
        SceneManager.LoadScene(0);
    }
}
