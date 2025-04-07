using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class pl_move : MonoBehaviour
{
    [Header("REFS")]
    [SerializeField] pl_refs refs;
    [SerializeField] List<AudioSource> list_footsteps_src;
    [SerializeField] AudioSource hit_ground_src;

    [Header("SETTINGS")]
    [SerializeField] float move_force_ground;
    [SerializeField] float move_force_air;

    [SerializeField] float drag_ground, drag_air;

    [SerializeField] float footsteps_interval;

    int footstep_last_idx;
    float footstep_last_time;

    float drag_add_ground_current;

    bool grounded_last_frame = true;

    public void set_add_drag_ground(float value)
    {
        drag_add_ground_current = value;
    }

    void FixedUpdate()
    {
        if (refs.groundcheck.is_grounded())
        {
            refs.gravity.enabled = false;
            refs.rb.linearDamping = drag_ground + drag_add_ground_current;
            apply_move_ground(get_dir());
            
            if(!grounded_last_frame)
            {
                //hit_ground_src.Play();
                handle_footstep_sfx();
            }

            grounded_last_frame = true;
        }
        else
        {
            refs.gravity.enabled = true;
            refs.rb.linearDamping = drag_air;
            apply_move_air(get_dir());
            grounded_last_frame = false;
        }
    }

    Vector3 get_dir()
    {
        Vector2 input = InputSystem.actions.FindAction("Move").ReadValue<Vector2>();

        Vector3 dir =
            refs.trans_orientation_ref.forward * input.y +
            refs.trans_orientation_ref.right * input.x;

        return dir.normalized;
    }

    void apply_move_ground(Vector3 dir)
    {
        if(dir == Vector3.zero)
        {
            if(refs.rb.linearVelocity.magnitude < 0.25f && refs.rb.linearVelocity.magnitude > 0.1f)
            {
                refs.rb.linearVelocity = Vector3.zero;
            }
        }
        else
        {
            refs.rb.AddForce(dir * move_force_ground);

            handle_footstep_sfx();
        }
    }

    void handle_footstep_sfx()
    {
        if (Time.time - footstep_last_time > footsteps_interval)
        {
            footstep_last_idx++;

            if (footstep_last_idx == list_footsteps_src.Count)
            {
                footstep_last_idx = 0;
            }

            list_footsteps_src[footstep_last_idx].Play();
            footstep_last_time = Time.time;
        }
    }

    void apply_move_air(Vector3 dir)
    {
        refs.rb.AddForce(dir * move_force_air);
    }
}
