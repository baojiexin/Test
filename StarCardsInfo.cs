using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarCardsInfo : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if(!this.transform.Find("Row1").gameObject.activeSelf && !this.transform.Find("Row2").gameObject.activeSelf && !this.transform.Find("Row3").gameObject.activeSelf)
        {
            this.gameObject.SetActive(false);
        }
    }
}
