using System;
using UnityEngine;

namespace Commons.SideScrolling
{
    public class ScrollBackground: MonoBehaviour
    {
        [SerializeField] private float scrollSpeed;
        [SerializeField] private float tileSize;

        private Vector3 startPosition;
        private bool pause = false;

        private void Start()
        {
            startPosition = transform.position;
        }

        private void Update()
        {
            if (!pause) {
                float newPosition = Mathf.Repeat(Time.time * scrollSpeed, tileSize);
                transform.position = startPosition - Vector3.up * newPosition;
            }
        }

        public void Enable()
        {
            gameObject.SetActive(true);
        }

        public void Disable()
        {
            gameObject.SetActive(false);
        }

        public void Pause()
        {
            pause = true;
        }

        public void Resume()
        {
            pause = false;
        }
    }
}
