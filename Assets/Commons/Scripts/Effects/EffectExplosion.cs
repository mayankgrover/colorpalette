using Commons.Singleton;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Commons.Effects
{
    public class EffectExplosion : MonoSingleton<EffectExplosion>
    {
        [SerializeField] private float explosionForce;

        private Rigidbody2D[] rigidbodies;
        private Image[] sprites;

        protected override void Awake()
        {
            base.Awake();
            rigidbodies = GetComponentsInChildren<Rigidbody2D>();
            sprites = GetComponentsInChildren<Image>();

            List<Rigidbody2D> rbs = new List<Rigidbody2D>();
            for(int i=0; i<rigidbodies.Length; i++) {
                if (rigidbodies[i].bodyType != RigidbodyType2D.Static)
                    rbs.Add(rigidbodies[i]);
            }

            rigidbodies = rbs.ToArray();
            Disable();
        }

        public void PlayExplosionEffect(Vector3 position, Color spriteColor)
        {
            position = Camera.main.WorldToScreenPoint(position);
            foreach (var rb in rigidbodies) {
                rb.transform.position = position;
            }

            foreach(var sprite in sprites) {
                sprite.color = spriteColor;
            }

            Enable();
            StopAllCoroutines();
            StartCoroutine(PlayEffect(position));
        }

        private IEnumerator PlayEffect(Vector3 position)
        {
            // wait one frame because some explosions instantiate debris which should then
            // be pushed by physics force
            yield return null;
            foreach (var rb in rigidbodies) {
                Vector2 dir = Random.insideUnitCircle;
                dir.y = Mathf.Abs(dir.y);
                rb.AddForceAtPosition(dir * explosionForce * (1 + Random.value), position, ForceMode2D.Impulse);
            }
        }
    }
}
