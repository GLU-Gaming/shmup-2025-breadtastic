using UnityEngine;
using UnityEngine.SceneManagement;

public class OnDead : MonoBehaviour
{
    private bool dead = false;
    private float timerDead = 0;
    [SerializeField] private float timerDeadEnd = 3;
    
    

    public void Dead(bool Isdead)
    {
        dead = Isdead;
    }

    private void Update()
    {
        if (dead)
        {
            if (timerDead > timerDeadEnd)
            {
                SceneManager.LoadScene("Retry");
            }
            timerDead += Time.deltaTime;
        }
    }
}
