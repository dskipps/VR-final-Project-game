using SniperDemo;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace SniperDemo
{
    public class EnemyDie : MonoBehaviour
    {

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Bullet")
            {
                gameObject.GetComponentInParent<NavMeshAgent>().Stop();
                gameObject.GetComponentInParent<EnemyController>().Die();

            }
        }


    }
}
