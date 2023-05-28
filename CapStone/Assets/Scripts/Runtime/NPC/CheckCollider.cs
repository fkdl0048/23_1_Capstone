using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckCollider : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("enter");
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("exit");
    }
}
