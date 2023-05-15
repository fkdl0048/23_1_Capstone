using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUIController : MonoBehaviour
{
    private UINavigation _uiNavigation;
    private GameUIView _gameUIView;
    // Start is called before the first frame update
    void Start()
    {
        _uiNavigation = new UINavigation();
        _gameUIView = _uiNavigation.UIViewPush("GameView") as GameUIView;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
