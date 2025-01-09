using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class options : MonoBehaviour
{
    private GameObject missions;
    private GameObject quit;

    void Start()
    {
        
    }
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("in trigger with: " + other.gameObject.tag);  // Logs the tag of the object we're colliding with
        //load mission scene
        if (other.gameObject.CompareTag("missions"))
        {
            StartCoroutine(LoadMissions(1f));
        }
        //exit game
        if (other.gameObject.CompareTag("leave"))
        {
            ExitGame();
        }
        //load mission2
        if (other.gameObject.CompareTag("mission2"))
        {
            StartCoroutine(LoadMission2(1f));
        }
        //load mission3
        if (other.gameObject.CompareTag("mission3"))
        {
            StartCoroutine(LoadMission3(1f));
        }
        //load mission1
        if (other.gameObject.CompareTag("again"))
        {
            StartCoroutine(LoadMission1(1f));
        }
    }

    IEnumerator LoadMissions(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene("missions");
    }
    IEnumerator LoadMission1(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene("operation 1");
    }
    IEnumerator LoadMission2(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene("operation 2");
    }
    IEnumerator LoadMission3(float delay)
    {
        Debug.Log("load 3 ");

        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene("operation 3");
    }
    void ExitGame()
    {
        Debug.Log("Exiting the game...");

        // Quit the application (works only in a build, not in the editor)
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
