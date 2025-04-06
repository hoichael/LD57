using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class g_manager : MonoBehaviour
{
    [SerializeField] float death_exit_delay;

    public void handle_death()
    {
        StartCoroutine(handle_death_exit_delay());
    }

    void Update()
    {
        scene_reload();
        handle_game_quit();
    }

    void handle_game_quit()
    {
        if(InputSystem.actions.FindAction("Exit").WasPressedThisFrame())
        {
            Application.Quit();
        }
    }

    IEnumerator handle_death_exit_delay()
    {
        yield return new WaitForSeconds(death_exit_delay);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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
