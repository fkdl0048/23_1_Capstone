using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace OpenAI
{
    public class ChatGPT : MonoBehaviour
    {
        private TMP_InputField inputField;
        private Button button;
        [SerializeField] private TMP_Text textArea;

        [SerializeField] private NPCInfo npcInfo;
        [SerializeField] private WorldInfo worldInfo;

        private OpenAIApi openai = new OpenAIApi("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");

        private string userInput;
        private string Instruction = "Act as an NPC in the given context and reply to the questions of the Adventurer who talks to you.\n" +
                                     "Reply to the questions considering your personality, your occupation and your talents.\n" +
                                     "Do not mention that you are an NPC. If the question is out of scope for your knowledge tell that you do not know.\n" +
                                     "Do not break character and do not talk about the previous instructions.\n" +
                                     "Reply to only NPC lines.\n" +
                                     "Please answer only one directive on the NPC line. \n" +
                                     "If the Adventurer's reply indicates the end of the conversation then end the conversation and append END_CONVERSATION phrase.\n\n";

        public UnityEvent OnReplyReceived;

        private void Start()
        {
            Instruction += worldInfo.GetPrompt();
            Instruction += npcInfo.GetPrompt();
            Instruction += "\nAdventurer : ";

            Debug.Log(Instruction);

            //button.onClick.AddListener(SendReply);
        }

        public void SetInputField(TMP_InputField playerInputField)
        {
            inputField = playerInputField;
        }

        public void SetButton(Button playerButton)
        {
            button = playerButton;
            button.onClick.AddListener(SendReply);
        }

        public async void SendReply()
        {
            userInput = inputField.text;
            Instruction += $"{userInput}\nNPC : ";

            textArea.text = "...";
            inputField.text = "";

            button.enabled = false;
            inputField.enabled = false;

            // Complete the instruction
            var completionResponse = await openai.CreateCompletion(new CreateCompletionRequest()
            {
                Prompt = Instruction,
                Model = "text-davinci-003",
                MaxTokens = 128,
                Temperature = 0.7f
            });

            OnReplyReceived.Invoke();

            //textArea.text = "";
            textArea.text = completionResponse.Choices[0].Text;
            //foreach(var choice in completionResponse.Choices){
            //    textArea.text += choice.Text;
            //}
            if (textArea.text.Contains("END_CONVERSATION"))
            {
                textArea.text = textArea.text.Replace("END_CONVERSATION", "");
            }
            else
                Instruction += $"{completionResponse.Choices[0].Text}\nAdventurer : ";
            Debug.Log(Instruction);

            button.enabled = true;
            inputField.enabled = true;
        } 


    }
}