using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.Rendering.UI;
using UnityEngine.UI;

public class Helt : MonoBehaviour
{
    [SerializeField] private int MaxHP = 15;
    private bool half = false;
    private int CurentHP;
    [SerializeField] private List<GameObject> HPUIList = new List<GameObject>();
    public int CurentUI;
    [SerializeField] GameObject broken;
    [SerializeField] private float End = 27.5f;

    private Rumble rumble;

    void Start()
    {
        CurentUI = 1+(MaxHP - 1)/2;

        rumble = FindFirstObjectByType<Rumble>();

    }

    private void OnTriggerEnter(Collider collision)
    {
        Debug.Log($"Player collided with: {collision.gameObject.name}");

        // Controleer of de speler een vijandelijke kogel raakt
        EnemyBullet enemyBullet = collision.GetComponent<EnemyBullet>();
        if (enemyBullet)
        {
            Debug.Log("Player hit by Enemy");
            if (rumble)
            {
                rumble.StartRumble(0.25f, 1f, 0.5f, 2);
            }
            CurentHP = enemyBullet.damage;
            HPBar();
            Destroy(collision.gameObject); // Vernietig de vijand            
        }

        // Controleer of de speler een vijand raakt
        Enemy enemy = collision.GetComponent<Enemy>();
        if (enemy)
        {
            Debug.Log("Player hit by Enemy");
            if (rumble)
            {
                rumble.StartRumble(0.25f, 1f, 0.5f, 2);
            }
            CurentHP = 1;
            HPBar();
            Destroy(collision.gameObject); // Vernietig de vijand
        }
    }

    private void HPBar()
    {
        for(int i = 0; i < CurentHP; i++)
        {
            if (half)
            {
                Image UI = HPUIList[CurentUI - 1].GetComponent<Image>();
                UI.fillAmount = 0.5f;
                half = false;
            }
            else
            {
                for (int j = 0; j < HPUIList.Count; j++)
                {
                    HPUIList[j].SetActive(false);
                }

                CurentUI -= 1;

                if (CurentUI < 0)
                {
                    CurentUI = 0;
                }

                for (int j = 0; j < CurentUI; j++)
                {
                    HPUIList[j].SetActive(true);
                }
                half = true;
                Debug.Log("okay");
            }

            broken.transform.position -= new Vector3(End, 0);
        }
    }
}
