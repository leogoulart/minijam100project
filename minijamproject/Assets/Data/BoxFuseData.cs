using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BoxFuse_", menuName = "My Assets/Box Fuse")]
public class BoxFuseData : ScriptableObject
{
    public bool haveEletrecit;

    public string boxRoom;

    public Sprite boxFuseSprite;

    public Dictionary<string, bool> switchOnOFF;
    public Dictionary<string, int> switchNumber;
    public Dictionary<string, Sprite> switchImage;

    public List<BoxFuse> boxesLinked;

    public int num_entrada;

    public Color cor_entrada;
    public Color cor_saida;
}
