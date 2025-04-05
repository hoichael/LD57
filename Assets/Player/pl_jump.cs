using UnityEngine;
using UnityEngine.InputSystem;

public class pl_jump : MonoBehaviour
{
    [Header("REFS")]
    [SerializeField] pl_refs refs;

    [Header("SETTINGS")]
    [SerializeField] float force;

    void Update()
    {
        if (InputSystem.actions.FindAction("Jump").WasPressedThisFrame() && refs.groundcheck.is_grounded())
        {
            exec_jump();
        }
    }

    void exec_jump()
    {
        refs.rb.linearVelocity = new Vector3(refs.rb.linearVelocity.x, 0, refs.rb.linearVelocity.z);
        refs.rb.AddForce(Vector3.up * force, ForceMode.Impulse);
    }
}
