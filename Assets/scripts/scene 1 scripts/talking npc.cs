using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;


public enum FriendState { DEFAULT, RUN, DEAD };

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Animator))]
public class talkingnpc : MonoBehaviour
{
    NavMeshAgent friend;
    Animator animator;
    GameObject target; 
    targetbehavior targetBehavior;
    public float deathradious = 1.0f;
    GameObject bullet;

    public FriendState state = FriendState.DEFAULT;
    protected Vector3 destination = Vector3.zero;

    private Vector3 RandomPosition()
    {
        //return new Vector3(Random.Range(-20.0f, 20.0f), 0, Random.Range(-20.0f, 20.0f));
        Vector3 randomDirection = new Vector3(Random.Range(-20.0f, 20.0f), 0, Random.Range(-20.0f, 20.0f));

        NavMeshHit hit;

        if (NavMesh.SamplePosition(randomDirection, out hit, 2.0f, NavMesh.AllAreas))
        {
            return hit.position;
        }

        return transform.position;
    }

    void Start()
    {
        targetBehavior = GameObject.FindWithTag("target").GetComponent<targetbehavior>();

        bullet = GameObject.FindWithTag("Bullet");
        friend = this.GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

    }


    void Update()
    {
        HandleFriendStates();
    }

    void HandleFriendStates()
    {
        switch (state)
        {
            // standing talking
            case FriendState.DEFAULT:

                if (Vector3.Distance(transform.position, bullet.transform.position) < deathradious)
                {
                    Debug.Log("This is in dead default");

                    state = FriendState.DEAD;
                    break;

                }

                if (targetBehavior != null && targetBehavior.state == TargetState.DEAD)
                {
                    destination = transform.position + RandomPosition();
                    friend.speed = 4.0f;
                    state = FriendState.RUN;
                }
                else
                {
                    animator.SetBool("talking", true);
                    animator.SetBool("running", false);

                }

                break;
            //gang member knifes him
           case FriendState.RUN:
                animator.SetBool("talking", false);
                animator.SetBool("running", true);
                break;

           case FriendState.DEAD:
                Debug.Log("This is in dead");
                animator.SetBool("talking", false);
                animator.SetBool("running", false);
                animator.SetBool("Dead", true);
                StartCoroutine(LoadLoseSceneAfterDelay(5f));

                break;
        }

        IEnumerator LoadLoseSceneAfterDelay(float delay)
        {
            yield return new WaitForSeconds(delay);
            SceneManager.LoadScene("lose"); // Replace "LoseScene" with your scene's name
        }
    }
}
