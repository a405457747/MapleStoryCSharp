using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MapleStory;
using UniRx;
using static MapleStory.LogNote;
public class FighterManager : MonoBehaviour
{
    private ResManager resManager;

    private Vector2 leftCreateFighterPoint;
    private Vector2 rightCreateFighterPoint;


    ReactiveDictionary<int, ReactiveCollection<Fighter>> allFighters;
    

    private void Start()
    {
        allFighters = new ReactiveDictionary<int, ReactiveCollection<Fighter>>();
        allFighters.Add(0, new ReactiveCollection<Fighter>());
        allFighters.Add(1,new ReactiveCollection<Fighter>());

        resManager = AppRoot.Instance.resManager;
        leftCreateFighterPoint = GameObject.Find("LeftCreateFighterPoint").transform.localPosition;
        rightCreateFighterPoint = GameObject.Find("RightCreateFighterPoint").transform.localPosition;
    }
   
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (Random.value > .5)
            {
                CreateFighter(0);
            }else
            {
                CreateFighter(1);
            }
 
        }
    }

    bool IsLeftSide(int sideEnum)
    {
        return sideEnum == 0;
    }

    void CreateFighter(int sideEnum)
    {
        Vector3 createPos;

        //Color co ;
        if (IsLeftSide(sideEnum))
        {
            createPos = leftCreateFighterPoint;
            //co = Color.red;
            Print("left and red");
        }else
        {
            createPos = rightCreateFighterPoint;
            //co = Color.green;
            Print("right and green");
        }
 


        GameObject go = resManager.LoadGameObject("Square");
        GameObject.Instantiate(go, createPos, Quaternion.identity);
/*        SpriteRenderer spriteRenderer = go.GetComponent<SpriteRenderer>();
        Print("sp==null", spriteRenderer == null,co);
        spriteRenderer.color = co;*/
        var fighter = go.GetComponent<Fighter>();

        allFighters[sideEnum].Add(fighter);
    }


}
