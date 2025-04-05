using UnityEngine;

public class room : MonoBehaviour
{
    [Header("REFS")]
    [SerializeField] GameObject container;

    [Header("SETTINGS")]
    [SerializeField] Transform trans_pl_spawn_ref;

    public void on_exit_trigger()
    {
        print("EXIT!");
    }
}
