using UnityEngine;
using UnityEngine.InputSystem;

public class pl_move : MonoBehaviour
{
    [Header("REFS")]
    [SerializeField] pl_refs refs;

    [Header("SETTINGS")]
    [SerializeField] float move_force_ground;

    void FixedUpdate()
    {
        apply_move_ground(get_dir());

        print("grounded? " + refs.groundcheck.is_grounded());
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
}
