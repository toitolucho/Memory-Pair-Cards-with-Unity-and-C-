
# Memory Pair Game with Unity and C#

In this project we are going to create a basic game to discover the all possible pair create in a grid.
For this purpose we are goin to use OOP and Components of Unity.
In the first hand we will use tree clasess initially, considering the basic functionality. These clasess will be CardManager, GridManager and the main class GameManager.
We will learn how to use an empty object as a reference for a component, I mean the GridManager.
We will be able to create comunication between objects using the GameManager which will works with the main functionality and will be a medium between the cards and other objects that will be created in the future.
We will use List<> of C# to load manually sprites and distribute them in the scene. Most of these values will be loaded using a matrix as datasource of images(Sprites)
Also we will use a List of Componentes, specifically the CardManager Component, that has the main atributes that we will need to manipulete, I mean, swaping, clonning, etc.
So let's dive in


## Initial Class Diagram

We will work on the base of:




## Initial Classes

- ![#f03c15](https://placehold.co/15x15/f03c15/f03c15.png) CardManager
- ![#f03c15](https://placehold.co/15x15/f03c15/f03c15.png) GridManager
- ![#f03c15](https://placehold.co/15x15/f03c15/f03c15.png) GameManager
- ![#1589F0](https://placehold.co/15x15/1589F0/1589F0.png) SoundsManager


Those marked with red are implemente in first hand, and the other ones will be implemented by yourself guided with a tutorial

## Classes General Description of Attributes

#### CardManager

```http
  Represents a card in the scene
```

| Attribute | Type     | Description                |
| :-------- | :------- | :------------------------- |
| `id` | `integer` | **Identifier** that represets an unique key value to review if there is a match in a selecction of two elements on the grid |
| `isHidden` | `boolean` | This is a boolean value, that indicates if the card is displaying the image of the category or is displaying the hidden image |
| `image` | `Sprite` | Actually, this is the image that must be displayed on the card, when it is not hidden |
| `gameManager` | `GameManager` | this is a reference of the gameManager in the scene |

#### GridManager

```http
  Represents the Grid that is created dinamically based on the numbers of rows and columns using a datasource of images
```

| Attribute | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `spriteSource      | Sprite[,] | A Matrix of Sprites that will be used in the game, each row is a category |
| `baseCard          | `GameObject (Prefab)` | **Prefab** that reference to a basic configuration of the card, considerint it to have a Collider, Animator and CardManager Components |
| `categoryList      | `List<Image>` | List of Spirtes of an specific category |
| `Rows              | `Integer` | Number of rows that must appear on the scene |
| `Columns           | `Integer` | Number of columns that must appear on the scene |
| `initialPoint      | `Transform` | Refence point where to start the first card. Based on it, the distribution takes the initial point of it to add a **DX** and **DY** |
| `cardList          | `List<CardManager>` | List of Components of the CardList of each object of Card  |
| `cardStack         | `Stack<CardManager>` | Stack of Components of the CardList of each object of Card  |
| `hiddenImage       | `Sprite` | Reference to the sprite that represents a Hidden image |
| `gameManager       | `GameManager` | Reference to the GameManager |
| `numberCategories  | `Integer` | **Const**. Number of categories |
| `numberElements    | `Integer` | **Const**. number of elements that has a category |

#### add(num1, num2)

Takes two numbers and returns the sum.
