using UnityEngine;

//Plataforma movil que se mueve entre dos puntos
public class MovingPlatform : MonoBehaviour
{
    [SerializeField] Vector2 pointA = new Vector2(-2f, 0f);
    [SerializeField] Vector2 pointB = new Vector2(2f, 0f);
    [SerializeField] float speed = 2f;

    Vector3 startPosition;
    bool movingToB = true;

    void Start()
    {
        startPosition = transform.position;
        transform.position = startPosition + (Vector3)pointA;
    }

    void Update()
    {
        //calcula el punto al que se dirige
        Vector3 target = startPosition + (Vector3)(movingToB ? pointB : pointA);

        //mueve la plataforma hacia el destino
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

        //cuando llega cambia de direccion
        if (Vector3.Distance(transform.position, target) < 0.05f)
        {
            movingToB = !movingToB;
        }
    }

    //cuando el player se sube lo hace hijo para que se mueva con la plataforma
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(transform);
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(null);
        }
    }
}