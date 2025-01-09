using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SniperDemo
{
    public class ExplosionTrigger : MonoBehaviour
    {
        public GameObject explodingPieces;
        public AudioClip explosionClip;
        public float speed;
        public Transform launchPoint;
        private Rigidbody rb;
        // Start is called before the first frame update
        void Start()
        {
            rb = gameObject.GetComponent<Rigidbody>();
            rb.AddForce(20 * gameObject.transform.forward, ForceMode.Impulse);

        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.tag == "Body" || collision.gameObject.tag == "Environment")
            {
                rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY;
                gameObject.GetComponent<AudioSource>().PlayOneShot(explosionClip);
                explodingPieces.SetActive(true);
                explodingPieces.transform.parent = null;
                Destroy(gameObject);
            }

        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Body")
            {
                rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY;
                gameObject.GetComponent<AudioSource>().PlayOneShot(explosionClip);
                explodingPieces.SetActive(true);
                explodingPieces.transform.parent = null;
                Destroy(gameObject);
            }

        }
    }
}
