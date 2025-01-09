using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SniperDemo
{
    public class HeadPieces : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            StartCoroutine(LongWaiter());
        }

        // Update is called once per frame
        void Update()
        {

        }

        void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.tag == "Environment" || other.gameObject.tag == "Body")
                StartCoroutine(Waiter());
        }
        public IEnumerator Waiter()
        {
            yield return new WaitForSeconds(1.0f);
            //var rb = gameObject.GetComponent<Rigidbody>();
            //rb.isKinematic = true;
            Destroy(gameObject);
        }

        public IEnumerator LongWaiter()
        {
            yield return new WaitForSeconds(10.0f);
            //var rb = gameObject.GetComponent<Rigidbody>();
            //rb.isKinematic = true;
            Destroy(gameObject);
        }
    }
}
