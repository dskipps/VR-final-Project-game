
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class shoot : MonoBehaviour
{

    public GameObject BulletPref;
    public Transform Spawn;
    public float Speed;
    public float gravity;



    void Start()
    {
        
    }

    void Update()
    {
        XRGrabInteractable grabbable = GetComponent<XRGrabInteractable>();
        grabbable.activated.AddListener(FireBullet);
    }
    public void FireBullet(ActivateEventArgs arg)
    {
        GameObject BulletSpawn = Instantiate(BulletPref, Spawn.position, Spawn.rotation);
        BulletSpawn.transform.position = Spawn.position;
        BulletSpawn.transform.rotation = Spawn.rotation; // this was added
        BulletSpawn.GetComponent<Rigidbody>().velocity = Spawn.forward * Speed;
        Destroy(BulletSpawn, 7);
    }
    /*void shot()
    {
        GameObject bullets = Instantiate(BulletPref, Spawn.position, Spawn.rotation);
        bullet bulletscript = bullets.GetComponent<bullet>();
        if (bulletscript)
        {
            bulletscript.Initialized(Spawn, Speed, gravity);

        }
        Destroy(Spawn, 7);

    }*/
}
