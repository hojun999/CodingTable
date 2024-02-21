using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneConverter : MonoBehaviour
{
    public void LoadShortestWayScene()
    {
        SceneManager.LoadScene("ShortestWay");
    }
}
