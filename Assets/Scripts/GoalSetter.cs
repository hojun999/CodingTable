using System.Collections;
using System.Collections.Generic;
//using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.UI;

public class GoalSetter : MonoBehaviour
{
    private SoundEffectManager soundEffectManager;

    public RectTransform car;
    public RectTransform goal;

    public GameObject clearUIPanel;
    public GameObject curUIPanel;
    public GameObject nextUIPanel;

    [HideInInspector] public bool isArrive = false;

    [Header("Button")]
    public List<Button> buttons = new List<Button>();

    private void Start()
    {
        soundEffectManager = GameObject.FindGameObjectWithTag("SoundEffectManager").GetComponent<SoundEffectManager>();
    }

    private void Update()
    {
        if (IsArrive(car, goal) && isArrive == false)
        {
            Debug.Log("스테이지 클리어");
            StartCoroutine(StageClear());
            isArrive = true;        // 클리어 한 번만 처리
        }
    }

    private bool IsArrive(RectTransform carRect, RectTransform goalRect)
    {
        Vector3[] carCorners = new Vector3[4];
        Vector3[] goalCorners = new Vector3[4];

        carRect.GetWorldCorners(carCorners);
        goalRect.GetWorldCorners(goalCorners);

        // car, goal ui 오브젝트 간의 recttransform 충돌 여부 확인
        for (int i = 0; i < 4; i++)
        {
            if (goalRect.rect.Contains(goalRect.InverseTransformPoint(carCorners[i])))
                return true;
        }

        // 타겟 ui 오브젝트의 위, 아래 방향으로 car가 충돌했을 때의 처리
        for (int i = 0; i < 4; i++)
        {
            if (carRect.rect.Contains(carRect.InverseTransformPoint(goalCorners[i])))
                return true;
        }

        return false;
    }

    IEnumerator StageClear()
    {
        yield return new WaitForSeconds(1.5f);

        foreach (var button in buttons)
        {
            button.interactable = false;
        }

        clearUIPanel.SetActive(true);
        soundEffectManager.GameClear();
        yield return new WaitForSeconds(1.25f);
        clearUIPanel.SetActive(false);
        curUIPanel.SetActive(false);
        nextUIPanel.SetActive(true);

        if(nextUIPanel.gameObject.name == "19_clear")
            soundEffectManager.PlayVoice_Clear();
    }
}
