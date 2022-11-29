using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject birds;
    public GameObject bouncyBirds;
    GameObject newBirds;
    public Transform rock;
    public int max = 4;
    public float minX = -6;
    public float maxX = 6;
    public GameObject foregroundBirds;
    bool canSpawn = true;
    int n;
    GameObject stuff;

    void Update()
    {
        if (transform.childCount < max && rock.position.y > 1 && canSpawn)
        {
            StartCoroutine(Fly());
            canSpawn = false;
        }
    }

    IEnumerator Fly()
    {
        stuff = Instantiate(foregroundBirds);
        yield return new WaitUntil(() => stuff.transform.position.x > 0);
        n = max - transform.childCount;
        for (int i = 0; i < n; i++)
        {
            if (PlayerPrefs.GetInt("BouncyBirds") == 1 && Random.Range(0, 4) == 0)
            {
                newBirds = Instantiate(bouncyBirds, new Vector3((float)Random.Range(minX * 10, maxX * 10 + 1) / 10, -3.2f, 0), Quaternion.identity);
            }
            else
            {
                newBirds = Instantiate(birds, new Vector3((float)Random.Range(minX * 10, maxX * 10 + 1) / 10, -3.2f, 0), Quaternion.identity);
            }
            newBirds.transform.SetParent(transform);
        }
        canSpawn = true;
    }
}
