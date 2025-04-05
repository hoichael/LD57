using UnityEngine;

public class pl_gravity : MonoBehaviour
{
    [Header("REFS")]
    [SerializeField] pl_refs refs;

    [Header("SETTINGS")]
    [SerializeField] float force_base;
    [SerializeField] float force_max;
    [SerializeField] float force_growth_factor;

    [Header("DEV")]
    [SerializeField] float grav_current; // serialized for db

    void OnEnable()
    {
        grav_current = force_base;
    }
    void FixedUpdate()
    {
        handle_growth();
        apply_force();
    }

    void handle_growth()
    {
        grav_current = Mathf.MoveTowards(grav_current, force_max, force_growth_factor);
    }

    void apply_force()
    {
        refs.rb.AddForce(Vector3.down * grav_current);
    }
}
