using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPointManager : MonoBehaviour
{
    // Location of spawn point
    public Transform[] spawnPoints;
    // Array that keeps track of which spawn points are in use
    public bool[] openSpawns;
    // Integer thate keeps track of how many spawn points are left
    private int numOpenSpawns;

    // Start is called before the first frame update
    void Start()
    {
        // Append all spawn points in level to an array of transforms
        spawnPoints = gameObject.GetComponentsInChildren<Transform>();
        // Set numOpenSpawns equal to the amount of spawn points
        numOpenSpawns = spawnPoints.Length;
        // Set openSpawns to a new array with a length equal to the number
        // of spawn points.
        openSpawns = new bool[spawnPoints.Length];
        // Set all spawn points to open in the openSpawns list
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            openSpawns[i] = true;
        }
    }

    // Gets the indicie of an open spawn point
    public int GetSpawnPoint()
    {
        // Select random spawn point from available spawn points
        int randSpawnIndex = -1;
        if (numOpenSpawns > 0)
        {
            while (randSpawnIndex == -1)
            {
                int currRandomIndex = Random.Range(0, spawnPoints.Length);
                if (openSpawns[currRandomIndex])
                {
                    randSpawnIndex = currRandomIndex;
                    openSpawns[currRandomIndex] = false;
                    numOpenSpawns--;
                }
            }
            return randSpawnIndex;
        }
        print("No available spawn point!");
        return -1;
    }

    // Opens a spawn point given an index
    public void OpenSpawnPoint(int index)
    {
        if (!openSpawns[index])
        {
            openSpawns[index] = true;
        }
    }
}
