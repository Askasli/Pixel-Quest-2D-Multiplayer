using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    public static SpawnManager instance;
    public Transform[] spawnPoints;


    private void Awake()
    {
        instance = this;
    }

}
