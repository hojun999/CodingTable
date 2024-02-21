using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FailSetter : MonoBehaviour
{
    public DirectionNodController directionNodController;
    public CarMoveController carMoveController;
    public GoalSetter goalSetter;
    private SoundEffectManager soundEffectManager;

    public RectTransform car;
    public List<RectTransform> obstacles = new List<RectTransform>();

    public List<RectTransform> barsOfmapoutline = new List<RectTransform>();        // 차가 맵 밖으로 나갔을 때 방지 collider

    //public GameObject curGameUIPanel;
    public GameObject failUIPanel;

    public Button resetButton;

    private bool isFail = false;

    private void Start()
    {
        soundEffectManager = GameObject.FindGameObjectWithTag("SoundEffectManager").GetComponent<SoundEffectManager>();
    }

    private void Update()
    {
        for (int i = 0; i < obstacles.Count; i++)
        {
            if (IsTouchObstacles(car, obstacles[i]) &&isFail == false)
            {
                Debug.Log("스테이지 실패 : 장애물");
                StartCoroutine(coFail());

                isFail = true;
            }
        }


        for (int i = 0; i < barsOfmapoutline.Count; i++)
        {
            if (IsCarGoingOutOfMap(car, barsOfmapoutline[i]) && isFail == false)
            {
                Debug.Log("스테이지 실패 : 맵 밖");
                StartCoroutine(coFail());

                isFail = true;
            }
        }

        
    }

    private bool IsTouchObstacles(RectTransform carRect, RectTransform obstacleRect)
    {
        Vector3[] carCorners = new Vector3[4];
        Vector3[] obstacleCorners = new Vector3[4];

        carRect.GetWorldCorners(carCorners);
        obstacleRect.GetWorldCorners(obstacleCorners);

        for (int i = 0; i < 4; i++)
        {
            if (obstacleRect.rect.Contains(obstacleRect.InverseTransformPoint(carCorners[i])))
                return true;
        }


        for (int i = 0; i < 4; i++)
        {
            if (carRect.rect.Contains(carRect.InverseTransformPoint(obstacleCorners[i])))
                return true;
        }
        return false;
    }

    private bool IsCarGoingOutOfMap(RectTransform carRect, RectTransform barRect)
    {
        Vector3[] carCorners = new Vector3[4];
        Vector3[] barCorners = new Vector3[4];

        carRect.GetWorldCorners(carCorners);
        barRect.GetWorldCorners(barCorners);

        for (int i = 0; i < 4; i++)
        {
            if (barRect.rect.Contains(barRect.InverseTransformPoint(carCorners[i])))
                return true;
        }

        for (int i = 0; i < 4; i++)
        {
            if (carRect.rect.Contains(carRect.InverseTransformPoint(barCorners[i])))
                return true;
        }

        return false;

    }

    IEnumerator coFail()
    {

        yield return new WaitForSeconds(1.5f);


        carMoveController.StopAllCoroutines();
        carMoveController.rightDirectionButton.interactable = true;
        carMoveController.leftDirectionButton.interactable = true;
        carMoveController.upperDirectionButton.interactable = true;
        carMoveController.downDirectionButton.interactable = true;
        carMoveController.submitButton.interactable = true;
        carMoveController.deleteButton.interactable = true;

        failUIPanel.SetActive(true);
        soundEffectManager.GameFail();
        soundEffectManager.PlayVoice_Fail();

        resetButton.onClick.AddListener(() => ResetGameUiPanel());

    }

    public void ResetGameUiPanel()
    {
        carMoveController.ResetMoveActions();
        directionNodController.ResetNod();

        isFail = false;

        carMoveController.carSprite.sprite = carMoveController.car_sideview;
        carMoveController.carRectTransform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));

        failUIPanel.SetActive(false);
    }

    IEnumerator coFail_OnUseAllDirection()
    {
        yield return new WaitForSeconds(.7f);

        if (directionNodController.usedCount == directionNodController.pullingCount && carMoveController.moveCount == directionNodController.pullingCount && !goalSetter.isArrive && directionNodController.isFullDirection)
        {
            Debug.Log("스테이지 실패 : 도착 실패");
            StartCoroutine(coFail());

            carMoveController.moveCount = 0;        // 조건 중 아무거나 초기화해서 중복 호출 방지
        }
    }

    public void Start_CoFail_OnUseAllDirection()
    {
        StartCoroutine(coFail_OnUseAllDirection());
    }
}
