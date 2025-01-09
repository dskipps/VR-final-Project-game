using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public enum HostageState { DEFAULT, TAKEN, MOVING, DEAD}

public class hostage : MonoBehaviour
{
    NavMeshAgent agent;
    Animator animator;
    GameObject criminal;
    enemyscene2 enemyscene2;

    public HostageState state = HostageState.DEFAULT;
    protected Vector3 destination = Vector3.zero;

    public float minX = 0;
    public float maxX = 0;
    public float minZ = 0;
    public float maxZ = 0;

    public float takenRadious = 0.5f;
    public float distanceInFrontOfCriminal = 0.5f;

    public Transform holdTransform;

    void Start()
    {
        enemyscene2 = GameObject.FindWithTag("enemy2").GetComponent<enemyscene2>();

        agent = this.GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        criminal = GameObject.FindWithTag("enemy2");

        holdTransform = criminal.GetComponent<enemyscene2>().hold;  

    }

    // Update is called once per frame
    void Update()
    {
        HandleHostageStates();
    }

    void HandleHostageStates()
    {
        switch (state)
        {
            case HostageState.DEFAULT:
                if (enemyscene2.state == CriminalState.TAKEHOSTAGE)
                {

                    state = HostageState.TAKEN;
                }
                else
                {
                    destination = RandomPosition();
                    agent.speed = 1.5f;
                    agent.SetDestination(destination);
                    state = HostageState.MOVING;
                    animator.SetBool("idle", false);
                    animator.SetBool("walking", true);
                }
                break;

            case HostageState.MOVING:
                if (Vector3.Distance(transform.position, destination) < 2.0f)
                {
                    animator.SetBool("walking", false);
                    animator.SetBool("idle", true);

                    state = HostageState.DEFAULT;
                }

                if ((Vector3.Distance(transform.position, criminal.transform.position) < takenRadious) && enemyscene2.state != CriminalState.DEAD)
                {
                    state = HostageState.TAKEN;
                }
                break;

            case HostageState.TAKEN:

                animator.SetBool("walking", false);
                animator.SetBool("idle", false);
                animator.SetBool("grabbed", true);

                agent.enabled = false;

                MoveHostageToHoldPosition();
                SetCiviliansStill();

                if (enemyscene2.state == CriminalState.DEAD)
                {
                    ResetCiviliansMovement();
                    state = HostageState.DEFAULT; 
                    agent.enabled = true;
                    animator.SetBool("walking", true);
                    animator.SetBool("grabbed", false);
                }
                break;
        
            case HostageState.DEAD:
                Debug.Log("This is in dead hostage");
                
                animator.SetBool("walking", false);
                animator.SetBool("idle", false);
                animator.SetBool("grabbed", false);
                animator.SetBool("taken", false);
                animator.SetBool("dead", true);
                StartCoroutine(LoadLose2SceneAfterDelay(5f));

                break;
            break;
        }
    }

    IEnumerator LoadLose2SceneAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene("lose2");
    }

    private void MoveHostageToHoldPosition()
    {
        // Ensure holdTransform is assigned before moving the hostage
        if (holdTransform != null)
        {

            transform.position = Vector3.Lerp(transform.position, holdTransform.position, Time.deltaTime * 5f);

            // Apply the desired rotation (slightly rotated to the right)
            transform.rotation = Quaternion.Euler(0, criminal.transform.rotation.eulerAngles.y + 15, 0);

            // Check if the hostage is close enough to the hold position and then enable the agent again
            
        }
        else
        {
            Debug.LogError("HoldTransform is not assigned correctly");
        }
    }
    
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

    void SetCiviliansStill()
    {
        // Get all NPCs within the radius
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 10.0f); // Adjust the radius to your needs
        foreach (var hitCollider in hitColliders)
        {
            npcscene2 civilian = hitCollider.GetComponent<npcscene2>();
            if (civilian != null)
            {
                civilian.SetStateStill();  
            }
        }
    }
    void ResetCiviliansMovement()
    {
        // Get all NPCs within a certain radius
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 10.0f); // Adjust the radius as needed
        foreach (var hitCollider in hitColliders)
        {
            npcscene2 civilian = hitCollider.GetComponent<npcscene2>();
            if (civilian != null)
            {
                civilian.SetStateDefault();  // Reset civilians to DEFAULT state
            }
        }
    }
}
