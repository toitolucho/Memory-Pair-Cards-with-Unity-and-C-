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
    /// identifica los click que se han hecho en el juego
    /// </summary>
    int numberOfClicks;
    CartaManager firstCardSelected;
    CartaManager secondCardSelected;
    int numberOfMatches;
    [SerializeField]
    Sprite hiddenImage;

    [SerializeField]
    GridCardManager gridManager;
    public void Start()
    {
       
        
        gridManager.Rows = rows;
        gridManager.Columns = columns;
        
        firstCardSelected = secondCardSelected = null;
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

    public void storeCardPlayed(CartaManager playedCard)
    {
        numberOfClicks++;
        if (numberOfClicks == 1)
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


}
