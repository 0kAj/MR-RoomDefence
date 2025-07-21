using TMPro;
using UnityEngine;

[RequireComponent(typeof(UIVisibilityToggle))]
public class GameMenu : MonoBehaviour
{
    [SerializeField] private TMP_Text gamoverText;
    
    void Start()
    {
        EventManager.Instance.StartGameListener += () => GetComponent<UIVisibilityToggle>().HideUI();
        
        EventManager.Instance.GameOverListener += GameOver;
        
        GetComponent<UIVisibilityToggle>().HideUI();
        
        
    }

    void GameOver()
    {
        GetComponent<UIVisibilityToggle>().ShowUI();
        gamoverText.text = EventManager.Instance.has_win ? "GAME OVER -  Win" : "GAME OVER -  Lose";
    }
}
