using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DoorData_0", menuName = "My Assets/Door Fuse")]
public class DoorFuseData : ScriptableObject
{
    public bool have_eletricity;

    public string door_room;

    public int num_entrada;

    public Sprite box_fuse_image;

    public Color cor_entrada;
}
