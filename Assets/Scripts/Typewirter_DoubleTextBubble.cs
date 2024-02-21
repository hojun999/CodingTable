using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Typewirter_DoubleTextBubble : MonoBehaviour
{
    [SerializeField] private float typingSpeed = 0.09f;
    [SerializeField] private string fullText;
    private string currentText = ""; // 현재까지 타이핑된 텍스트
    //public TextMeshProUGUI textComponent;
    public List<TextMeshProUGUI> textList = new List<TextMeshProUGUI>();
    Coroutine typingTextCoroutine;

    private void Start()
    {
        foreach (TextMeshProUGUI text in textList)
        {
            text.raycastTarget = false;
        }

        typingTextCoroutine = StartCoroutine(TypeText());
    }

    public void StopTypingAndFillingText()
    {
        StopCoroutine(typingTextCoroutine);
        //textComponent.text = fullText;
    }

    IEnumerator TypeText()      // text 순서대로 
    {
        for (int i = 0; i < textList.Count; i++)
        {
            fullText = textList[i].text;
            textList[i].gameObject.SetActive(true);

            for (int j = 0; j < fullText.Length; j++)
            {
                currentText = fullText.Substring(0, j + 1);
                textList[i].text = currentText;

                yield return new WaitForSeconds(typingSpeed);

            }
        }
    }


    //public void ResetText()
    //{

    //    StopCoroutine(typingTextCoroutine);

    //    textList[1].gameObject.SetActive(false);

    //    typingTextCoroutine = StartCoroutine(TypeText());
    //}
}
