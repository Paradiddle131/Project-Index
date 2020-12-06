using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class SpawnBalls : MonoBehaviour
{
    public float boundaryX;
    public float boundaryY;

    public Transform SpawnPosition;
    public GameObject Tom;
    [SerializeField] public int numberOfToms;
    [SerializeField] public float radius;

    public int score = 0;

    Dictionary<System.DateTime, GameObject> toms = new Dictionary<System.DateTime, GameObject>();
    System.DateTime turnedGreen;

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
        GameObject txtScoreObject = new GameObject("txtScore");
        txtScoreObject.transform.SetParent(this.transform);

        Text txtScore = txtScoreObject.AddComponent<Text>();
        txtScore.text = "Ta-dah!";
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
        if (!Tom)
        {
            Tom = new GameObject();
        }
        SpawnObject(Tom, 1);
        double timeToDestroy = Random.Range(0.75f, 6.0f);
        if (System.DateTime.Now > toms.ElementAt(0).Key.AddSeconds(timeToDestroy))
        {
            TurnTomToGreen(toms.ElementAt(0).Value);
            turnedGreen = System.DateTime.Now;
        }
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D[] hits = Physics2D.RaycastAll(ray.origin, ray.direction);
            foreach (RaycastHit2D hit in hits)
            {
                if (hit.collider.gameObject.tag == "Tom" && hit.collider.gameObject.GetComponent<SpriteRenderer>().color == new Color(0f, 1f, 0f, 1f))
                {
                    Destroy(hit.collider.gameObject);
                    toms.Remove(toms.ElementAt(0).Key);
                    //Debug.Log(System.DateTime.Now - turnedGreen);
                    Debug.Log((System.DateTime.Now - turnedGreen).TotalMilliseconds);
                    //score += System.DateTime.Now - turnedGreen;
                    //TurnTomToGreen(hit.collider.gameObject);
                }
                else
                {
                    Debug.Log("else");
                }
            }
        }
    }
}
