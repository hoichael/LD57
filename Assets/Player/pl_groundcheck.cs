using UnityEngine;

public class pl_groundcheck : MonoBehaviour
{
    [Header("REFS")]
    [SerializeField] pl_refs refs;
    [SerializeField] Transform check_trans;

    [Header("SETTINGS")]
    [SerializeField] float check_radius;

    public bool is_grounded()
    {
        return Physics.CheckSphere(
            check_trans.position,
            check_radius,
            refs.mask_solid);
    }
}
