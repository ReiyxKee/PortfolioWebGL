using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Portfolio
{
    [CreateAssetMenu(menuName = "Portfolio Item")]
    public class PortfolioItem : ScriptableObject
    {
        [SerializeField] private string projectName;
        [SerializeField, TextArea] private string projectDesc;
        [SerializeField] private string url;
        [SerializeField] private List<ProjectImplementation> projectImplementations;
        [SerializeField] private Sprite thumbnail;
        [SerializeField] private List<Sprite> screenshots;

        public string GetProjectName()
        {
            return projectName;
        }

        public string GetProjectDesc()
        {
            return projectDesc;
        }

        public List<ProjectImplementation> GetProjectImplementations()
        {
            return projectImplementations;
        }

        public Sprite GetThumbnail()
        {
            return thumbnail;
        }

        public List<Sprite> GetScreenshots()
        {
            return screenshots;
        }

        public string GetURL()
        {
            return url;
        }
    }
}