using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Portfolio
{
    public class VersionNumber : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
#if DEVELOPMENT_BUILD
            this.GetComponent<TextMeshProUGUI>().text = "Development Build : " + Application.version;    
#else
            this.GetComponent<TextMeshProUGUI>().text = "";
#endif
        }

        void SetVersionNumber()
        {

        }
    }
}