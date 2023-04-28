using UnityEngine;

public class UIManager
{
    private WarningToolTip _warningToolTip;
    private NotiToolTip _notiToolTip;
    
    private const string WarningToolTipPath = "Prefabs/UI/WarningToolTip";
    private const string NotiToolTipPath = "Prefabs/UI/NotiToolTip";

    public void Init()
    {
        _warningToolTip = GameManager.Resource.Load<WarningToolTip>(WarningToolTipPath);
        _warningToolTip = GameObject.Instantiate(_warningToolTip);
        _warningToolTip.gameObject.SetActive(false);

        _notiToolTip = GameManager.Resource.Load<NotiToolTip>(NotiToolTipPath);
        _notiToolTip = GameObject.Instantiate(_notiToolTip);
        _notiToolTip.gameObject.SetActive(false);
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
}
