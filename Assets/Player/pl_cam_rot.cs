using UnityEngine;
using UnityEngine.InputSystem;

public class pl_cam_rot : MonoBehaviour
{
    [Header("REFS")]
    [SerializeField] pl_refs refs;
    [SerializeField] Transform cam_holder;

    [Header("SETTINGS")]
    [SerializeField] float sens;

    float rot_current_x, rot_current_y;

    Quaternion death_rot_target;

    void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void init_death_rot(Vector3 pos_target)
    {
        death_rot_target = Quaternion.LookRotation(pos_target - cam_holder.position, Vector3.up);
    }

    void Update()
    {
        if(death_rot_target.eulerAngles != Vector3.zero) // l0l
        {
            exec_death_rot();
        }
        else
        {
            handle_input();
            apply_rotation();
        }
    }

    void handle_input()
    {
        Vector2 input = InputSystem.actions.FindAction("Look").ReadValue<Vector2>();

        rot_current_y += input.x * sens;
        rot_current_x -= input.y * sens;

        rot_current_x = Mathf.Clamp(rot_current_x, -89.5f, 89.5f);
    }

    void apply_rotation()
    {
        cam_holder.localRotation = Quaternion.Euler(rot_current_x, rot_current_y, 0);

        refs.trans_orientation_ref.rotation = Quaternion.Euler(0, rot_current_y, 0);
    }

    void exec_death_rot()
    {
        cam_holder.rotation = Quaternion.Slerp(cam_holder.rotation, death_rot_target, 12 * Time.deltaTime);
    }
}
