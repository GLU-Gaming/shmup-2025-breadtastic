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
    public int CurentHP;
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

        // Spelers kleuren zetten naar default kleur
        playerMat1.SetColor("_BaseColor", originalColor1);
        playerMat2.SetColor("_BaseColor", originalColor1);
        playerMat3.SetColor("_BaseColor", originalColor2);

        playerMat1.SetColor("_1st_ShadeColor", secondColor1);
        playerMat2.SetColor("_1st_ShadeColor", secondColor1);
        playerMat3.SetColor("_1st_ShadeColor", secondColor2);
    }

    private void OnTriggerEnter(Collider collision)
    {
        Debug.Log($"Player collided with: {collision.gameObject.name}");

        // Check if the player is hit by an enemy bullet
        EnemyBullet enemyBullet = collision.GetComponent<EnemyBullet>();
        if (enemyBullet)
        {
            Debug.Log("Player hit by Enemy");
            if (rumble)
            {
                rumble.StartRumble(0.75f, 1f, 0.5f, 2);
            }
            CurentHP -= enemyBullet.damage; // Decrement health by the damage amount
            CurentHP = Mathf.Max(CurentHP, 0); // Ensure health does not go below 0
            HPBar();
            Destroy(collision.gameObject); // Destroy the enemy bullet
        }

        // Check if the player is hit by an enemy
        Enemy enemy = collision.GetComponent<Enemy>();
        if (enemy)
        {
            Debug.Log("Player hit by Enemy");
            if (rumble)
            {
                rumble.StartRumble(0.25f, 1f, 0.5f, 2);
            }
            CurentHP -= 1; // Decrement health by 1
            CurentHP = Mathf.Max(CurentHP, 0); // Ensure health does not go below 0
            HPBar();
            Destroy(collision.gameObject); // Destroy the enemy
        }
    }

    public void HPBar()
    {
        // Handle healing
        while (CurentUI < CurentHP / 2) // Process only the healing
        {
            if (half)
            {
                Image UI = HPUIList[CurentUI].GetComponent<Image>();
                UI.fillAmount = 1.0f; // Restore the UI element to full
                half = false;
            }
            else
            {
                if (CurentUI < HPUIList.Count)
                {
                    HPUIList[CurentUI].SetActive(true); // Reactivate the UI element
                    CurentUI += 1;
                }
                half = true;
            }
            broken.transform.position += new Vector3(End, 0); // Adjust the position
        }

        // Handle damage
        while (CurentUI > CurentHP / 2) // Process only the damage taken
        {
            if (half)
            {
                Image UI = HPUIList[CurentUI].GetComponent<Image>();
                UI.fillAmount = 0.5f;
                half = false;
            }
            else
            {
                if (CurentUI > 0)
                {
                    HPUIList[CurentUI - 1].SetActive(false);
                    CurentUI -= 1;
                }
                half = true;
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
    public void Heal(int amount)
    {
        CurentHP = Mathf.Min(CurentHP + amount, MaxHP); // Increase health but do not exceed MaxHP
        HPBar(); // Update the health bar
        Debug.Log($"Player healed by {amount}. Current HP: {CurentHP}");
    }
}
