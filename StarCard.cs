using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarCard : MonoBehaviour
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
                this.transform.eulerAngles = new Vector3(0, 180, -90);
                flag = false;
            }
        }
    }
    bool InBox()
    {
        Vector2 cardPositon = new Vector2(this.transform.position.x, this.transform.position.y);
        Vector2 boxPositon = new Vector2(this.transform.parent.parent.Find("SelectionArea").position.x, this.transform.parent.parent.Find("SelectionArea").position.y);
        if( Vector2.Distance(cardPositon,boxPositon) < 0.2f)
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
        if (this.name.Equals("1"))
        {
            this.transform.parent = this.transform.parent.parent.parent.Find("Functions");
           
        }
        else
        {
            this.transform.parent = this.transform.parent.parent.parent.Find("Scores");
        }
        GameObject newCard = GameObject.Instantiate(this.gameObject);
        this.GetComponent<StarCard>().enabled = false;
        newCard.SetActive(false);
        newCard.transform.parent = this.transform.parent.parent.Find("StarCards").Find(parentName);
        newCard.transform.position = originalPos;
        newCard.name = this.name;
        this.transform.eulerAngles = new Vector3(-90, 90, 0);
        if (this.name.Equals("1"))
        {
            GameObject stair = GameObject.Instantiate(this.transform.parent.parent.Find("Items").Find("STAIRWAY").gameObject);
            stair.name = "STAIRWAY";
            stair.transform.parent = this.transform.parent;
            stair.gameObject.AddComponent<Stair>();
            stair.GetComponent<Stair>().enabled = false;
            stair.gameObject.AddComponent<Moving>();
            stair.GetComponent<Stair>().enabled = true;
            stair.transform.parent.parent.Find("StarCards").Find(parentName).gameObject.SetActive(false);
            Destroy(this.gameObject);
        }
        
        if (this.name.Equals("2"))
        {
            this.transform.parent = this.transform.parent.Find("Item1");
            this.transform.parent.parent.parent.GetComponent<GameInfo>().item_1Score += 1;
            this.transform.parent.parent.parent.Find("StarCards").Find(parentName).gameObject.SetActive(false);
            this.gameObject.AddComponent<Moving>();
        }
        else if (this.name.Equals("3"))
        {
            this.transform.parent = this.transform.parent.Find("Item2");
            this.transform.parent.parent.parent.GetComponent<GameInfo>().item_2Score += 1;
            this.transform.parent.parent.parent.Find("StarCards").Find(parentName).gameObject.SetActive(false);
            this.gameObject.AddComponent<Moving>();
        }
        else if (this.name.Equals("4"))
        {
            this.transform.parent = this.transform.parent.Find("Item3");
            this.transform.parent.parent.parent.GetComponent<GameInfo>().item_3Score += 1;
            this.transform.parent.parent.parent.Find("StarCards").Find(parentName).gameObject.SetActive(false);
            this.gameObject.AddComponent<Moving>();
        }
        else if (this.name.Equals("5"))
        {
            this.transform.parent = this.transform.parent.Find("Item4");
            this.transform.parent.parent.parent.GetComponent<GameInfo>().item_4Score += 1;
            this.transform.parent.parent.parent.Find("StarCards").Find(parentName).gameObject.SetActive(false);
            this.gameObject.AddComponent<Moving>();
        }
        
        newCard.SetActive(true);
        Destroy(this.GetComponent<StarCard>());
        
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
