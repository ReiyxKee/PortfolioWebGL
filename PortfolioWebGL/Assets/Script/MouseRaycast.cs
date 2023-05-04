using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Portfolio
{
    public class MouseRaycast : MonoBehaviour
    {
        public delegate void OnObjectHit(GameObject hitObject);
        public static event OnObjectHit ObjectHitEvent;

        private static MouseRaycast instance;

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

        void Update()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (ObjectHitEvent != null)
                {
                    ObjectHitEvent(hit.transform.gameObject);
                }
            }
            else
            {
                ObjectHitEvent(null);
            }
        }
    }
}