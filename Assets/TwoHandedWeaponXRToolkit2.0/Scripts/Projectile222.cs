using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
namespace SniperDemo
{
    public class Projectile222 : MonoBehaviour
    {
        public int force = 5;
        public float lifetime = 4;


        public void Launch()
        {
            Rigidbody rigidbody = GetComponent<Rigidbody>();
            rigidbody.AddRelativeForce(Vector3.forward * force, ForceMode.Impulse);
            Destroy(gameObject, lifetime);
        }
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.collider.CompareTag("NPC"))
            {
                npcs npc = collision.collider.GetComponent<npcs>();
                if (npc != null)
                {
                    npc.state = CiviState.DEAD;
                }

                Destroy(gameObject);
            }

            if (collision.collider.CompareTag("enemy"))
            {
                enemy enemy1 = collision.collider.GetComponent<enemy>();
                if (enemy1 != null)
                {
                    enemy1.state = AssasinState.DEAD;
                }

                Destroy(gameObject);
            }

            if (collision.collider.CompareTag("target"))
            {
                targetbehavior target = collision.collider.GetComponent<targetbehavior>();
                if (target != null)
                {
                    target.state = TargetState.DEAD;
                }

                Destroy(gameObject);
            }

            if (collision.collider.CompareTag("friend"))
            {
                talkingnpc friend = collision.collider.GetComponent<talkingnpc>();
                if (friend != null)
                {
                    friend.state = FriendState.DEAD;
                }

                Destroy(gameObject);
            }

            if (collision.collider.CompareTag("NPC2"))
            {
                npcscene2 npc2 = collision.collider.GetComponent<npcscene2>();
                if (npc2 != null)
                {
                    npc2.state = Civi2State.DEAD;
                }

                Destroy(gameObject);
            }
            if (collision.collider.CompareTag("hostage"))
            {
                hostage hostages = collision.collider.GetComponent<hostage>();
                if (hostages != null)
                {
                    hostages.state = HostageState.DEAD;
                }

                Destroy(gameObject);
            }
            if (collision.collider.CompareTag("enemy2"))
            {
                enemyscene2 criminal = collision.collider.GetComponent<enemyscene2>();
                if (criminal != null)
                {
                    criminal.state = CriminalState.DEAD;
                }

                Destroy(gameObject);
            }
            if (collision.collider.CompareTag("enemy3"))
            {
                criminals3 criminals = collision.collider.GetComponent<criminals3>();
                if (criminals != null)
                {
                    criminals.state = shooterState.DEAD;
                }

                Destroy(gameObject);
            }
        }

    }
}
