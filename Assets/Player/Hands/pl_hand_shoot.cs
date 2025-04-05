using UnityEngine;

public class pl_hand_shoot : pl_hand_base
{
    [Header("SHOOT")]
    [SerializeField] Transform origin;
    [SerializeField] ParticleSystem particles;
    [SerializeField] LayerMask hit_mask;

    protected override void init_active()
    {
        base.init_active();

        //particles.Play();
        handle_ray();
    }

    void handle_ray()
    {
        RaycastHit hit;

        if (Physics.Raycast(refs.cam.transform.position, refs.cam.transform.forward, out hit, Mathf.Infinity, hit_mask))

        {
            if(hit.transform.gameObject.layer == LayerMask.NameToLayer("Solid"))
            {

            }
            else
            {
                hit.transform.GetComponentInChildren<enemy>().handle_hit_by_pl_shoot();
            }
        }
    }
}
