using UnityEngine;

public class Target : MonoBehaviour
{
    public ParticleSystem explosionFx;
    public int pointValue = 1;

    private GameManager gameManagerScript;
    private Rigidbody targetRigidBody;

    private readonly float minSpeed = 12f;
    private readonly float maxSpeed = 15f;
    private readonly float rangeTorqueXYZ = 10f;
    private readonly float rangeSpawnX = 4f;
    private readonly float spawnY = -2f;

    void Start()
    {
        gameManagerScript = GameObject.Find("Game Manager").GetComponent<GameManager>();

        targetRigidBody = GetComponent<Rigidbody>();

        //Aggiunge la forca per far saltare l'oggetto in aria
        targetRigidBody.AddForce(RandommForce(), ForceMode.Impulse);

        //Aggiunge che fa ruotare l'oggetto sull'asse di rotazione
        targetRigidBody.AddTorque(RandomTorque(), RandomTorque(), RandomTorque(), ForceMode.Impulse);

        targetRigidBody.position = RandomSpawnPosition();
    }

    private Vector3 RandommForce() => Vector3.up * Random.Range(minSpeed, maxSpeed);

    private float RandomTorque() => Random.Range(-rangeTorqueXYZ, rangeTorqueXYZ);

    private Vector3 RandomSpawnPosition() => new(Random.Range(-rangeSpawnX, rangeSpawnX), spawnY);

    //Questo pezzo di codice verrà eseguito quando l'utente farà click su un oggetto con un collider
    private void OnMouseDown()
    {
        //In questo csao col fatto che l'esplosione non è presente nella scena dobbiamo prima crearla
        //Quindi se faccio explosionFx.Play() non farà nulla perchè non esiste
        Instantiate(explosionFx, transform.position, explosionFx.transform.rotation);
        Destroy(gameObject);

        if (!gameObject.CompareTag("Bad"))
            gameManagerScript.UpdateScore(pointValue);
        else gameManagerScript.GameOver();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!gameObject.CompareTag("Bad"))
        {
            gameManagerScript.GameOver();
        }
        Destroy(gameObject);
    }
}
