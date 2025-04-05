using UnityEngine;

public class pl_hand_push : pl_hand_base
{
    [Header("PUSH")]
    [SerializeField] LayerMask mask_hit;
    [SerializeField] Vector3 col_halfextents;

    Collider[] arr_col_check = new Collider[33];

    protected override void init_active()
    {
        base.init_active();

        int hit_en_amount = Physics.OverlapBoxNonAlloc(refs.cam.transform.position + refs.cam.transform.forward * col_halfextents.z, col_halfextents, arr_col_check, Quaternion.LookRotation(refs.cam.transform.forward), mask_hit);

        for(int i = 0; i < hit_en_amount; i++)
        {
            arr_col_check[i].GetComponentInChildren<enemy>().handle_hit_by_pl_push(refs.cam.transform.forward);
        }
    }
}
