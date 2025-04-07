using UnityEngine;

public class room_red : room
{
    public override void init()
    {
        bgm_manager.i.init_heartbeat();

        base.init();
    }

    public override void on_exit_trigger()
    {
        bgm_manager.i.exit_heartbeat();

        base.on_exit_trigger();
    }
}
