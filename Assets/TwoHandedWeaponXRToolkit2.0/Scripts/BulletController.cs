using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SniperDemo
{
    public class BulletController : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            DestroyBullet();
        }

        // Update is called once per frame
        void Update()
        {

        }

        void DestroyBullet()
        {
            Destroy(gameObject, 4f);
        }
    }
}
