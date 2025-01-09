using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SniperDemo
{
    public class ExplodingPieces : MonoBehaviour
    {
        public float radius = 6.0F;
        public float power = 20.0F;

        void Start()
        {
            Vector3 explosionPos = transform.position;
            Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);
            foreach (Collider hit in colliders)
            {
                Rigidbody rb = hit.GetComponent<Rigidbody>();
                power = Random.Range(power, 50);
                radius = Random.Range(radius, 20);
                if (rb != null)
                    rb.AddExplosionForce(power, explosionPos, radius, 0F);
            }
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}