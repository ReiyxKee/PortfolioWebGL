using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using ReiyxDev;

namespace Portfolio
{
    public class ListItemPlaceholder : MonoBehaviour
    {
        [SerializeField] private Image thumbnail;
        [SerializeField] private TextMeshProUGUI title;
        [SerializeField] private TextMeshProUGUI desc;
        [SerializeField] private Button site;
        [SerializeField] private Image siteImage;
        [SerializeField] private Transform implementations;
        [SerializeField] private GameObject implementationPrefab;
        [SerializeField] private List<SiteIcon> siteIcon;
        ReiyxDev.EnumStringConversion _eSC = new ReiyxDev.EnumStringConversion();
        ReiyxDev.WebDomainInterpreter _domainCheck = new ReiyxDev.WebDomainInterpreter();

        public void PatchItem(PortfolioItem _item)
        {
            thumbnail.sprite = _item.GetThumbnail();

            title.text = _item.GetProjectName();

            string _description = _item.GetProjectDesc(); 
            desc.text = _description.Length <= 110 ? _description : _description.Substring(0, 110) + "...";


            siteImage.sprite = GetSiteIcon(_item.GetURL());
        

            site.onClick.RemoveAllListeners();
            site.onClick.AddListener(() => OpenSite(_item.GetURL()));

            foreach (ProjectImplementation imp in _item.GetProjectImplementations())
            {
                GameObject implement = Instantiate(implementationPrefab, implementations);
                implement.GetComponentInChildren<TextMeshProUGUI>().text = _eSC.EnumToString(imp);
            }
        }

        public Sprite GetSiteIcon(string _url)
        {
            return siteIcon.Find(s => s.name == _domainCheck.CheckDomain(_url)).sprite;
        }

        public void OpenSite(string url)
        {
            Application.OpenURL(url);
        }
    }
}