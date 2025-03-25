using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.UI;

public class Helt : MonoBehaviour
{
    [SerializeField] private int MaxHP;
    private int CurentHP;
    [SerializeField] private List<GameObject> HPUIList = new List<GameObject>();
    private int DemageTake;

    void Start()
    {
        CurentHP = MaxHP;
        CurentHP = HPUIList.Count;
        HPBar();
    }

    private void OnTriggerEnter(Collider other)
    {
        TestDemageEnamy Demage = other.gameObject.GetComponent<TestDemageEnamy>();

        if (Demage)
        {
            CurentHP -= Demage.EnemyDemage;
            HPBar();
        }
    }

    private void HPBar()
    {
        for(int i = 0; i < HPUIList.Count; i++)
        {
            HPUIList[i].SetActive(false);
        }

        for(int i = 0; i < CurentHP; i++)
        {
            HPUIList[i].SetActive(true);
        }
    }
}
