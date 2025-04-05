using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class g_manager : MonoBehaviour
{
    void Update()
    {
        scene_reload();
    }

    #region DEV
    void scene_reload()
    {
        if (InputSystem.actions.FindAction("Reload").WasPressedThisFrame())
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
    #endregion DEV
}
