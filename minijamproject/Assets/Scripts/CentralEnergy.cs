using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CentralEnergy : MonoBehaviour
{
    private bool switchChanged;

    private List<BoxFuse> allBoxes;
    [SerializeField]
    private List<BoxFuse> currentRoomBoxes;

    private List<DoorBehaviour> allDoors;
    private List<DoorBehaviour> currentRoomDoors;

    public string currentPlayerRoom = "Test1";

    public string[] rooms_name =
    {
        "Armazem",
        "Laboratorio",
        "Celas",
        "Sala de Seguranca"
    };

    private void Awake()
    {
        allBoxes = FindObjectsOfType<BoxFuse>().ToList();
        allDoors = FindObjectsOfType<DoorBehaviour>().ToList();
        currentRoomDoors = new List<DoorBehaviour>();
        currentRoomBoxes = new List<BoxFuse>();

        ChangeBoxesMatch();
        //RandomizeNumberAndColor();
    }

    private void Update()
    {
        if(switchChanged)
            EnergyDistribuition();
    }

    public void ControlFuseBoxes(BoxFuse activedBox, int num_saida, string dis_name)
    {
        currentRoomBoxes.Clear();
        currentRoomDoors.Clear();

        foreach (DoorBehaviour doors in allDoors)
        {
            if(doors.door_fuse.door_room == currentPlayerRoom)
            {
                currentRoomDoors.Add(doors);
            }
        }

        foreach (BoxFuse boxes in allBoxes)
        {
            if(boxes.fuseData.boxRoom == currentPlayerRoom)
            {
                currentRoomBoxes.Add(boxes);
            }
        }

        foreach(BoxFuse boxes in currentRoomBoxes)
        {
            if(activedBox != boxes)
            {
                if(boxes.fuseData.cor_entrada == activedBox.fuseData.cor_saida && boxes.fuseData.num_entrada == num_saida)
                {
                    if (activedBox.fuseData.switchOnOFF[dis_name] && activedBox.fuseData.haveEletrecit)
                    {
                        boxes.fuseData.haveEletrecit = true;
                    }
                    else
                    {
                        boxes.fuseData.haveEletrecit = false;
                    }
                }
            }
        }

        foreach (DoorBehaviour doors in currentRoomDoors)
        {
            if(doors.door_fuse.cor_entrada == activedBox.fuseData.cor_saida && doors.door_fuse.num_entrada == num_saida)
            {
                if(activedBox.fuseData.switchOnOFF[dis_name] && activedBox.fuseData.haveEletrecit)
                {
                    doors.door_fuse.have_eletricity = true;
                }
                else
                {
                    doors.door_fuse.have_eletricity = false;
                }
            }
        }

        switchChanged = true;
    }

    private void EnergyDistribuition()
    {
        /*foreach(DoorBehaviour doors in allDoors) //verificação se as portas estao recebendo energia
        {
            if (doors.box_match.fuseData.haveEletrecit)//se a box_match da porta atual tem energia ele continua
            {
                foreach(KeyValuePair<string, int> numbers in doors.box_match.fuseData.switchNumber)//foreach pelos dados do dictionary de numeros da box_match
                {
                    if(numbers.Value == doors.door_fuse.num_entrada)//procura o numero de saida igual ao numero de entrada da porta
                    {
                        if (doors.box_match.fuseData.switchOnOFF[numbers.Key])//verifica se o disjuntor do numero de saida igual ao numero de entrada da porta esta ligada
                        {
                            doors.door_fuse.have_eletricity = true;
                        }
                        else
                        {
                            doors.door_fuse.have_eletricity = false;
                        }
                    }
                }
            }
            else//se for false ele desativa a eletrecidade da porta e volta a percorrer o loop de portas
            {
                doors.door_fuse.have_eletricity = false;
            }
        }*/

        foreach(BoxFuse boxes in allBoxes)
        {
            if(boxes.box_match != null)
            {
                if (boxes.box_match.fuseData.haveEletrecit)
                {
                    foreach(KeyValuePair<string, int> numbers in boxes.box_match.fuseData.switchNumber)
                    {
                        if(numbers.Value == boxes.fuseData.num_entrada)
                        {
                            if (boxes.box_match.fuseData.switchOnOFF[numbers.Key])
                                boxes.fuseData.haveEletrecit = true;
                            else
                                boxes.fuseData.haveEletrecit = false;
                        }
                    }
                }
                else
                {
                    boxes.fuseData.haveEletrecit = false;
                }
            }
        }
        switchChanged = false;
    }

    private void RandomizeNumberAndColor()
    {
        List<BoxFuse> UnitCentralBoxes = new List<BoxFuse>();
        currentRoomBoxes.Clear();
        currentRoomDoors.Clear();

        foreach (BoxFuse unitBoxes in allBoxes)//percorre todas as boxes do jogo e adiciona na lista somente as com unitcentralchosse = true
        {
            if (unitBoxes.UnitCentralChosse)
            {
                UnitCentralBoxes.Add(unitBoxes);
                unitBoxes.fuseData.switchNumber.Clear();
            }
        }

        foreach (BoxFuse unitBoxes in UnitCentralBoxes)//percorre a lista de boxes escolhidas pela unidade central e preenche os dados necessarios
        {
            unitBoxes.fuseData.num_entrada = 0;
            for (int i = 1; i <= 3; i++)
            {
                var dis_name = "Disjuntor_0" + i;
                unitBoxes.fuseData.switchNumber.Add(dis_name, Random.Range(1, 99));
            }
            unitBoxes.fuseData.cor_entrada = new Color(Random.Range(0.0f,1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), 1.0f);
            unitBoxes.fuseData.cor_saida = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), 1.0f);
        }

        foreach(BoxFuse unitBoxes in UnitCentralBoxes)//percorre a lista de boxes selecionadas pela unidade central
        {
            foreach(BoxFuse room_boxes in allBoxes)//percorre todas a boxes do jogo
            {
                if (room_boxes != unitBoxes)
                {
                    if (room_boxes.fuseData.boxRoom == unitBoxes.fuseData.boxRoom)
                    {
                        currentRoomBoxes.Add(room_boxes);//adiciona na lista currentboxesroom somente as boxes que tenham o mesmo valor da boxroom da unidade central atual
                    }
                }
            }

            foreach (BoxFuse currentBoxes in currentRoomBoxes)
            {

            }
        }


       /* foreach (BoxFuse boxes in allBoxes)
        {
            
            boxes.fuseData.cor_entrada = Color.clear;
            boxes.fuseData.cor_saida = Color.clear;
            

            

            foreach(BoxFuse currBoxes in allBoxes)
            {
                if(currBoxes.fuseData.boxRoom == boxes.fuseData.boxRoom)
                {
                    currentRoomBoxes.Add(boxes);
                }
            }
            foreach(BoxFuse currBoxes in currentRoomBoxes)
            {
                Debug.Log(currBoxes);
                for(int i = 1; i <= 3; i++)
                {
                    var dis_name = "Disjuntor_0" + i;
                    Debug.Log(dis_name);
                    currBoxes.fuseData.switchNumber.Add(dis_name, Random.Range(1, 99)); ;
                }
                if (currBoxes.UnitCentralChosse)
                {
                    currBoxes.fuseData.num_entrada = 0;
                }
                else
                {
                    var chossed_num = Random.Range(1, 3);
                    var dis_name = "Disjuntor_0" + chossed_num;
                    Debug.Log(dis_name);
                    Debug.Log(currBoxes.box_match.fuseData.switchNumber.Count);
                    currBoxes.fuseData.num_entrada = currBoxes.box_match.fuseData.switchNumber[dis_name];
                }
            }
        }
        */

    }

    private void ElectrictyFlux(BoxFuse unitcentralboxe)
    {

    }

    private void ChangeBoxesMatch()
    {
        currentRoomBoxes.Clear();
        currentRoomDoors.Clear();
        /*foreach(DoorBehaviour doors in allDoors)
        {
            if(doors.box_match == null)
            {
                foreach(BoxFuse allBoxes in allBoxes)
                {
                    if(allBoxes.fuseData.boxRoom == doors.door_fuse.door_room)
                    {
                        currentRoomBoxes.Add(allBoxes);
                    }
                }
                foreach(BoxFuse currBoxes in currentRoomBoxes)
                {
                    if (!currBoxes.UnitCentralChosse)
                    {

                    }
                }
            }
            currentRoomDoors.Clear();   
        }*/

        List<BoxFuse> UnitCentralBoxes = new List<BoxFuse>();
        foreach (BoxFuse unitCentral in allBoxes)
        {
            if (unitCentral.UnitCentralChosse)
            {
                UnitCentralBoxes.Add(unitCentral);
            }
        }

        foreach (BoxFuse unitBoxes in UnitCentralBoxes)//percorre a lista de boxes selecionadas pela unidade central
        {
            currentRoomBoxes.Clear();
            foreach (BoxFuse room_boxes in allBoxes)//percorre todas a boxes do jogo
            {
                if (room_boxes != unitBoxes)
                {
                    if (room_boxes.fuseData.boxRoom == unitBoxes.fuseData.boxRoom)
                    {
                        currentRoomBoxes.Add(room_boxes);//adiciona na lista currentboxesroom somente as boxes que tenham o mesmo valor da boxroom da unidade central atual
                    }
                }
            }

            var unitchoose = Random.Range(0, currentRoomBoxes.Count-1);
            currentRoomBoxes[unitchoose].box_match = unitBoxes;
            currentRoomBoxes[unitchoose].fuseData.boxesLinked.Add(unitBoxes);
            ElectrictyFlux(unitBoxes);
            /*int index = 0;

            while(index < currentRoomBoxes.Count)
            {
                var choose = Random.Range(0, currentRoomBoxes.Count - 1);
                if(currentRoomBoxes[index] != unitBoxes)
                {
                    if(currentRoomBoxes[choose].fuseData.boxesLinked.Count < 2)
                    {
                        for(int i = 0; i < currentRoomBoxes[choose].fuseData.boxesLinked.Count-1; i++)
                        {

                        }
                    }
                }*/
                //var choose = Random.Range(0, currentRoomBoxes.Count - 1);
                /*if(currentRoomBoxes[index] != currentRoomBoxes[choose])
                {
                    Debug.Log(currentRoomBoxes[choose]);
                    index++;
                }*/

            /*foreach (BoxFuse currentBoxes in currentRoomBoxes)
            {
                if (currentBoxes.box_match == null)
                {
                    var random_chosse = currentRoomBoxes[Random.Range(0, currentRoomBoxes.Count - 1)];
                    if(currentBoxes != random_chosse)
                    {
                        currentBoxes.box_match = random_chosse;
                    }
                    else
                    {
                        currentBoxes.box_match = currentRoomBoxes[Random.Range(0, currentRoomBoxes.Count - 1)];
                    }
                }
            }*/
        }
    }
}
