using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Container : MonoBehaviour
{
    public GameObject prefab;
    public GameObject[] container = new GameObject[100];
    public float spawnBallSize;
    public float moveAllowance;
    public Vector3 spawnPointA;
    public Vector3 spawnPointB;
    public float SpawnBallSize { get => spawnBallSize; set => spawnBallSize = value; }

    private float Vector3Modules(Vector3 v3)
    {
        return Mathf.Abs(Mathf.Pow(
                      v3.x * v3.x
                    + v3.y * v3.y
                    + v3.z * v3.z, 0.5f));
    }
    private float Vector3Modules(Vector3 v3, Vector3 v3Reference)
    {
        return Mathf.Abs(Mathf.Pow(
                      (v3.x - v3Reference.x) * (v3.x - v3Reference.x)
                    + (v3.y - v3Reference.y) * (v3.y - v3Reference.y)
                    + (v3.z - v3Reference.z) * (v3.z - v3Reference.z), 0.5f));
    }

    // Start is called before the first frame update
    private void Start()
    {
        for (int i = 0; i < container.Length; i++)
        {
            container[i] = Instantiate(prefab);
            container[i].name = "Monoer"+i.ToString();
            container[i].transform.parent = gameObject.transform;
            container[i].GetComponent<Monoer>().ballSize = spawnBallSize + moveAllowance;
            //container[i].transform.position = new Vector3(Random.Range(-10f, 10f), Random.Range(-10f, 10f), Random.Range(-10f, 10f));
            //
            Vector3 tposition;
            float lengthabs;
            if (Random.Range(0f, 1f) >= 0.5f)
            {
                do
                {
                    tposition = new Vector3(
                        Random.Range(-spawnBallSize, spawnBallSize),
                        Random.Range(-spawnBallSize, spawnBallSize),
                        Random.Range(-spawnBallSize, spawnBallSize));
                    tposition = new Vector3(tposition.x+spawnPointA.x,
                        tposition.y + spawnPointA.y,
                        tposition.z + spawnPointA.z);

                    lengthabs = Vector3Modules(tposition,spawnPointA);
            }
                while (lengthabs >= spawnBallSize);
            }

            else
            {
                do
                {
                    tposition = new Vector3(
                        Random.Range(-spawnBallSize, spawnBallSize),
                        Random.Range(-spawnBallSize, spawnBallSize),
                        Random.Range(-spawnBallSize, spawnBallSize));
                    tposition += spawnPointB;

                    lengthabs = Vector3Modules(tposition, spawnPointB);

                }
                while (lengthabs >= spawnBallSize);
            }
            container[i].transform.position = tposition;
            ////
            if (i == 0)
            {
                container[i].GetComponent<Monoer>().State = 2;//一个感染者
            }
        }
    }

    // Update is called once per frame
    private void Update()
    {

    }
}
