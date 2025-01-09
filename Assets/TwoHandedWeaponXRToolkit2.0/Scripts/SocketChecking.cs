using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace SniperDemo
{
    public class SocketChecking : MonoBehaviour
    {
        private XRSocketInteractor xRSocketInteractor;
        // Start is called before the first frame update
        public GameObject rpgWarhead;
        public Transform launchPoint;
        public AudioClip rpgLoadAudioClip;
        public AudioSource rpgLoadAudioSource;

        public AudioClip rpgFireAudioClip;
        public AudioSource rpgFireAudioSource;
        private IXRSelectInteractable previousInteractable;
        public GameObject dummyMagazine;
        public XRSocketInteractor socket;
        public GameObject xrSocket;

        void Start()
        {
            xRSocketInteractor = GetComponent<XRSocketInteractor>();

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void CheckDummyMag()
        {
            if (dummyMagazine.active)
            {
                dummyMagazine.SetActive(false);
                //IXRSelectInteractable objName = socket.GetOldestInteractableSelected();
                //objName.transform.gameObject.SetActive(true);
                var rb = previousInteractable.transform.GetComponent<Rigidbody>();
                rb.useGravity = true;
                previousInteractable.transform.position = dummyMagazine.transform.position;
                previousInteractable.transform.rotation = dummyMagazine.transform.rotation;
                previousInteractable.transform.gameObject.SetActive(true);
                rb.constraints = RigidbodyConstraints.None;

                //previousInteractable.transform.parent = null;
            }
        }

        public void CheckSocket()
        {
            //var socket = gameObject.GetComponent<XRSocketInteractor>();

            if (socket.hasSelection)
            {
                IXRSelectInteractable objName = socket.GetOldestInteractableSelected();
                previousInteractable = objName;
                //objName.transform.parent = gameObject.transform;
                objName.transform.gameObject.SetActive(false);
                dummyMagazine.SetActive(true);
                var rigB = objName.transform.gameObject.GetComponent<Rigidbody>();
                rigB.freezeRotation = true;
                rigB.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezePositionY;
                //objName.transform.gameObject.GetComponent<BoxCollider>().enabled = false;
            }
            else
            {
                xrSocket.SetActive(false);


            }


        }

        public void Fire()
        {

            GameObject spawnedBullet = Instantiate(rpgWarhead, launchPoint.position, launchPoint.rotation);
            //rpgWarhead.transform.GetChild(0).gameObject.SetActive(true);
            var rb = spawnedBullet.GetComponent<Rigidbody>();
            var col = spawnedBullet.GetComponent<BoxCollider>();
            //var ex = spawnedBullet.GetComponent<ExplosionTrigger>();
            //ex.enabled = true;
            //StartCoroutine(Waiter(rb));
            //rb.AddForce(20 * launchPoint.forward, ForceMode.Impulse);
            //rb.isKinematic = true;
            // col.isTrigger = true;
            //rb.velocity = 20 * launchPoint.forward;
            rb.freezeRotation = true;
            rb.useGravity = false;
            // audioSource.PlayOneShot(audioClip);
            rpgFireAudioSource.PlayOneShot(rpgFireAudioClip);

            Destroy(spawnedBullet, 3);
        }

        private IEnumerator Waiter(Rigidbody rb)
        {
            yield return new WaitForSeconds(1.0f);

        }

        private IEnumerator RotateWithVelocity(Rigidbody rigidbody)
        {
            yield return new WaitForFixedUpdate();

            //while (inAir)
            //{
            Quaternion newRotation = Quaternion.LookRotation(rigidbody.velocity, transform.up);
            transform.rotation = newRotation;
            yield return null;
            //}
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "RPG")
            {
                rpgLoadAudioSource.PlayOneShot(rpgLoadAudioClip);
            }
        }

        public void CheckSocketAndFire()
        {
            IXRSelectInteractable objName = xRSocketInteractor.GetOldestInteractableSelected();
            var name = objName.transform.name;
            // var otherName = objName.transform.parent.name;
            if (name.Contains("RPGWarheadGrab"))
            {
                //var obj = objName.transform.GetComponent<Rigidbody>();
                objName.transform.gameObject.SetActive(false);
                Fire();

                //StartCoroutine(RotateWithVelocity(obj));
                //Vector3 force = transform.forward * (-100 * 3);

                //var obj = objName.transform.GetComponent<Rigidbody>();
                //var obj2 = objName.colliders[0].gameObject.transform.GetComponent<Rigidbody>();

                //var obj2 = objName.firstInteractorSelecting.transform.transform.GetComponent<Rigidbody>();
                //objName.colliders[0].enabled = false;
                //obj.isKinematic = false;
                //obj.useGravity = true;
                //obj.transform.parent = null;
                //obj2.isKinematic = false;
                //obj2.useGravity = true;
                //obj2.transform.parent = null;
                //objName.transform.parent.GetComponent<Rigidbody>().AddForce(force);
                //obj2.AddForce(force, ForceMode.Acceleration);
                //obj.AddForce(force, ForceMode.Force);
                //obj.velocity = force;

                //obj.AddForce(force, ForceMode.Acceleration);

                // obj.velocity = force;

                //obj2.AddForce(force);
            }

        }
    }
}