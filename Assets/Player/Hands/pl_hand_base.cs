using UnityEngine;
using UnityEngine.InputSystem;

public class pl_hand_base : MonoBehaviour
{
    [Header("REFS")]
    [SerializeField] protected pl_refs refs;
    [SerializeField] protected Animator anim;

    [Header("SETTINGS IDLE")]
    [SerializeField] protected string anim_name_idle;
    [SerializeField] protected Vector3 idle_pos, idle_rot;

    [Header("SETTINGS ACTIVE")]
    [SerializeField] protected string anim_name_active;
    [SerializeField] protected Vector3 active_pos, active_rot;
    [SerializeField] protected float active_duration;

    [Header("SETTINGS RELOAD")]
    [SerializeField] protected string anim_name_reload;
    [SerializeField] protected Vector3 reload_pos, reload_rot;
    [SerializeField] protected float reload_duration;

    [Header("SETTINGS MISCC")]
    [SerializeField] protected string ip_activator_name;

    protected pl_hand_state state_current;

    float timer_current;

    protected virtual void Start()
    {
        init_idle();
    }

    protected virtual void Update()
    {
        switch (state_current)
        {
            case pl_hand_state.idle:
                if (InputSystem.actions.FindAction(ip_activator_name).WasPressedThisFrame())
                {
                    init_active();
                }
                break;

            case pl_hand_state.active:
                handle_timer_active();
                break;

            case pl_hand_state.reload:
                handle_timer_reload();
                break;
        }
    }

    protected virtual void init_active()
    {
        init_generic(anim_name_active, active_pos, active_rot);
        timer_current = active_duration;
        state_current = pl_hand_state.active;
    }

    protected virtual void init_reload()
    {
        init_generic(anim_name_reload, reload_pos, reload_rot);
        timer_current = reload_duration;
        state_current = pl_hand_state.reload;
    }

    protected virtual void init_idle()
    {
        init_generic(anim_name_idle, idle_pos, idle_rot);
        state_current = pl_hand_state.idle;
    }

    protected virtual void init_generic(string anim_name, Vector3 pos, Vector3 rot)
    {
        anim.Play(anim_name);
        anim.transform.localPosition = pos;
        anim.transform.localRotation = Quaternion.Euler(rot);
    }

    protected virtual void handle_timer_active()
    {
        timer_current -= Time.deltaTime;
        if(timer_current <= 0)
        {
            //Debug.Break();
            //return;
            init_reload();
        }
    }

    protected virtual void handle_timer_reload()
    {
        timer_current -= Time.deltaTime;
        if (timer_current <= 0)
        {
            //Debug.Break();
            //return;
            init_idle();
        }
    }
}

public enum pl_hand_state
{
    idle = 0,
    active = 1,
    reload = 2,
}