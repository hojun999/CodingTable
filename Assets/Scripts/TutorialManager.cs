using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TutorialManager : MonoBehaviour
{
    //06_Tutorial > GameLayoutBG > car¿¡ ÇÒ´ç

    public GameObject curUIPanel;
    public GameObject gameOverUIPanel;

    public void TutorialAction()
    {
        StartCoroutine(coTutorialAction());
    }

    IEnumerator coTutorialAction()
    {
        Vector2 move1Value = transform.localPosition + new Vector3(140 , 0, 0);
        transform.DOLocalMove(move1Value, 2f, true);

        Vector2 move2Value = transform.localPosition + new Vector3(280, 0, 0);
        yield return new WaitForSeconds(2f);

        transform.DOLocalMove(move2Value, 2f, true);
        yield return new WaitForSeconds(1.5f);
        curUIPanel.SetActive(false);
        gameOverUIPanel.SetActive(true);
    }
}
