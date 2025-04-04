using System.Collections;
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
    [SerializeField] Material playerMat1;
    [SerializeField] Material playerMat2;
    [SerializeField] Material playerMat3;
    [SerializeField] float flashTime;
    [SerializeField] Color flashColor;
    private Color originalColor1 = new Color(1f, 1f, 1f);
    private Color originalColor2 = new Color(0.4745098f, 0.4392156f, 0.4862745f);
    private Color secondColor1 = new Color(0.6470588f, 0.6588235f, 0.7803922f);
    private Color secondColor2 = new Color(0.4745098f, 0.4392156f, 0.4862745f);

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

        StartCoroutine(playerFlash());

    }

    IEnumerator playerFlash()
    {
        playerMat1.SetColor("_BaseColor", flashColor);
        playerMat2.SetColor("_BaseColor", flashColor);
        playerMat3.SetColor("_BaseColor", flashColor);

        playerMat1.SetColor("_1st_ShadeColor", flashColor);
        playerMat2.SetColor("_1st_ShadeColor", flashColor);
        playerMat3.SetColor("_1st_ShadeColor", flashColor);

        yield return new WaitForSecondsRealtime(flashTime);

        playerMat1.SetColor("_BaseColor", originalColor1);
        playerMat2.SetColor("_BaseColor", originalColor1);
        playerMat3.SetColor("_BaseColor", originalColor2);

        playerMat1.SetColor("_1st_ShadeColor", secondColor1);
        playerMat2.SetColor("_1st_ShadeColor", secondColor1);
        playerMat3.SetColor("_1st_ShadeColor", secondColor2);
    }
}
