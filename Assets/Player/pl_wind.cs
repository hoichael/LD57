using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class pl_wind : MonoBehaviour
{
    [Header("REFS")]
    [SerializeField] Transform dev_ref_cube;
    [SerializeField] pl_refs refs;
    [SerializeField] ui_circle circle_renderer;
    [SerializeField] AudioSource sfx_charge;
    [SerializeField] AudioSource sfx_release_default;
    [SerializeField] AudioSource sfx_release_overcharged;
    [SerializeField] pl_move move;
    [SerializeField] pl_jump jump;

    [Header("SETTINGS")]
    [SerializeField] AnimationCurve curve_charge;
    [SerializeField] float charge_duration_min;
    [SerializeField] float charge_duration_max;
    [SerializeField] float reticle_radius_min, reticle_radius_max;
    [SerializeField] float cooldown_duration;
    [SerializeField] float col_halfextents_min, col_halfextents_max;
    [SerializeField] float knockback_min, knockback_max;
    [SerializeField] float drag_add_max;
    [SerializeField] AnimationCurve drag_curve;
    [SerializeField] LayerMask mask_hit;

    const float col_depth_halfextents = 10; 

    Collider[] arr_col_check = new Collider[33];

    bool currently_charging;
    float charge_duration_current;

    float cooldown_timer_current;

    void Update()
    {
        if(currently_charging)
        {
            if (!InputSystem.actions.FindAction("Attack").IsPressed() && charge_duration_current > charge_duration_min)
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
        else
        {
            circle_renderer.update_circle(get_lerped_value(reticle_radius_min, reticle_radius_max));

            float charge_factor = charge_duration_current / charge_duration_max;
            float drag_ground_add = Mathf.Lerp(
                0,
                drag_add_max,
                drag_curve.Evaluate(charge_factor)
                );

            move.set_add_drag_ground(drag_ground_add);
        }
    }

    void init_charge()
    {
        currently_charging = true;

        charge_duration_current = 0;

        circle_renderer.gameObject.SetActive(true);
        circle_renderer.update_circle(reticle_radius_min);

        sfx_charge.Play();

        jump.toggle_can_jump(false);
    }

    void handle_release()
    {
        currently_charging = false;
        circle_renderer.gameObject.SetActive(false);
        cooldown_timer_current = 0;

        move.set_add_drag_ground(0);
        jump.toggle_can_jump(true);

        sfx_charge.Stop();

        bool killed_en = false;

        float factor = charge_duration_current / charge_duration_max;

        if (factor < 0.97f)
        {
            sfx_release_default.volume = Mathf.Lerp(0.85f, 1f, factor);
            sfx_release_default.pitch = Mathf.Lerp(0.88f, 1.05f, factor);

            sfx_release_default.Play();
        }

        float halfextents = get_lerped_value(col_halfextents_min, col_halfextents_max);

        //dev_ref_cube.position = refs.cam.transform.position + refs.cam.transform.forward * col_depth_halfextents;
        //dev_ref_cube.rotation = Quaternion.LookRotation(refs.cam.transform.forward);
        //dev_ref_cube.localScale = new Vector3(
        //    halfextents * 2,
        //    halfextents * 2,
        //    col_depth_halfextents * 2
        //    );

        //Debug.Break();

        Vector3 halfextents_final = new Vector3(
            halfextents,
            halfextents,
            col_depth_halfextents
            );

        int hit_en_amount = Physics.OverlapBoxNonAlloc(refs.cam.transform.position + refs.cam.transform.forward * col_depth_halfextents, halfextents_final, arr_col_check, Quaternion.LookRotation(refs.cam.transform.forward), mask_hit);

        for (int i = 0; i < hit_en_amount; i++)
        {
            if(factor > 0.97f)
            {
                arr_col_check[i].GetComponentInChildren<enemy>().handle_hit_by_pl_shoot();

                killed_en = true;

                break;
            }
            else
            {
                float force = Mathf.Clamp(knockback_max * factor, knockback_min, knockback_max);
                force += Random.Range(-2f, 3f);

                float distance_factor = Vector3.Distance(refs.cam.transform.position, arr_col_check[i].transform.position) / (col_depth_halfextents * 2);

                force *= (1 - distance_factor);
                factor -= (distance_factor * 0.9f);

                arr_col_check[i].GetComponentInChildren<enemy>().handle_hit_by_pl_push(refs.cam.transform.forward, force, factor);
            }
        }

        if (factor > 0.97f)
        {
            if(killed_en)
            {
                sfx_release_default.volume = 1f;
                sfx_release_default.pitch = 1f;

                sfx_release_default.Play();
            }
            else
            {
                sfx_release_overcharged.Play();
            }
        }
    }

    #region UTIL
    float get_lerped_value(float min, float max)
    {
        float charge_factor = charge_duration_current / charge_duration_max;

        float value = Mathf.Lerp(
            min,
            max,
            curve_charge.Evaluate(charge_factor)
            );

        return value;
    }
    #endregion UTIL
}