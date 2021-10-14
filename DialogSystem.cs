using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

public class DialogSystem : MonoBehaviour
{
    public GameObject dialogBox;
    public Text dialogText;

    public string[] text;

    public int currentText;
    public float speedText;

    public bool inRange;
    public bool isCompleted;

    void Start()
    {
        
    }

    void Update()
    {
        try
        {
            dialogText.text = text[currentText];
        }
        catch (System.IndexOutOfRangeException)
        {
            dialogBox.SetActive(false);
            currentText -= 1;
            Destroy(gameObject);
            ClearLog();
            throw;
        }
        if (inRange == true)
        {
            dialogBox.SetActive(true);
        }
        else
        {
            dialogBox.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.Z) && inRange == true)
        {
            currentText += 1;
            StartCoroutine("showText", text[currentText]);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            inRange = true;
        }
    }

    void OnTriggerStay2D(Collider2D other)
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
        //dialogText.GetComponent<Animator>().Play("Idle");
        //dialogText.GetComponent<Animator>().Play("TextAppear");
        dialogText.text = text;
        yield return new WaitForSeconds(speedText);
        /*int textLength = dialogText.text.Length;

        
        int i = 0;
        while (i <= textLength)
        {
            //UnityEngine.Color textColor = dialogText.GetComponent<Text>().color;

            dialogText.text = text.Substring(0, i);
            i++;
            yield return new WaitForSeconds(speedText);
        }
        if (i >= textLength)
        {
            isCompleted = true;
        }*/
    }

    public void ClearLog()
    {
        var assembly = Assembly.GetAssembly(typeof(UnityEditor.Editor));
        var type = assembly.GetType("UnityEditor.LogEntries");
        var method = type.GetMethod("Clear");
        method.Invoke(new object(), null);
    }
}
