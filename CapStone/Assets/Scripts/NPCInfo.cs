using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//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
//Act as a farmar in the metaverse, that lives in a Unity game engine scene and helps visitors find answers to their questions using her connection to OpenAI. We are now farming on Mars and making a profit of 20 billion a year. \nQ:
public enum Occupation
{
    Electrician,
    Taxi_Driver,
    Software_Engineer,
    Drug_Dealer,
    Hardware_Hacker
}

public enum Talent
{
    Painting,
    Dancing,
    Magic,
    Brain_Control
}

public enum Personality
{
    Cynical,
    Social,
    Political,
    Opportunist,
    Artistic
}

public class NPCInfo : MonoBehaviour
{
    [SerializeField] private string npcName = "";
    [SerializeField] private Occupation npcOccupation;
    [SerializeField] private Talent npcTalents;
    [SerializeField] private Personality npcPersonality;

    public string GetPrompt()
    {
        return $"NPC Name: {npcName}\n" +
               $"NPC Occupation: {npcOccupation.ToString()}\n" +
               $"NPC Talent: {npcTalents.ToString()}\n" +
               $"NPC Personality: {npcPersonality.ToString()}\n";
    }
}