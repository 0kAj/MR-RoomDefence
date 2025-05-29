using System;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif


public class EventManager : MonoBehaviour
{
    public static EventManager Instance { get; private set; }

    public event Action StartGameListener;
    public event Action GameOverListener;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void TriggerStartGame()
    {
        Debug.Log("StartGame Triggered");
        StartGameListener?.Invoke();
    }

    public void TriggerGameOver(bool win)
    {
        Debug.Log("GameOver: win=" + win);
        GameOverListener?.Invoke();
    }
}

#if UNITY_EDITOR

[CustomEditor(typeof(EventManager))]
public class EventManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        EventManager manager = (EventManager)target;
        if (GUILayout.Button("Trigger Start Game"))
        {
            manager.TriggerStartGame();
        }
    }
}
#endif
