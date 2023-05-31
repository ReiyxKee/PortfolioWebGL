using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Portfolio
{
    public class PortfolioHandler : MonoBehaviour
    {
        [SerializeField] private Transform selectionList;
        [SerializeField] private List<PortfolioItem> portfolioList;

        [SerializeField] private GameObject portfolioListPrefab;

        [SerializeField] private Transform parentList;
        [SerializeField] private Transform parentDetial;

        [SerializeField] private DetialContentPlaceholder fullPagePlaceholder;
        // Start is called before the first frame update
        void Start()
        {
            InitializePortfolio();
        }

        // Update is called once per frame
        void Update()
        {

        }

        void InitializePortfolio()
        {
            foreach (PortfolioItem portfolioItem in portfolioList)
            {
                GameObject item = Instantiate(portfolioListPrefab, parentList);
                
                item.AddComponent<PortfolioOnClick>();

                AssignOnClickEvent(item, portfolioItem);
                PatchList(item, portfolioItem);
            }
        }

        void AssignOnClickEvent(GameObject _prefab, PortfolioItem _item)
        {
            PortfolioOnClick onClick = _prefab.AddComponent<PortfolioOnClick>();

            onClick.SetParameters(fullPagePlaceholder, _item, selectionList);

            if (_prefab.GetComponent<Button>() == null)
            {
                _prefab.AddComponent<Button>().onClick.AddListener(() => onClick.Action());
            }
            else
            {
                _prefab.GetComponent<Button>().onClick.AddListener(() => onClick.Action());
            }
        }

        void PatchList(GameObject listPrefab, PortfolioItem _pI)
        {
            ListItemPlaceholder listItem = listPrefab.GetComponent<ListItemPlaceholder>();
            listItem.PatchItem(_pI);
        }

        void PatchDetialPage(GameObject pagePrefab)
        {

        }
    }

    public enum ProjectImplementation
    {
        Unity,
        Blender,
        NodeJs,
        MongoDB,
        DynamoDB,
        AR,
        _3D
    }

    [System.Serializable]
    public struct SiteIcon
    {
        public string name;
        public Sprite sprite;
    }
}
