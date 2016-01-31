using UnityEngine;
using System.Collections;

public class StarSpawner : MonoBehaviour {
    public float distance;

	// Use this for initialization
    void Start () {
        for (int i = 0; i < 10; i++)
        {
            Vector3 spawnPos = Camera.main.transform.position + GetRandomQuaternion() * (Vector3.up * distance);

        }
	}
	
	// Update is called once per frame
	void Update () {	
	}

    Quaternion GetRandomQuaternion(){
        float xRot = Random.Range(20.0f, 90.0f);
        float yRot = Random.Range(0, 360.0f);
        return Quaternion.Euler(new Vector3(xRot, yRot, 0));
    }
}
