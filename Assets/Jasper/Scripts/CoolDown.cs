using UnityEngine;
using UnityEngine.UI;

public class CoolDown : MonoBehaviour
{
    public float LaserTimeCooldown = 10f;
    public float LaserCooldown = 0f;
    public Image image;

    void Start()
    {
        LaserCooldown = LaserTimeCooldown;
    }

    // Update is called once per frame
    void Update()
    {
        LaserCooldown += Time.deltaTime;
        UpdateCooldown();
    }

    private void UpdateCooldown()
    {
        image.fillAmount = LaserCooldown / LaserTimeCooldown;
    }
}
