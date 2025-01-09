using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public enum AssasinState { FASTCHASE, DEFAULT, ATTACK, DEAD};

[RequireComponent(typeof(NavMeshAgent))]

public class enemy : MonoBehaviour
{
    NavMeshAgent Assasin;
    Animator animator;
    float fastchaserange = 10.0f;
    GameObject target;
    public GameObject knife;
    //added
    public Transform hand;
    public float deathradious = 1f;
    protected GameObject bullet;

    public AssasinState state = AssasinState.DEFAULT;
    protected Vector3 destination = Vector3.zero;




    void Start()
    {
        Assasin = this.GetComponent<NavMeshAgent>();
        target = GameObject.FindWithTag("target");
        animator = GetComponent<Animator>();
        bullet = GameObject.FindWithTag("Bullet");

        if (hand == null)
        {
            hand = transform.Find("hand"); 
        }
    }

    void Update()
    {
        HandleAssasinStates();
    }

    void HandleAssasinStates()
    {

        switch (state)
        {
            case AssasinState.DEFAULT:
                Debug.Log(" default ");

                destination = target.transform.position;
                Assasin.speed = 1.5f;
                Assasin.SetDestination(destination);
                animator.SetBool("walking", true);

               
                //Debug.Log("Bullet Position: " + bullet.transform.position);
                //Debug.Log("Enemy Position: " + transform.position);
                //Debug.Log("Distance to Bullet: " + Vector3.Distance(transform.position, bullet.transform.position));
                

                //if (Vector3.Distance(transform.position, bullet.transform.position) < deathradious)
                //{
                //    Debug.Log(" default dead");
                //
                //    state = AssasinState.DEAD;
                //    break;
                //}

                if (Vector3.Distance(transform.position, target.transform.position) <= fastchaserange)
                    {
                    Debug.Log(" default fastchase");

                    state = AssasinState.FASTCHASE;
                   
                }
                break;

            case AssasinState.FASTCHASE:
                Debug.Log("fastchase");

                Assasin.stoppingDistance = 1.0f;
                destination = target.transform.position;
                Assasin.speed = 4.0f;
                Assasin.SetDestination(destination);
                animator.SetBool("walking", false);
                animator.SetBool("running", true);

                //if (Vector3.Distance(transform.position, bullet.transform.position) < deathradious)
                //{
                //    Debug.Log("fastchase dead ");
                //
                //    state = AssasinState.DEAD;
                //    break;
                //}

                if (Vector3.Distance(transform.position, target.transform.position) <= Assasin.stoppingDistance)
                {
                    Debug.Log("fastchase attack");

                    SpawnKnife();
                    state = AssasinState.ATTACK;
                }
                break;

            case AssasinState.ATTACK:
                animator.SetBool("running", false);
                animator.SetBool("attacking", true);

                //if (Vector3.Distance(transform.position, bullet.transform.position) < deathradious)
                //{
                //
                //    state = AssasinState.DEAD;
                //    break;
                //}

                break;

            case AssasinState.DEAD:
                Debug.Log("dead");
                Assasin.speed = 0.0f;
                Assasin.isStopped = true;

                animator.SetBool("attacking", false);
                animator.SetBool("running", false);
                animator.SetBool("walking", false);
                animator.SetBool("dead", true);
                StartCoroutine(LoadWinSceneAfterDelay(5f));
                break;
        }

    }
    void SpawnKnife()
    {
        if (knife != null && hand != null)
        {
            // You can adjust the spawn position here (for example, slightly in front of the enemy)
            GameObject spawnedKnife = Instantiate(knife, hand.position, hand.rotation);
            spawnedKnife.transform.SetParent(hand); // Parent the knife to the hand socket

            spawnedKnife.transform.Rotate(Vector3.right * 90f); // Rotates 90 degrees on the X-axis

        }
    }
    IEnumerator LoadWinSceneAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene("win");
    }
}
