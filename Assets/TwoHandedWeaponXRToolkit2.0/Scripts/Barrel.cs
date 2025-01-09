using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace SniperDemo
{
    public class Barrel : MonoBehaviour
    {
        public float fireWait = 0.05f;
        public GameObject projectilePrefab = null;

        public Gun gun = null;
        private WaitForSeconds wait = null;
        private Coroutine firingRoutine = null;
        public GameObject muzzleFlashPrefab;
        public Transform barrelLocation;
        private float destroyTimer = 2f;
        public Rigidbody gunRb;
        public GameObject casingPrefab;
        public Transform casingExitLocation;
        private float ejectPower = 2.7f;
        public WeaponRecoil recoil;
        public XRSocketInteractor socket;

        public AudioSource audioSourceShoot;
        public AudioClip audioClipShoot;

        public AudioSource audioSourceNoAmmo;
        public AudioClip audioClipNoAmmo;

        

        private void Awake()
        {
            wait = new WaitForSeconds(fireWait);
        }

        public void StartFiring()
        {
            //if has magazine in soxket
            if (socket.hasSelection)
            {
                firingRoutine = StartCoroutine(FiringSequence());

            }
            else
            {
                audioSourceNoAmmo.PlayOneShot(audioClipNoAmmo);

            }
        }

        private IEnumerator FiringSequence()
        {
            while (gameObject.activeSelf)
            {
                CreateProjectile();
                //gun.ApplyRecoil();
               // ApplyRecoil();
                yield return wait;

            }
        }

        public void ApplyRecoil()
        {

            gunRb.AddRelativeForce(Vector3.back * 25, ForceMode.Impulse);
            //gunRb.velocity = Vector3.back * 25;
        }

        private void CreateProjectile()
        {

            GameObject tempFlash;
            tempFlash = Instantiate(muzzleFlashPrefab, barrelLocation.position, barrelLocation.rotation);

            //Destroy the muzzle flash effect
            Destroy(tempFlash, destroyTimer);

            GameObject projectileObject = Instantiate(projectilePrefab, transform.position, transform.rotation);
            Projectile222 projectile = projectileObject.GetComponent<Projectile222>();
            projectile.Launch();
            CasingRelease();
            recoil.StartRecoil(20.0f, 3f, 30f, 5.0f);
            audioSourceShoot.PlayOneShot(audioClipShoot);

        }

        public void StopFiring()
        {
            StopCoroutine(firingRoutine);
        }

        void CasingRelease()
        {
            //Cancels function if ejection slot hasn't been set or there's no casing
            //if (!casingExitLocation || !casingPrefab)
            //{ return; }

            //Create the casing
            GameObject tempCasing;
            tempCasing = Instantiate(casingPrefab, casingExitLocation.position, casingExitLocation.rotation) as GameObject;
            //Add force on casing to push it out
            tempCasing.GetComponent<Rigidbody>().AddExplosionForce(Random.Range(ejectPower * 0.7f, ejectPower), (casingExitLocation.position - casingExitLocation.right * 0.3f - casingExitLocation.up * 0.6f), 1f);
            //Add torque to make casing spin in random direction
            tempCasing.GetComponent<Rigidbody>().AddTorque(new Vector3(0, Random.Range(100f, 500f), Random.Range(100f, 1000f)), ForceMode.Impulse);

            //Destroy casing after X seconds
            Destroy(tempCasing, destroyTimer);
        }

    }
}
