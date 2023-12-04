using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] private Transform winUI;
    [SerializeField] private Transform loseUI;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        Time.timeScale = 1;

    }

    public void Win()
    {
        winUI.gameObject.SetActive(true);
        Time.timeScale = 0;
    }

    public void Lose()
    {
        loseUI.gameObject.SetActive(true);
        Time.timeScale = 0;
    }
}
