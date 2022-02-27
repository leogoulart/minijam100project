using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject gameOverUI;
    public GameObject outbreakUI;

    public void BtRestartClick()
    {
        Debug.Log("Reiniciar");
    }

    public void BtMenuClick()
    {
        Application.Quit();
    }

}
