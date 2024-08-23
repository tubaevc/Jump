using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class LevelCreator : MonoBehaviour
{
    [SerializeField] private float monsterSpawnChance = 0.5f;
    [SerializeField] [Range(0, 1)] private float yaySpawnChance = 0.1f;
    [SerializeField] private float propellerSpawnchance = 0.4f;
    [SerializeField] private float trampolineSpawnChance;
    [SerializeField] private float jetpackSpawnChance = 0.1f;
    
    
    [SerializeField] private GameObject[] platformPrefabs;
    [SerializeField] private GameObject[] monsterPrefabs;
    [SerializeField] private GameObject yay;
    [SerializeField] private GameObject propeller;
    [SerializeField] private GameObject trampoline;
    [SerializeField] private GameObject jetpack;
    
    
    [SerializeField] private int platformNum = 200;
    [SerializeField] private float yayOffset = 0.1f;
    [SerializeField] private float trampolineOffset = 0.3f;
    [SerializeField] private float jetpackOffset = 1f;


    private void Start()
    {
        LevelCreate();
    }

    private void LevelCreate()
    {
        Vector2 platformVector = new Vector2();
        for (int i = 0; i < platformNum; i++)
        {
            GameObject platformPrefab = platformPrefabs[Random.Range(0, platformPrefabs.Length)];
            GameObject tempPlatform = Instantiate(platformPrefab);
            platformVector.x = Random.Range(-2.1f, 2.1f);
            platformVector.y += Random.Range(1f, 1.3f);

            tempPlatform.transform.position = platformVector;

            if (tempPlatform.CompareTag("BrokenTile") && tempPlatform.CompareTag("MovingTile"))
            {
                continue; //not spawn in brokentile and movingTile
            }

            bool hasSpawnedItem = false;

            if (!tempPlatform.CompareTag("BrokenTile") && !tempPlatform.CompareTag("MovingTile") &&
                Random.value <= yaySpawnChance && !hasSpawnedItem)
            {
                GameObject tempYay = Instantiate(yay);
                float platformHeight = platformPrefab.GetComponent<Collider2D>().bounds.size.y;
                float yayHeight = yay.GetComponent<Collider2D>().bounds.size.y;
                tempYay.transform.position = new Vector2(platformVector.x,
                    platformVector.y + (platformHeight / 2) + (yayHeight / 2) + yayOffset);
                hasSpawnedItem = true;
            }

            if (!tempPlatform.CompareTag("BrokenTile") && !tempPlatform.CompareTag("MovingTile") &&
                Random.value <= trampolineSpawnChance && !hasSpawnedItem)
            {
                GameObject tempTrampoline = Instantiate(trampoline);
                float platformHeight = platformPrefab.GetComponent<Collider2D>().bounds.size.y;
                float trampolineHeight = trampoline.GetComponent<Collider2D>().bounds.size.y;
                tempTrampoline.transform.position = new Vector2(platformVector.x,
                    platformVector.y + (platformHeight / 2) + (trampolineHeight / 2) + trampolineOffset);
                hasSpawnedItem = true;
            }

            if (!tempPlatform.CompareTag("BrokenTile") && !tempPlatform.CompareTag("MovingTile") &&
                Random.value <= jetpackSpawnChance && !hasSpawnedItem)
            {
                GameObject tempJetpack = Instantiate(jetpack);
                float platformHeight = platformPrefab.GetComponent<Collider2D>().bounds.size.y;
                float jetpackHeight = jetpack.GetComponent<Collider2D>().bounds.size.y;
                tempJetpack.transform.position = new Vector2(platformVector.x,
                    platformVector.y + (platformHeight / 2) + (jetpackHeight / 2) + jetpackOffset);
                hasSpawnedItem = true;
            }

            if (!tempPlatform.CompareTag("BrokenTile") && !tempPlatform.CompareTag("MovingTile") &&
                Random.value <= propellerSpawnchance && !hasSpawnedItem)
            {
                GameObject tempPropeller = Instantiate(propeller);
                tempPropeller.transform.position = new Vector2(platformVector.x, platformVector.y + 0.5f);
                hasSpawnedItem = true;
            }

            if (Random.value <= monsterSpawnChance && !hasSpawnedItem)
            {
                GameObject monsterPrefab = monsterPrefabs[Random.Range(0, monsterPrefabs.Length)];
                GameObject tempMonster = Instantiate(monsterPrefab);
                tempMonster.transform.position = new Vector2(platformVector.x, platformVector.y + 0.5f);
            }
        }
    }
}