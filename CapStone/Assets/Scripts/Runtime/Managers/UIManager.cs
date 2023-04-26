using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager
{
    private WarningToolTip _warningToolTip;
    private const string WarningToolTipPath = "Prefabs/UI/WarningToolTip";
    public void Init()
    {
        _warningToolTip = GameManager.Resource.Load<WarningToolTip>(WarningToolTipPath);
        _warningToolTip = GameObject.Instantiate(_warningToolTip);
        _warningToolTip.gameObject.SetActive(false);
    }
    
    public void SetWarningText(string warningText)
    {
        _warningToolTip.SetWarningText(warningText);
        _warningToolTip.gameObject.SetActive(true);
    }
}
