using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SniperDemo
{
    public class ActivateGrabPoint : MonoBehaviour
    {
        public GameObject grabPoint;
        public GameObject grabPoint2;

        // Start is called before the first frame update
        void Start()
        {
            StartCoroutine(ActivateGrabPointDelayed());
        }

        // Update is called once per frame
        void Update()
        {

        }

        public IEnumerator ActivateGrabPointDelayed()
        {
            yield return new WaitForSeconds(1.0f);
            grabPoint.SetActive(true);
            grabPoint2.SetActive(true);
        }
    }
}