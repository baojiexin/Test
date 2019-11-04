using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterRotation : MonoBehaviour
{
    public float rotationSpeed = 20f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 positon = new Vector3(this.transform.position.x, 0, this.transform.position.y);
        this.transform.RotateAround(this.transform.Find("Center").position,Vector3.up,rotationSpeed*Time.deltaTime);
    }
}
