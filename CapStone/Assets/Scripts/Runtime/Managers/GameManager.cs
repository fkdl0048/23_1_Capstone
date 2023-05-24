using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance { get { Init(); return _instance; } }

    private readonly InputManager _input = new InputManager();
    private readonly ResourceManager _resource = new ResourceManager();
    private readonly DataManager _data = new DataManager();
    private readonly SoundManager _sound = new SoundManager();
    private readonly UIManager _ui = new UIManager();
    private readonly PlayfabManager _playfab = new PlayfabManager();
    private readonly PlayerManager _player= new PlayerManager();

    public static InputManager Input => _instance._input;
    public static ResourceManager Resource => _instance._resource;
    public static DataManager Data => _instance._data;
    public static SoundManager Sound => _instance._sound;
    public static UIManager UI => _instance._ui;
    public static PlayfabManager Playfab => _instance._playfab;
    public static PlayerManager Player => _instance._player;

    static void Init()
    {
        if (_instance == null)
        {
            GameObject go = GameObject.Find("@GameManager");
            if (go == null)
            {
                go = new GameObject { name = "@GameManager" };
                go.AddComponent<GameManager>();
            }

            DontDestroyOnLoad(go);
            _instance = go.GetComponent<GameManager>();
            
            // 추가적인 매니저 초기화
            _instance._playfab.Init();
            _instance._ui.Init();
        }
    }

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Init();
    }
    
    public void Clear()
    {
        
    }
}
