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
            Debug.Log("�������� Ŭ����");
            StartCoroutine(StageClear());
            isArrive = true;        // Ŭ���� �� ���� ó��
        }
    }

    private bool IsArrive(RectTransform carRect, RectTransform goalRect)
    {
        Vector3[] carCorners = new Vector3[4];
        Vector3[] goalCorners = new Vector3[4];

        carRect.GetWorldCorners(carCorners);
        goalRect.GetWorldCorners(goalCorners);

        // car, goal ui ������Ʈ ���� recttransform �浹 ���� Ȯ��
        for (int i = 0; i < 4; i++)
        {
            if (goalRect.rect.Contains(goalRect.InverseTransformPoint(carCorners[i])))
                return true;
        }

        // Ÿ�� ui ������Ʈ�� ��, �Ʒ� �������� car�� �浹���� ���� ó��
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
