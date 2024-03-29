using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    const short numberCategories = 1;
    const short numberElements = 9;
    public int rows, columns;

    //logic of the game
    /// <summary>
    /// identifica la cantidad de click que se han hecho en el juego
    /// servira para contar hasta dos clicks, en ese instante se debe evaluar el emparejamiento
    /// </summary>
    int numberOfClicks;

    /// <summary>
    /// Variable auxiliar de referencia que representa a la <bold>Primera carta seleccionada</bold> , cuando el contado de clicks sea 1, esta variable debe copiar el valor de dicha carta
    /// </summary>
    CartaManager firstCardSelected;

    /// <summary>
    /// Variable auxiliar de referencia que representa a la <bold>Segunda carta seleccionada</bold>, cuando el contador de clicks sea 2, esta carta copiara el valor de la carta seleccionada
    /// e inmediatamente se evaluara la partida
    /// </summary>
    CartaManager secondCardSelected;

    /// <summary>
    /// Numero de Pares CORRECTOS encontrados
    /// </summary>
    int numberOfMatches;

    /// <summary>
    /// Referencia a la imagen que hara de carta oculta en el juego, es necesaria configurarla al inicio
    /// </summary>
    [SerializeField]
    Sprite hiddenImage;

    /// <summary>
    /// Referencia al objeto que realizara el trabajo de crear, distribuir y barajear las cartas en el juego
    /// </summary>
    [SerializeField]
    GridCardManager gridManager;
    public void Start()
    {
       
        
        //gridManager.Rows = rows;
        //gridManager.Columns = columns;
        //configuramos los valores iniciales con los cuales el gridamanager lograra crear la matriz de cartas distribuida en filas por columnas
        
        //inicializamos las variables auxiliares de cartas en nulas
        
    }
    
    public void hideCard(CartaManager card)
    {
        
    }

    public void reviewMatch() 
    {
        
    }

    public void storeCardPlayed(CartaManager playedCard)
    {
       

    }


}
