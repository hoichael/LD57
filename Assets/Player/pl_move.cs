using UnityEngine;
using UnityEngine.InputSystem;

public class pl_move : MonoBehaviour
{
    [Header("REFS")]
    [SerializeField] pl_refs refs;

    [Header("SETTINGS")]
    [SerializeField] float move_force_ground;
    [SerializeField] float move_force_air;

    [SerializeField] float drag_ground, drag_air;

    void FixedUpdate()
    {
        if (refs.groundcheck.is_grounded())
        {
            refs.gravity.enabled = false;
            refs.rb.linearDamping = drag_ground;
            apply_move_ground(get_dir());
        }
        else
        {
            refs.gravity.enabled = true;
            refs.rb.linearDamping = drag_air;
            apply_move_air(get_dir());
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
        refs.rb.AddForce(dir * move_force_ground);
    }

    void apply_move_air(Vector3 dir)
    {
        refs.rb.AddForce(dir * move_force_air);
    }
}
