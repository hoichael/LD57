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

        handle_game_quit();
    }

    void handle_game_quit()
    {
        if (InputSystem.actions.FindAction("Exit").WasPressedThisFrame())
        {
            Application.Quit();
        }
    }

}
