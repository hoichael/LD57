using UnityEngine;
using System.Collections.Generic;

public class room_manager : MonoBehaviour
{
    [Header("REFS")]
    [SerializeField] SO_room_init_override init_override_data;

    [Header("SETTINGS")]
    [SerializeField] List<room_layer_list> list_layer = new List<room_layer_list>();
    //[SerializeField] AnimationCurve curve_depth_by_layer;
    [SerializeField] float depth_max;
    [SerializeField] Camera cam;
    [SerializeField] pl_cam_rot cam_rot;

    //[SerializeField] Vector2 DEV_init_room_override;

    int layer_current = -1;
    int layer_max = 7;

    int room_current;

    void Start()
    {
        enter_new_room(true);
        //DEV_init_room_override = Vector2.zero;
    }

    public void on_pl_death()
    {
        init_override_data.layer = layer_current;
        init_override_data.room = room_current;
    }

    public void on_room_exit()
    {
        print("ROOM EXIT! (from manager)");
        enter_new_room(false);
    }

    void enter_new_room(bool from_start)
    {
        int idx_random;

        if(init_override_data.layer != 0)
        {
            layer_current = (int)init_override_data.layer;
            idx_random = (int)init_override_data.room;
        }
        else
        {
            layer_current = Mathf.Clamp(layer_current + 1, 0, layer_max);
            idx_random = 0;
        }

        room_current = idx_random;

        //init_override_data.layer = 0;
        //init_override_data.room = 0;

        room room_new = Instantiate(list_layer[layer_current].list_room[idx_random], Vector3.zero, Quaternion.identity);
        room_new.init();     
        g_refs.i.pl_trans.GetComponent<Rigidbody>().position = room_new.trans_pl_spawn_ref.position;

        cam.backgroundColor = room_new.color_cam_bg;

        set_new_cam_depth(room_new.render_depth);

        if(from_start && layer_current == 1)
        {
            cam_rot.randomize_look_dir();
        }
    }

    void set_new_cam_depth(float depth_new)
    {
        g_refs.i.pl_cam_depth.set_depth_base(0.5f, true);

        //float factor = ((float)layer_max - (float)layer_current) / (float)layer_max;
        //float depth_new = depth_max * curve_depth_by_layer.Evaluate(factor);

        //if(layer_current == 0)
        if(depth_new > 56)
        {
            g_refs.i.pl_cam_depth.set_depth_base(depth_new, true);
        }
        else
        {
            g_refs.i.pl_cam_depth.set_depth_base(depth_new, false);
        }
    }
}

[System.Serializable]
public class room_layer_list
{
    [field:SerializeField] public List<room> list_room { get; private set; }
}
