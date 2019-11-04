using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stars : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector3 originalPos;
    public Vector3 originalEng;
    public bool flag = false;
 
    // Update is called once per frame
    void Update()
    {
        if (flag == true)
        {

            int boardNumber = GetClosestItem();
            print(boardNumber);
            if (boardNumber != 999)
            {
                PutStar(boardNumber);
                UpdataGameInfo(boardNumber);
            }
            else
            {
                this.transform.position = originalPos;
                this.transform.eulerAngles = new Vector3(-90, 90, 0);
                flag = false;
            }
        }
    }
    void PutStar(int boardNumber)
    {
        float new_x = this.transform.parent.parent.GetComponent<GameInfo>().boardObjsPos[boardNumber].x;
        float new_y = this.transform.parent.parent.GetComponent<GameInfo>().boardObjsPos[boardNumber].y + 0.008f; //+ this.transform.lossyScale.y;
        float new_z = this.transform.parent.parent.GetComponent<GameInfo>().boardObjsPos[boardNumber].z;
        this.transform.position = new Vector3(new_x, new_y, new_z);
        this.transform.eulerAngles = new Vector3(-90, 90, 0);
        this.transform.parent.parent.GetComponent<GameInfo>().boardObjsPos[boardNumber].y = new_y;
    }

    private void UpdataGameInfo(int boardNumber)
    {
        this.transform.parent.parent.gameObject.GetComponent<GameInfo>().itemLayers[boardNumber] += 1;
        this.GetComponent<Microsoft.MixedReality.Toolkit.UI.ManipulationHandler>().enabled = false;
        this.GetComponent<Microsoft.MixedReality.Toolkit.Input.NearInteractionGrabbable>().enabled = false;
        this.GetComponent<Stars>().enabled = false;
        CreateScoreItem();
        this.transform.parent.parent.GetComponent<GameInfo>().scoreItemPos_y[1] -= (0.07f / 4);
        this.transform.parent.parent.GetComponent<GameInfo>().NewBoardItem(boardNumber, this.gameObject);
    }
    public void SetTrue()
    {
        this.flag = true;
    }
    int GetClosestItem()
    {
        for (int i = 0; i < 9; i++)
        {
            if (Vector2.Distance(new Vector2(this.transform.position.x, this.transform.position.z),
                new Vector2(this.transform.parent.parent.Find("Board").GetChild(i).position.x, this.transform.parent.parent.Find("Board").GetChild(i).position.z))
                < 0.1 / 2)
            {

                return i;

            }
        }
        return 999;
    }
    
    void CreateScoreItem()
    {
        GameObject parent_1 = this.transform.parent.parent.GetComponent<GameInfo>().boardObjs[GetClosestItem()];
        if (parent_1.name.Equals("0"))
        {
            this.transform.parent.parent.Find("StarCards").gameObject.SetActive(true);
            this.transform.parent.parent.Find("StarCards").Find("Row1").gameObject.SetActive(true);
            this.transform.parent.parent.GetComponent<GameInfo>().scoreItemPos_y[0] -= (0.07f / 4);
        }
        else if (parent_1.name.Equals("7"))
        {
            if (this.transform.parent.parent.GetComponent<GameInfo>().LockersNumber.Count != 0)
            {
                int LockNumber = this.transform.parent.parent.GetComponent<GameInfo>().LockersNumber.Dequeue();
                this.transform.parent.parent.Find("Locks").GetChild(LockNumber).gameObject.SetActive(true);
                this.transform.parent.parent.GetComponent<GameInfo>().scoreItemPos_y[0] -= (0.07f / 4);
            }
        }
        else if (parent_1.name.Equals("6"))
        {
            this.transform.parent.parent.Find("ExchangeCards").GetComponent<ExchangeCardsInfo>().maxRow = 1;
            this.transform.parent.parent.Find("ExchangeCards").gameObject.SetActive(true);
            this.transform.parent.parent.GetComponent<GameInfo>().scoreItemPos_y[0] -= (0.07f / 4);
        }
        else
        {
            CreateScoreCard(parent_1);
        }

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
            this.transform.parent.parent.GetComponent<GameInfo>().scoreItemPos_y[0] -= (0.07f / 4);
        }
        else if (parent.name.Equals("2"))
        {
            GameObject scoredItem = Instantiate(parent, parent.transform.position, Quaternion.identity);
            scoredItem.name = parent.name;
            scoredItem.transform.eulerAngles = new Vector3(-90, 90, 0);
            scoredItem.transform.parent = this.transform.parent.parent.Find("Scores").Find("Item1");
            this.transform.parent.parent.GetComponent<GameInfo>().item_1Score += 1;
            scoredItem.AddComponent<Moving>();
            this.transform.parent.parent.GetComponent<GameInfo>().scoreItemPos_y[0] -= (0.07f / 4);
        }
        else if (parent.name.Equals("3"))
        {
            GameObject scoredItem = Instantiate(parent, parent.transform.position, Quaternion.identity);
            scoredItem.name = parent.name;
            scoredItem.transform.eulerAngles = new Vector3(-90, 90, 0);
            scoredItem.transform.parent = this.transform.parent.parent.Find("Scores").Find("Item2");
            this.transform.parent.parent.GetComponent<GameInfo>().item_2Score += 1;
            scoredItem.AddComponent<Moving>();
            this.transform.parent.parent.GetComponent<GameInfo>().scoreItemPos_y[0] -= (0.07f / 4);
        }
        else if (parent.name.Equals("4"))
        {
            GameObject scoredItem = Instantiate(parent, parent.transform.position, Quaternion.identity);
            scoredItem.name = parent.name;
            scoredItem.transform.eulerAngles = new Vector3(-90, 90, 0);
            scoredItem.transform.parent = this.transform.parent.parent.Find("Scores").Find("Item3");
            this.transform.parent.parent.GetComponent<GameInfo>().item_3Score += 1;
            scoredItem.AddComponent<Moving>();
            this.transform.parent.parent.GetComponent<GameInfo>().scoreItemPos_y[0] -= (0.07f / 4);
        }
        else if (parent.name.Equals("5"))
        {
            GameObject scoredItem = Instantiate(parent, parent.transform.position, Quaternion.identity);
            scoredItem.name = parent.name;
            scoredItem.transform.eulerAngles = new Vector3(-90, 90, 0);
            scoredItem.transform.parent = this.transform.parent.parent.Find("Scores").Find("Item4");
            this.transform.parent.parent.GetComponent<GameInfo>().item_4Score += 1;
            scoredItem.AddComponent<Moving>();
            this.transform.parent.parent.GetComponent<GameInfo>().scoreItemPos_y[0] -= (0.07f / 4);
        }

    }
}
