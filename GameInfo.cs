using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInfo : MonoBehaviour
{
    public int[] itemLayers;
    public GameObject[] boardObjs;
    public Vector3[] boardObjsPos;
    public float[] scoreItemPos_z;
    public float scoreItemPos_x;
    public float[] scoreItemPos_y;

    public int collectionQtty;
    public GameObject[] Cards;
    public GameObject[] Items;// game items types, it is not visible in game;
    public int[] cardsLocationInfo;
    public Dictionary<int, Vector3> cardsLocation;
    int topCard = 14;
    //GameObject cardsCollection;
    public int movSpeed = 8;
    public Vector3[] Locations;
    public Queue<int> LockersNumber;
    public int item_1Score = 0;
    public int item_2Score = 0;
    public int item_3Score = 0;
    public int item_4Score = 0;




    void Awake()
    {
        LockersNumber = new Queue<int>();
        LockersNumber.Enqueue(0);
        LockersNumber.Enqueue(1);
        cardsLocation = new Dictionary<int, Vector3>();
        itemLayers = new int[9];
        boardObjs = new GameObject[9];
        scoreItemPos_y = new float[8];
        scoreItemPos_x = -0.15f;
        scoreItemPos_z = new float[8];
        Cards = new GameObject[15];
        Items = new GameObject[8];
        cardsLocationInfo = new int[3];
        Locations = new Vector3[3];
        boardObjsPos = new Vector3[9];

        for(int i =0; i <3; i++)
        {
            cardsLocationInfo[i] = 0;
        }
       for(int i = 0; i < 8; i++)
        {
            itemLayers[i] = 0;
            Items[i] = this.transform.Find("Items").GetChild(i).gameObject;
            scoreItemPos_z[i] = 0.9f - (float)((float)i * 0.1);
            scoreItemPos_y[i] = -0.5f;
        }
        //print(scoreItemPos_x[0])
       Locations[0] = new Vector3(-0.07f, -0.5f, 0.42f);
       Locations[1] = new Vector3(0.0f, -0.5f, 0.42f);
       Locations[2] = new Vector3(0.07f, -0.5f, 0.42f);
    }
    private void Start()
    {
        initialCards();
        initialBoards();
        initialStair();
        for(int i=0; i < 9; i++)
        {
            boardObjs[i] = this.transform.Find("Board").GetChild(i).gameObject;
            boardObjsPos[i] = this.transform.Find("Board").GetChild(i).position;
        }
        this.transform.Find("scoreSpot").gameObject.SetActive(true);

    }
    private void Update()
    {
        if(topCard >= 0)
        {
            if (cardsLocationInfo[0] == 0)
            {
                UpdateUsingCards(0);
               
                topCard -= 1;
                cardsLocationInfo[0] = 1;
            }
            if (cardsLocationInfo[1] == 0)
            {
                UpdateUsingCards(1);
                topCard -= 1;
                cardsLocationInfo[1] = 1;
            }
            if (cardsLocationInfo[2] == 0)
            {
                UpdateUsingCards(2);
                topCard -= 1;
                cardsLocationInfo[2] = 1;
            }
        }

    }
    private void UpdateUsingCards(int i)
    {
        Transform Card = this.transform.Find("cardsCollection").GetChild(topCard);
        Card.GetComponent<cardItems>().enabled = true;
        Card.GetComponent<cardItems>().destination = Locations[i];
        Card.GetComponent<cardItems>().locationNum = i;
        Card.GetComponent<cardItems>().destinationSet = true;
        Card.GetComponent<Microsoft.MixedReality.Toolkit.UI.ManipulationHandler>().enabled = true;
    }
  
    public void NewBoardItem(int boardItemIndex, GameObject obj)
    {
        
            this.boardObjs[boardItemIndex] = obj;
        
        
    }
    public void initialCards()
    {
        
        for(int i = 0; i < 15; i++)
        {
            Transform Card = this.transform.Find("cardsCollection").GetChild(i);
            int item1Number = Random.Range(1, Items.Length-1);
            int item2Number = Random.Range(1, Items.Length-1);
            GameObject item1 = GameObject.Instantiate(Items[item1Number]);
            GameObject item2 = GameObject.Instantiate(Items[item2Number]);
            item1.transform.parent = Card;
            item2.transform.parent = Card;
            item1.transform.localPosition = new Vector3(0, 0, 0.125f/4);
            item2.transform.localPosition = new Vector3(0, 0, -0.125f/4);
            item1.transform.name = item1Number.ToString();
            item2.transform.name = item2Number.ToString();
            
        }
    }
    private void initialBoards()
    {
        float position_x = -0.25f/4;
        float position_y = 0;
        float position_z = 0.25f/4;
        for(int i =0; i < 3; i++)
        {
            for(int j = 0; j < 3; j++)
            {
                int itemNumber = Random.Range(1, Items.Length-1);
                GameObject item = GameObject.Instantiate(Items[itemNumber]);
                item.name = itemNumber.ToString();
                item.transform.parent = this.transform.Find("Board");
                item.transform.localPosition = new Vector3(position_x, position_y, position_z);
                position_x += 0.25f/4;
            }
            position_x = -0.25f/4;
            position_z += 0.25f/4;
        }
    }
    public void initialStair()
    {
        GameObject stair = GameObject.Instantiate(this.transform.Find("Items").GetChild(8).gameObject);
        stair.name = "STAIRWAY";
        stair.transform.position = new Vector3(this.transform.Find("Functions").position.x,
                    this.scoreItemPos_y[1], this.transform.Find("Functions").position.z);
        stair.GetComponent<Stair>().enabled = true;
        stair.GetComponent<Microsoft.MixedReality.Toolkit.UI.ManipulationHandler>().enabled = true;
        stair.transform.parent = this.transform.Find("Functions");
        stair.GetComponent<Stair>().originalPos = new Vector3(this.transform.Find("Functions").position.x,
                    this.scoreItemPos_y[1], this.transform.Find("Functions").position.z);
        this.scoreItemPos_y[1] += (0.07f/4);

    }

   
    
}
