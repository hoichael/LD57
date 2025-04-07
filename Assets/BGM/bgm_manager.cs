using UnityEngine;
using System.Collections.Generic;

public class bgm_manager : ut_singleton<bgm_manager>
{
    [SerializeField] List<AudioSource> list_src_heart;
    [SerializeField] float heart_interval;
    [SerializeField] AudioSource src_bgm_intro;
    [SerializeField] AudioSource src_bgm_main;

    bool heartbeat_active;

    int idx_heart_current;
    float time_heart_last;

    void Start()
    {
        DontDestroyOnLoad(this.gameObject);    
    }

    public void init_heartbeat()
    {
        heartbeat_active = true;
    }

    public void exit_heartbeat()
    {
        heartbeat_active = false;
    }

    public void pinglol(string id)
    {
        if(id == "room_0(Clone)")
        {
            src_bgm_intro.Play();
            src_bgm_main.PlayDelayed(45);
        }
        else if(id != "room_1(Clone)")
        {
            src_bgm_main.Stop();
        }
    }

    void Update()
    {
        if (!heartbeat_active) return;

        if(Time.time - time_heart_last > heart_interval)
        {
            time_heart_last = Time.time;
            idx_heart_current++;
            if(idx_heart_current >= list_src_heart.Count)
            {
                idx_heart_current = 0;
            }

            list_src_heart[idx_heart_current].Play();
        }
    }
}
