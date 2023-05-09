using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Esta clase representa la carta como tal dentro del juego
/// </summary>
public class CartaManager : MonoBehaviour
{
    // Start is called before the first frame update
    /// <summary>
    /// Este atributo representa el identificador de la carta, en la escena, siempre habra una replica de este identificado
    /// para encontrar su par, los identificadores son numeros que empiezan desde el 1 hasta n, segun sea necesario
    /// </summary>
    [SerializeField]
    int id;//identificador
    /// <summary>
    /// atributo de estado que indica si una carta esta volcada boca abajo(oculta), o esta visualizando su contenido como tal
    /// </summary>
    bool isHidden;//estaOculto
    /// <summary>
    /// La imagen que se visualizara cuando la carta no este oculta de acuerdo a la categoria seleccionada
    /// </summary>
    [SerializeField]
    Sprite image;//imagen
    /// <summary>
    /// Referencia al gameManager del juego, que permite lograr la comunicacion entre objetos y controlas la logica del juego
    /// </summary>
    GameManager gameManager;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    /// <summary>
    ///funcion que permite ocultar la imagen
    /// cambia el sprite original al sprite oculto
    /// </summary>
    /// <param name="hiddenImage"> Imagen oculta a mostrar como carta volcada</param>

    public void hide(Sprite hiddenImage)
    {
        this.GetComponent<SpriteRenderer>().sprite = hiddenImage;
        isHidden = true;
    }

    //funcion que permite mostrar la imagen original
    //es decir le asigna la imagen de la clase
    /// <summary>
    ////funcion que permite mostrar la imagen original
    ///es decir le asigna la imagen de la clase   
    /// </summary>
    public void showUp()
    {
        this.GetComponent<SpriteRenderer>().sprite = image;
        isHidden = false;
        gameManager.storeCardPlayed(this);
    }
    /// <summary>
    /// Funcion que va hacer desparecer al objeto, incluyendo animacion
    /// </summary>
    public void disAppear() {
        Destroy(this.gameObject);
        //TODO: Debemos incorporar el codogio o la llamada a funcion del componente para hacer aparecer la animacion
    }

    /// <summary>
    /// Configura la carta con sus atributos,
    /// es decir, su identificador, su imagen original
    /// y la imagen oculta
    /// </summary>
    /// <param name="id"> Identificador</param>
    /// <param name="image">imagen de la cateogira</param>
    /// <param name="hideImage"> imagen que representa la carta volcada (oculta)</param>
    /// <param name="manager"> manejador del juego</param>
    public void setInitialValues(int id, Sprite image, Sprite hideImage, GameManager manager) {
        this.id = id;
        this.image = image;
        this.isHidden = true;
        this.gameManager = manager;
        GetComponent<SpriteRenderer>().sprite = hideImage;
    }
    /// <summary>
    /// Esta funcion copia todos los valores de la carta que se pasa como parametro a los atributos de la clase
    /// </summary>
    /// <param name="baseCard"> Es la carta base que se desea clonar, es decir copiar sus valores</param>
    /// <summary>
    /// Funcion que permite intercambiar los valores entre dos cartas
    /// es decir entre la clase y la carta que recibimos como parametro
    /// </summary>
    public void swap(CartaManager carta)
    {
        //creamos un objeto de este tipo
        CartaManager aux = new CartaManager();
        //clonamos de forma temporal el objeto de la clase a la variable auxiliar
        aux.clone(this);
        //clonamos(es decir copiamos los valores de los atributos) los valores del parametro carta, al ojbeto de la clase actual
        this.clone(carta);
        //this = carta;
        //recuperaos los valores temporalmente almacenados en aux, para copiarlos a la carta
        carta.clone(aux);
    }
    /// <summary>
    /// Metodo que copia los valores del parametro recibido al objeto acutla
    /// </summary>
    /// <param name="baseCard"></param>
    public void clone(CartaManager baseCard) 
    {
        this.id = baseCard.id;
        this.image = baseCard.image;
        this.isHidden = baseCard.isHidden;
        this.gameManager = baseCard.gameManager;
        //print("Objecto " + gameObject.GetComponent<SpriteRenderer>().sprite.name);

        //print("objeto a clonar " + baseCard.gameObject.GetComponent<SpriteRenderer>().sprite);
        //gameObject.GetComponent<SpriteRenderer>().sprite = baseCard.gameObject.GetComponent<SpriteRenderer>().sprite; 
        //baseCard.gameObject.

        //TODO: verificar como se puede clonar los valores
    }  

    /// <summary>
    /// para ver actualmente en la consola los valores del identificado y el nombre de la imagen asignada al objeto
    /// </summary>
    public void debug()
    {
        print("Identificador " + id + ", Imagen " + image.name);
    }



    /// <summary>
    /// sobre escritura del meotod OnMOuseDonw para detectar los click
    /// </summary>
    public void OnMouseDown()
    {
        //print("Me pincharon");
        if (isHidden)// si la imagen esta oculta, entonces hay que mostrarla
            showUp();
        else// caso contrario, debemos ocultarla
            gameManager.hideCard(this);
    }

    /// <summary>
    /// Funcion booleana en forma de pregunta (HaceParejaCon?), es decir verifica si el objeto card es una replica del objeto actual
    /// revisando que los identificadores sean iguales
    /// </summary>
    /// <param name="card">Carta con la cual queremos verificar si hace un par</param>
    /// <returns>Devuelve verdadero si los dientificadores son iguales</returns>
    public bool isMatchWith(CartaManager card)
    {
        return this.id == card.id;
    }    
}
