using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using ReiyxDev;

namespace Portfolio
{
    public class DetialContentPlaceholder : MonoBehaviour
    {
        private Transform portfolioList;
        private Image screenshotHolder;
        private List<Sprite> screenshots;
        private GameObject screenshotBulletPrefab;
        private Transform bulletParent;

        private TextMeshProUGUI title;
        private TextMeshProUGUI desc;

        private Button site;
        private Button back;

        public void PatchDetial(PortfolioItem _item)
        {
            title.text = _item.GetProjectName();
            
            desc.text = _item.GetProjectDesc();

            site.onClick.RemoveAllListeners();
            site.onClick.AddListener(() => OpenSite(_item.GetURL()));

            back.onClick.RemoveAllListeners();
            back.onClick.AddListener(() => BackList());

            screenshots = _item.GetScreenshots();
            screenshotHolder.sprite = screenshots[0];

            HideAllBullet();

            if (bulletParent.childCount < screenshots.Count)
            {
                for (int i = 0; i < screenshots.Count - bulletParent.childCount; i++)
                {
                    Instantiate(screenshotBulletPrefab, bulletParent);
                }
            }

            for (int i = 0; i < screenshots.Count; i++)
            {
                AssignSpriteToBullet(GetSetActiveBullet(i).GetComponent<Button>(), i);
            }
        }

        public GameObject GetSetActiveBullet(int i)
        {
            bulletParent.GetChild(i).gameObject.SetActive(true);

            return bulletParent.GetChild(i).gameObject;
        }

        public void HideAllBullet()
        {
            foreach (Transform _child in bulletParent)
            {
                if (_child.gameObject.activeInHierarchy)
                {
                    _child.gameObject.SetActive(false);
                }
            }
        }
        public void AssignSpriteToBullet(Button _button, int _ss)
        {
            _button.onClick.RemoveAllListeners();
            _button.onClick.AddListener(() => ShowScreenshot(screenshots[_ss]));
        }

        public void ShowScreenshot(Sprite _sprite)
        {
            screenshotHolder.sprite = _sprite;
        }

        public void OpenSite(string url)
        {
            Application.OpenURL(url);
        }

        public void BackList()
        {
            this.gameObject.SetActive(false);
            portfolioList.gameObject.SetActive(true);
        }
    }
}