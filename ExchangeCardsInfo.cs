using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExchangeCardsInfo : MonoBehaviour
{
    public int rowNumber = 0;
    public int maxRow = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (this.maxRow == 0)
        {
            this.gameObject.SetActive(false);
        }
    }
    public void ActiveRow()
    {
        string activeRow = "";
        if(rowNumber == 1)
        {
            activeRow = "Row1";
        }
        else if (rowNumber == 2)
        {
            activeRow = "Row2";
        }
        else if (rowNumber == 3)
        {
            activeRow = "Row3";
        }
        this.transform.Find(activeRow).gameObject.SetActive(true);
    }
}
