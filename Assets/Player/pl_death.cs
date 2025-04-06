using UnityEngine;
using System.Collections;

public class pl_death : MonoBehaviour
{
    [SerializeField] pl_refs refs;
    [SerializeField] pl_cam_rot cam_rot;
    [SerializeField] GameObject text;
    bool dead;

    public void kill_player(Vector3 pos_killer)
    {
        if (dead) return;

        dead = true;
        print("DEAD LOL");
        cam_rot.init_death_rot(pos_killer);

        refs.rb.isKinematic = true;

        StartCoroutine(handle_text_delay());

        g_refs.i.room_manager.on_pl_death();
        g_refs.i.g_manager.handle_death();
    }

    IEnumerator handle_text_delay()
    {
        yield return new WaitForSeconds(1);
        text.SetActive(true);
        // also play some sfx
    }
}
