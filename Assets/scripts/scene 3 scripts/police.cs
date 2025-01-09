using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;

public enum copState { DEFAULT, SHOOT, RELOAD, DEAD };

public class police : MonoBehaviour
{
    private static int totalDeaths = 0;
    private const int loseThreshold = 5;

    Animator animator;
    public copState state = copState.DEFAULT;
    private NavMeshAgent cop;

    private int shootCount = 0;
    private float nextShootTime = 0f;
    private float shootDelay = 5f;
    private bool hasDied = false;

    private float nextDeathTime = 0f;

    void Start()
    {
        cop = this.GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        ScheduleNextShoot();
        ScheduleNextDeath();
    }

    void Update()
    {
        HandlecopStates();

        if (Time.time >= nextDeathTime && state != copState.DEAD)
        {
            Die();
        }
    }
    void HandlecopStates()
    {
        switch (state)
        {
            case copState.DEFAULT:
                Debug.Log(" default ");

                animator.SetBool("shoot", false);
                animator.SetBool("reload", false);
                animator.SetBool("crouch", true);
                if (Time.time >= nextShootTime && shootCount < 3)
                {
                    state = copState.SHOOT;
                }
                break;

            case copState.SHOOT:
                Debug.Log(" shoot ");

                Shoot();
                animator.SetBool("shoot", false);
                animator.SetBool("crouch", true);
                break;

            case copState.RELOAD:
                Debug.Log(" reload ");
                animator.SetBool("reload", true);

                Reload();

                animator.SetBool("crouch", true);
                state = copState.DEFAULT;

                break;

            case copState.DEAD:
                Debug.Log(" dead ");
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
            state = copState.RELOAD;
        }
        else
        {

            // Schedule next shoot
            ScheduleNextShoot();
            state = copState.DEFAULT;
        }
    }

    void Reload()
    {


        shootCount = 0;
        ScheduleNextShoot();

        // After reloading, go back to default state
        state = copState.DEFAULT;
    }
    void ScheduleNextShoot()
    {
        nextShootTime = Time.time + Random.Range(1f, shootDelay); // Random delay between 1 and shootDelay
    }

    void ScheduleNextDeath()
    {
        nextDeathTime = Time.time + Random.Range(40f, 60f); 
    }
    void Die()
    {
        state = copState.DEAD;
    }
    void IncreaseDeathCount()
    {
        Debug.Log("NPC died. Total deaths: " + totalDeaths);

        totalDeaths++;
        if (totalDeaths >= loseThreshold)
        {
            SceneManager.LoadScene("lose3"); 
        }
    }
}
