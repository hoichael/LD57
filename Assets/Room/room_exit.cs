using UnityEngine;

public class room_exit : MonoBehaviour
{
    [SerializeField] room room;

    void OnTriggerEnter(Collider other)
    {
        room.on_exit_trigger();
    }
}
