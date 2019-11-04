using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EXCardMoving : MonoBehaviour
{
    public Vector3 originalPos = new Vector3();
    public bool flag = false;
    bool canMove = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(flag == true)
        {
            if (canMove)
            {
                if (InBox())
                {
                    
                    if (this.transform.parent.parent.parent.Find("ExchangeCards").GetComponent<ExchangeCardsInfo>().rowNumber <
                        this.transform.parent.parent.parent.Find("ExchangeCards").GetComponent<ExchangeCardsInfo>().maxRow)
                    {
                        this.transform.parent.parent.parent.GetComponent<GameInfo>().scoreItemPos_y[ItemNumber()] -= 0.07f/4;
                        this.transform.parent.parent.parent.Find("ExchangeCards").GetComponent<ExchangeCardsInfo>().rowNumber += 1;
                        this.transform.parent.parent.parent.Find("ExchangeCards").GetComponent<ExchangeCardsInfo>().ActiveRow();
                        if (this.name.Equals("2"))
                        {
                            this.transform.parent.parent.parent.GetComponent<GameInfo>().item_1Score -= 1;
                        }
                        else if (this.name.Equals("3"))
                        {
                            this.transform.parent.parent.parent.GetComponent<GameInfo>().item_2Score -= 1;
                        }
                        else if (this.name.Equals("4"))
                        {
                            this.transform.parent.parent.parent.GetComponent<GameInfo>().item_3Score -= 1;
                        }
                        else if (this.name.Equals("5"))
                        {
                            this.transform.parent.parent.parent.GetComponent<GameInfo>().item_4Score -= 1;
                        }

                        // 打印出目前分数
                        //print(this.transform.parent.parent.GetComponent<GameInfo>().item_1Score);
                        //print(this.transform.parent.parent.GetComponent<GameInfo>().item_2Score);
                        //print(this.transform.parent.parent.GetComponent<GameInfo>().item_3Score);
                        //print(this.transform.parent.parent.GetComponent<GameInfo>().item_4Score);

                        Destroy(this.gameObject);
                    }
                    else
                    {
                        this.transform.position = originalPos;
                        this.transform.eulerAngles = new Vector3(-90, 90, 0);
                        canMove = false;
                    }
                    
                }
                else
                {
                    this.transform.position = originalPos;
                    this.transform.eulerAngles = new Vector3(-90, 90, 0);
                    canMove = false;
                }
            }
           
        }
    }
    bool InBox()
    {
        Vector2 cardPositon = new Vector3(this.transform.position.x, this.transform.position.y,this.transform.position.z);
        Vector2 boxPositon = new Vector3(this.transform.parent.parent.parent.Find("ExchangeCards").Find("ExchangeArea").position.x,
            this.transform.parent.parent.parent.Find("ExchangeCards").Find("ExchangeArea").position.y, this.transform.parent.parent.parent.Find("ExchangeCards").Find("ExchangeArea").position.z);
        if (Vector2.Distance(cardPositon, boxPositon) < 0.2f)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    int ItemNumber()
    {
        if(this.name == "2")
        {
            return 2;
        }
        if (this.name == "3")
        {
            return 3;
        }
        if (this.name == "4")
        {
            return 4;
        }
        if (this.name == "5")
        {
            return 5;
        }
        return 999;
    }
    public void SetTrue()
    {
        this.canMove = true;
    }
}
