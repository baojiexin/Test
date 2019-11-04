using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterPlay : MonoBehaviour
{
    float time = 0;
    Animator animator;
    Random ran = new Random();
    // Start is called before the first frame update
    void Start()
    {
        
        animator = this.GetComponent<Animator>();
        RuntimeAnimatorController anim = (RuntimeAnimatorController)Resources.Load("Character1Playing");
        animator.runtimeAnimatorController = anim;
    }

    // Update is called once per frame
    void Update()
    {
        this.time += Time.deltaTime;
        if(this.time >= 5)
        {
            int number = Random.Range(0, 2);    
            if(number < 1)
            {
                if (animator.GetBool("0") == false)
                {
                    animator.SetBool("0", true);
                    this.time = 0;
                }
                else
                {
                    animator.SetBool("0", false);
                    this.time = 0;
                }
            }
            else
            {
                if (animator.GetBool("1") == false)
                {
                    animator.SetBool("1", true);
                    this.time = 0;
                }
                else
                {
                    animator.SetBool("1", false);
                    this.time = 0;
                }
            }
            
        }
    }
    void PlayOnBoard()
    {
        
    }
}
