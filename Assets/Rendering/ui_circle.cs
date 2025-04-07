using UnityEngine;

public class ui_circle : MonoBehaviour
{
    [Header("REFS")]
    [SerializeField] LineRenderer line;

    [Header("SETTINGS")]
    [SerializeField] int subdivisions;
    [SerializeField] float radius_default;

    void Start()
    {
        update_circle(radius_default);    
    }

    public void update_circle(float radius)
    {
        line.positionCount = subdivisions;

        float angle_step = 2f * Mathf.PI / subdivisions;

        for(int i = 0; i < subdivisions; i++)
        {
            float pos_x = radius * Mathf.Cos(angle_step * i);
            float pos_z = radius * Mathf.Sin(angle_step * i);

            Vector3 point = new Vector3(pos_x, 0f, pos_z);
            line.SetPosition(i, point);
        }
    }
}
