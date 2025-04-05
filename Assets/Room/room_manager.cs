using UnityEngine;
using System.Collections.Generic;

public class room_manager : MonoBehaviour
{
    [Header("SETTINGS")]
    [SerializeField] List<room_layer_list> list_layer = new List<room_layer_list>();

    int layer_current;
    int layer_max;

    public void on_room_exit()
    {
        print("ROOM EXIT! (from manager)");
    }

    void enter_new_room()
    {

    }
}

[System.Serializable]
public class room_layer_list
{
    [SerializeField] List<room> list_room;
}
