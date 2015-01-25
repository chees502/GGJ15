using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class CreditsSpawner : MonoBehaviour {
    IList<string> people = new List<string>();
    public GameObject creditOBJ;
    float spawnDelay;
	// Use this for initialization
	void Start () {
        people.Add("Daniel Langley");
        people.Add("Michell Durrin");
        people.Add("Michael Lorenzo");
        people.Add("Joel Johnson");
        people.Add("Perry Rasumussen");
        people.Add("Elena Fowler");
        people.Add("David Yow");
        people.Add("Jacob Penner");
        people.Add("Vachara Lertackakorn");
        people.Add("Greg Dunthie");
        people.Add("Brandon Farrell");
           
	}
	
	// Update is called once per frame
	void Update () {
        if (spawnDelay < Time.time)
        {
            spawnDelay = Time.time + 1;
            Spawn();
        }
	}
    void Spawn()
    {
        GameObject tempObj = Instantiate(creditOBJ, transform.position, transform.rotation) as GameObject;
        tempObj.GetComponent<CreditName>().setName(people[Mathf.FloorToInt(Random.Range(0,people.Count))]);
        tempObj.transform.Translate(0, Random.Range(-2, 2), 0);
    }
}
