using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    Sprite[,] spriteSource;
    [SerializeField]
    GameObject baseCard;
    List<Sprite> categoryList;
    public int rows, columns;
    [SerializeField]
    Transform initialPoint;
    List<CartaManager> cardList;
    Stack<CartaManager> cardStack;
    [SerializeField]
    Sprite hiddenImage;

    const short numberCategories = 1;
    const short numberElements = 9;


    //logic of the game
    /// <summary>
    /// identifica los click que se han hecho en el juego
    /// </summary>
    int numberOfClicks;
    CartaManager firstCardSelected;
    CartaManager secondCardSelected;
    int numberOfMatches;
    public void Start()
    {
        spriteSource = new Sprite[numberCategories,numberElements];
        categoryList = new List<Sprite>();
        cardList = new List<CartaManager>();
        cardStack = new Stack<CartaManager>();    

        loadResources();
        firstCardSelected = secondCardSelected = null;
    }
    public void loadResources()
    {
        string[] names = { "01", "02", "03" , "04" , "05" , "06" , "07" , "08", "09" };
        int index = 0;
        foreach (string name in names)
        {
            Sprite sprite = Resources.Load<Sprite>("Vegetables/" + name);
            //print(sprite.name);
            spriteSource[0, index] = sprite;
            index++;
        }

    }

    public void storeCardPlayed(CartaManager playedCard) 
    {
        numberOfClicks++;
        if(numberOfClicks == 1) 
        {
            firstCardSelected = playedCard;
        }
        else if (numberOfClicks == 2)
        {
            secondCardSelected = playedCard;
            //reviewMatch();
            Invoke("reviewMatch", 1);
        }

    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            createScene();
        }
    }

    public void createScene() {

        randomizeCategory();
        suffleCategory();
        Vector3 posicion = initialPoint.position;
        float dx = 3.5f;
        float dy = 4;
        for(int j = 0; j<rows; j++)
        {
            for (int i = 0; i < columns; i++)
            {

                GameObject card = Instantiate(baseCard, posicion, Quaternion.identity);
                cardList.Add(card.GetComponent<CartaManager>());
                posicion.x += dx;
                //card.GetComponent<SpriteRenderer>().sprite = categoryList[i];   
            }
            posicion.x = initialPoint.position.x;
            posicion.y -= dy;

        }
        
        distributeCardsData();
        print("Escena creada");
    }

    /// <summary>
    /// este metodo se hara cargo de generar la metadata de cada carta y ponerlo en una lista 
    /// </summary>
    public void distributeCardsData() {
        // Primeramente randorizamos los elementos que utilizaremos de una cateogiria
        //randomizeCategory();

        //calculamos la cantidad de cartas que requeriemos considerando la mitad de las que estan presentes en la escena
        // total_cartas = filas x  columnas
        // cartas_diferentes = total_cartas / 2
        int nro_cartas_diferentes =( rows * columns) / 2;

        //introduciremos todos los elementos unicos en una pila
        //limpiamos la pila
        cardStack.Clear();
        for(int i = 0; i< nro_cartas_diferentes; i++)
        {
            Sprite image = categoryList[i];
            int identificador = i + 1;
            cardList[i].setInitialValues(identificador, image, hiddenImage,this);
            cardStack.Push(cardList[i]);
        }
        //calculamos el indice que esta despues de la mitad de las cartas
        int indice = nro_cartas_diferentes;

        //recoremos la pila, sacando cada elemento y clonandolo en la posicion respectiva
        while(cardStack.Count>0)
        {
            CartaManager carta = cardStack.Pop();
            //carta.debug();

            cardList[indice].clone(carta);
            indice++;
        }

        //nuestra lista ya se encuentra con datos, ahora corresponde barajear esos datos de las cartas
        suffleCards();


    }
    /// <summary>
    /// Selecciona una categoria al azar del datasource de imagenes
    /// </summary>
    public void randomizeCategory() 
    {
       // print(spriteSource.GetLength(0) + " " + spriteSource.GetLength(1));
        
        categoryList.Clear();
        int k = Random.Range(0, numberCategories);
        for (int i = 0; i< spriteSource.GetLength(1); i++ )
        {
            
            //print(spriteSource[k, i].name);
            categoryList.Add(spriteSource[k,i]);
        }
    }
    /// <summary>
    /// metodo que permite barajear la lista de imagenes actual
    /// </summary>
    public void suffleCategory() 
    {
        //categoryList[0] = categoryList[1];
        for(int i=categoryList.Count-1;i>=0; i-- )
        {
            int k = Random.Range(0, i );
            /// intercambiar imagenes en los indices
            /// int a = 10; int b=5; int aux;
            /// aux = a;
            /// a = b;
            /// b = aux;
            Sprite aux = categoryList[i];
            categoryList[i] = categoryList[k];
            categoryList[k]  = aux;

        }

    }    
    public void suffleCards() {
        for (int i = cardList.Count - 1; i >= 0; i--)
        {
            int k = Random.Range(0, i);
            cardList[i].swap(cardList[k]);

        }

        


    }
    public void hideCard(CartaManager card)
    {
        card.hide(hiddenImage);
        numberOfClicks--;
    }

    public void reviewMatch() 
    {
        if(numberOfClicks == 2)
        {
            if (firstCardSelected.isMatchWith(secondCardSelected))
            {
                print("you find a pair " );
                firstCardSelected.debug();
                secondCardSelected.debug(); 
                firstCardSelected.disAppear();
                secondCardSelected.disAppear();
                numberOfMatches++;
                if(numberOfMatches == rows*columns/2) 
                {
                    print("You did it!!");
                }
            }
            else
            {
                print("is not a match");
                firstCardSelected.hide(hiddenImage);
                secondCardSelected.hide(hiddenImage);
            }
            numberOfClicks = 0;
        }
    }














    //public Color colorActual;


    //public void setColorActual(Color colorSeleccionado)
    //{
    //    colorActual = colorSeleccionado;
    //}
    //public Color getColorActual()
    //{ 
    //    return colorActual; 
    //}


















    //// Start is called before the first frame update
    //[SerializeField]
    //GameObject original;
    //[SerializeField]
    //Sprite[] avatars;

    //[SerializeField]
    //GameObject raceBorderRigth, raceBorderLeft;
    //[SerializeField]
    //GameObject targetBorderRigth;
    //[SerializeField]
    //GameObject car;
    //void Start()
    //{
    //    //print(original.transform.position.x);
    //    InvokeRepeating("crearAuto", 2f, 2f);
    //}

    //// Update is called once per frame
    //void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.C))
    //    {
    //        crearAuto();
    //    }
    //    if (Input.GetKeyDown(KeyCode.Space))
    //    {
    //        stopCars();
    //    }
    //    if (Input.GetKeyDown(KeyCode.R))
    //    {
    //        CancelInvoke();
    //    }
    //    if (Input.GetKeyDown(KeyCode.I))
    //    {
    //        increaseRoad();
    //        //car.GetComponent<CarManager>().increaseLimitRoads();
    //    }

    //}

    //public void crearAuto()
    //{
    //    //saca una copia del objeto base, en funcion
    //    // a una posicion y punto de rotacion
    //    int x = Random.Range(1, car.GetComponent<CarManager>().LimitRoads+1);
    //    print(x);
    //    Vector3 posicion = new Vector3(car.GetComponent<CarManager>().PositionRoadsX[x], 7, 0);
    //    Instantiate(original, posicion, Quaternion.identity);       
    //}
    //public void stopBordersRace()
    //{
    //    raceBorderRigth.GetComponent<ObjectRaceManager>().stopMovement();
    //    raceBorderLeft.GetComponent<ObjectRaceManager>().stopMovement();
    //}

    //public void stopCars()
    //{
    //    //funcion que permite buscar en la escena
    //    //todos los objectos que tengan la etiqueta
    //    //"CarEnemie" y te vuelve un vector de objetos
    //    GameObject[] cars = GameObject.FindGameObjectsWithTag("CarEnemie");
    //    //revisamos si "cars" tiene elementos
    //    if (cars.Length > 0)
    //    {
    //        for (int i = 0; i < cars.Length; i++)
    //        {
    //            GameObject car = cars[i];
    //            car.GetComponent<CarEnemie>().Stop();
    //        }
    //    }
    //    CancelInvoke();
    //    stopBordersRace();
    //}
    //public void increaseRoad()
    //{
    //    if(raceBorderRigth.transform.position.x<3)
    //    {
    //        raceBorderRigth.transform.position = 
    //            new Vector3(
    //                raceBorderRigth.transform.position.x + 3,
    //                raceBorderRigth.transform.position.y,
    //                0);
    //        targetBorderRigth.transform.position = new Vector3(
    //            raceBorderRigth.transform.position.x,
    //            targetBorderRigth.transform.position.y,
    //            0);
    //        raceBorderRigth.GetComponent<ObjectRaceManager>().restartInitTarget(targetBorderRigth.transform.position);
    //        car.GetComponent<CarManager>().increaseLimitRoads();
    //    }
    //}
    //public void OnCollisionEnter2D(Collision2D collision)
    //{
    //    Destroy(collision.gameObject);  
    //}
}
