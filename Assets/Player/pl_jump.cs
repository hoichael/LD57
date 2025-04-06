using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class pl_jump : MonoBehaviour
{
    [Header("REFS")]
    [SerializeField] pl_refs refs;
    [SerializeField] List<AudioSource> list_audiosrc;

    [Header("SETTINGS")]
    [SerializeField] float force;

    int sfx_last_idx;

    void Update()
    {
        if (InputSystem.actions.FindAction("Jump").WasPressedThisFrame() && refs.groundcheck.is_grounded())
        {
            exec_jump();
            handle_sfx();
        }
    }

    void exec_jump()
    {
        refs.rb.linearVelocity = new Vector3(refs.rb.linearVelocity.x, 0, refs.rb.linearVelocity.z);
        refs.rb.AddForce(Vector3.up * force, ForceMode.Impulse);
    }

    void handle_sfx()
    {
        sfx_last_idx++;

        if (sfx_last_idx == list_audiosrc.Count)
        {
            sfx_last_idx = 0;
        }

        list_audiosrc[sfx_last_idx].Play();
    }
}
