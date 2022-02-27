using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InterfaceManager : MonoBehaviour
{
    public GameObject panelBoxFuse;
    private BoxFuse boxFuse;
    private BoxFuseData boxData;
    
    private bool openClose = false;

    public Image boxFuseImage;
    public Image DisjuntorSprite_01;
    public Image DisjuntorSprite_02;
    public Image DisjuntorSprite_03;

    public Image cor_saida;
    public Image cor_entrada;

    public Text DisjuntorNumber_01;
    public Text DisjuntorNumber_02;
    public Text DisjuntorNumber_03;

    public Text DisjuntorNumber_INN;

    public delegate void BoxFuseINFOCENTRAL(BoxFuse box, bool disActive, string disName, int disNumber, Color outColor);
    public event BoxFuseINFOCENTRAL InfoCentral;

    public delegate void ControlBoxesHandler(BoxFuse activeBox, int dis_num, string dis_name);
    public event ControlBoxesHandler ControlBoxes;

    private void Awake()
    {
        panelBoxFuse.SetActive(false);
    }

    #region Manage BoxFuse Interface
    public void OpenBoxFuse(BoxFuse box)
    {
        openClose = true;
        panelBoxFuse.SetActive(openClose);
        boxFuse = box;
        boxData = box.fuseData;
        ChangeUIInfo();
    }

    public void DisjuntorInteract(string selectedDisjuntor)
    {
        boxData.switchOnOFF[selectedDisjuntor] = !boxData.switchOnOFF[selectedDisjuntor];
        ChangeSwitchState(selectedDisjuntor);
    }

    private void ChangeUIInfo()
    {
        boxFuseImage.sprite = boxData.boxFuseSprite;
        cor_entrada.color = boxData.cor_entrada;
        cor_saida.color = boxData.cor_saida;

        DisjuntorSprite_01.sprite = boxData.switchImage["Disjuntor_01"];
        DisjuntorSprite_02.sprite = boxData.switchImage["Disjuntor_02"];
        DisjuntorSprite_03.sprite = boxData.switchImage["Disjuntor_03"];

        DisjuntorNumber_01.text = boxData.switchNumber["Disjuntor_01"].ToString();
        DisjuntorNumber_02.text = boxData.switchNumber["Disjuntor_02"].ToString();
        DisjuntorNumber_03.text = boxData.switchNumber["Disjuntor_03"].ToString();
        DisjuntorNumber_INN.text = boxData.num_entrada.ToString();
    }

    public void CloseBoxFuse()
    {
        openClose = false;
        panelBoxFuse.SetActive(openClose);
    }

    private void ChangeSwitchState(string dis_name)
    {
        List<string> tempBox = new List<string>();
        foreach (KeyValuePair<string, bool> boxBool in boxData.switchOnOFF)
        {
            if (boxBool.Value)
                tempBox.Add(boxBool.Key);
        }

        if (tempBox.Count > 1)
        {
            for (int i = 0; i < boxData.switchOnOFF.Count; i++)
            {
                boxData.switchOnOFF["Disjuntor_0" + (i + 1)] = false;
                tempBox.Clear();
            }
        }

        foreach (KeyValuePair<string, bool> box in boxData.switchOnOFF)
        {
            if (box.Value)
            {
                boxData.switchImage[box.Key] = boxFuse.switchSpriteOn;
            }
            else
            {
                boxData.switchImage[box.Key] = boxFuse.switchSpriteOff;
            }
        }

        ControlBoxes?.Invoke(boxFuse, boxData.switchNumber[dis_name], dis_name);
        ChangeUIInfo();
    }
    #endregion
}
