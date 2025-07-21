using UnityEngine;
using UnityEngine.SceneManagement;

public class DebugRunner : MonoBehaviour
{

    void Update()
    {
        if (OVRInput.GetDown(OVRInput.RawButton.Y))
        {
            Global.INSTANCE.Reload();
        }

        if (OVRInput.GetDown(OVRInput.RawButton.X))
        {
            EventManager.Instance.TriggerStartGame();
        }
    }
}
