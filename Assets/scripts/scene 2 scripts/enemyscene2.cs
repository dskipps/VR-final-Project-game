using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public enum CriminalState {DEFAULT, TAKEHOSTAGE, DEAD };

[RequireComponent(typeof(NavMeshAgent))]

public class enemyscene2 : MonoBehaviour
{
    NavMeshAgent criminal;
    Animator animator;
    float fastchaserange = 10.0f;
    GameObject target;
    public GameObject knife;
    //added
    public Transform hand;
    public Transform hold;
    public CriminalState state = CriminalState.DEFAULT;
    protected Vector3 destination = Vector3.zero;




    void Start()
    {
        criminal = this.GetComponent<NavMeshAgent>();
        target = GameObject.FindWithTag("hostage");
        animator = GetComponent<Animator>();

        if (hand == null)
        {
            hand = transform.Find("hand"); 
        }

        if (hold == null)  
        {
            hold = transform.Find("hold");  
        }
    }

    void Update()
    {
        HandleCriminalStates();
    }

    void HandleCriminalStates()
    {
        criminal.speed = 2.5f;

        switch (state)
        {

            case CriminalState.DEFAULT:
                destination = target.transform.position;
                criminal.speed = 3.0f;
                criminal.SetDestination(destination);
                animator.SetBool("walking", true);
                if (Vector3.Distance(transform.position, target.transform.position) <= criminal.stoppingDistance)
                {
                    SpawnKnife();
                    state = CriminalState.TAKEHOSTAGE;
                }
                break;


            case CriminalState.TAKEHOSTAGE:
                animator.SetBool("walking", false);
                animator.SetBool("grabhostage", true);

                StartCoroutine(WaitToHold(1f));


                break;

            case CriminalState.DEAD:

                animator.SetBool("walking", false);
                animator.SetBool("grabhostage", false);
                animator.SetBool("holding", false);
                animator.SetBool("dead", true);
                StartCoroutine(LoadWin2SceneAfterDelay(5f));

                break;
        }

    }

    IEnumerator WaitToHold(float delay)
    {
        yield return new WaitForSeconds(delay);
        animator.SetBool("grabhostage", false);
        animator.SetBool("holding", true);
    }
    IEnumerator LoadWin2SceneAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene("win2");
    }
    void SpawnKnife()
    {
        if (knife != null && hand != null)
        {
            GameObject spawnedKnife = Instantiate(knife, hand.position, hand.rotation);
            spawnedKnife.transform.SetParent(hand); // Parent the knife to the hand socket

            
        }
    }
}
