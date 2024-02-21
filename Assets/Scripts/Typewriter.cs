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
            for (int i = 0; i < text.Length; i++)   // Ÿ���� ȿ�� + ��ġ �ؽ�Ʈ �±� ó��
            {
                if (text[i] == '<')     // ��ġ �ؽ�Ʈ �±� >> �ϼ��� �±�
                    textField.text += GetCompleteRichTextTag(ref i);
                else
                    textField.text += text[i];  // �ϼ��� �±װ� �߰��� ���ο� text input �߰�

                //float currentSpeed = typingSpeed;
                //if (skip)
                //    currentSpeed = 0f;

                yield return new WaitForSeconds(typingSpeed);
        }

    }

    string GetCompleteRichTextTag(ref int index)
    {
        string completeTag = "";

        while(index < text.Length)  // �ϼ��� �±� �߰�
        {
            completeTag += text[index];

            if (text[index] == '>')
                return completeTag;

            index++;
        }

        return "";
    }
}
