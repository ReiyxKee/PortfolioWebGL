using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Portfolio
{
    public class PortfolioHandler : MonoBehaviour
    {
        [SerializeField] private Transform selectionList;
        [SerializeField] private List<PortfolioItem> portfolioList;

        [SerializeField] private GameObject portfolioListPrefab;
        [SerializeField] private GameObject portfolioDetialPrefab;

        [SerializeField] private Transform parentList;
        [SerializeField] private Transform parentDetial;

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

                PatchList(item);
            }
        }

        void PatchList(GameObject listPrefab)
        {

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

}
