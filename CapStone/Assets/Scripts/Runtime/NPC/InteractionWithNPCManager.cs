using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionWithNPCManager : MonoBehaviour
{
    public float interactionDistance = 3f;

    private GameObject currentNPC;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (currentNPC != null)
            {
                InteractWithNPC(currentNPC);
            }
            print(currentNPC);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("NPC"))
        {
            currentNPC = other.gameObject;
        }
        print("enter");
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("NPC"))
        {
            currentNPC = null;
        }
        print("exit");
    }

    void InteractWithNPC(GameObject npc)
    {
        Debug.Log("interact with NPC");
    }
}
