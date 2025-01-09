using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;

public enum shooterState { DEFAULT, SHOOT, RELOAD, DEAD };

public class criminals3 : MonoBehaviour
{
    private static int totalDeaths = 0;
    private const int winThreshold = 5;

    Animator animator;
    public shooterState state = shooterState.DEFAULT;
    private NavMeshAgent shooter;

    private int shootCount = 0;
    private float nextShootTime = 0f;
    private float shootDelay = 5f;
    private bool hasDied = false;

    void Start()
    {
        shooter = this.GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        ScheduleNextShoot();

    }

    void Update()
    {
        HandleshooterStates();
    }

    void HandleshooterStates()
    {
        switch(state)
        {
            case shooterState.DEFAULT:
                //Debug.Log(" default ");

                animator.SetBool("shoot", false);
                animator.SetBool("reload", false);
                animator.SetBool("crouch", true);
                if (Time.time >= nextShootTime && shootCount < 3)
                {
                    state = shooterState.SHOOT;
                }
                break;

            case shooterState.SHOOT:
                //Debug.Log(" shoot ");
                
                Shoot();
                animator.SetBool("shoot", false);
                animator.SetBool("crouch", true);
                break;

            case shooterState.RELOAD:
                //Debug.Log(" reload ");
                animator.SetBool("reload", true);

                Reload();

                animator.SetBool("crouch", true);
                state = shooterState.DEFAULT; 

                break;

            case shooterState.DEAD:
                //Debug.Log(" dead ");
                animator.SetBool("shoot", false);
                animator.SetBool("crouch", false);
                animator.SetBool("reload", false);
                animator.SetBool("dead", true);

                if (!hasDied)
                {
                    IncreaseDeathCount();
                    hasDied = true;  
                }


                break;
        }
    }
    void Shoot()
    {
        

        // Increment shoot count
        shootCount++;

        // After 3 shots, switch to reload state
        if (shootCount >= 3)
        {
            state = shooterState.RELOAD;
        }
        else
        {
            
            // Schedule next shoot
            ScheduleNextShoot();
            state = shooterState.DEFAULT;
        }
    }
    void Reload()
    {
       

        shootCount = 0;
        ScheduleNextShoot();

        // After reloading, go back to default state
        state = shooterState.DEFAULT;
    }
    void ScheduleNextShoot()
    {
        nextShootTime = Time.time + Random.Range(1f, shootDelay); // Random delay between 1 and shootDelay
    }

    void IncreaseDeathCount()
    {
        totalDeaths++;  // Increase the death count by 1

        Debug.Log("NPC died. Total deaths: " + totalDeaths);

        if (totalDeaths >= winThreshold)
        {
            WinCondition();
        }
    }
    void WinCondition()
    {
        Debug.Log("You Win! 5 NPCs are dead.");
        StartCoroutine(LoadWin3SceneAfterDelay(5f));

    }

    IEnumerator LoadWin3SceneAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene("win3");
    }
}
