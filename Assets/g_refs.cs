using UnityEngine;

public class g_refs : ut_singleton<g_refs>
{
    [field:SerializeField] public Transform pl_trans { get; private set; }
    [field: SerializeField] public g_manager g_manager { get; private set; }
    [field:SerializeField] public room_manager room_manager { get; private set; }
    [field: SerializeField] public pl_cam_depth pl_cam_depth { get; private set; }
}
