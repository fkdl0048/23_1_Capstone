using System;
using UnityEngine;

public class UIManager
{
    private const string UIViewPath = "Prefabs/UI/";
    
    public UIView GetUIView(string uiViewName)
    {
        try
        {
            var uiView = GameObject.Find(uiViewName)?.GetComponent<UIView>();

            if (uiView != null)
            {
                return uiView;
            }

            uiView = GameManager.Resource.Load<UIView>(UIViewPath + uiViewName);
            if (uiView == null)
            {
                throw new Exception("Resource 폴더에 없거나 씬에 존재하지 않는 UIView입니다..! : " + uiViewName + "을 확인해주세요!");
            }

            uiView = GameObject.Instantiate(uiView);
            uiView.name = uiViewName;
            
            return uiView;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}
