using TMPro;
using UnityEngine;

public class DefalutPopup : UIView
{
    [SerializeField] private TextMeshProUGUI setText;
    
    public void SetText(string text)
    {
        setText.text = text;
    }
}
