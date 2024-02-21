using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DirectionNodController : MonoBehaviour
{
    //DirectionButtonLayout�� ��ũ��Ʈ �Ҵ���

    public GameObject rightDirectionPrefab;
    [HideInInspector] public List<GameObject> rightDirections = new List<GameObject>();       // Ǯ���� ���� ����Ʈ ����

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


    [HideInInspector]public int pullingCount;   // Ǯ���� ����� ����
    [HideInInspector]public int useCount;

    private void Start()
    {
        pullingCount = directionNodHolder.Count;

        for (int i = 0; i < pullingCount; i++)
        {
            if (i == pullingCount)
            {
                // ��� Ȧ�� ���� ��ŭ �����Ǹ� ���� ����(���� ��� ���� ��� ��� ����)
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

            // Ǯ���� ������Ʈ ��Ȱ��ȭ
            rightDirections[i].SetActive(false);
            leftDirections[i].SetActive(false);
            upperDirections[i].SetActive(false);
            downDirections[i].SetActive(false);
        }

        // direction ��ư�� �̺�Ʈ �߰�
        rightDirectionButton.onClick.AddListener(() => ClickRightDirectionNod());
        leftDirectionButton.onClick.AddListener(() => ClickLeftDirectionNod());
        upperDirectionButton.onClick.AddListener(() => ClickUpperDirectionNod());
        downDirectionButton.onClick.AddListener(() => ClickDownDirectionNod());

        // delete ��ư�� �̺�Ʈ �߰�
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

    // ȭ��ǥ ��ư�� �߰�
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
            direction.transform.parent = directionNodHolder[useCount].transform;                // ��� Ȧ���� �ڽ����� ����
            direction.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);       // ������ ��带 Ȧ�� �߾ӿ� ����
            useCount++;

            if (directionNodHolder.Count > pullingCount)
            {
                Debug.Log("ȭ��ǥ ��� FULL");
            }
        }
    }

    public void ResetUseCount()     // ��� ���� ���ȳ���,, �׳� ���
    {
        useCount = 0;
    }

    private void DeleteMoveAction()
    {
        // �ֻ�� �̵� �׼� ���� ����
        lastIndex_MoveAction = carMoveController.moveActions.Count - 1;
        carMoveController.moveActions.RemoveAt(lastIndex_MoveAction);
    }

    private void DeleteDirectionNod()
    {
        // �ֻ�� ȭ��ǥ ��� UI �̹��� ��Ȱ��ȭ

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
            Debug.Log("��Ȱ�� ���� ��� ��Ȱ��ȭ��");
            return;
        }
    }

    public void ResetNod()
    {
        Debug.Log("ȭ��ǥ ��� �ʱ�ȭ");
        ResetUseCount();
        usedCount = 0;

        for (int i = 0; i < directionNodHolder.Count; i++)      // ȭ��ǥ Ȧ���� Ȱ��ȭ�Ǿ� �ִ� �ڽ� Ǯ��������Ʈ(ȭ��ǥ ���) ��Ȱ��ȭ
        {
            for (int j = 0; j < directionNodHolder[i].transform.childCount; j++)
            {
                directionNodHolder[i].transform.GetChild(j).gameObject.SetActive(false);
            }
        }
        
    }
}
