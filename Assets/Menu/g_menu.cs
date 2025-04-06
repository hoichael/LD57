using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class g_menu : MonoBehaviour
{
    [SerializeField] List<Terrain> list_terrain;
    [SerializeField] Material mat_black;
    [SerializeField] float terrain_scroll_speed;

    float terrain_size = 80;
    float terrain_scroll_breakpoint;

    int idx_terrain_front = 0;
    int idx_terrain_back = 1;

    void Start()
    {
        foreach (Terrain terrain in list_terrain)
        {
            terrain.materialTemplate = mat_black;
        }

        terrain_scroll_breakpoint = (terrain_size + 10) * -1;
    }

    void Update()
    {
        if(InputSystem.actions.FindAction("Enter").WasPressedThisFrame())
        {
            SceneManager.LoadScene(1);
        }

        handle_terrain_scroll();
        handle_game_quit();
    }

    void handle_game_quit()
    {
        if (InputSystem.actions.FindAction("Exit").WasPressedThisFrame())
        {
            Application.Quit();
        }
    }

    void handle_terrain_scroll()
    {
        foreach (Terrain terrain in list_terrain)
        {
            terrain.transform.position += Vector3.forward * -terrain_scroll_speed * Time.deltaTime;
        }

        if (list_terrain[idx_terrain_front].transform.position.z < terrain_scroll_breakpoint)
        {
            list_terrain[idx_terrain_front].transform.position = list_terrain[idx_terrain_back].transform.position + Vector3.forward * terrain_size;

            (idx_terrain_front, idx_terrain_back) = (idx_terrain_back, idx_terrain_front);
        }
    }
}
