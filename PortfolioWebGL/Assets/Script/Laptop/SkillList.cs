using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Portfolio
{
    public class SkillList : MonoBehaviour
    {
        [SerializeField]
        private List<Skills> skillList;

        [SerializeField]
        private GameObject prefab;

        [SerializeField] private Image _codingButton;
        [SerializeField] private Image _gameButton;
        [SerializeField] private Image _webButton;
        [SerializeField] private Image _discordBotButton;
        [SerializeField] private Image _databaseButton;
        [SerializeField] private Image _3DButton;

        private Dictionary<Skills, GameObject> skillDictionary;

        private Dictionary<SkillType, bool> skillDisplay ;

        // Start is called before the first frame update
        void Start()
        {
            skillDictionary = new Dictionary<Skills, GameObject>();

            InstantiateSkillBadges();

            skillDisplay = new Dictionary<SkillType, bool>();
            skillDisplay.Add(SkillType._coding, true);
            skillDisplay.Add(SkillType._3D, true);
            skillDisplay.Add(SkillType._database, true);
            skillDisplay.Add(SkillType._discordBot, true);
            skillDisplay.Add(SkillType._game, true);
            skillDisplay.Add(SkillType._web, true);

        }

        private void InstantiateSkillBadges()
        {
            foreach (Skills _skill in skillList)
            {
                GameObject _badge = Instantiate(prefab, this.transform);

                _badge.name = _skill.GetName();
                _badge.GetComponent<SkillPrefabData>().AssignSkill(_skill);

                skillDictionary.Add(_skill, _badge);
            }
        }

        public void ToggleDisplay(string _skillType)
        {
            switch (_skillType)
            {
                case "coding":
                    skillDisplay[SkillType._coding] = !skillDisplay[SkillType._coding];
                    _codingButton.color = skillDisplay[SkillType._coding] ? Color.white : Color.clear;
                    break;
                case "game": 
                    skillDisplay[SkillType._game] = !skillDisplay[SkillType._game];
                    _gameButton.color = skillDisplay[SkillType._game] ? Color.white : Color.clear;
                    break;
                case "web": 
                    skillDisplay[SkillType._web] = !skillDisplay[SkillType._web];
                    _webButton.color = skillDisplay[SkillType._web] ? Color.white : Color.clear;
                    break;
                case "discordBot":
                    skillDisplay[SkillType._discordBot] = !skillDisplay[SkillType._discordBot];
                    _discordBotButton.color = skillDisplay[SkillType._discordBot] ? Color.white : Color.clear;
                    break;
                case "database":
                    skillDisplay[SkillType._database] = !skillDisplay[SkillType._database];
                    _databaseButton.color = skillDisplay[SkillType._database] ? Color.white : Color.clear;
                    break;
                case "3D": 
                    skillDisplay[SkillType._3D] = !skillDisplay[SkillType._3D];
                    _3DButton.color = skillDisplay[SkillType._3D] ? Color.white : Color.clear;
                    break;
            }
        }

        public void UpdateFilter()
        {
            HideAll();

            foreach (KeyValuePair<SkillType, bool> _visiblity in skillDisplay)
            {
                if (_visiblity.Value == true)
                {
                    ShowType(_visiblity.Key);
                }
            }
        }

        private void HideAll()
        {
            foreach (KeyValuePair<Skills, GameObject> _skill in skillDictionary)
            {
                _skill.Value.SetActive(false);
            }
        }

        private void ShowType(SkillType _skillType)
        {
            foreach (KeyValuePair<Skills, GameObject> _skill in skillDictionary)
            {
                if (_skill.Key.GetSkillTypes().Contains(_skillType))
                {
                    if (!_skill.Value.activeInHierarchy)
                    {
                        _skill.Value.SetActive(true);
                    }
                }
            }
        }

    }
}