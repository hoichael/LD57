using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class g_menu : MonoBehaviour
{
    void Update()
    {
        if(InputSystem.actions.FindAction("Enter").WasPressedThisFrame())
        {
            SceneManager.LoadScene(1);
        }    
    }
}
