using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Portfolio
{
    [CreateAssetMenu(menuName = "Skill")]
    public class Skills : ScriptableObject
    {
        [SerializeField]
        private string skillName;
        [SerializeField]
        private Sprite icon;
        [SerializeField]
        private List<SkillType> skillTypes;

        public string GetName()
        {
            return skillName;
        }
        public Sprite GetIcon()
        {
            return icon;
        }
        public List<SkillType> GetSkillTypes()
        {
            return skillTypes;
        }
    }

    public enum SkillType
    {
        All,
        GameDevelopment,
        WebFrontend,
        WebBackend,
        _3D
    }

}