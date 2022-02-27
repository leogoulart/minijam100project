using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DoorBehaviour : MonoBehaviour
{
    public BoxFuse box_match;
    public DoorFuseData door_fuse;

    public bool openDoor;

    private void Awake()
    {
    }

    private void Update()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag == "Lucy" && door_fuse.have_eletricity)
        {
            Debug.Log("test");
            openDoor = true;
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.tag == "Lucy" && openDoor)
        {
            openDoor = false;
        }
    }
}
