using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace ReiyxDev
{
    public class TextGlitchAnimation
    {
        //Do use fonts that supports the block texts, such as Noto Sans Japanese
        char[] blockChars = new char[] { '\u2591', '\u2592', '\u2593', '\u2588', '\u2592' };
        public IEnumerator GlitchCoroutine(TextMeshProUGUI textMeshItem, string originalText, float interval, int iteration)
        {
            int originalLength = originalText.Length;
            int maxGlitchedChars = originalLength / 2;
            int charsGlitched = 0;

            for (int i = 0; i < iteration; i++)
            {
                string glitchedStr = "";

                // For the first iteration, replace all characters with block characters
                if (i == 0)
                {
                    foreach (char c in originalText)
                    {
                        glitchedStr += blockChars[Random.Range(0, blockChars.Length)];
                    }
                }
                else
                {
                    foreach (char c in textMeshItem.text)
                    {
                        // Randomly decide whether to replace the character with a block character or not
                        if (Random.Range(0f, 1f) < 0.5f && charsGlitched < maxGlitchedChars)
                        {
                            glitchedStr += blockChars[Random.Range(0, blockChars.Length)];
                            charsGlitched++;
                        }
                        else
                        {
                            glitchedStr += originalText[Random.Range(0, originalLength)];
                        }
                    }
                }

                textMeshItem.text = glitchedStr;
                yield return new WaitForSecondsRealtime(interval);
            }

            textMeshItem.text = originalText;
        }

    }
}