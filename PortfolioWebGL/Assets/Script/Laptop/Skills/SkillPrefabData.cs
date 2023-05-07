using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Portfolio
{
    public class SkillPrefabData : MonoBehaviour
    {
        [SerializeField] private Image skillIcon;
        [SerializeField] private TextMeshProUGUI skillName;

        private Skills skill;

        // Update is called once per frame
        void Update()
        {
            if (skill == null) return;

            if (skillName.text != skill.GetName())
            {
                skillName.text = skill.GetName();
            }

            if (skillIcon.sprite != skill.GetIcon())
            {
                skillIcon.sprite = skill.GetIcon();
            }
        }

        public void AssignSkill(Skills _skill)
        {
            skill = _skill;
        }
    }
}