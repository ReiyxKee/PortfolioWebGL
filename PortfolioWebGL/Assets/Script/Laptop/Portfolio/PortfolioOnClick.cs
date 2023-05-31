using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Portfolio
{
    public class PortfolioOnClick : MonoBehaviour
    {
        private DetialContentPlaceholder page;
        private PortfolioItem item;
        private Transform listParent;

        public void SetParameters(DetialContentPlaceholder _page, PortfolioItem _item, Transform _listParent)
        {
            page = _page;
            item = _item;
            listParent = _listParent;
        }

        public void Action()
        {
            page.PatchDetial(item);
            listParent.gameObject.SetActive(false);
            page.transform.gameObject.SetActive(true);
            page.StartSlideshowCoroutine();
        }
    }
}