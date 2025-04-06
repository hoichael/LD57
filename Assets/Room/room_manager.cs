using UnityEngine;
using System.Collections.Generic;

public class room_manager : MonoBehaviour
{
    [Header("SETTINGS")]
    [SerializeField] List<room_layer_list> list_layer = new List<room_layer_list>();
    [SerializeField] AnimationCurve curve_depth_by_layer;
    [SerializeField] float depth_max;
    [SerializeField] Camera cam;

    [SerializeField] Vector2 DEV_init_room_override;

    int layer_current = -1;
    int layer_max = 11;

    void Start()
    {
        enter_new_room();
        DEV_init_room_override = Vector2.zero;
    }

    public void on_room_exit()
    {
        print("ROOM EXIT! (from manager)");
        enter_new_room();
    }

    void enter_new_room()
    {
        int idx_random;

        if(DEV_init_room_override.x != 0)
        {
            layer_current = (int)DEV_init_room_override.x;
            idx_random = (int)DEV_init_room_override.y;
        }
        else
        {
            layer_current = Mathf.Clamp(layer_current + 1, 0, layer_max);
            idx_random = 0;
        }

        room room_new = Instantiate(list_layer[layer_current].list_room[idx_random], Vector3.zero, Quaternion.identity);
        room_new.init();     
        g_refs.i.pl_trans.GetComponent<Rigidbody>().position = room_new.trans_pl_spawn_ref.position;

        cam.backgroundColor = room_new.color_cam_bg;

        set_new_cam_depth();
    }

    void set_new_cam_depth()
    {
        float factor = ((float)layer_max - (float)layer_current) / (float)layer_max;
        float depth_new = depth_max * curve_depth_by_layer.Evaluate(factor);
        g_refs.i.pl_cam_depth.set_depth_base(depth_new, true);
    }
}

[System.Serializable]
public class room_layer_list
{
    [field:SerializeField] public List<room> list_room { get; private set; }
}
