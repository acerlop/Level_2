using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private int score;
    private bool win;

    public static GameManager instance;

    public int Score { get => score; set => score = value; }
    public bool Win { get => win; set => win = value; }

    private void Awake()
    {
        //first time
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }        
        else Destroy(gameObject);
    }
}
