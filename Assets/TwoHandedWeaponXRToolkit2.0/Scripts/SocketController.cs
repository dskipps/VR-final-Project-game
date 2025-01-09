using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace SniperDemo
{
    public class SocketController : MonoBehaviour
    {
        public GameObject xrSocket;
        public XRSocketInteractor socket;
        public GameObject dummyMagazine;

        public AudioSource audioSourceReload;
        public AudioClip audioClipReload;

        public GameObject grabPoint1;
        public GameObject grabPoint2;

        private IXRSelectInteractable previousInteractable;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void PlayReloadSound()
        {
            IXRSelectInteractable objName = socket.GetOldestInteractableSelected();
            if (objName.transform.name.Contains("Magazine"))
            {
                audioSourceReload.PlayOneShot(audioClipReload);

            }
        }

        public void CheckIfGrabbedBySocket()
        {
            IXRSelectInteractable objName = socket.GetOldestInteractableSelected();
            if (objName.transform.name.Contains("AK47"))
            {
                Transform[] allChildren = objName.transform.GetComponentsInChildren<Transform>();
                foreach (Transform child in allChildren)
                {
                    if (child.name.Contains("Second Grab Point"))
                    {
                        child.gameObject.SetActive(false);

                    }
                }
            }
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
    }
}