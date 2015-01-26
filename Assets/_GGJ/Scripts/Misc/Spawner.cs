using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {
    public GameObject[] objects;
    public int count = 10;
    public float radius = 10;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        for (int x = 0; x < count; x++)
        {
            Vector2 pos2d=Random.insideUnitCircle;
            //Instantiate(GetOBJ(),Random.insideUnitCircle
        }
	}
    GameObject GetOBJ()
    {
        int index = Mathf.FloorToInt(Random.Range(0,objects.Length));
        return objects[index];
    }
}
