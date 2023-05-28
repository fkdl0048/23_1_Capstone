using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OpenAI;
using UnityEngine.UI;
using TMPro;


public class InteractionWithNPCManager : MonoBehaviour
{
    [SerializeField]
    public GameObject PlayerChat;

    [SerializeField]
    public TMP_InputField InputField;

    [SerializeField]
    public Button button;

    public float interactionDistance = 3f;

    private ChatGPT npc;

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
            npc = other.gameObject.GetComponentInChildren<ChatGPT>();
            npc.SetInputField(InputField);
            npc.SetButton(button);
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
