using UnityEngine;

public class g_fx : MonoBehaviour
{
    [SerializeField] ParticleSystem particles;
    [SerializeField] AudioSource audiosrc;

    public void init()
    {
        particles.Play();
        audiosrc.Play();
    }
}
