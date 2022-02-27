using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BoxFuse : MonoBehaviour
{
    public bool UnitCentralChosse = false;

    public BoxFuse box_match;

    public BoxFuseData fuseData;
    
    public Sprite boxFuseOn;
    public Sprite boxFuseOff;
    public Sprite switchSpriteOn;
    public Sprite switchSpriteOff;

    private void Awake()
    {
        fuseData.switchOnOFF = new Dictionary<string, bool>();
        fuseData.switchNumber = new Dictionary<string, int>();
        fuseData.switchImage = new Dictionary<string, Sprite>();

        fuseData.switchOnOFF.Add("Disjuntor_01", false);
        fuseData.switchOnOFF.Add("Disjuntor_02", false);
        fuseData.switchOnOFF.Add("Disjuntor_03", false);

        fuseData.switchNumber.Clear();
        /*fuseData.switchNumber.Add("Disjuntor_01", 1);
        fuseData.switchNumber.Add("Disjuntor_02", 2);
        fuseData.switchNumber.Add("Disjuntor_03", 3);*/

        fuseData.switchImage.Add("Disjuntor_01", switchSpriteOff);
        fuseData.switchImage.Add("Disjuntor_02", switchSpriteOff);
        fuseData.switchImage.Add("Disjuntor_03", switchSpriteOff);
    }

    private void Update()
    {
        if (fuseData.haveEletrecit)
            fuseData.boxFuseSprite = boxFuseOn;
        else
            fuseData.boxFuseSprite = boxFuseOff;
    }
}
