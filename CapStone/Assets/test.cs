using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    public void SetTest()
    {
        GameManager.Data.GetItem("HardWood");
        //GameManager.Data.SetInventoryCustomData("One", null);
    }
}
