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
    [SerializeField] List<AudioSource> list_sfx_release;

    [Header("SETTINGS")]
    [SerializeField] AnimationCurve curve_charge;
    [SerializeField] float charge_duration_min;
    [SerializeField] float charge_duration_max;
    [SerializeField] float reticle_radius_min, reticle_radius_max;
    [SerializeField] float cooldown_duration;
    [SerializeField] float col_halfextents_min, col_halfextents_max;
    [SerializeField] LayerMask mask_hit;

    const float col_depth_halfextents = 12; 

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
            //float charge_factor = charge_duration_current / charge_duration_max;

            //float reticle_radius_new = Mathf.Lerp(
            //    reticle_radius_min,
            //    reticle_radius_max,
            //    curve_charge.Evaluate(charge_factor)
            //    );

            circle_renderer.update_circle(get_lerped_value(reticle_radius_min, reticle_radius_max));
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

        float halfextents = get_lerped_value(col_halfextents_min, col_halfextents_max);

        dev_ref_cube.position = refs.cam.transform.position + refs.cam.transform.forward * col_depth_halfextents;
        dev_ref_cube.rotation = Quaternion.LookRotation(refs.cam.transform.forward);
        dev_ref_cube.localScale = new Vector3(
            halfextents * 2,
            halfextents * 2,
            col_depth_halfextents * 2
            );

        Debug.Break();

        //int hit_en_amount = Physics.OverlapBoxNonAlloc(refs.cam.transform.position + refs.cam.transform.forward * col_halfextents.z, col_halfextents, arr_col_check, Quaternion.LookRotation(refs.cam.transform.forward), mask_hit);

        //for (int i = 0; i < hit_en_amount; i++)
        //{
        //    arr_col_check[i].GetComponentInChildren<enemy>().handle_hit_by_pl_push(refs.cam.transform.forward);
        //}
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