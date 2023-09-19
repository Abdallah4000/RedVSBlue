using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContainerCell : MonoBehaviour
{
    public bool embty = true;
    public int value = 0;
    public int Firevalue = 0;


    public void chengeImge (Sprite g)
    {
         gameObject.GetComponent<Image>().sprite = g;
    }

}
