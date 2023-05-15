using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UINavigation
{
    private UIView _currentView;
    private readonly Stack<UIView> _viewStack = new Stack<UIView>();
    private readonly Queue<UIView> _popupQueue = new Queue<UIView>();
    
    private readonly List<UIView> _uiViews = new List<UIView>();

    public UIView UIViewPush(string uiViewName)
    {
        UIView view = null;
        
        view = _uiViews.Find((x) => x.name == uiViewName);
        if (view == null)
        {
            view = GameManager.UI.GetUIView(uiViewName);
            _uiViews.Add(view);
        }

        if (_currentView != null)
        {
            _currentView.Hide();
            _viewStack.Push(_currentView);
        }

        _currentView = view;
        _currentView.Show();

        return view;
    }

    public UIView UIViewPop()
    {
        if (_currentView != null)
        {
            _currentView.Hide();
        }

        if (_viewStack.Count > 1)
        {
            _currentView = _viewStack.Pop();
            _currentView.Show();
        }
        
        return _currentView;
    }
    
    public UIView PopupPush(string uiViewName)
    {
        UIView view = null;
        
        view = _uiViews.Find((x) => x.name == uiViewName);
        if (view == null)
        {
            view = GameManager.UI.GetUIView(uiViewName);
            _uiViews.Add(view);
        }
        
        _popupQueue.Enqueue(view);
        view.Show();

        return view;
    }
    
    public UIView PopupPop()
    {
        UIView view = null;
        if (_popupQueue.Count > 0)
        {
            view = _popupQueue.Dequeue();
            view.Hide();
        }
        
        return view;
    }
}
