using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class pl_death : MonoBehaviour
{
    [SerializeField] pl_refs refs;
    [SerializeField] pl_cam_rot cam_rot;
    [SerializeField] GameObject text;
    [SerializeField] GameObject reticle;
    [SerializeField] List<MonoBehaviour> list_script_to_disable;

    [SerializeField] AudioSource src_death_instant, src_death_text;

    bool dead;

    public void kill_player(Vector3 pos_killer, bool instant)
    {
        if (dead) return;

        dead = true;
        print("DEAD LOL");

        if(instant)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        src_death_instant.Play();

        cam_rot.init_death_rot(pos_killer);

        refs.rb.isKinematic = true;

        reticle.SetActive(false);

        foreach(MonoBehaviour script in list_script_to_disable)
        {
            script.enabled = false;
        }

        StartCoroutine(handle_text_delay());

        g_refs.i.room_manager.on_pl_death();
        g_refs.i.g_manager.handle_death();
    }

    IEnumerator handle_text_delay()
    {
        yield return new WaitForSeconds(1.5f);
        text.SetActive(true);
        src_death_text.Play();
    }
}
