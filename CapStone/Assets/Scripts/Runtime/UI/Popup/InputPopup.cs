using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;
public class InputPopup : UIView
{
    [SerializeField] private TextMeshProUGUI _explanationText;
    [SerializeField] private TextMeshProUGUI _RequestButtonText;
    [SerializeField] private TextMeshProUGUI _inputText;
    [SerializeField] private Button _requestButton;
    
    public void SetInputPopup(string explanation, string buttonText, System.Action<string> buttonAction)
    {
        _explanationText.text = explanation;
        _RequestButtonText.text = buttonText;
        
        _requestButton.onClick.RemoveAllListeners();
        _requestButton.onClick.AddListener(() =>
        {
            buttonAction(_inputText.text);
        });
    }
}
