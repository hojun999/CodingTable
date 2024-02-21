using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Typewriter : MonoBehaviour
{
    string text;
    [SerializeField] float typingSpeed = 0.09f;

    bool skip;

    private void Start()
    {
        text = gameObject.GetComponent<TextMeshProUGUI>().text;
        gameObject.GetComponent<TextMeshProUGUI>().raycastTarget = false;

        StartEffect();
    }

    public void StartEffect()
    {
        if(TryGetComponent(out TextMeshProUGUI textField))
        {
            StopAllCoroutines();
            skip = false;

            textField.text = "";
            StartCoroutine(TypeText(textField));
        }
    }

    IEnumerator TypeText(TextMeshProUGUI textField)
    {
            for (int i = 0; i < text.Length; i++)   // 타이핑 효과 + 리치 텍스트 태그 처리
            {
                if (text[i] == '<')     // 리치 텍스트 태그 >> 완성된 태그
                    textField.text += GetCompleteRichTextTag(ref i);
                else
                    textField.text += text[i];  // 완성된 태그가 추가된 새로운 text input 추가

                //float currentSpeed = typingSpeed;
                //if (skip)
                //    currentSpeed = 0f;

                yield return new WaitForSeconds(typingSpeed);
        }

    }

    string GetCompleteRichTextTag(ref int index)
    {
        string completeTag = "";

        while(index < text.Length)  // 완성된 태그 추가
        {
            completeTag += text[index];

            if (text[index] == '>')
                return completeTag;

            index++;
        }

        return "";
    }
}
