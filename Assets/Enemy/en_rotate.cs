using UnityEngine;

public class en_rotate : MonoBehaviour
{
    [SerializeField] Rigidbody rb;

    void FixedUpdate()
    {
        Vector3 dir_to_pl = (g_refs.i.pl_trans.position + Vector3.up) - transform.position;

        rb.rotation = Quaternion.LookRotation(dir_to_pl, Vector3.up);
    }
}
