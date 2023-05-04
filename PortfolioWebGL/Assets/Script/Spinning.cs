using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Portfolio
{
    public class Spinning : MonoBehaviour
    {
        [SerializeField] private Vector3 _rotateSpeed;
        bool _spin;
        private void Start()
        {
            StartSpin();
        }
        private void Update()
        {
            if (!_spin) return;

            this.transform.Rotate(_rotateSpeed * Time.deltaTime);
        }

        public void StopSpin()
        {
            if (!_spin) return;

            _spin = false;
        }

        public void StartSpin()
        {
            if (_spin) return;

            _spin = true;
        }
    }
}