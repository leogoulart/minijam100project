using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public PlayerController player;
    public CursorController cursorController;
    public InterfaceManager interfaceManager;
    public CentralEnergy centralEnergy;

    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        cursorController = FindObjectOfType<CursorController>();
        interfaceManager = FindObjectOfType<InterfaceManager>();
        centralEnergy = FindObjectOfType<CentralEnergy>();

        cursorController.OpenBoxFuse += interfaceManager.OpenBoxFuse;
        interfaceManager.ControlBoxes += centralEnergy.ControlFuseBoxes;
    }
}
