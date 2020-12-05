using UnityEngine;

public class SpawnBalls : MonoBehaviour
{
    public float boundaryX;
    public float boundaryY;

    public Transform SpawnPosition;
    public GameObject Tom;
    [SerializeField] public int numberOfToms;
    [SerializeField] public float radius;

    Vector2 GetRandomPoint()
    {
        float randomX = Random.Range(-boundaryX, boundaryX);
        float randomY = Random.Range(-boundaryY, boundaryY);
        return new Vector2(randomX, randomY);
    }

    void SpawnObject(GameObject go, int amount)
    {
        if (go == null) return;
        //float objectRadius = go.GetComponent<Collider2D>().bounds.extents.x;
        //GameObject[] toms = GameObject.FindGameObjectsWithTag("Tom").;
        //Debug.Log('x');
        int countSpawned = 0;
        int numIterated = 0;
        //radius = Tom.transform.lossyScale[0] / 2;
        //ContactFilter2D contactFilter = new ContactFilter2D();
        //Debug.Log(new Vector2(-2.124238f, 2.313077f));
        //Debug.Log(new Vector2(-2.114317f, 2.082304f));
        LayerMask mask = LayerMask.GetMask("Boundary");
        do
        {
            Vector2 randomPoint = GetRandomPoint();
            //Collider2D circleCollider = Physics2D.OverlapPoint(randomPoint);
            if (Physics2D.OverlapCircle(randomPoint, radius, mask))
            {
                Debug.Log("COLLIDED ON: " + randomPoint);
                numIterated++;
                if (numIterated == amount*2)
                {
                    break;
                }
                continue;
            }
            GameObject tmp;
            tmp = Instantiate(go);
            tmp.transform.position = randomPoint;

            //GameObject tmp = Instantiate(go);
            //tmp.gameObject.transform.position = randomPoint;
            countSpawned++;
        } while (countSpawned != amount);


        //for (int i = 0; i < amount; i++)
        //{
        //    Vector2 randomPoint = GetRandomPoint();
        //    Collider2D CollisionWithEnemy = Physics2D.OverlapCircle(randomPoint, objectRadius);
        //    if (CollisionWithEnemy == false)
        //    {
        //        GameObject tmp = Instantiate(go);
        //        tmp.gameObject.transform.position = randomPoint;
        //    }
        //}
    }
    void Start()
    {
        SpawnObject(Tom, numberOfToms);
    }

    void Update()
    {

    }
}
