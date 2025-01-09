using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SniperDemo
{
    public class ExplodeOnImpact : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public float radius = 5.0F;
        public float power = 10.0F;
        public float lift = 30;
        public float speed = 10;
        public bool explode = false;
        public GameObject fracturedPieces;
        public GameObject trail;

        void FixedUpdate()
        {
            if (explode)
            {
                Vector3 explosionPos = transform.position;
                Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);
                foreach (Collider hit in colliders)
                {
                    var hitPoint = hit.GetComponent<Rigidbody>();
                    if (hitPoint != null)
                    {
                        hitPoint.AddExplosionForce(power, explosionPos, radius, lift);
                    }
                }
            }
        }
        void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.tag == "Environment" || collision.gameObject.tag == "Body")
            {
                explode = true;
                fracturedPieces.SetActive(true);
                gameObject.GetComponent<MeshRenderer>().enabled = false;
                trail.gameObject.SetActive(false);
            }
        }
    }
}
