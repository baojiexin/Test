using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : MonoBehaviour
{
    // Start is called before the first frame update
    public int score = 0;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(this.score >= 0 && this.score <4)
        {
            this.transform.Find("1").gameObject.SetActive(true);
        }
        else if (this.score >=4 && this.score < 8)
        {
            this.transform.Find("1").gameObject.SetActive(false);
            this.transform.Find("3").gameObject.SetActive(true);
        }
        else
        {
            this.transform.Find("3").gameObject.SetActive(false);
            this.transform.Find("4").gameObject.SetActive(true);
        }
    }
}
