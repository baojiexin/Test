using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving : MonoBehaviour
{
    
    public bool flag = false;
    bool initial = false;
    public int movSpeed = 8;
    Vector3 startPosition;
    int itemNumber;
   
    // Update is called once per frame
    private void Awake()
    {
        startPosition = GetCollectionPositon(this.transform);
        print(this.name);
        if (this.name.Equals("STAIRWAY"))
        {
            this.GetComponent<Stair>().originalPos = startPosition;
        }
        else
        {
            this.GetComponent<EXCardMoving>().originalPos = startPosition;
            this.GetComponent<EXCardMoving>().flag = true;
        }
        UpdataNextItemPos();
        
    }
    void Update()
    {
        if (flag == false)
        {
            if (this.transform.position != startPosition)
            {
                this.transform.position = Vector3.MoveTowards(this.transform.position, startPosition, movSpeed * Time.deltaTime);
                
            }
            else
            {
                flag = true;

            }
        }
        else
        {
            if (initial == false)
            {
                ActiveManiHandler(this.gameObject);
                initial = true;
                this.GetComponent<Moving>().enabled = false;
            }
        }
    }

    void UpdataNextItemPos()
    {
        if (this.itemNumber == 1)
        {
            this.transform.parent.parent.GetComponent<GameInfo>().scoreItemPos_y[this.itemNumber] += (0.07f/4);
        }
        else
        {
            this.transform.parent.parent.parent.GetComponent<GameInfo>().scoreItemPos_y[this.itemNumber] += (0.07f/4);
        }
        
    }
    private Vector3 GetCollectionPositon(Transform item)
    {
        string itemName = item.name;
        switch (itemName)
        {
            /*case "0":
                this.itemNumber = 0;
                Vector3 position_0 = new Vector3(this.transform.parent.parent.GetComponent<GameInfo>().scoreItemPos_x,
                    this.transform.parent.parent.GetComponent<GameInfo>().scoreItemPos_y[itemNumber], this.transform.parent.parent.GetComponent<GameInfo>().scoreItemPos_z[itemNumber]);
                return position_0;
            case "1":
                this.itemNumber = 1;
                Vector3 position_1 = new Vector3(this.transform.parent.parent.GetComponent<GameInfo>().scoreItemPos_x,
                    this.transform.parent.parent.GetComponent<GameInfo>().scoreItemPos_y[itemNumber], this.transform.parent.parent.GetComponent<GameInfo>().scoreItemPos_z[itemNumber]);
                return position_1;*/
            case "STAIRWAY":
                this.itemNumber = 1;
                Vector3 position_1 = new Vector3(this.transform.parent.parent.Find("Functions").position.x,
                    this.transform.parent.parent.GetComponent<GameInfo>().scoreItemPos_y[itemNumber], this.transform.parent.parent.Find("Functions").position.z);
                return position_1;

            case "2":
                this.itemNumber = 2; 
                Vector3 position_2 = new Vector3(this.transform.parent.parent.parent.GetComponent<GameInfo>().scoreItemPos_x,
                    this.transform.parent.parent.parent.GetComponent<GameInfo>().scoreItemPos_y[itemNumber], this.transform.parent.parent.parent.GetComponent<GameInfo>().scoreItemPos_z[itemNumber]);
                return position_2;
            case "3":
                this.itemNumber = 3;
                Vector3 position_3 = new Vector3(this.transform.parent.parent.parent.GetComponent<GameInfo>().scoreItemPos_x,
                    this.transform.parent.parent.parent.GetComponent<GameInfo>().scoreItemPos_y[itemNumber], this.transform.parent.parent.parent.GetComponent<GameInfo>().scoreItemPos_z[itemNumber]);
                return position_3;
            case "4":
                this.itemNumber = 4;
                Vector3 position_4 = new Vector3(this.transform.parent.parent.parent.GetComponent<GameInfo>().scoreItemPos_x,
                    this.transform.parent.parent.parent.GetComponent<GameInfo>().scoreItemPos_y[itemNumber], this.transform.parent.parent.parent.GetComponent<GameInfo>().scoreItemPos_z[itemNumber]);
                return position_4;
            case "5":
                this.itemNumber = 5;
                Vector3 position_5 = new Vector3(this.transform.parent.parent.parent.GetComponent<GameInfo>().scoreItemPos_x,
                    this.transform.parent.parent.parent.GetComponent<GameInfo>().scoreItemPos_y[itemNumber], this.transform.parent.parent.parent.GetComponent<GameInfo>().scoreItemPos_z[itemNumber]);
                return position_5;
            case "6":
                this.itemNumber = 6;
                Vector3 position_6 = new Vector3(this.transform.parent.parent.parent.GetComponent<GameInfo>().scoreItemPos_x,
                    this.transform.parent.parent.parent.GetComponent<GameInfo>().scoreItemPos_y[itemNumber], this.transform.parent.parent.parent.GetComponent<GameInfo>().scoreItemPos_z[itemNumber]);
                return position_6;
            //case "7":
            //    this.itemNumber = 7;
            //    Vector3 position_7 = new Vector3(this.transform.parent.parent.GetComponent<GameInfo>().scoreItemPos_x,
            //        this.transform.parent.parent.GetComponent<GameInfo>().scoreItemPos_y[itemNumber], this.transform.parent.parent.GetComponent<GameInfo>().scoreItemPos_z[itemNumber]);
            //    return position_7;
            //case "8":
            //    this.itemNumber = 8;
            //    Vector3 position_8 = new Vector3(this.transform.parent.parent.GetComponent<GameInfo>().scoreItemPos_x,
            //        this.transform.parent.parent.GetComponent<GameInfo>().scoreItemPos_y[itemNumber], this.transform.parent.parent.GetComponent<GameInfo>().scoreItemPos_z[itemNumber]);
            //    return position_8;
            default:
                return new Vector3(999, 999, 999);


        }

    }
    private void ActiveManiHandler(GameObject obj)
    {
        obj.GetComponent<Microsoft.MixedReality.Toolkit.UI.ManipulationHandler>().enabled = true;
    }
}
