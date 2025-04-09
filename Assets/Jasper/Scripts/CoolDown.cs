using UnityEngine;
using UnityEngine.UI;

public class CoolDown : MonoBehaviour
{
    public float LaserTimeCooldown = 10f;
    public float LaserCooldown = 0f;
    public Image image;

    private audioManager audManager;

    void Start()
    {
        LaserCooldown = LaserTimeCooldown;

        audManager = Object.FindFirstObjectByType<audioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (LaserCooldown != LaserTimeCooldown)
        {
            LaserCooldown += Time.deltaTime;
            if (LaserCooldown > LaserTimeCooldown)
            {
                audManager.instance.PlayChargedSound();
                LaserCooldown = Mathf.Clamp(LaserCooldown, 0, LaserTimeCooldown);
            }
        }
        UpdateCooldown();
    }

    private void UpdateCooldown()
    {
        image.fillAmount = LaserCooldown / LaserTimeCooldown;
    }
}
