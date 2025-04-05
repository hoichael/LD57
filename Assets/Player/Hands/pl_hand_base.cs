using UnityEngine;
using UnityEngine.InputSystem;

public class pl_hand_base : MonoBehaviour
{
    [Header("REFS")]
    [SerializeField] Animator anim;

    [Header("SETTINGS")]
    [SerializeField] protected string anim_name_idle;
    [SerializeField] protected string anim_name_active;
    [SerializeField] protected string anim_name_reload;
    [SerializeField] protected float active_duration;
    [SerializeField] protected float reload_duration;
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
        anim.Play(anim_name_active);
        timer_current = active_duration;
        state_current = pl_hand_state.active;
    }

    protected virtual void init_reload()
    {
        anim.Play(anim_name_reload);
        timer_current = reload_duration;
        state_current = pl_hand_state.reload;
    }

    protected virtual void init_idle()
    {
        anim.Play(anim_name_idle);
        state_current = pl_hand_state.idle;
    }

    protected virtual void handle_timer_active()
    {
        timer_current -= Time.deltaTime;
        if(timer_current <= 0)
        {
            init_reload();
        }
    }

    protected virtual void handle_timer_reload()
    {
        timer_current -= Time.deltaTime;
        if (timer_current <= 0)
        {
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