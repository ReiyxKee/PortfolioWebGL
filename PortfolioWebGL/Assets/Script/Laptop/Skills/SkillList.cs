using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using ReiyxDev;

namespace Portfolio
{
    public class SkillList : MonoBehaviour
    {
        [SerializeField]
        private List<Skills> skillList;

        [SerializeField]
        private GameObject prefab;
        [SerializeField]
        private Transform skillListParent;

        [SerializeField]
        private GameObject buttonPrefab;
        [SerializeField]
        private Transform buttonPrefabParent;

        private Dictionary<Skills, GameObject> skillDictionary;

        private Dictionary<SkillType, (bool, Image)> skillDisplay ;

        private EnumStringConversion _eSC = new EnumStringConversion();
        // Start is called before the first frame update
        void Start()
        {
            skillDictionary = new Dictionary<Skills, GameObject>();

            skillDisplay = new Dictionary<SkillType, (bool, Image)>();

            InitiateSkillDisplay();
            InstantiateSkillBadges();
            InstantiateSelectionButtons();
            UpdateButtonDisplay();
        }

        private void InitiateSkillDisplay()
        {
            foreach (SkillType skillType in Enum.GetValues(typeof(SkillType)))
            {
                skillDisplay[skillType] = (false, null);
            }

            skillDisplay[SkillType.All] = (true, null);
        }

        private void InstantiateSelectionButtons()
        {
            foreach (SkillType skillType in Enum.GetValues(typeof(SkillType)))
            {
                GameObject _button = Instantiate(buttonPrefab, buttonPrefabParent);

                _button.GetComponent<Button>().onClick.AddListener(()=> ToggleSkill(skillType.ToString()));
                _button.GetComponent<Button>().onClick.AddListener(() => UpdateFilter());
                _button.GetComponent<Button>().onClick.AddListener(()=> UpdateButtonDisplay());

                _button.GetComponentInChildren<TextMeshProUGUI>().text = _eSC.EnumToString(skillType);

                skillDisplay[skillType] = (skillDisplay[skillType].Item1, _button.GetComponent<Image>());
            }
        }

        private void InstantiateSkillBadges()
        {
            foreach (Skills _skill in skillList)
            {
                GameObject _badge = Instantiate(prefab, skillListParent);

                _badge.name = _skill.GetName();
                _badge.GetComponent<SkillPrefabData>().AssignSkill(_skill);

                skillDictionary.Add(_skill, _badge);
            }
        }

        public void UpdateButtonDisplay()
        {
            foreach (KeyValuePair<SkillType, (bool, Image)> _button in skillDisplay)
            {
                _button.Value.Item2.color = _button.Value.Item1 ? Color.gray : Color.clear;
            }
        }

        public void ToggleSkill(string _skillType)
        {
            SkillType parsedSkillType = (SkillType)_eSC.StringToEnum(typeof(SkillType), _skillType);

            Debug.Log(_skillType + " " + parsedSkillType);

            foreach (SkillType skillType in Enum.GetValues(typeof(SkillType)))
            {
                if (skillType == parsedSkillType)
                {
                    skillDisplay[skillType] = (true, skillDisplay[skillType].Item2);
                }
                else
                {
                    skillDisplay[skillType] = (false, skillDisplay[skillType].Item2);
                }
            }
        }

        public void UpdateFilter()
        {
            Debug.Log("UpdateFilter");

            HideAll();

            foreach (KeyValuePair<SkillType, (bool, Image)> _visiblity in skillDisplay)
            {
                if (_visiblity.Value.Item1 == true)
                {
                    Debug.Log("UpdateFilter for " + _visiblity);
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
                if (_skillType == SkillType.All)
                {
                    _skill.Value.SetActive(true);
                }
                else if (_skill.Key.GetSkillTypes().Contains(_skillType))
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