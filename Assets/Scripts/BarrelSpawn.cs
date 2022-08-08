using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelSpawn : MonoBehaviour
{

    public int min = 2;
    public int max = 3;
    public GameObject barrel;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnBarrel());
    }

    IEnumerator SpawnBarrel() {
        Instantiate(barrel, this.gameObject.transform.position, this.gameObject.transform.rotation);
        System.Random rnd = new System.Random();
        yield return new WaitForSeconds(rnd.Next(min, max));
        StartCoroutine(SpawnBarrel());
    }
}
