using System.Collections;
using System.Collections.Generic;
using System.Data;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.UI;
using UnityEngine.UI;

public class Helt : MonoBehaviour
{
    [SerializeField] private int MaxHP = 15;
    private int CurentHP;
    [SerializeField] private Image HPUI;
    private int CurentUI;
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
        CurentUI = CurentHP;

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
        }
    }

    public void HPBar(int Lives)
    {
        HPUI.fillAmount = Lives / CurentUI;

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
