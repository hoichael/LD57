using UnityEngine;

public class pl_cam_depth : MonoBehaviour
{
    [SerializeField] Camera cam;
    [SerializeField] AnimationCurve curve_bounce;
    [SerializeField] float linear_transition_speed;
    [SerializeField] float bounce_transition_speed;

    float depth_base;

    bool bounce_in_progress;
    float bounce_depth_target;
    float bounce_factor_current;


    public float get_depth_base()
    {
        return depth_base;
    }

    public void set_depth_base(float value, bool set_instant)
    {
        depth_base = value;
        bounce_in_progress = false;

        if(set_instant)
        {
            cam.farClipPlane = depth_base;
        }
    }

    public void init_transition_bounce(float depth_target)
    {
        bounce_factor_current = 0;
        bounce_depth_target = depth_target;
        bounce_in_progress = true;
    }

    void Update()
    {
        if(bounce_in_progress)
        {
            exec_bounce();
            return;
        }

        if(!Mathf.Approximately(depth_base, cam.farClipPlane))
        {
            cam.farClipPlane = Mathf.MoveTowards(cam.farClipPlane, depth_base, linear_transition_speed * Time.deltaTime);
        }    
    }

    void exec_bounce()
    {
        bounce_factor_current = Mathf.MoveTowards(bounce_factor_current, 1, bounce_transition_speed * Time.deltaTime);

        float depth_new = Mathf.Lerp(
            depth_base,
            bounce_depth_target,
            Mathf.PingPong(bounce_factor_current, 0.5f)
            );

        cam.farClipPlane = depth_new;

        if(bounce_factor_current == 1)
        {
            bounce_in_progress = false;
        }
    }
}
