using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellState : MonoBehaviour
{

    public void ImContainer()
    {

        GameManeger.GM.CheckThat(this.gameObject);

    }

    public void ImFire(bool red)
    {
        
        GameManeger.GM.HoldMe(this.gameObject);


    }

}



//void Update()
//{
//    //for 3d objects
//    //if (Input.GetMouseButtonDown(0))
//    //{
//    //    RaycastHit hit;
//    //    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

//    //    if (Physics.Raycast(ray, out hit, 100f))
//    //    {
//    //        if (hit.transform != null)
//    //        {
//    //            printThat(hit.transform.gameObject);
//    //        }
//    //    }
//    //}

//}