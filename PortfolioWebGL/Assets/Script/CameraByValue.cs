using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Portfolio
{
    public class CameraByValue : MonoBehaviour
    {
        private static CameraByValue instance;
        private float value;

        [SerializeField] float lerpDuration = 1.5f;
        [SerializeField] float headerValue = 0f;
        [SerializeField] float laptopValue = -80f;

        public static CameraByValue GetInstance()
        {
            return instance;
        }

        void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else if (instance != this)
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            UserInput.OnScroll += Movement;
        }

        // Update is called once per frame
        void Update()
        {
            this.transform.position = SetPos();
        }

        public Vector3 SetPos()
        {
            return new Vector3(0, value, -15);
        }

        public void Movement(float _input)
        {
            if (_input == 0) return;

            if (_input < 0 && value == headerValue)
            {
                StartCoroutine(LerpFloat(headerValue, laptopValue, lerpDuration));
            }
            else if (_input > 0 && value == laptopValue)
            {
                StartCoroutine(LerpFloat(laptopValue, headerValue, lerpDuration));
            }
        }

        IEnumerator LerpFloat(float startValue, float targetValue, float duration)
        {
            float elapsedTime = 0;

            while (elapsedTime < duration)
            {
                value = Mathf.Lerp(startValue, targetValue, elapsedTime / duration);
                yield return null;
                elapsedTime += Time.deltaTime;
            }

            value = targetValue;
        }

    }
}