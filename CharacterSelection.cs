using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelection : MonoBehaviour
{
    private Animator anim;
    public int item_1ScoreNeed ;
    public int item_2ScoreNeed ;
    public int item_3ScoreNeed ;
    public int item_4ScoreNeed ;
    public Vector3 originalPos = new Vector3();
    public Vector3 origianlRot = new Vector3();
    Transform scoreBoard;
    public bool selection = false;
    public bool choiose = false;
    float itemRotation = 0;
   


    // Start is called before the first frame update
    void Start()
    {
        if (this.name.Equals("1"))
        {
            item_1ScoreNeed = 2;
            item_2ScoreNeed = 2;
            item_3ScoreNeed = 0;
            item_4ScoreNeed = 0;

        }
        else if(this.name.Equals("2"))
        {
            item_1ScoreNeed = 0;
            item_2ScoreNeed = 0;
            item_3ScoreNeed = 2;
            item_4ScoreNeed = 2;
        }
        else if (this.name.Equals("Goku"))
        {
            item_1ScoreNeed = 2;
            item_2ScoreNeed = 2;
            item_3ScoreNeed = 2;
            item_4ScoreNeed = 2;
        }
        anim = this.GetComponent<Animator>();
        anim.SetBool("ScoreEnough", false);
        anim.SetBool("Selection", false);
        scoreBoard = this.transform.parent.parent.Find("scoreSpot");
        if (this.name.Equals("1"))
        {
            this.itemRotation = 180;
        }
        else if (this.name.Equals("2"))
        {
            this.itemRotation = 180;
        }


    }

    // Update is called once per frame
    void Update()
    {
        int score_1 = this.transform.parent.parent.GetComponent<GameInfo>().item_1Score;
        int score_2 = this.transform.parent.parent.GetComponent<GameInfo>().item_2Score;
        int score_3 = this.transform.parent.parent.GetComponent<GameInfo>().item_3Score;
        int score_4 = this.transform.parent.parent.GetComponent<GameInfo>().item_4Score;

        if (score_1 >= this.item_1ScoreNeed  &&  score_2 >= this.item_2ScoreNeed && score_3 >= this.item_3ScoreNeed && score_4 >= this.item_4ScoreNeed)
        {
            anim.SetBool("ScoreEnough", true);
            this.GetComponent<Microsoft.MixedReality.Toolkit.UI.ManipulationHandler>().enabled = true;
        }
        else
        {
            if (anim.GetBool("ScoreEnough"))
            {
                anim.SetBool("ScoreEnough", false);
            }
            this.GetComponent<Microsoft.MixedReality.Toolkit.UI.ManipulationHandler>().enabled = false;
        }
    }
    public void stopRotate()
    {
        //this.transform.parent.GetComponent<CharacterRotation>().enabled = false;
        originalPos = this.transform.position;
        origianlRot = new Vector3(0, this.itemRotation, 0);
        this.selection = false;
        
    }
    public void startRotate()
    {
        
        float distance = Vector2.Distance(new Vector2(this.transform.position.x, this.transform.position.z), new Vector2(scoreBoard.position.x, scoreBoard.position.z));
        if(distance <= scoreBoard.Find("walls").Find("plane").lossyScale.x)
        {
            this.transform.parent = scoreBoard;
            this.transform.parent.parent.Find("House").GetComponent<House>().score += this.transform.Find("Cost").childCount;
            this.GetComponent<Microsoft.MixedReality.Toolkit.UI.ManipulationHandler>().OnHoverEntered = new Microsoft.MixedReality.Toolkit.UI.ManipulationEvent();
            this.GetComponent<Microsoft.MixedReality.Toolkit.UI.ManipulationHandler>().OnHoverExited = new Microsoft.MixedReality.Toolkit.UI.ManipulationEvent();
            Destroy(this.transform.Find("Cost").gameObject);
            this.transform.localScale = new Vector3(1.5f, 1, 1.5f);
            this.transform.localEulerAngles = new Vector3(0, 180, 0);
            this.transform.localPosition = new Vector3(this.transform.localPosition.x, 0, this.transform.localPosition.z);
            settleScores();
            //this.transform.parent.parent.Find("Characters").GetComponent<CharacterRotation>().enabled = true;
            this.gameObject.AddComponent<CharacterOnBoard>();
            this.GetComponent<CharacterOnBoard>().originalPos = this.transform.position;
            this.gameObject.GetComponent<CharacterOnBoard>().enabled = true;
            if (!this.name.Equals("Goku"))
            {
                this.gameObject.GetComponent<CharacterPlay>().enabled = true;
            }
            CreateStar();
            Destroy(this.GetComponent<CharacterSelection>());
        }
        else
        {
            this.transform.position = originalPos;
            this.transform.localEulerAngles = origianlRot;
            //this.transform.parent.GetComponent<CharacterRotation>().enabled = true;
            this.selection = true;

        }
        

    }
    public void Wave()
    {
        anim.SetBool("Selection", true);
    }
    public void StopWave()
    {
        anim.SetBool("Selection", false);
    }
   
    
    void settleScores()
    {
        if(this.item_1ScoreNeed != 0)
        {
            int scoreRemains = this.item_1ScoreNeed;
            int itemNumb = this.transform.parent.parent.Find("Scores").Find("Item1").childCount;
            while (scoreRemains > 0)
            {
                this.transform.parent.parent.GetComponent<GameInfo>().scoreItemPos_y[2] -= 0.07f/4;
                Destroy(this.transform.parent.parent.Find("Scores").Find("Item1").GetChild(itemNumb - 1).gameObject);
                itemNumb -= 1;
                scoreRemains -= 1;

            }
        }
        if (this.item_2ScoreNeed != 0)
        {
            int scoreRemains = this.item_2ScoreNeed;
            int itemNumb = this.transform.parent.parent.Find("Scores").Find("Item2").childCount;
            while (scoreRemains > 0)
            {
                this.transform.parent.parent.GetComponent<GameInfo>().scoreItemPos_y[3] -= 0.07f/4;
                Destroy(this.transform.parent.parent.Find("Scores").Find("Item2").GetChild(itemNumb - 1).gameObject);
                itemNumb -= 1;
                scoreRemains -= 1;

            }
        }
        if (this.item_3ScoreNeed != 0)
        {
            int scoreRemains = this.item_3ScoreNeed;
            int itemNumb = this.transform.parent.parent.Find("Scores").Find("Item3").childCount;
            while (scoreRemains > 0)
            {
                this.transform.parent.parent.GetComponent<GameInfo>().scoreItemPos_y[4] -= 0.07f/4;
                Destroy(this.transform.parent.parent.Find("Scores").Find("Item3").GetChild(itemNumb - 1).gameObject);
                itemNumb -= 1;
                scoreRemains -= 1;

            }
        }
        if (this.item_4ScoreNeed != 0)
        {
            int scoreRemains = this.item_4ScoreNeed;
            int itemNumb = this.transform.parent.parent.Find("Scores").Find("Item4").childCount;
            while (scoreRemains > 0)
            {
                this.transform.parent.parent.GetComponent<GameInfo>().scoreItemPos_y[5] -= 0.07f/4;
                Destroy(this.transform.parent.parent.Find("Scores").Find("Item4").GetChild(itemNumb - 1).gameObject);
                itemNumb -= 1;
                scoreRemains -= 1;

            }
        }


    }
    
    void CreateStar()
    {
        GameObject star = GameObject.Instantiate(this.transform.parent.parent.Find("Items").GetChild(0).gameObject);
        star.name = "0";
        star.transform.position = new Vector3(this.transform.parent.parent.Find("Stars").position.x,
                    this.transform.parent.parent.GetComponent<GameInfo>().scoreItemPos_y[0], this.transform.parent.parent.Find("Stars").position.z);
        star.GetComponent<Microsoft.MixedReality.Toolkit.UI.ManipulationHandler>().enabled = true;
        star.transform.parent = this.transform.parent.parent.Find("Stars");
        star.GetComponent<Stars>().originalPos = new Vector3(this.transform.parent.parent.Find("Stars").position.x,
                    this.transform.parent.parent.GetComponent<GameInfo>().scoreItemPos_y[0], this.transform.parent.parent.Find("Stars").position.z);
        this.transform.parent.parent.GetComponent<GameInfo>().scoreItemPos_y[0] += (0.07f / 4);
    }


}
