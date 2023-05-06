using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

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

                    Debug.DrawRay(Camera.main.ScreenToWorldPoint(Input.mousePosition), hit.transform.position, Color.green);
                }

            }
            else
            {
                Debug.DrawRay(Camera.main.ScreenToWorldPoint(Input.mousePosition), ray.direction  *1000, Color.green);
                ObjectHitEvent(null);
            }
        }

        public bool IsCursorOnWorldSpaceUI()
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                // Check if the UI element is in world space
                if (EventSystem.current.currentSelectedGameObject != null && EventSystem.current.currentSelectedGameObject.GetComponent<RectTransform>() == null)
                {
                    return true;
                }
            }
            return false;
        }
    }
}