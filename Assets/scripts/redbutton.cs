using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class redbutton : MonoBehaviour
{
    private GameObject missions;

    void Start()
    {
        missions = GameObject.FindWithTag("right");
        Debug.Log("in the button script");
    }
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("in trigger with: " + other.gameObject.tag);  // Logs the tag of the object we're colliding with
        if (other.gameObject.CompareTag("right"))
        {
            StartCoroutine(LoadMissions(1f));
        }
    }

    IEnumerator LoadMissions(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene("missions");
    }
}
