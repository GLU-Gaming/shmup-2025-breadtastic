using System.Collections.Generic;
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

    void Start()
    {
        CurentHP = MaxHP;
        CurentUI = 1+(MaxHP - 1)/2;
    }

    private void OnTriggerEnter(Collider other)
    {
        TestDemageEnamy Demage = other.GetComponent<TestDemageEnamy>();

        if (Demage)
        {
            CurentHP -= Demage.EnemyDemage;
            HPBar();
        }
    }

    private void HPBar()
    {
        if (half)
        {
            Image UI = HPUIList[CurentUI - 1].GetComponent<Image>();
            UI.fillAmount = 0.5f;
            half = false;

            Debug.Log(UI.fillAmount);
        }
        else
        {
            for (int i = 0; i < HPUIList.Count; i++)
            {
                HPUIList[i].SetActive(false);
            }

            CurentUI -= 1;

            for (int i = 0; i < CurentUI; i++)
            {
                HPUIList[i].SetActive(true);
            }
            half = true;
        }
        broken.transform.position -= new Vector3(End, 0);
    }
}
