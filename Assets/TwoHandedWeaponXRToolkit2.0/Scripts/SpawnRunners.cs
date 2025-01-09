using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SniperDemo
{
    public class SpawnRunners : MonoBehaviour
    {
        public GameObject runner;

        public List<Transform> spawnPointList;
        private int removeSpawnPoints;
        public int maxDrunkRunners;
        //public AudioSource ding;

        void Start()
        {
            //if(ding != null)
            //{
            //    ding.PlayOneShot(ding.clip);

            //}
            maxDrunkRunners = 8;

            InvokeRepeating("SpawnIfNone", 3.0f, 2.0f);
        }

        void Update()
        {

        }

        void SpawnIfNone()
        {
            var canSpawn = GameObject.FindGameObjectsWithTag("Body");
            removeSpawnPoints = 0;
            maxDrunkRunners = spawnPointList.Count - removeSpawnPoints;

            if (gameObject.tag == "Enemy")
            {
                if (canSpawn.Length < maxDrunkRunners)
                {
                    for (int i = 0; i < spawnPointList.Count - removeSpawnPoints; i++)
                    {
                        StartCoroutine(Waiter(spawnPointList[i]));
                    }
                }
            }
        }


        public IEnumerator Waiter(Transform spawnPoints)
        {
            var number = Random.Range(0, 4.0f);
            yield return new WaitForSeconds(number);
            Instantiate(runner, spawnPoints.position, Quaternion.Euler(0, 0, 0));

        }

    }
}
