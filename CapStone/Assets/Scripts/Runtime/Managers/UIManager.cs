using UnityEngine;
using System.Threading.Tasks;

public class UIManager
{
    private WarningToolTip _warningToolTip;
    private NotiToolTip _notiToolTip;
    private InputToolTip _inputToolTip;
    
    private const string WarningToolTipPath = "Prefabs/UI/WarningToolTip";
    private const string NotiToolTipPath = "Prefabs/UI/NotiToolTip";
    private const string InputToolTipPath = "Prefabs/UI/InputToolTip";

    public void Init()
    {
        _warningToolTip = GameManager.Resource.Load<WarningToolTip>(WarningToolTipPath);
        _warningToolTip = GameObject.Instantiate(_warningToolTip);
        _warningToolTip.gameObject.SetActive(false);

        _notiToolTip = GameManager.Resource.Load<NotiToolTip>(NotiToolTipPath);
        _notiToolTip = GameObject.Instantiate(_notiToolTip);
        _notiToolTip.gameObject.SetActive(false);
        
        _inputToolTip = GameManager.Resource.Load<InputToolTip>(InputToolTipPath);
        _inputToolTip = GameObject.Instantiate(_inputToolTip);
        _inputToolTip.gameObject.SetActive(false);
    }
    
    public void SetWarningText(string warningText)
    {
        _warningToolTip.SetWarningText(warningText);
        _warningToolTip.gameObject.SetActive(true);
    }
    
    public void SetNotiText(string notiText)
    {
        _notiToolTip.SetNotiText(notiText);
        _notiToolTip.gameObject.SetActive(true);
    }
    
    public void SetInputTextAndAction(string explanation, string buttonText, System.Func<string, bool> buttonAction)
    {
        _inputToolTip.SetInputToolTip(explanation, buttonText, buttonAction);
        _inputToolTip.gameObject.SetActive(true);
    }
}
