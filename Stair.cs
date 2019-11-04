using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stair : MonoBehaviour
{
    public Vector3 originalPos;
    public bool flag = false;
    // Start is called before the first frame update
    

    // Update is called once per frame
    void Update()
    {
        
        if(flag == true)
        {
            
            int boardNumber = GetClosestItem();
            print(boardNumber);
            if(boardNumber !=999)
            {
                PutStair(boardNumber);
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
    int GetClosestItem()
    {
        for (int i = 0; i < 9; i++)
        {
            if (Vector2.Distance(new Vector2(this.transform.position.x, this.transform.position.z),
                new Vector2(this.transform.parent.parent.Find("Board").GetChild(i).position.x, this.transform.parent.parent.Find("Board").GetChild(i).position.z))
                < 0.1/2)
            {
                
                return i;
                
            }
        }
        return 999;
    }
    void PutStair(int boardNumber)
    {
        float new_x = this.transform.parent.parent.GetComponent<GameInfo>().boardObjsPos[boardNumber].x;
        float new_y = this.transform.parent.parent.GetComponent<GameInfo>().boardObjsPos[boardNumber].y + 0.008f; //+ this.transform.lossyScale.y;
        float new_z = this.transform.parent.parent.GetComponent<GameInfo>().boardObjsPos[boardNumber].z;
        this.transform.position = new Vector3(new_x, new_y, new_z);
        this.transform.eulerAngles = new Vector3(-90, 90, 0);
        print("新高度为 " + this.transform.lossyScale.y);
        this.transform.parent.parent.GetComponent<GameInfo>().boardObjsPos[boardNumber].y = new_y;
    }
    private void UpdataGameInfo(int boardNumber)
    {
        this.transform.parent.parent.gameObject.GetComponent<GameInfo>().itemLayers[boardNumber] += 1;
        this.GetComponent<Microsoft.MixedReality.Toolkit.UI.ManipulationHandler>().enabled = false;
        this.GetComponent<Microsoft.MixedReality.Toolkit.Input.NearInteractionGrabbable>().enabled = false;
        this.GetComponent<Stair>().enabled = false;
        this.transform.parent.parent.GetComponent<GameInfo>().scoreItemPos_y[1] -= (0.07f/4);
    }
    public void SetTrue()
    {
        this.flag = true;
    }
}
