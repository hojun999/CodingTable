using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DirectionNodController : MonoBehaviour
{
    //DirectionButtonLayout에 스크립트 할당함

    public GameObject rightDirectionPrefab;
    [HideInInspector] public List<GameObject> rightDirections = new List<GameObject>();       // 풀링을 위한 리스트 선언

    public GameObject leftDirectionPrefab;
    [HideInInspector] public List<GameObject> leftDirections = new List<GameObject>();

    public GameObject upperDirectionPrefab;
    [HideInInspector] public List<GameObject> upperDirections = new List<GameObject>();

    public GameObject downDirectionPrefab;
    [HideInInspector] public List<GameObject> downDirections = new List<GameObject>();


    public List<GameObject> directionNodHolder;

    [Header("Delete")]
    public CarMoveController carMoveController;
    public Button deleteButton;
    [HideInInspector]public int usedCount;
    private int lastIndex_MoveAction;
    [HideInInspector] public bool isFullDirection;

    [Header("Create Nod")]
    public Button rightDirectionButton;
    public Button leftDirectionButton;
    public Button upperDirectionButton;
    public Button downDirectionButton;


    [HideInInspector]public int pullingCount;   // 풀링할 노드의 개수
    [HideInInspector]public int useCount;

    private void Start()
    {
        pullingCount = directionNodHolder.Count;

        for (int i = 0; i < pullingCount; i++)
        {
            if (i == pullingCount)
            {
                // 노드 홀더 개수 만큼 생성되면 생성 중지(같은 노드 연속 사용 경우 대응)
                break;
            }

            GameObject rightDirection = Instantiate(rightDirectionPrefab);
            rightDirections.Add(rightDirection);

            GameObject leftDirection = Instantiate(leftDirectionPrefab);
            leftDirections.Add(leftDirection);

            GameObject upperDirection = Instantiate(upperDirectionPrefab);
            upperDirections.Add(upperDirection);

            GameObject downDirection = Instantiate(downDirectionPrefab);
            downDirections.Add(downDirection);

            // 풀링한 오브젝트 비활성화
            rightDirections[i].SetActive(false);
            leftDirections[i].SetActive(false);
            upperDirections[i].SetActive(false);
            downDirections[i].SetActive(false);
        }

        // direction 버튼에 이벤트 추가
        rightDirectionButton.onClick.AddListener(() => ClickRightDirectionNod());
        leftDirectionButton.onClick.AddListener(() => ClickLeftDirectionNod());
        upperDirectionButton.onClick.AddListener(() => ClickUpperDirectionNod());
        downDirectionButton.onClick.AddListener(() => ClickDownDirectionNod());

        // delete 버튼에 이벤트 추가
        deleteButton.onClick.AddListener(() => DeleteMoveAction());
        deleteButton.onClick.AddListener(() => DeleteDirectionNod());

    }

    private void Update()
    {
        if (usedCount == pullingCount)
            isFullDirection = true;
        else
            isFullDirection = false;
    }

    // 화살표 버튼에 추가
    public void ClickRightDirectionNod()
    {
        CreateNodInHolder(rightDirections[useCount]);
    }

    public void ClickLeftDirectionNod()
    {
        CreateNodInHolder(leftDirections[useCount]);
    }

    public void ClickUpperDirectionNod()
    {
        CreateNodInHolder(upperDirections[useCount]);
    }

    public void ClickDownDirectionNod()
    {
        CreateNodInHolder(downDirections[useCount]);
    }

    private void CreateNodInHolder(GameObject direction)
    {
        if(useCount < pullingCount)
        {
            direction.SetActive(true);
            direction.transform.parent = directionNodHolder[useCount].transform;                // 노드 홀더의 자식으로 생성
            direction.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);       // 생성된 노드를 홀더 중앙에 고정
            useCount++;

            if (directionNodHolder.Count > pullingCount)
            {
                Debug.Log("화살표 노드 FULL");
            }
        }
    }

    public void ResetUseCount()     // 어디에 쓴지 기억안나서,, 그냥 사용
    {
        useCount = 0;
    }

    private void DeleteMoveAction()
    {
        // 최상단 이동 액션 스택 삭제
        lastIndex_MoveAction = carMoveController.moveActions.Count - 1;
        carMoveController.moveActions.RemoveAt(lastIndex_MoveAction);
    }

    private void DeleteDirectionNod()
    {
        // 최상단 화살표 노드 UI 이미지 비활성화

        Debug.Log(useCount);
        Debug.Log(usedCount);

        GameObject deletedDirectionNod = directionNodHolder[useCount-1];


        if (useCount > usedCount)
        {
            for (int i = 0; i < deletedDirectionNod.transform.childCount; i++)
            {
                deletedDirectionNod.transform.GetChild(i).gameObject.SetActive(false);
            }

            useCount--;
        }
        else
        {
            Debug.Log("비활성 가능 노드 비활성화함");
            return;
        }
    }

    public void ResetNod()
    {
        Debug.Log("화살표 노드 초기화");
        ResetUseCount();
        usedCount = 0;

        for (int i = 0; i < directionNodHolder.Count; i++)      // 화살표 홀더에 활성화되어 있는 자식 풀링오브젝트(화살표 노드) 비활성화
        {
            for (int j = 0; j < directionNodHolder[i].transform.childCount; j++)
            {
                directionNodHolder[i].transform.GetChild(j).gameObject.SetActive(false);
            }
        }
        
    }
}
