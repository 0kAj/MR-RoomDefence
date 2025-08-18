using UnityEngine;

public class UIVisibilityToggle : MonoBehaviour
{
    public GameObject uiRoot; // Hier das übergeordnete UI-Objekt (z.B. vrUI)

    private bool isVisible = true;

    //void Update()
    //{
        // Beispiel: Toggle bei Tastendruck (auf PC: Taste "X")
        //if (Input.GetKeyDown(KeyCode.X))
        //{
        //    ToggleUI();
        //}

        // Beispiel: Toggle über Oculus Touch Controller (Button A)
        //if (OVRInput.GetDown(OVRInput.Button.One)) // "A"-Button rechts
        //{
        //    ToggleUI();
        //}
    //}

    public void ToggleUI()
    {
        isVisible = !isVisible;
        uiRoot.SetActive(isVisible);
    }
    
    // Kannst du als public Methode im selben Script nutzen
    public void HideUI()
    {
        isVisible = false;
        uiRoot.SetActive(false);
    }
    
    public void ShowUI()
    {
        isVisible = true;
        uiRoot.SetActive(true);
    }

}
