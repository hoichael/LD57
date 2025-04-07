using UnityEngine;

public class nv_killbox : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        g_refs.i.pl_trans.GetComponentInChildren<pl_death>().kill_player(transform.position, true);
    }
}
