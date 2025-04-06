using UnityEngine;
using UnityEngine.InputSystem;

public class pl_jump : MonoBehaviour
{
    [Header("REFS")]
    [SerializeField] pl_refs refs;
    [SerializeField] AudioSource audiosrc;

    [Header("SETTINGS")]
    [SerializeField] float force;

    void Update()
    {
        if (InputSystem.actions.FindAction("Jump").WasPressedThisFrame() && refs.groundcheck.is_grounded())
        {
            exec_jump();
            audiosrc.Play();
        }
    }

    void exec_jump()
    {
        refs.rb.linearVelocity = new Vector3(refs.rb.linearVelocity.x, 0, refs.rb.linearVelocity.z);
        refs.rb.AddForce(Vector3.up * force, ForceMode.Impulse);
    }
}
