using UnityEngine;

public class enemy : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    [SerializeField] float move_speed;
    [SerializeField] float pl_kill_dist;

    void FixedUpdate()
    {
        Vector3 dir_to_pl = (g_refs.i.pl_trans.position + Vector3.up) - transform.position;
    
        if(dir_to_pl.magnitude < pl_kill_dist)
        {
            g_refs.i.pl_trans.GetComponentInChildren<pl_death>().kill_player(transform.position);
        }
        else
        {
            rb.rotation = Quaternion.LookRotation(dir_to_pl, Vector3.up);
            rb.AddForce(dir_to_pl.normalized * move_speed);
        }
    }
}
