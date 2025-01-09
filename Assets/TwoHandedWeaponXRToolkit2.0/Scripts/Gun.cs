using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SniperDemo
{
    public class Gun : MonoBehaviour
    {
        public float speed = 50;
        public GameObject bullet;
        public Transform barrel;
        public AudioSource audioSource;
        //public AudioClip audioClip;
        public Transform barrelLocation;
        public GameObject muzzleFlashPrefab;
        private float destroyTimer = 2f;
        public Rigidbody gunRb;
        private Rigidbody rb;

        private void Start()
        {
            rb = gameObject.GetComponent<Rigidbody>();

        }

        public void Fire()
        {
            //if (muzzleFlashPrefab)
            //{
            //    //Create the muzzle flash
            //    GameObject tempFlash;
            //    tempFlash = Instantiate(muzzleFlashPrefab, barrelLocation.position, barrelLocation.rotation);

            //    //Destroy the muzzle flash effect
            //    Destroy(tempFlash, destroyTimer);
            //}

            GameObject spawnedBullet = Instantiate(bullet, barrel.position, barrel.rotation);
            spawnedBullet.GetComponent<Rigidbody>().velocity = speed * barrel.forward;
            // audioSource.PlayOneShot(audioClip);
            Destroy(spawnedBullet, 2);
        }

        public void ActivateWeapon()
        {
            //var rb = gameObject.GetComponent<Rigidbody>();
            //rb.freezeRotation = false;
            //rb.constraints = RigidbodyConstraints.None;
        }

        public void DeactivateWeapon()
        {
            //var rb = gameObject.GetComponent<Rigidbody>();
            //rb.freezeRotation = true;
            //rb.constraints = RigidbodyConstraints.FreezeAll;
        }

        public void ApplyRecoil()
        {

            rb.AddRelativeForce(Vector3.back * 25, ForceMode.Impulse);
            //rb.velocity = Vector3.back * 25;
        }
    }
}
