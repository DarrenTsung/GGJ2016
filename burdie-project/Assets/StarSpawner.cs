using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Diagnostics;

public class StarSpawner : MonoBehaviour {
    [System.Serializable]
    public class StarParams
    {
        public StarEnum starType;
        public int duration;
        public int pause;
        public bool randomSpawn;
    }

    [System.Serializable]
    public class Level
    {
        public StarParams[] stars;
    }
        
    public Level[] levels;

    public enum StarEnum : int {
        A = 0,
        B,
        X,
        Y
    };       

    public Transform starholder;
    public GameObject starPrefabA;
    public GameObject starPrefabB;
    public GameObject starPrefabX;
    public GameObject starPrefabY;
    public float distance;
    public float minAngle = 20.0f;
    public float maxAngle = 90.0f;
    private float timeElapsed = 0.0f;
    private float nextStarTrigger = 0;   
    private float destroyTime = 0;
    private int currentIdx = 0;
    private int m_currentLevel = 0;
    private GameObject m_lastStar = null;
   
    void Start () { 
	}
        
    List<List<int>> ArrFromString(string[] levelStr){
        List<List<int>> level = new List<List<int>>();
        foreach (string numbers in levelStr)
        {            
            List<int> current = new List<int>();

            foreach(string number in numbers.Split(','))
            {
                int num = 0;
                if (current.Count == 0)
                {
                    num = (int)Enum.Parse(typeof(StarEnum), number);
                } else
                {
                    num = Int32.Parse(number.Trim());
                }
                current.Add(num);
            }
            level.Add(current);
        }
        return level;
    }
	
	// Update is called once per frame
	void Update () 
    {               
        timeElapsed += Time.deltaTime;       
        if (timeElapsed > nextStarTrigger)
        {           
            if (currentIdx < levels [m_currentLevel].stars.GetLength(0))
            {
                destroyTime = levels [m_currentLevel].stars [currentIdx].duration;
                nextStarTrigger = levels [m_currentLevel].stars [currentIdx].duration + levels [m_currentLevel].stars [currentIdx].pause; //Have to wait this long to create the next star
                Vector3 spawnPos;
                if (levels [m_currentLevel].stars [currentIdx].randomSpawn)
                {
                    spawnPos = Camera.main.transform.position + GetRandomQuaternion() * (Vector3.up * distance);
                } else
                {
                    spawnPos = starholder.position;
                }
                GameObject star = null;
                switch (levels [m_currentLevel].stars [currentIdx].starType)
                {
                    case StarEnum.A:
                        star = (GameObject)GameObject.Instantiate(starPrefabA, spawnPos, Quaternion.identity);
                        break;
                    case StarEnum.B:
                        star = (GameObject)GameObject.Instantiate(starPrefabB, spawnPos, Quaternion.identity);
                        break;
                    case StarEnum.X:
                        star = (GameObject)GameObject.Instantiate(starPrefabX, spawnPos, Quaternion.identity);
                        break;
                    case StarEnum.Y:
                        star = (GameObject)GameObject.Instantiate(starPrefabY, spawnPos, Quaternion.identity);
                        break;
                }
                if (star != null)
                {
                    star.GetComponent<Billboard>().AnimateStar(levels [m_currentLevel].stars [currentIdx].duration);
                }
                timeElapsed = 0.0f;
                currentIdx++;
                m_lastStar = star;
            } 
            else
            {
                m_currentLevel++;
                currentIdx = 0;
            }
        }
        if (timeElapsed > destroyTime)
        {
            Destroy(m_lastStar);
        }
	}        
    Quaternion GetRandomQuaternion(){
        float xRot = UnityEngine.Random.Range(minAngle, maxAngle);
        float yRot = UnityEngine.Random.Range(0, 360.0f);
        return Quaternion.Euler(new Vector3(xRot, yRot, 0));
    }        
}
