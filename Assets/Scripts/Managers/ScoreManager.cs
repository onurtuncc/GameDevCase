using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{

    private ScoreManager instance;
    public ScoreManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new ScoreManager();
            }
            return instance;
        }
        set
        {
            instance = value;
        }
    }
    private float score;
    public float Score { get { return score; } set { score = value; } }


    void Awake()
    {
        if (instance == null)
        {
            Instance = this;
        }
        score = 0;
    }


    
}
