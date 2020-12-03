# Práctica 3: Delegados y eventos

![Error](/images/scene.png)

## Agregar dos objetos en la escena: A y B. Cuando el jugador colisiona con un objeto de tipo B, el objeto A volcará en consola un mensaje. Cuando toca el objeto A se incrementará un contador en el objeto B.

Para lograr esto, haremos un script que portará el jugador que funcionará como delegado:

```csharp
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEventScript : MonoBehaviour
{
    public delegate void delegateFunction();
    public event delegateFunction aEvent;
    public event delegateFunction bEvent;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "A")
            aEvent();
        else if (collision.gameObject.tag == "B")
            bEvent();
    }
}
```

Una vez hecho esto deberemos crear los scripts de los eventos pertinentes: 

### Evento del objeto A:

```csharp
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AEvent : MonoBehaviour
{
    public int counter = 0;
    public PlayerEventScript collisionEvent;
    
    // Start is called before the first frame update
    void Start()
    {
        collisionEvent = GameObject.FindWithTag("Player").GetComponent<PlayerEventScript>();
        collisionEvent.aEvent += IncCount;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void IncCount()
    {
        counter++;
        Debug.Log(counter);
    }
}
```

### Evento del objeto B:

```csharp
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BEvent : MonoBehaviour
{
    public PlayerEventScript collisionEvent = null;
    
    // Start is called before the first frame update
    void Start()
    {
        collisionEvent = GameObject.FindWithTag("Player").GetComponent<PlayerEventScript>();
        collisionEvent.bEvent += PrintMessage;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void PrintMessage()
    {
        Debug.Log("Colisión");
    }
}
```

## Implementar un controlador de escena usando el patrón delegado que gestione las siguientes acciones:

### Si el jugador dispara, los objetos de tipo A que estén a una distancia media serán desplazados y los que estén a una distancia pequeña serán destruidos. Los umbrales que marcan los límites se deben configurar en el inspector

Creamos el script del controlador de escena que portará la cámara principal: 

```csharp
public class SceneController : MonoBehaviour
{
    public delegate void delegateHandler();
    public event delegateHandler shot;
    public event delegateHandler hitB;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
            if (shot != null)
                shot();
    }

    public void BCollision()
    {
        hitB();
    }
}
```

Creamos el script del evento de disparo que portarán los objetos tipo A:

```csharp
public class ShotEvent : MonoBehaviour
{
    public SceneController sceneController;
    private GameObject player;
    private Transform playerTransform;
    private Transform selfTransform;
    private Rigidbody selfRigidbody;
    public float destroyDistance = 2.0f;
    public float pushDistance = 6.0f;
    
    // Start is called before the first frame update
    void Start()
    {
        sceneController = GameObject.FindWithTag("MainCamera").GetComponent<SceneController>();
        sceneController.shot += PlayerShot;
    }

    // Update is called once per frame
    void PlayerShot()
    {
        player = GameObject.FindWithTag("Player");
        playerTransform = player.GetComponent<Transform>();
        selfTransform = GetComponent<Transform>();

        Vector3 pushDirection = selfTransform.position - playerTransform.position;

        float distance = Vector3.Distance(selfTransform.position, playerTransform.position);
        if (distance <= destroyDistance)
        {
            Destroy(gameObject);
            sceneController.shot -= PlayerShot;
        }

        else if (distance <= pushDistance)
        {
            selfRigidbody = GetComponent<Rigidbody>();
            selfRigidbody.AddForce(pushDirection, ForceMode.Impulse);
        }
    }
}

![Error](/images/shotA.gif)

### Si el jugador choca con objetos de tipo B, todos los de ese tipo sufrirán alguna transformación o algún cambio en su apariencia y decrementarán el poder del jugador.

Creamos el script del evento de que el jugador colisione con un objeto tipo B. Este script lo portarán todos los objetos tipo B:

```csharp
public class HitBEvent : MonoBehaviour

{
    public SceneController sceneController;
    private float randomDecrement;

    // Start is called before the first frame update
    void Start()
    {
        sceneController = GameObject.FindWithTag("MainCamera").GetComponent<SceneController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player") ;
            sceneController.hitB += Transformation;
    }

    void Transformation()
    {
        GetComponent<Renderer>().material.color = new Color(Random.Range(0F, 1F), Random.Range(0, 1F), Random.Range(0, 1F));
        randomDecrement = Random.Range(0F, 0.1F);
        GetComponent<Transform>().localScale -= new Vector3(randomDecrement, randomDecrement, randomDecrement);
    }
}
```

Y adicionalmente crearemos un script que llame al evento anterior cuando detecte que el jugador ha colisionado con un objeto tipo B:

```csharp
public class HittingB : MonoBehaviour
{
    public SceneController sceneController;

    // Start is called before the first frame update
    void Start()
    {
        sceneController = GameObject.FindWithTag("MainCamera").GetComponent<SceneController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "B")
            sceneController.BCollision();
    }
}
```

![Error](/images/bCollision.gif)

### En la escena habrá ubicados un tipo de objetos que al ser recolectados por el jugador harán que ciertos obstáculos se desplacen desbloqueando algún espacio.

Usaremos un objeto tipo Key que al cogerlo el jugador se destruirá la pared que bloquea parte del escenario. Para conseguirlo crearemos el script KeyScript que 
funcionará como delegado.

```csharp
public class KeyScript : MonoBehaviour
{
    private Transform keyTransform;
    private GameObject player;
    public delegate void delegateHandler();
    public event delegateHandler playerGetKey;

    // Start is called before the first frame update
    void Start()
    {
        keyTransform = GetComponent<Transform>();
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        keyTransform.Rotate(0, 0, 3);
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerGetKey();
            Destroy(gameObject);
        }
    }
}
```

Y el script que destruirá el obstáculo una vez se produzca el evento de que el jugador coja la llave:

```csharp
public class ObstacleScript : MonoBehaviour
{
    public KeyScript getKeyHandler;
    
    // Start is called before the first frame update
    void Start()
    {
        getKeyHandler = GameObject.FindWithTag("Key").GetComponent<KeyScript>();
        getKeyHandler.playerGetKey += RemoveObstacle;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void RemoveObstacle()
    { 
        Destroy(gameObject);
    }
}
```

![Error](/images/key.gif)

##  Incorporar un elemento que sirva para encender o apagar un foco utilizando el teclado.

Creamos el script que estará a la escucha de que el jugador lo pulse para pulsar el interruptor:

```csharp
public class SwitchController : MonoBehaviour
{
    public delegate void delegateHandler();
    public event delegateHandler switchPressed;
    private GameObject player;
    private Transform playerTransform, selfTransform;
    public float minimumDistanceToActivate = 3.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        player = GameObject.FindWithTag("Player");
        playerTransform = player.GetComponent<Transform>();
        selfTransform = GetComponent<Transform>();

        if (Vector3.Distance(playerTransform.position, selfTransform.position) <= minimumDistanceToActivate && Input.GetKeyDown("e"))
            switchPressed();
    }
}
```

Y el script que se ejecutará cuando el jugador pulse el interruptor, y se encargará simplemente de apagar la luz:

```csharp 
public class LightEvent : MonoBehaviour
{
    public SwitchController switchController;
    
    // Start is called before the first frame update
    void Start()
    {
        switchController = GameObject.FindWithTag("Switch").GetComponent<SwitchController>();
        switchController.switchPressed += TurnLight;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void TurnLight()
    {
        gameObject.GetComponent<Light>().enabled = !gameObject.GetComponent<Light>().enabled;
    }
}
```

![Error](/images/light.gif)
