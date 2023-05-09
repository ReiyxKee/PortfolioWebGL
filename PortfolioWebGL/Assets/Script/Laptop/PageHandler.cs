using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Portfolio 
{
    public class PageHandler : MonoBehaviour
    {
        [SerializeField] private List<Transform> pages;

        private Dictionary<string, Transform> _pagesPair = new Dictionary<string, Transform>();

        // Start is called before the first frame update
        void Start()
        {
            InitializePages();
        }

        void InitializePages()
        {
            foreach (Transform page in pages)
            {
                _pagesPair.Add(page.name, page);
            }

            HideAllPages();

            DisplayPage(pages[0].name);
        }

        public void DisplayPage(string pageName)
        {
            _pagesPair[pageName].gameObject.SetActive(true);
        }

        public void HideAllPages()
        {
            foreach (KeyValuePair<string, Transform> page in _pagesPair)
            {
                page.Value.gameObject.SetActive(false);
            }
        }
    }

}