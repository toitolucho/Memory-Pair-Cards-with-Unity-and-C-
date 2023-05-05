using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartaManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    int id;//identificador
    bool isHidden;//estaOculto
    [SerializeField]
    Sprite image;//imagen
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
        this.GetComponent <SpriteRenderer>().sprite = image;
        isHidden = false;
        gameManager.storeCardPlayed(this);
    }
    /// <summary>
    /// Funcion que va hacer desparecer al objeto, incluyendo animacion
    /// </summary>
    public void disAppear() {
        Destroy(this.gameObject);  
    }
    
    /// <summary>
    /// Configura la carta con sus atributos,
    /// es decir, su identificador, su imagen original
    /// y la imagen oculta
    /// </summary>
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
        CartaManager aux = new CartaManager();
        aux.clone(this);
        this.clone(carta);
        //this = carta;
        carta.clone(aux);
    }
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

    public void debug()
    {
        print("Identificador " + id + ", Imagen " + image.name);
    }


    public void OnMouseDown()
    {
        //print("Me pincharon");
        if (isHidden)
            showUp();
        else
            gameManager.hideCard(this);
    }

    public bool isMatchWith(CartaManager card)
    {
        return this.id == card.id;
    }    
}
