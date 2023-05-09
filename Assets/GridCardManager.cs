using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;


/// <summary>
/// Esta clase tiene la funcion de poder organizar, distribuir y barajear los elementos cartas que entraran en juego, en funcion
/// a una determinada cantidad de valores representados por filas y columnas
/// estos valores deben generar siempre una matriz con un numero par de elementos.
/// </summary>
public class GridCardManager : MonoBehaviour
{
    /// <summary>
    /// este atributo permite tener todas las imagenes y categorias que se tendra en el juego
    /// </summary>
    Sprite[,] spriteSource;
    /// <summary>
    /// Prefab  que representa una carta base, con todos sus componentes configurados correctamente para poder ir 
    /// sacando los clones necesarios de acuerdo al tamanio de la matriz
    /// </summary>
    [SerializeField]    
    GameObject baseCard;
    /// <summary>
    /// lista con las imagenes de una deterinada categoria que estara en juego
    /// </summary>
    List<Sprite> categoryList;


    public int Rows { get; set ; }
    public int Columns { get; set; }
    /// <summary>
    /// Punto de referencia donde se iniciara inicialmente la distribucion de las carts
    /// </summary>
    [SerializeField]
    Transform initialPoint;
    /// <summary>
    /// Lista de las cartas que se encuentran en la escena, CONSIDERANDO LA REFERENCIA A SU COMPONENTE CartaManager para solamente hacer la 
    /// manipulacion de sus atributos, y no tanto asi en el gameObject.
    /// </summary>
    List<CartaManager> cardList;
    /// <summary>
    /// Pila de cartas, considerando aquelllas que inicialmente han sido generadas randomicamente y no tienen repeticiones
    /// Se utilizara esta estructura, para ir sacando replicas de las originales
    /// </summary>
    Stack<CartaManager> cardStack;
    /// <summary>
    /// Imagen que represent a la carta Oculta
    /// </summary>
    [SerializeField]
    Sprite hiddenImage;
    /// <summary>
    /// Manejador del juego.
    /// </summary>
    [SerializeField]
    GameManager gameManager;

    /// <summary>
    /// considrando que se tiene un limit de numero de categorias, inicialmente de 1
    /// </summary>
    const short numberCategories = 1;
    const short numberElements = 9;


    // Start is called before the first frame update
    void Start()
    {
        spriteSource = new Sprite[numberCategories, numberElements];
        categoryList = new List<Sprite>();
        cardList = new List<CartaManager>();
        cardStack = new Stack<CartaManager>();

        loadResources();
        
    }

    /// <summary>
    /// Este metodo tiene el objetivo de pasar los valores de las filas y columnas que debe tener la grilla
    /// </summary>
    /// <param name="rows">Numero de Filas</param>
    /// <param name="columns">Numero de columnas</param>
    public void configureValues(int rows, int columns)
    {
        if(rows*columns %2 !=0)
        {
            Debug.LogWarning("No se puede crear una distribucion que no forme una cantidad par de elementos");
            Debug.Log("Se inicializara por defecto el escenario de 2x3");
            rows = 2;
            columns = 3;
            return;

        }
        Rows = rows; Columns = columns;
    }

    public void Reset()
    {
        foreach (var card in cardList)
        {
            Destroy(card.gameObject);
        }
        cardList.Clear();   
        cardStack.Clear();  
    }

    /// <summary>
    /// metodo que permite cargar los recursos de manera estatica por codigo, sin necesidad de utilizar el editor de unity
    /// </summary>
    public void loadResources()
    {
        // introduzca los nombres de mis recursos en una sola linea, para no estar copiando la misma linea de codigo y cambiar solo el nombre
        string[] names = { "01", "02", "03", "04", "05", "06", "07", "08", "09" };
        int index = 0;
        // utilizo un bucle para recorer los nombres de las imagenes, y solamente concatenar para cargar dinamicamente
        foreach (string name in names)
        {
            Sprite sprite = Resources.Load<Sprite>("Vegetables/" + name);
            //print(sprite.name);
            spriteSource[0, index] = sprite;
            index++;
        }

    }


    // solo para demostrar con la tecla espacio, que se puede crear el escenario
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            createScene();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            Reset();
        }
    }

    /// <summary>
    /// meotodo que se encarga de crear la escena con las configuracion preestablecidas
    /// este metodo debe ser llamado por el manager del juego
    /// </summary>
    public void createScene()
    {

         //randomizamos una categoria y cargamos la lista de imagenes correspondientes a esa categoria
        randomizeCategory();

        //barajeamos las imagenes en la categoria seleccionada
        suffleCategory();

        //apuntamos al punto de referencia de arranque para crear los objetos cartas
        Vector3 posicion = initialPoint.position;
        float dx = 3.5f; //espacio de separacion entre punto pivote de cada imagen en X
        float dy = 4;//espacio de separacion entre punto pivote de cada imagen en Y
        for (int j = 0; j < Rows; j++)
        {
            for (int i = 0; i < Columns; i++)
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
    public void distributeCardsData()
    {
        // Primeramente randorizamos los elementos que utilizaremos de una cateogiria
        //randomizeCategory();

        //calculamos la cantidad de cartas que requeriemos considerando la mitad de las que estan presentes en la escena
        // total_cartas = filas x  columnas
        // cartas_diferentes = total_cartas / 2
        int nro_cartas_diferentes = (Rows * Columns) / 2;

        //introduciremos todos los elementos unicos en una pila
        //limpiamos la pila
        cardStack.Clear();
        for (int i = 0; i < nro_cartas_diferentes; i++)
        {
            Sprite image = categoryList[i];
            int identificador = i + 1;
            cardList[i].setInitialValues(identificador, image, hiddenImage, gameManager);
            cardStack.Push(cardList[i]);
        }
        //calculamos el indice que esta despues de la mitad de las cartas
        int indice = nro_cartas_diferentes;

        //recoremos la pila, sacando cada elemento y clonandolo en la posicion respectiva
        while (cardStack.Count > 0)
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
        for (int i = 0; i < spriteSource.GetLength(1); i++)
        {

            //print(spriteSource[k, i].name);
            categoryList.Add(spriteSource[k, i]);
        }
    }
    /// <summary>
    /// metodo que permite barajear la lista de imagenes actual
    /// </summary>
    public void suffleCategory()
    {
        //categoryList[0] = categoryList[1];
        for (int i = categoryList.Count - 1; i >= 0; i--)
        {
            int k = Random.Range(0, i);
            /// intercambiar imagenes en los indices
            /// int a = 10; int b=5; int aux;
            /// aux = a;
            /// a = b;
            /// b = aux;
            Sprite aux = categoryList[i];
            categoryList[i] = categoryList[k];
            categoryList[k] = aux;

        }

    }

    /// <summary>
    /// este metodo randomiza las cartas de la escena y las barajea para que no salgan de forma repetida y se distribuyan randomicamente.
    /// </summary>
    public void suffleCards()
    {
        for (int i = cardList.Count - 1; i >= 0; i--)
        {
            int k = Random.Range(0, i);
            cardList[i].swap(cardList[k]);

        }




    }

}
