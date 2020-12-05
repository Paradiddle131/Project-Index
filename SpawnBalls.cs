using System.Collections.Generic;
using UnityEngine;

public class SpawnBalls : MonoBehaviour
{
    public float boundaryX;
    public float boundaryY;

    public Transform SpawnPosition;
    public GameObject Tom;
    [SerializeField] public int numberOfToms;
    [SerializeField] public float radius;

    Dictionary<System.DateTime, GameObject> toms = new Dictionary<System.DateTime, GameObject>();

    Vector2 GetRandomPoint()
    {
        float randomX = Random.Range(-boundaryX, boundaryX);
        float randomY = Random.Range(-boundaryY, boundaryY);
        return new Vector2(randomX, randomY);
    }

    void SpawnObject(GameObject go, int amount)
    {
        if (go == null) return;
        int countSpawned = 0;
        int numIterated = 0;
        do
        {
            Vector2 randomPoint = GetRandomPoint();
            if (toms.Count > 0 || Physics2D.OverlapCircle(randomPoint, radius))
            {
                //Debug.Log("COLLIDED ON: " + randomPoint);
                numIterated++;
                if (numIterated == amount * 2)
                {
                    break;
                }
                continue;
            }
            GameObject tom;
            tom = Instantiate(go);
            tom.transform.position = randomPoint;
            toms.Add(System.DateTime.Now, tom);
            System.Threading.Thread.Sleep(100);
            countSpawned++;
            //TurnTomToGreen(tom);
        } while (countSpawned != amount);
    }
    void Start()
    {
        initializeTomAsRed();
        //SpawnObject(Tom, numberOfToms);
    }

    void OnMouseDown()
    {
        //TurnTomToGreen(this.gameObject);
    }

    void initializeTomAsRed()
    {
        SpriteRenderer renderer = Tom.GetComponent<SpriteRenderer>();
        renderer.color = new Color(1f, 0f, 0f, 1f);
    }

    void TurnTomToGreen(GameObject go)
    {
        SpriteRenderer renderer = go.GetComponent<SpriteRenderer>();
        renderer.color = new Color(0f, 1f, 0f, 1f);
    }

    void Update()
    {
        SpawnObject(Tom, 1);
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D[] hits = Physics2D.RaycastAll(ray.origin, ray.direction);
            foreach (RaycastHit2D hit in hits)
            {
                Debug.Log(hit);
                if (hit.collider.gameObject.tag == "Tom")
                {
                    Debug.Log(hit.collider.gameObject.name);
                    //Destroy(hit.collider.gameObject);
                    TurnTomToGreen(hit.collider.gameObject);
                }
                else
                {
                    Debug.Log("else");
                }
            }
        }
    }
}
