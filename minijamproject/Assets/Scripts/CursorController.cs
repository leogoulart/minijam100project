using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : MonoBehaviour
{
    private Camera mainCamera;

    public delegate void FreezePlayerMovementsHandler();
    public event FreezePlayerMovementsHandler FreezeMovements;
    public delegate void OpenCloseBoxFuseHandler(BoxFuse box);
    public event OpenCloseBoxFuseHandler OpenCloseBoxFuse;
    public delegate void DisjuntorInteractionHandler(string name);
    public event DisjuntorInteractionHandler DisjuntorInteraction;

    public delegate void OpenBoxFuseHandler(BoxFuse boxFuse);
    public event OpenBoxFuseHandler OpenBoxFuse;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            DetecteObject();
    }

    private void DetecteObject()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit2d = Physics2D.GetRayIntersection(ray);
        if (hit2d.collider != null)
        {
            if(hit2d.transform.tag == "BoxFus")
            {
                OpenBoxFuse?.Invoke(hit2d.transform.gameObject.GetComponent<BoxFuse>());
            }
        }
    }
}
