using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ReiyxDev;
using TMPro;

namespace Portfolio
{
    public class EventHandler : MonoBehaviour
    {
        [SerializeField] Vector3 glitchPosition;
        [SerializeField] private TextMeshProUGUI aboutMe;
        [SerializeField] private float interval = 0.15f;
        [SerializeField] private int iteration = 3;

        private bool playAboutPlayer;
        private string aboutMeText = "";

        private TextGlitchAnimation glitchAnimation = new TextGlitchAnimation();

        // Start is called before the first frame update
        void Start()
        {
            aboutMeText = aboutMe.text;
            aboutMe.text = "";
            playAboutPlayer = false;
        }

        // Update is called once per frame
        void Update()
        {
            if (Vector3.Distance(CameraByValue.GetInstance().GetPos(), glitchPosition) <= 0.5f)
            {
                TriggerGlitchText();
            }
        }

        void TriggerGlitchText()
        {
            if (playAboutPlayer) return;

            Debug.Log("Glitch");
            playAboutPlayer = true;
            
            StartCoroutine(glitchAnimation.GlitchCoroutine(aboutMe, aboutMeText, interval, iteration));
            
        }
    }
}