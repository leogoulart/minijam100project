using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTrigger : MonoBehaviour
{
    public string room;

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.transform.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerController>().currentRoom = room;
        }
    }
}
