using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarMoveController : MonoBehaviour
{
    public DirectionNodController directionNodController;
    public FailSetter failSetter;
    [HideInInspector] public Image carSprite;

    [Header("Position")]
    public RectTransform standardRectTransform;
    public RectTransform rightRectTransform;
    public RectTransform downRectTransform;
    public RectTransform goalRectTransform;
    public RectTransform startRectTransform;
    [HideInInspector] public RectTransform carRectTransform;

    private float xOffset;
    private float yOffset;

    public List<Action> moveActions = new List<Action>();

    private Coroutine moveCoroutine;
    private Coroutine submitCoroutine;

    public float moveTime;

    [HideInInspector] public int moveCount;
    //private bool isGoal = false;

    [Header("Button")]
    public Button rightDirectionButton;
    public Button leftDirectionButton;
    public Button upperDirectionButton;
    public Button downDirectionButton;
    public Button submitButton;
    public Button deleteButton;

    [Header("Sprties")]
    public Sprite car_topview;
    public Sprite car_sideview;


    private void Start()
    {
        carRectTransform = gameObject.GetComponent<RectTransform>();
        carSprite = gameObject.GetComponent<Image>();

        SetMoveDirection();
        SetButton();
    }

    private void SetMoveDirection()
    {
        xOffset = rightRectTransform.localPosition.x - standardRectTransform.localPosition.x;
        Debug.Log(xOffset);
        yOffset = standardRectTransform.localPosition.y - downRectTransform.localPosition.y;
        Debug.Log(yOffset);
    }

    private void SetButton()
    {
        Debug.Log("버튼 이벤트 배치");
        rightDirectionButton.onClick.AddListener(() => RightMoveToAction());
        leftDirectionButton.onClick.AddListener(() => LeftMoveToAction());
        upperDirectionButton.onClick.AddListener(() => UpperMoveToAction());
        downDirectionButton.onClick.AddListener(() => DownMoveToAction());
        submitButton.onClick.AddListener(() => SubmitMoveInfo());
    }

    IEnumerator MoveCar(float xOffset, float yOffset)
    {
        
        Vector2 moveValue = transform.localPosition + new Vector3(xOffset, yOffset, 0f);
        transform.DOLocalMove(moveValue, moveTime, true);

        failSetter.Start_CoFail_OnUseAllDirection();
        yield return new WaitForSeconds(moveTime);
    }

    private void Info_MoveRight()
    {
        Debug.Log("오른쪽 저장");

        carSprite.sprite = car_sideview;
        carRectTransform.localScale = new Vector3(1,1,1);
        carRectTransform.localRotation = Quaternion.Euler(new Vector3(0,0,0));

        moveCoroutine = StartCoroutine(MoveCar(xOffset, 0));
        moveCount++;
    }

    private void Info_MoveLeft()
    {
        Debug.Log("왼쪽 저장");

        carSprite.sprite = car_sideview;
        carRectTransform.localScale = new Vector3(-1,1,1);
        carRectTransform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));

        moveCoroutine = StartCoroutine(MoveCar(-xOffset, 0));
        moveCount++;
    }

    private void Info_MoveUpper() 
    {
        Debug.Log("위쪽 저장");

        carSprite.sprite = car_topview;
        carRectTransform.localScale = new Vector3(1,1,1);
        carRectTransform.localRotation = Quaternion.Euler(new Vector3(0, 0, -90));

        moveCoroutine = StartCoroutine(MoveCar(0, yOffset));
        moveCount++;
    }

    private void Info_MoveDown()
    {
        Debug.Log("아래쪽 저장");

        carSprite.sprite = car_topview;
        carRectTransform.localScale = new Vector3(1, -1, 1);
        carRectTransform.localRotation = Quaternion.Euler(new Vector3(0, 0, 90));

        moveCoroutine = StartCoroutine(MoveCar(0, -yOffset));
        moveCount++;
    }

    // 버튼 이벤트에 등록되는 함수
    public void RightMoveToAction()
    {
        if(moveActions.Count < directionNodController.pullingCount)     // move 액션이 계속 추가되는 것 방지
        {
            moveActions.Add(Info_MoveRight);
        }
    }

    public void LeftMoveToAction()
    {
        if (moveActions.Count < directionNodController.pullingCount)
        {
            moveActions.Add(Info_MoveLeft);
        }
    }

    public void UpperMoveToAction()
    {
        if (moveActions.Count < directionNodController.pullingCount)
        {
            moveActions.Add(Info_MoveUpper);
        }
    }

    public void DownMoveToAction()
    {
        if (moveActions.Count < directionNodController.pullingCount)
        {
            moveActions.Add(Info_MoveDown);
        }
    }

    public void SubmitMoveInfo()
    {
        submitCoroutine = StartCoroutine(coSubmitMoveInfo());
        directionNodController.usedCount += moveActions.Count;
    }

    IEnumerator coSubmitMoveInfo()
    {
        rightDirectionButton.interactable = false;
        leftDirectionButton.interactable = false;
        upperDirectionButton.interactable = false;
        downDirectionButton.interactable = false;
        submitButton.interactable = false;
        deleteButton.interactable = false;

        for (int i = 0; i < moveActions.Count; i++)
        {
            moveActions[i]?.Invoke();
            yield return new WaitForSeconds(moveTime);
        }

        moveActions.Clear();
        rightDirectionButton.interactable = true;
        leftDirectionButton.interactable = true;
        upperDirectionButton.interactable = true;
        downDirectionButton.interactable = true;
        submitButton.interactable = true;
        deleteButton.interactable = true;

    }

    public void ResetMoveActions()
    {
        Debug.Log("차 위치, 무브액션 초기화");

        moveActions.Clear();

        transform.localPosition = startRectTransform.localPosition;
        moveCount = 0;
    }
}
