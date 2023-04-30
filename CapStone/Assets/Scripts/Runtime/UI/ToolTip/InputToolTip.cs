using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;
public class InputToolTip : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _explanationText;
    [SerializeField] private TextMeshProUGUI _RequestButtonText;
    [SerializeField] private TextMeshProUGUI _inputText;
    [SerializeField] private Button _requestButton;
    
    public void SetInputToolTip(string explanation, string buttonText, System.Func<string, bool> buttonAction)
    {
        _explanationText.text = explanation;
        _RequestButtonText.text = buttonText;
        
        _requestButton.onClick.RemoveAllListeners();
        _requestButton.onClick.AddListener(() =>
        {
            var requestBool = buttonAction(_inputText.text);
            if (requestBool)
                gameObject.SetActive(false);
        });
    }
}
