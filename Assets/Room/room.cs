using UnityEngine;
using System.Collections.Generic;

public class room : MonoBehaviour
{
    [Header("REFS")]
    [SerializeField] GameObject container;
    [SerializeField] List<Terrain> list_terrain;

    [Header("SETTINGS")]
    [field:SerializeField] public Transform trans_pl_spawn_ref { get; private set; }
    [SerializeField] Material mat_terrain;
    [field: SerializeField] public Color color_cam_bg { get; private set; } = Color.black;

    void Start()
    {
        for(int i = 0; i < list_terrain.Count; i++)
        {

            list_terrain[i].gameObject.SetActive(true);
            list_terrain[i].gameObject.layer = LayerMask.NameToLayer("Solid");

            list_terrain[i].materialTemplate = mat_terrain; 

            if(i != 0)
            {
                list_terrain[i].GetComponent<TerrainCollider>().enabled = false;
            }
        }    
    }

    public void init()
    {
        container.SetActive(true);
    }

    public void on_exit_trigger()
    {
        print("ROOM EXIT! (from room)");
        g_refs.i.room_manager.on_room_exit();
        //container.SetActive(false);
        Destroy(gameObject);
    }
}
