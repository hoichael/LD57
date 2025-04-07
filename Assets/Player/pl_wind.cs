using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class pl_wind : MonoBehaviour
{
    [Header("REFS")]
    [SerializeField] ui_circle circle_renderer;
    [SerializeField] AudioSource sfx_charge;
    [SerializeField] List<AudioSource> list_sfx_release;

    [Header("SETTINGS")]
    [SerializeField] float charge_duration_max;
    [SerializeField] float reticle_radius_min, reticle_radius_max;
    [SerializeField] AnimationCurve curve_reticle_radius;
    [SerializeField] float cooldown_duration;
 
    bool currently_charging;
    float charge_duration_current;

    float cooldown_timer_current;

    void Update()
    {
        if(currently_charging)
        {
            if (!InputSystem.actions.FindAction("Attack").IsPressed())
            {
                handle_release();
            }
            else
            {
                handle_charge();
            }
        }
        else
        {
            if(cooldown_timer_current < cooldown_duration)
            {
                cooldown_timer_current += Time.deltaTime;
            }
            else if (InputSystem.actions.FindAction("Attack").IsPressed())
            {
                init_charge();
            }
        }
    }

    void handle_charge()
    {
        charge_duration_current += Time.deltaTime;
    
        if(charge_duration_current > charge_duration_max)
        {
            handle_release();
        }
    }

    void init_charge()
    {
        print("wind CHARGE");

        currently_charging = true;

        charge_duration_current = 0;

        circle_renderer.gameObject.SetActive(true);
        circle_renderer.update_circle(reticle_radius_min);
    }

    void handle_release()
    {
        print("wind RELEASE");

        currently_charging = false;

        circle_renderer.gameObject.SetActive(false);


        cooldown_timer_current = 0;
    }
}
