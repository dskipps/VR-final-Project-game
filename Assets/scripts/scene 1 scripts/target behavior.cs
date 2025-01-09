using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;


public enum TargetState { DEFAULT, DEAD};

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Animator))]

public class targetbehavior : MonoBehaviour
{
    NavMeshAgent agent;
    Animator animator;
    GameObject assasin;


    public TargetState state = TargetState.DEFAULT;

    public float deathradious = 2.0f;
    GameObject bullet;
    public float Bdeathradious = 1.0f;

    void Start()
    {
        agent = this.GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        assasin = GameObject.FindWithTag("enemy");
        bullet = GameObject.FindWithTag("Bullet");

    }


    void Update()
    {
        HandleTargetStates();
    }

    void HandleTargetStates()
    {
        switch (state)
        {
            // standing talking
            case TargetState.DEFAULT:
                //if (Vector3.Distance(transform.position, bullet.transform.position) < deathradious)
                //{
                //    Debug.Log("This is in dead default");
                //
                //    state = TargetState.DEAD;
                //    break;

                //}
                if (Vector3.Distance(transform.position, assasin.transform.position) < Bdeathradious)
                {
                    state = TargetState.DEAD;
                }
                else
                {
                    animator.SetBool("talking", true);
                    animator.SetBool("Dead", false);

                }

                break;
            //gang member knifes him
            case TargetState.DEAD:
                animator.SetBool("talking", false);
                animator.SetBool("Dead", true);

                StartCoroutine(LoadLoseSceneAfterDelay(5f));

                break;
        }


    }
    IEnumerator LoadLoseSceneAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene("lose"); // Replace "LoseScene" with your scene's name
    }
}
