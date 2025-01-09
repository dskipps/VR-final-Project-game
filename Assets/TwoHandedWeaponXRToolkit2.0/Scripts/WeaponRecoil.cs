using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SniperDemo
{
    public class WeaponRecoil : MonoBehaviour
    {
        private float recoil = 0.0f;
        private float maxRecoil_x = -10f;
        private float maxRecoil_y = 20f;
        private float maxRecoil_z = 10f;

        private float recoilSpeed = 2f;


        public void StartRecoil(float recoilParam, float maxRecoil_xParam, float maxRecoil_yParam, float recoilSpeedParam)
        {
            // in seconds
            recoil = recoilParam;
            //maxRecoil_x = maxRecoil_xParam;
            maxRecoil_x = 0;
            recoilSpeed = recoilSpeedParam;
            maxRecoil_y = Random.Range(-maxRecoil_xParam, maxRecoil_xParam);
            maxRecoil_z = Random.Range(0, maxRecoil_z);
            //maxRecoil_y = maxRecoil_yParam;
        }

        public void Recoiling()
        {

            if (recoil > 0f)
            {
                // transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(359f, 359f, transform.rotation.z), 0.05f * Time.deltaTime * 60f);
                //transform.position = Vector3.Lerp(transform.position, recoilEndPos.position, Time.deltaTime * recoilSpeed);

                Quaternion maxRecoil = Quaternion.Euler(0, 0, maxRecoil_y);
                //Quaternion maxRecoil = Quaternion.Euler(50, 0, 0);

                // Dampen towards the target rotation
                transform.localRotation = Quaternion.Slerp(transform.localRotation, maxRecoil, Time.deltaTime * recoilSpeed);
                recoil -= Time.deltaTime;
            }
            else
            {
                recoil = 0f;
                // Dampen towards the target rotation
                transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.identity, Time.deltaTime * recoilSpeed / 2);
            }
        }

        // Update is called once per frame
        void Update()
        {
            Recoiling();
        }
    }
}