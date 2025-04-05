using UnityEngine;

public class pl_death : MonoBehaviour
{
    bool dead;

    public void kill_player(Vector3 pos_killer)
    {
        if (dead) return;

        dead = true;
        print("DEAD LOL");
    }
}
