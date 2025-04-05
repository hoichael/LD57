using UnityEngine;

public class g_refs : ut_singleton<g_refs>
{
    [field:SerializeField] public Transform pl_trans { get; private set; }
    [field:SerializeField] public room_manager room_manager { get; private set; }
}
