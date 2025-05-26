using System;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif


public class EventManager : MonoBehaviour
{
    public static EventManager INSTANCE { get; private set; }

    public event Action StartGameListener;

    private void Awake()
    {
        if (INSTANCE != null && INSTANCE != this)
        {
            Destroy(gameObject);
            return;
        }

        INSTANCE = this;
        DontDestroyOnLoad(gameObject);
    }

    public void TriggerStartGame()
    {
        Debug.Log("StartGame Triggered");
        StartGameListener?.Invoke();
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
