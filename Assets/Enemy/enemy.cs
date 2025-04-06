using UnityEngine;
using System.Collections;

public class enemy : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    [SerializeField] g_fx fx_death;
    [SerializeField] float move_speed;
    [SerializeField] float pl_kill_dist;
    [SerializeField] AudioSource audiosrc;
    [SerializeField] float pl_chase_dist;

    bool killed_pl;
    bool currently_being_pushed;

    bool aggroed;

    bool played_idle_sfx;

    public void handle_hit_by_pl_shoot()
    {
        g_fx instance = Instantiate(fx_death, transform.position, Quaternion.identity);
        instance.init();
        g_refs.i.pl_cam_depth.init_transition_bounce(g_refs.i.pl_cam_depth.get_depth_base() * 5);
        Destroy(rb.gameObject);
    }

    public void handle_hit_by_pl_push(Vector3 dir)
    {
        currently_being_pushed = true;
        rb.AddForce(dir.normalized * 20, ForceMode.Impulse);
        rb.AddTorque(new Vector3(Random.Range(-1, 1), Random.Range(-1, 1), Random.Range(-1, 1)) * 5, ForceMode.Impulse);

        StartCoroutine(handle_push_duration());
    }

    void FixedUpdate()
    {
        if (currently_being_pushed)
        {
            rb.AddTorque(-rb.angularVelocity * 0.5f);
            return;
        }

        Vector3 dir_to_pl = (g_refs.i.pl_trans.position + Vector3.up) - transform.position;

        float dist_to_pl = dir_to_pl.magnitude;

        if(!aggroed)
        {
            if(dist_to_pl > pl_chase_dist)
            {
                return;
            }
            else
            {
                aggroed = true;
            }
        }

        rb.rotation = Quaternion.Slerp(rb.rotation, Quaternion.LookRotation(dir_to_pl, Vector3.up), 8 * Time.deltaTime);

        if (killed_pl) return;
    
        if(dist_to_pl < pl_kill_dist)
        {
            g_refs.i.pl_trans.GetComponentInChildren<pl_death>().kill_player(transform.position);
            rb.linearVelocity = Vector3.zero;
        }
        else
        {
            rb.AddForce(dir_to_pl.normalized * move_speed);
        
            
            if(!played_idle_sfx && dist_to_pl < pl_kill_dist * 5)
            {
                audiosrc.Play();
                played_idle_sfx = true;
            }
        }
    }

    IEnumerator handle_push_duration()
    {
        yield return new WaitForSeconds(1.5f);

        rb.angularVelocity = Vector3.zero;
        currently_being_pushed = false;
    }
}
