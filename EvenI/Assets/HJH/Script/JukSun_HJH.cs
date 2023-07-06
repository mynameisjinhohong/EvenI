using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JukSun_HJH : Object_Manager_shj
{



    //ItemManager_HJH itemManager;
    // Start is called before the first frame update
    //void Start()
    //{
    //    //itemManager = GameObject.Find("ItemManager").GetComponent<ItemManager_HJH>(); 
    //}

    //// Update is called once per frame
    //void Update()
    //{

    //}

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.gameObject.name == "Player")
    //    {
    //        itemManager.jukSunSu++;
    //        Destroy(gameObject);
    //    }
    //}
    public override void Item_Active(GameObject player)
    {
        //UI연동해서 스코어 올릴수있는 스크립트 작성해야함
        GameObject.Find("InGame_UI").GetComponent<InGame_UI_shj>().Count++;
        Debug.Log(GameObject.Find("InGame_UI").GetComponent<InGame_UI_shj>().Count);
    }
}
