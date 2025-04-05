using UnityEngine;

public class pl_refs : MonoBehaviour
{
    [field: SerializeField] public Rigidbody rb { get; private set; }
    [field: SerializeField] public Transform trans_orientation_ref { get; private set; }
    [field: SerializeField] public LayerMask mask_solid { get; private set; }
    [field: SerializeField] public pl_groundcheck groundcheck { get; private set; }
    [field: SerializeField] public pl_gravity gravity { get; private set; }
}
