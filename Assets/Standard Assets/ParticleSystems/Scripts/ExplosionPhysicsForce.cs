using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace UnityStandardAssets.Effects
{
    public class ExplosionPhysicsForce : MonoBehaviour
    {
        public float explosionForce = 4;

        private float multiplier = 10f; //GetComponent<ParticleSystemMultiplier>().multiplier;



        private IEnumerator PlayEffect()
        {
            // wait one frame because some explosions instantiate debris which should then
            // be pushed by physics force
            yield return null;

            float r = 10*multiplier;
            //var cols = Physics.OverlapSphere(transform.position, r);
            var rigidbodies = GetComponentsInChildren<Rigidbody2D>(); // new List<Rigidbody2D>();
            //foreach (var col in cols)
            //{
            //    if (col.attachedRigidbody != null && !rigidbodies.Contains(col.attachedRigidbody))
            //    {
            //        rigidbodies.Add(col.attachedRigidbody);
            //    }
            //}
            Random.InitState((int)Time.time);
            foreach (var rb in rigidbodies)
            {
                //rb.AddExplosionForce(explosionForce*multiplier, transform.position, r, 1*multiplier, ForceMode.Impulse);
                rb.AddForceAtPosition(Random.insideUnitCircle * multiplier * explosionForce, transform.position, ForceMode2D.Impulse);
            }
        }
    }
}
