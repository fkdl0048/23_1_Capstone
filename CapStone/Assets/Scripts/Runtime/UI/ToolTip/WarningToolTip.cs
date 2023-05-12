using TMPro;
using UnityEngine;

public class WarningToolTip : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _warningText;
    
    public void SetWarningText(string warningText)
    {
        _warningText.text = warningText;
    }
}
