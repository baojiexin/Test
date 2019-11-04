using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterOnBoard : MonoBehaviour
{
    public Vector3 originalPos = new Vector3();
    // Start is called before the first frame update
    

    // Update is called once per frame
    void Update()
    {
        float max_distance =  this.transform.parent.Find("walls").Find("plane").lossyScale.x;
        float distance = Vector3.Distance(this.transform.position, this.transform.parent.position);
        if(distance >= max_distance)
        {
            this.transform.position = originalPos;
        }
    }
}
