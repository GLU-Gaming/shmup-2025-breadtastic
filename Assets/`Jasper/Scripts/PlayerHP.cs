using UnityEngine;

public class Helt : MonoBehaviour
{
    [SerializeField] private int MaxHP;
    private int CurentHP;
    void Start()
    {
        CurentHP = MaxHP;
    }

    private void OnCollisionEnter(Collision collision)
    {
        TestDemageEnamy Demage = collision.gameObject.GetComponent<TestDemageEnamy>();

        if (Demage)
        {
            CurentHP -= Demage.EnemyDemage;
            Debug.Log(CurentHP);
        }
    }
}
