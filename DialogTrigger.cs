using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogTrigger : MonoBehaviour
{
    public Text textUI;
    public float speedText;

    public string text1 = "Диалог";
    public string text2 = "Диалог";
    public string text3 = "Диалог";
    public string text4 = "Диалог";
    public string text5 = "Диалог";
    public string text6 = "Диалог";
    public string text7 = "Диалог";
    public string text8 = "Диалог";
    public string text9 = "Диалог";
    public string text10 = "Диалог";

    public bool isCompleted;
    private bool inRange;

    void Update()
    {
        if (inRange)
        {
            StartCoroutine("showText", text1);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            inRange = true;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            inRange = false;
        }
    }

    IEnumerator showText(string text)
    {
        int i = 0;
        while (i <= text.Length)
        {
            textUI.text = text.Substring(0, i);
            i++;

            yield return new WaitForSeconds(speedText);
        }
        if (i >= text.Length)
        {
            isCompleted = true;
        }
    }
}
