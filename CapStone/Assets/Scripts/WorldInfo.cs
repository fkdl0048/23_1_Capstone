using UnityEngine;

public class WorldInfo : MonoBehaviour
{
    [SerializeField, TextArea] private string gameStory;
    [SerializeField, TextArea] private string gameWorld;

    public string GetPrompt()
    {
        return $"Game Story: {gameStory}\n" +
               $"Game World: {gameWorld}\n";
    }
}