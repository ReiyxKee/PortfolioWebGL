using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Portfolio
{
    public class UserInput : MonoBehaviour
    {
        public delegate void ScrollDelegate(float delta);
        public static event ScrollDelegate OnScroll;

        private static UserInput instance;

        public static UserInput GetInstance()
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

        void Update()
        {
            float scrollDelta = Input.GetAxis("Mouse ScrollWheel");

            if (scrollDelta != 0)
            {
                OnScroll?.Invoke(scrollDelta);
            }

            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                Vector2 touchDelta = Input.GetTouch(0).deltaPosition;

                OnScroll?.Invoke(-touchDelta.y);
            }
        }
    }
}