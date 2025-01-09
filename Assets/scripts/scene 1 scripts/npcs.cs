using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;


public enum CiviState {MOVING, DEFAULT, DEAD};

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Animator))] 

public class npcs : MonoBehaviour
{
    NavMeshAgent agent;
    Animator animator;
    GameObject bullet;


    public CiviState state = CiviState.DEFAULT;
    protected Vector3 destination = Vector3.zero;
    public float deathradious = 1.0f;

    //added
    public float minX = -41.0f;
    public float maxX = 43.0f;
    public float minZ = -64.0f;
    public float maxZ = 55.0f;


    private Vector3 RandomPosition()
    {
        //Vector3 randomDirection = new Vector3(Random.Range(-20.0f, 20.0f), 0, Random.Range(-20.0f, 20.0f)); 

        Vector3 randomDirection = new Vector3(Random.Range(minX, maxX), 0, Random.Range(minZ, maxZ));

        NavMeshHit hit;

        float sampleRadius = 100.0f;  // Increase this value if necessary to avoid stuck NPCs near edges

        if (NavMesh.SamplePosition(randomDirection, out hit, sampleRadius, NavMesh.AllAreas))
        {
            return hit.position; 
        }

        return transform.position;
    }

    void Start()
    {
        agent = this.GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        bullet = GameObject.FindWithTag("Bullet");


    }


    void Update()
    {
        HandleNPCStates();
    }

    void HandleNPCStates()
    {
        switch (state)
        {
            case CiviState.DEFAULT:
                Debug.Log("This is default");
               
                //destination = transform.position + RandomPosition();
                Debug.Log("This is default 2");

                destination = RandomPosition();
                agent.speed = 1.5f;
                agent.SetDestination(destination);
                state = CiviState.MOVING;
                animator.SetBool("idle", false);
                animator.SetBool("walking", true);
                //if (Vector3.Distance(transform.position, bullet.transform.position) < deathradious)
                //{
                //    Debug.Log("This is in dead default");
                //
                //    state = CiviState.DEAD;
                //    break;
                //}
                break;

            case CiviState.MOVING:
                Debug.Log("This is moving");

                if (Vector3.Distance(transform.position, destination) < 2.0f)
                {
                    //if (Vector3.Distance(transform.position, bullet.transform.position) < deathradious)
                    //{
                    //    Debug.Log("This is in dead moving");
                    //    state = CiviState.DEAD;
                    //    break;
                    //}
                    animator.SetBool("walking", false);
                    animator.SetBool("idle", true);

                    state = CiviState.DEFAULT;
                }
                break;
            case CiviState.DEAD:
                Debug.Log("This is in dead");
                agent.speed = 0.0f;
                agent.isStopped = true;
                animator.SetBool("walking", false);
                animator.SetBool("idle", false);
                animator.SetBool("dead", true);
                StartCoroutine(LoadLoseSceneAfterDelay(5f));

                break;
        }
       
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            Debug.Log("Hit by bullet!");

            state = CiviState.DEAD;
        }
    }

    IEnumerator LoadLoseSceneAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene("lose");
    }
}
