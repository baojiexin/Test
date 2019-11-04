using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cardItems : MonoBehaviour
{
    Transform item_1;
    Transform item_2;
    Transform boards;
    public bool flag;
    private Vector3 originalPosition;
    public int locationNum;
    int movSpeed = 8;
    public Vector3 destination;
    public bool destinationSet = false;
    bool isReady = false;
   

    // Start is called before the first frame update
    void Start()
    {
        this.item_1 = this.transform.GetChild(1);
        this.item_2 = this.transform.GetChild(2);
        this.boards = this.transform.parent.parent.Find("Board");
        originalPosition = this.transform.parent.parent.GetComponent<GameInfo>().Locations[this.locationNum];
        
       
        
    }

    // Update is called once per frame
    void Update()
    {
        if(this.destinationSet == true)
        {
            if(this.isReady == false)
            {
                if(this.transform.position != this.destination)
                {
                    MoveCard();
                }
                else
                {
                    this.isReady = true;
                }
            }
            else
            {
                if(flag == true)
                {
                    
                    if ((GetClosestItem(this.item_1) != 999) && (GetClosestItem(this.item_2) != 999))
                    {
                        int boardItem_1Index = GetClosestItem(this.item_1);
                        int boardItem_2Index = GetClosestItem(this.item_2);

                        //print("一开始");
                        //print("第一个 "+ this.transform.parent.parent.GetComponent<GameInfo>().boardObjs[boardItem_1Index].name);
                        //print("第二个" + this.transform.parent.parent.GetComponent<GameInfo>().boardObjs[boardItem_2Index].name);

                        int Item_1Layer = this.transform.parent.parent.gameObject.GetComponent<GameInfo>().itemLayers[boardItem_1Index];
                        int Item_2Layer = this.transform.parent.parent.gameObject.GetComponent<GameInfo>().itemLayers[boardItem_2Index];
                        if (Item_1Layer == Item_2Layer && flag == true)
                        {
                            //print("第二个判断");
                            if (System.Math.Abs(boardItem_1Index - boardItem_2Index) == 1)
                            {
                                //print("chenggong");
                                PutCardVertical(boardItem_1Index, boardItem_2Index);
                                UpdataGameInfo(boardItem_1Index, boardItem_2Index);
                            }
                            else if (System.Math.Abs(boardItem_1Index - boardItem_2Index) == 3)
                            {
                                //print("chenggong");
                                PutCardHorizontal(boardItem_1Index, boardItem_2Index);
                                UpdataGameInfo(boardItem_1Index, boardItem_2Index);
                            }
                            else
                            {
                                flag = false;
                                this.transform.position = originalPosition;
                                this.transform.eulerAngles = new Vector3(0, 0, 0);
                            }
                        }
                        else
                        {
                            flag = false;
                            this.transform.position = originalPosition;
                            this.transform.eulerAngles = new Vector3(0, 0, 0);
                        }
                    }
                    else
                    {
                        flag = false;
                        this.transform.position = originalPosition;
                        this.transform.eulerAngles = new Vector3(0, 0, 0);
                    }
                }
                
                
            }
        }
       
        
    }
    private void PutCardVertical(int boardItem_1Index, int boardItem_2Index)
    {
        float new_x = ((this.transform.parent.parent.GetComponent<GameInfo>().boardObjsPos[boardItem_1Index].x) +
                                (this.transform.parent.parent.GetComponent<GameInfo>().boardObjsPos[boardItem_2Index].x)) / 2;
        float new_y = ((this.transform.parent.parent.GetComponent<GameInfo>().boardObjsPos[boardItem_1Index].y) +
            0.008f);
        float new_z = (this.transform.parent.parent.GetComponent<GameInfo>().boardObjsPos[boardItem_1Index].z);
        this.transform.position = new Vector3(new_x, new_y, new_z);
        
        this.transform.parent.parent.GetComponent<GameInfo>().boardObjsPos[boardItem_1Index].y = new_y;
        this.transform.parent.parent.GetComponent<GameInfo>().boardObjsPos[boardItem_2Index].y = new_y;

        this.transform.eulerAngles = new Vector3(0, 90, 0);
    }
    private void PutCardHorizontal(int boardItem_1Index, int boardItem_2Index)
    {
        float new_z = ((this.transform.parent.parent.GetComponent<GameInfo>().boardObjsPos[boardItem_1Index].z) +
                               (this.transform.parent.parent.GetComponent<GameInfo>().boardObjsPos[boardItem_2Index].z)) / 2;
        float new_y = ((this.transform.parent.parent.GetComponent<GameInfo>().boardObjsPos[boardItem_1Index].y) +
            0.008f);
        float new_x = (this.transform.parent.parent.GetComponent<GameInfo>().boardObjsPos[boardItem_1Index].x);
        this.transform.parent.parent.GetComponent<GameInfo>().boardObjsPos[boardItem_1Index].y = new_y;
        this.transform.parent.parent.GetComponent<GameInfo>().boardObjsPos[boardItem_2Index].y = new_y;
        this.transform.position = new Vector3(new_x, new_y, new_z);
        //print(this.transform.position);
        this.transform.eulerAngles = new Vector3(0, 0, 0);
    }
    private void UpdataGameInfo(int boardItem_1Index, int boardItem_2Index)
    {
        this.transform.parent.parent.gameObject.GetComponent<GameInfo>().itemLayers[boardItem_1Index] += 1;
        this.transform.parent.parent.gameObject.GetComponent<GameInfo>().itemLayers[boardItem_2Index] += 1;
        this.GetComponent<Microsoft.MixedReality.Toolkit.UI.ManipulationHandler>().enabled = false;
        this.GetComponent<Microsoft.MixedReality.Toolkit.Input.NearInteractionGrabbable>().enabled = false;
        this.GetComponent<cardItems>().enabled = false;
        this.transform.parent.parent.GetComponent<GameInfo>().cardsLocationInfo[this.locationNum] = 0;
        CreateScoreItems();
        this.transform.parent.parent.GetComponent<GameInfo>().NewBoardItem(boardItem_1Index, item_1.gameObject);
        this.transform.parent.parent.GetComponent<GameInfo>().NewBoardItem(boardItem_2Index, item_2.gameObject);
        //print("更新后");
        print(this.transform.parent.parent.GetComponent<GameInfo>().boardObjs[boardItem_1Index].name);
        print(this.transform.parent.parent.GetComponent<GameInfo>().boardObjs[boardItem_2Index].name);
    }
    
    public void SetFlagFalse()
    {
        this.flag = false;
    }
    public void SetFlagTrue()
    {
        this.flag = true;
    }
    private int GetClosestItem(Transform obj)
    {
        for(int i = 0; i < 9; i++)
        {
            float board_x = boards.GetChild(i).position.x;
            float board_z = boards.GetChild(i).position.z;
            var distance = Vector2.Distance(new Vector2(board_x, board_z), new Vector2(obj.position.x, obj.position.z));
            if (distance < 0.02)
            {
                return i;
            }

            //if(distance > System.Math.Sqrt(System.Math.Abs(obj.position.x - board_x) * System.Math.Abs(obj.position.x - board_x)
            //    + System.Math.Abs(obj.position.z - board_z) * System.Math.Abs(obj.position.z - board_z)))
            //{
            //    distance = System.Math.Sqrt(System.Math.Abs(obj.position.x - board_x) * System.Math.Abs(obj.position.x - board_x)
            //    + System.Math.Abs(obj.position.z - board_z) * System.Math.Abs(obj.position.z - board_z));
            //    if (distance <= 0.1)
            //    {
            //        return i;
            //    }
            //}
        }
        return 999;
    }
    private void CreateScoreItems()
    {
        GameObject parent_1 = this.transform.parent.parent.GetComponent<GameInfo>().boardObjs[GetClosestItem(this.item_1)];
        GameObject parent_2 = this.transform.parent.parent.GetComponent<GameInfo>().boardObjs[GetClosestItem(this.item_2)];
        int starCardNum = 0;
        int exchangeCardNum = 0;
        if(parent_1.name == "0" || parent_1.name == "7" || parent_1.name == "6" ||
            parent_2.name == "0" || parent_2.name == "7" || parent_2.name == "6")
        {
            if (parent_1.name == "0" || parent_2.name == "0")
            {
                if (parent_1.name == "0")
                {
                    starCardNum += 1;

                }
                if (parent_2.name == "0")
                {
                    starCardNum += 1;

                }
                if (starCardNum > 0)
                {
                    this.transform.parent.parent.Find("StarCards").gameObject.SetActive(true);
                    if (starCardNum == 1)
                    {
                        this.transform.parent.parent.Find("StarCards").Find("Row1").gameObject.SetActive(true);
                        if (parent_1.name == "0")
                        {
                            CreateScoreCard(parent_2);
                        }
                        else
                        {
                            CreateScoreCard(parent_1);
                        }


                    }
                    else if (starCardNum == 2)
                    {
                        this.transform.parent.parent.Find("StarCards").Find("Row1").gameObject.SetActive(true);
                        this.transform.parent.parent.Find("StarCards").Find("Row2").gameObject.SetActive(true);
                        this.transform.parent.parent.Find("StarCards").Find("Row3").gameObject.SetActive(true);

                    }
                }
            }
            if (parent_1.name == "7" || parent_2.name == "7")
            {
                if (parent_1.name == "7")
                {
                    if (this.transform.parent.parent.GetComponent<GameInfo>().LockersNumber.Count != 0)
                    {
                        int LockNumber = this.transform.parent.parent.GetComponent<GameInfo>().LockersNumber.Dequeue();
                        this.transform.parent.parent.Find("Locks").GetChild(LockNumber).gameObject.SetActive(true);
                    }
                    CreateScoreCard(parent_2);
                }
                if (parent_2.name == "7")
                {
                    if (this.transform.parent.parent.GetComponent<GameInfo>().LockersNumber.Count != 0)
                    {
                        int LockNumber = this.transform.parent.parent.GetComponent<GameInfo>().LockersNumber.Dequeue();
                        this.transform.parent.parent.Find("Locks").GetChild(LockNumber).gameObject.SetActive(true);
                    }
                    CreateScoreCard(parent_1);
                }
            }
            if (parent_1.name == "6" || parent_2.name == "6")
            {
                if (parent_1.name == "6")
                {
                    exchangeCardNum += 1;

                }
                if (parent_2.name == "6")
                {
                    exchangeCardNum += 1;

                }
                if (exchangeCardNum > 0)
                {
                    if (exchangeCardNum == 1)
                    {
                        this.transform.parent.parent.Find("ExchangeCards").GetComponent<ExchangeCardsInfo>().maxRow = 1;
                        if (parent_1.name == "6")
                        {
                            CreateScoreCard(parent_2);
                            if (parent_2.name.Equals("1"))
                            {
                                this.transform.parent.parent.Find("ExchangeCards").GetComponent<ExchangeCardsInfo>().maxRow = 0;
                            }
                            this.transform.parent.parent.Find("ExchangeCards").gameObject.SetActive(true);

                        }
                        else
                        {
                            CreateScoreCard(parent_1);
                            if (parent_1.name.Equals("1"))
                            {
                                this.transform.parent.parent.Find("ExchangeCards").GetComponent<ExchangeCardsInfo>().maxRow = 0;
                            }
                            this.transform.parent.parent.Find("ExchangeCards").gameObject.SetActive(true);

                        }


                    }
                    else if (exchangeCardNum == 2)
                    {

                        if ((this.transform.parent.parent.GetComponent<GameInfo>().item_1Score + this.transform.parent.parent.GetComponent<GameInfo>().item_2Score +
                             this.transform.parent.parent.GetComponent<GameInfo>().item_3Score + this.transform.parent.parent.GetComponent<GameInfo>().item_4Score) >= 1)
                        {
                            this.transform.parent.parent.Find("ExchangeCards").gameObject.SetActive(true);
                            this.transform.parent.parent.Find("ExchangeCards").GetComponent<ExchangeCardsInfo>().maxRow = 3;

                        }


                    }
                }
            }
        }
      
        else
        {
            if (parent_1.name.Equals(parent_2.name))
            {
                CreateScoreCard(parent_1);
                CreateScoreCard(parent_1);
                CreateScoreCard(parent_2);
            }
            else
            {
                CreateScoreCard(parent_1);
                CreateScoreCard(parent_2);
            }
        }


    }
    private void MoveCard()
    {
        this.transform.position = Vector3.MoveTowards(this.transform.position, this.destination, movSpeed * Time.deltaTime);
    }
    void CreateScoreCard(GameObject parent)
    {
        
        if (parent.name.Equals("1"))
        {
            GameObject scoredItem = Instantiate(this.transform.parent.parent.Find("Items").Find("STAIRWAY").gameObject, parent.transform.position, Quaternion.identity);
            scoredItem.name = "STAIRWAY";
            scoredItem.transform.eulerAngles = new Vector3(-90, 90, 0);
            scoredItem.transform.parent = this.transform.parent.parent.Find("Functions");
            scoredItem.GetComponent<Stair>().enabled = true;
            scoredItem.AddComponent<Moving>();

        }
        else if (parent.name.Equals("2"))
        {
            GameObject scoredItem = Instantiate(parent, parent.transform.position, Quaternion.identity);
            scoredItem.name = parent.name;
            scoredItem.transform.eulerAngles = new Vector3(-90, 90, 0);
            scoredItem.transform.parent = this.transform.parent.parent.Find("Scores").Find("Item1");
            this.transform.parent.parent.GetComponent<GameInfo>().item_1Score += 1;
            scoredItem.AddComponent<Moving>();
        }
        else if (parent.name.Equals("3"))
        {
            GameObject scoredItem = Instantiate(parent, parent.transform.position, Quaternion.identity);
            scoredItem.name = parent.name;
            scoredItem.transform.eulerAngles = new Vector3(-90, 90, 0);
            scoredItem.transform.parent = this.transform.parent.parent.Find("Scores").Find("Item2");
            this.transform.parent.parent.GetComponent<GameInfo>().item_2Score += 1;
            scoredItem.AddComponent<Moving>();
        }
        else if (parent.name.Equals("4"))
        {
            GameObject scoredItem = Instantiate(parent, parent.transform.position, Quaternion.identity);
            scoredItem.name = parent.name;
            scoredItem.transform.eulerAngles = new Vector3(-90, 90, 0);
            scoredItem.transform.parent = this.transform.parent.parent.Find("Scores").Find("Item3");
            this.transform.parent.parent.GetComponent<GameInfo>().item_3Score += 1;
            scoredItem.AddComponent<Moving>();
        }
        else if (parent.name.Equals("5"))
        {
            GameObject scoredItem = Instantiate(parent, parent.transform.position, Quaternion.identity);
            scoredItem.name = parent.name;
            scoredItem.transform.eulerAngles = new Vector3(-90, 90, 0);
            scoredItem.transform.parent = this.transform.parent.parent.Find("Scores").Find("Item4");
            this.transform.parent.parent.GetComponent<GameInfo>().item_4Score += 1;
            scoredItem.AddComponent<Moving>();
        }
     
    }
    
   
}
