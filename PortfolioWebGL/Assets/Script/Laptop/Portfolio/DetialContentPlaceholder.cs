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
        [SerializeField] private Transform portfolioList;
        [SerializeField] private Image screenshotHolder;
        [SerializeField] private Image downloadFrom;
        [SerializeField] private List<Sprite> screenshots;
        [SerializeField] private GameObject screenshotBulletPrefab;
        [SerializeField] private Transform bulletParent;

        [SerializeField] private TextMeshProUGUI title;
        [SerializeField] private TextMeshProUGUI desc;

        [SerializeField] private Button site;
        [SerializeField] private Button back;

        [SerializeField] private List<SiteIcon> siteIcon;
        ReiyxDev.WebDomainInterpreter _domainCheck = new ReiyxDev.WebDomainInterpreter();

        private int defaultScreenshotPoolCount = 10;

        private float slideshowDuration = 2;
        private int currentSlideshow = 0;

        private Coroutine slideshowCycleCor;
        private void Start()
        {
            for (int i = 0; i < defaultScreenshotPoolCount; i++)
            {
                Instantiate(screenshotBulletPrefab, bulletParent);
            }


            HideAllBullet();

            BackList();
        }

        private IEnumerator SlideshowCycle()
        {   
            while (true)
            {
                yield return new WaitForSeconds(slideshowDuration);

                if (currentSlideshow + 1 > screenshots.Count - 1)
                {
                    currentSlideshow = 0;
                }
                else
                {
                    currentSlideshow++;
                }

                ShowScreenshot(screenshots[currentSlideshow]);
            }
        }

        public void PatchDetial(PortfolioItem _item)
        {
            currentSlideshow = 0;

            title.text = _item.GetProjectName();
            
            desc.text = _item.GetProjectDesc();

            site.onClick.RemoveAllListeners();
            site.onClick.AddListener(() => OpenSite(_item.GetURL()));

            back.onClick.RemoveAllListeners();
            back.onClick.AddListener(() => BackList());

            downloadFrom.sprite = GetSiteIcon(_item.GetURL());

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

        public Sprite GetSiteIcon(string _url)
        {
            return siteIcon.Find(s => s.name == _domainCheck.CheckDomain(_url)).sprite;
        }

        public void StartSlideshowCoroutine()
        {
            slideshowCycleCor = StartCoroutine(SlideshowCycle());
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
            screenshotHolder.preserveAspect = true;
        }

        public void OpenSite(string url)
        {
            Application.OpenURL(url);
        }

        public void BackList()
        {
            if (slideshowCycleCor != null)
            {
                StopCoroutine(slideshowCycleCor);
            }

            this.gameObject.SetActive(false);
            portfolioList.gameObject.SetActive(true);
        }
    }
}