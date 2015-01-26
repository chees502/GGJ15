﻿using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {
    public GameObject[] objects;
    public int count = 10;
    public float radius = 10;
	// Use this for initialization
	void Start () {

        for (int x = 0; x < count; x++)
        {
            Vector2 pos2d = Random.insideUnitCircle;
            Vector3 pos3d = new Vector3(pos2d.x * radius, 0, pos2d.y * radius);
            Instantiate(GetOBJ(), pos3d, Random.rotation);
        }
	}
	
	// Update is called once per frame
	void Update () {
	}
    GameObject GetOBJ()
    {
        int index = Mathf.FloorToInt(Random.Range(0,objects.Length));
        return objects[index];
    }
}