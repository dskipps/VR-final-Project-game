using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class missionselectscreen : MonoBehaviour
{
    private GameObject Range;
    private GameObject op1;
    private GameObject op2;
    private GameObject op3;

    void Start()
    {
        Range = GameObject.FindWithTag("Range");
        op1 = GameObject.FindWithTag("1");
        op2 = GameObject.FindWithTag("2");
        op3 = GameObject.FindWithTag("3");

    }

    void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag("Range"))
        {
            StartCoroutine(LoadRange(1f)); 
        }
        else if (collision.gameObject.CompareTag("1"))
        {
            StartCoroutine(LoadOperation1(1f));
        }
        else if (collision.gameObject.CompareTag("2"))
        {
            StartCoroutine(LoadOperation2(1f));
        }
        else if (collision.gameObject.CompareTag("3"))
        {
            StartCoroutine(LoadOperation3(1f));
        }
        else if (collision.gameObject.CompareTag("MM"))
        {
            StartCoroutine(LoadMM(1f));
        }
    }



    IEnumerator LoadRange(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene("tutorial");
    }
    IEnumerator LoadOperation1(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene("operation 1");
    }
    IEnumerator LoadOperation2(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene("operation 2");
    }
    IEnumerator LoadOperation3(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene("operation 3");
    }

    IEnumerator LoadMM(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene("Main Menu");
    }
}
