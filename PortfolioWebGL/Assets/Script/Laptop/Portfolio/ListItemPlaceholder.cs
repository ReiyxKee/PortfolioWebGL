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
        [SerializeField] private Transform implementations;
        [SerializeField] private GameObject implementationPrefab;
        ReiyxDev.EnumStringConversion _eSC = new ReiyxDev.EnumStringConversion();

        public void PatchItem(PortfolioItem _item)
        {
            thumbnail.sprite = _item.GetThumbnail();

            title.text = _item.GetProjectName();

            string _description = _item.GetProjectDesc(); 
            desc.text = _description.Length <= 110 ? _description : _description.Substring(0, 110) + "...";

            site.onClick.RemoveAllListeners();
            site.onClick.AddListener(() => OpenSite(_item.GetURL()));

            foreach (ProjectImplementation imp in _item.GetProjectImplementations())
            {
                GameObject implement = Instantiate(implementationPrefab, implementations);
                implement.GetComponentInChildren<TextMeshProUGUI>().text = _eSC.EnumToString(imp);
            }
        }

        public void OpenSite(string url)
        {
            Application.OpenURL(url);
        }
    }
}