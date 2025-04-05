using UnityEngine;
using UnityEngine.InputSystem;

public class pl_cam_rot : MonoBehaviour
{
    [Header("REFS")]
    [SerializeField] Transform cam_holder;
    [SerializeField] Transform trans_orientation_ref;

    [Header("SETTINGS")]
    [SerializeField] float sens;

    float rot_current_x, rot_current_y;

    void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        handle_input();
        apply_rotation();
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

        trans_orientation_ref.rotation = Quaternion.Euler(0, rot_current_y, 0);
    }
}
