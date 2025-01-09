using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;


public enum Civi2State { MOVING, DEFAULT, DEAD, STILL };

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Animator))]

public class npcscene2 : MonoBehaviour
{
    NavMeshAgent agent;
    Animator animator;


    public Civi2State state = Civi2State.DEFAULT;
    protected Vector3 destination = Vector3.zero;

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

        float sampleRadius = 50.0f;  // Increase this value if necessary to avoid stuck NPCs near edges

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

        
    }


    void Update()
    {
        HandleNPC2States();
    }

    void HandleNPC2States()
    {
        switch (state)
        {
            case Civi2State.DEFAULT:
                //destination = transform.position + RandomPosition();
                destination = RandomPosition();
                agent.speed = 1.5f;
                agent.SetDestination(destination);
                state = Civi2State.MOVING;
                animator.SetBool("idle", false);
                animator.SetBool("walking", true);
                break;

            case Civi2State.MOVING:
                if (Vector3.Distance(transform.position, destination) < 2.0f)
                {
                    animator.SetBool("walking", false);
                    animator.SetBool("idle", true);

                    state = Civi2State.DEFAULT;
                }
                break;

            case Civi2State.STILL:
                agent.isStopped = true;
                animator.SetBool("walking", false);
                animator.SetBool("idle", true);
                break;

            case Civi2State.DEAD:
                Debug.Log("This is in dead");
                agent.speed = 0.0f;
                agent.isStopped = true;
                animator.SetBool("walking", false);
                animator.SetBool("idle", false);
                animator.SetBool("dead", true);
                StartCoroutine(LoadLose2SceneAfterDelay(5f));

                break;
        }
    }
    IEnumerator LoadLose2SceneAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene("lose2");
    }

    public void SetStateStill()
    {
        state = Civi2State.STILL;
    }

    public void SetStateDefault()
    {
        state = Civi2State.MOVING;
    }

}
