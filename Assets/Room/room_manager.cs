using UnityEngine;
using System.Collections.Generic;

public class room_manager : MonoBehaviour
{
    [Header("SETTINGS")]
    [SerializeField] List<room_layer_list> list_layer = new List<room_layer_list>();

    int layer_current;
    int layer_max = 1337;

    public void on_room_exit()
    {
        print("ROOM EXIT! (from manager)");
        layer_current = Mathf.Clamp(layer_current + 1, 0, layer_max);
        enter_new_room();
    }

    void enter_new_room()
    {
        int idx_random = 0;

        room room_new = list_layer[layer_current].list_room[idx_random];
        room_new.init();     
        g_refs.i.pl_trans.GetComponent<Rigidbody>().position = room_new.trans_pl_spawn_ref.position;
    }
}

[System.Serializable]
public class room_layer_list
{
    [field:SerializeField] public List<room> list_room { get; private set; }
}
