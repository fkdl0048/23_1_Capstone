using TMPro;
using UnityEngine;

public class NotiToolTip : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _notiText;
    
    public void SetNotiText(string warningText)
    {
        _notiText.text = warningText;
    }
}
