using UnityEngine;

public class room : MonoBehaviour
{
    [Header("REFS")]
    [SerializeField] GameObject container;

    [Header("SETTINGS")]
    [field:SerializeField] public Transform trans_pl_spawn_ref { get; private set; }

    public void init()
    {
        container.SetActive(true);
    }

    public void on_exit_trigger()
    {
        print("ROOM EXIT! (from room)");
        container.SetActive(false);
        g_refs.i.room_manager.on_room_exit();
    }
}
