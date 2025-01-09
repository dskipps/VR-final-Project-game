using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace SniperDemo
{
    public class EnemyController : MonoBehaviour
    {
        private NavMeshAgent nav;
        private GameObject player;
        public bool isDead;
        public GameObject wholeBody;
        //public GameObject fracturedHead;
        public GameObject fracturedBody;
        public Animator m_Animator;

        void Start()
        {
            isDead = false;
            player = GameObject.Find("XR Rig");
            nav = GetComponent<NavMeshAgent>();
        }

        void Update()
        {
            if (!isDead)
            {
                nav.SetDestination(player.transform.position);
            }
            else if (isDead)
            {
                gameObject.GetComponent<BoxCollider>().enabled = false;
                nav.isStopped = true;
                StartCoroutine(Waiter());
            }
            if(Vector3.Distance(gameObject.transform.position, player.transform.position) < 4)
            {
                nav.isStopped = true;
                m_Animator.SetBool("nearTarget", true);

            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.tag == "Bullet")
            {
                gameObject.GetComponent<BoxCollider>().enabled = false;
            }
        }

        public void Die()
        {
            isDead = true;
            wholeBody.SetActive(false);
            fracturedBody.SetActive(true);
            //fracturedHead.SetActive(true);
        }

        public IEnumerator Waiter()
        {
            yield return new WaitForSeconds(3.0f);
            Destroy(gameObject);

        }


    }
}
