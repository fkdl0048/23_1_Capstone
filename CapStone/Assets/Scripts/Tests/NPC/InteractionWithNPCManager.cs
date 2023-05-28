using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionWithNPCManager : MonoBehaviour
{
    [SerializeField]
    public GameObject PlayerChat;

    public float interactionDistance = 3f;

    private GameObject currentNPC;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (currentNPC != null)
            {
                PlayerChat.SetActive(true);
                InteractWithNPC(currentNPC);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("NPC"))
        {
            currentNPC = other.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("NPC"))
        {
            PlayerChat.SetActive(false);
            currentNPC = null;
        }
    }

    void InteractWithNPC(GameObject npc)
    {
        Debug.Log("interact with NPC");
    }
}
