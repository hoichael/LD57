using UnityEngine;

public class en_move : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    [SerializeField] float speed;

    void FixedUpdate()
    {
        // should cache this in some data class shared w/ other systems on same en
        Vector3 dir_to_pl = g_refs.i.pl_trans.position - transform.position;

        rb.AddForce(dir_to_pl.normalized * speed);
    }
}
