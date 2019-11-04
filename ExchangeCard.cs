using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExchangeCard : MonoBehaviour
{
    Vector3 originalPos = new Vector3();
    bool flag = false;
    string parentName;
    // Start is called before the first frame update
    void Start()
    {
        originalPos = this.transform.position;
        parentName = this.transform.parent.name;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (flag == true)
        {
            if (InBox())
            {
                AddCard();
            }
            else
            {
                this.transform.position = originalPos;
                flag = false;
            }
        }
    }
    bool InBox()
    {
        Vector2 cardPositon = new Vector2(this.transform.position.x, this.transform.position.y);
        Vector2 boxPositon = new Vector2(this.transform.parent.parent.Find("ExchangeArea").position.x, this.transform.parent.parent.Find("ExchangeArea").position.y);
        if (Vector2.Distance(cardPositon, boxPositon) < 0.2f)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    void AddCard()
    {
       
        this.transform.parent = this.transform.parent.parent.parent.Find("Scores");
        GameObject newCard = GameObject.Instantiate(this.gameObject);
        this.GetComponent<ExchangeCard>().enabled = false;
        newCard.SetActive(false);
        newCard.transform.parent = this.transform.parent.parent.Find("ExchangeCards").Find(parentName);
        newCard.transform.position = originalPos;
        newCard.name = this.name;
        this.transform.eulerAngles = new Vector3(-90, 90, 0);
        
        this.transform.parent.parent.Find("ExchangeCards").Find(parentName).gameObject.SetActive(false);
        this.transform.parent.parent.Find("ExchangeCards").GetComponent<ExchangeCardsInfo>().rowNumber -= 1;
        this.transform.parent.parent.Find("ExchangeCards").GetComponent<ExchangeCardsInfo>().maxRow -= 1;
        newCard.SetActive(true);
        this.GetComponent<EXCardMoving>().enabled = true;
        if (this.name.Equals("2"))
        {
            this.transform.parent = this.transform.parent.Find("Item1");
            this.transform.parent.parent.parent.GetComponent<GameInfo>().item_1Score += 1;
            this.gameObject.AddComponent<Moving>();
        }
        else if (this.name.Equals("3"))
        {
            this.transform.parent = this.transform.parent.Find("Item2");
            this.transform.parent.parent.parent.GetComponent<GameInfo>().item_2Score += 1;
            this.gameObject.AddComponent<Moving>();
        }
        else if (this.name.Equals("4"))
        {
            this.transform.parent = this.transform.parent.Find("Item3");
            this.transform.parent.parent.parent.GetComponent<GameInfo>().item_3Score += 1;
            this.gameObject.AddComponent<Moving>();
        }
        else if (this.name.Equals("5"))
        {
            this.transform.parent = this.transform.parent.Find("Item4");
            this.transform.parent.parent.parent.GetComponent<GameInfo>().item_4Score += 1;
            this.gameObject.AddComponent<Moving>();
        }

        // 打印出目前分数
        //print(this.transform.parent.parent.GetComponent<GameInfo>().item_1Score);
        //print(this.transform.parent.parent.GetComponent<GameInfo>().item_2Score);
        //print(this.transform.parent.parent.GetComponent<GameInfo>().item_3Score);
        //print(this.transform.parent.parent.GetComponent<GameInfo>().item_4Score);
        Destroy(this.GetComponent<ExchangeCard>());

    }
    public void SetFalse()
    {
        this.flag = false;
    }
    public void SetTrue()
    {
        this.flag = true;
    }
}
