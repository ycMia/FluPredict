using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Monoer : MonoBehaviour
{
    public float ballSize;
    private int state = 0;
    public int State { get => state; set => state = value; }

    //0 health
    //1 infected_incubation period
    //2 infected

    public Vector3 direction = new Vector3(0, 0, 0);
    public float alphaValue;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<MeshRenderer>().material.color = Color.green;
    }
    //public LayerMask mask = 1<<8;
    public void Infect()
    {
        float radius = 1.5f;
        Collider[] cols = Physics.OverlapSphere(this.transform.position, radius/*, LayerMask.NameToLayer("layername")*/);
        if (cols.Length > 0)
        {
            for (int i = 0; i < cols.Length; i++)
            {
                if(cols[i].CompareTag("Wall"))
                {
                    break;
                }
                else if (cols[i].CompareTag("Monoer"))
                {
                    if(cols[i].gameObject.GetComponent<Monoer>().State != 2)
                        cols[i].gameObject.GetComponent<Monoer>().State = 1;
                }
                else
                {

                }
            }
        }
    }

    private float Vector3Modules(Vector3 v3)
    {
        return Mathf.Pow(
                      v3.x * v3.x
                    + v3.y * v3.y
                    + v3.z * v3.z, 0.5f);
    }
    private float Vector3Modules(Vector3 v3,Vector3 v3Reference)
    {
        return Mathf.Pow(
                      (v3.x - v3Reference.x) * (v3.x - v3Reference.x)
                    + (v3.x - v3Reference.y) * (v3.x - v3Reference.y)
                    + (v3.x - v3Reference.z) * (v3.x - v3Reference.z), 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        #region
        if (State == 0)
        {
            gameObject.GetComponent<MeshRenderer>().material.color = Color.green;
        }
        else if (State == 1)
        {
            //gameObject.GetComponent<Material>().color = Color.yellow;
            State = 2;
        }
        else if (State == 2)
        {
            gameObject.GetComponent<MeshRenderer>().material.color = Color.red;
        }
        else
        {
            State = 0;
        }
        #endregion
        //着色
        if(State == 2)
            Infect();

        Vector3 t_vector = gameObject.GetComponent<Rigidbody>().velocity + gameObject.transform.position;
        if(Vector3Modules(gameObject.transform.position) >= ballSize)
        {
            Vector3 n_vector = new Vector3(
                -gameObject.transform.position.x / Vector3Modules(gameObject.transform.position),
                -gameObject.transform.position.y / Vector3Modules(gameObject.transform.position),
                -gameObject.transform.position.z / Vector3Modules(gameObject.transform.position));
            gameObject.GetComponent<Rigidbody>().AddForce(n_vector);
        }

        else if (Vector3Modules(t_vector) >= ballSize)
        {
            Vector3 n_vector = new Vector3(
                gameObject.transform.position.x / Vector3Modules(gameObject.transform.position),
                gameObject.transform.position.y / Vector3Modules(gameObject.transform.position),
                gameObject.transform.position.z / Vector3Modules(gameObject.transform.position));

            Vector3 l_vector = gameObject.GetComponent<Rigidbody>().velocity;

            gameObject.GetComponent<Rigidbody>().velocity = - 2 * (Vector3.Dot(l_vector, n_vector)) * n_vector + l_vector;
        }

        else
        {
            if (Random.Range(0f, 1f) <= 0.1f)
            {
                direction = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f));
                gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(direction.x * alphaValue, direction.y * alphaValue, direction.z * alphaValue));
            }
        }
    }
}
