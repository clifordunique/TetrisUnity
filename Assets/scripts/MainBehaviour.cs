using Assets.scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainBehaviour : MonoBehaviour
{
    public GameObject prefabTetramino;
    public GameObject debugblock;
    public bool goDown = true;
    public int spawnPiece = -1;
    GameObject[] TetrisPiece = new GameObject[4];
    Block[,] field = new Block[22, 10];
    public float fallingSpeed = 0.2f;
    int[] x = new int[4];
    int[] y = new int[4];
    int currentPiece = -1;
    int rotation = -1;

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.A))
        {
            string a = "";
            for (int i = 0; i < 22; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    a += field[i, j].Number;
                }
                a += "\n";
            }
            Debug.Log(a);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            moveLeftRight(1);
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            moveLeftRight(-1);
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            rotate();
        }
    }
    void Start()
    {

        for (int i = 0; i < 22; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                field[i, j] = new Block();
                //debugblock.GetComponent<TextMesh>().text = i + "," + j;
                //debugblock.name = i + "," + j;
                //Instantiate(debugblock, new Vector3(j, i, 0), prefabTetramino.transform.rotation);
            }
        }

        if (spawnPiece == -1) spawn(Random.Range(0, 7));
        else spawn(spawnPiece);

        if (goDown) InvokeRepeating("moveDown", fallingSpeed, fallingSpeed);

    }

    void findTetraminoes(bool setToZero = true)
    {
        x = new int[4];
        y = new int[4];
        for (int i = 0; i < 22; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                for (int k = 0; k < 4; k++)
                {
                    if (field[i, j].Number == k + 1)
                    {
                        y[k] = i;
                        x[k] = j;
                        if (setToZero) field[i, j].Number = 0;
                    }

                }
            }
        }
    }
    void spawn(int pieceIndex)
    {
        int[] yy = new int[4];
        int[] xx = new int[4];
        Color color = Color.white;
        rotation = 0;

        if (pieceIndex == 0)
        {
            yy = new int[4] { 20, 20, 20, 20 };
            xx = new int[4] { 3, 4, 5, 6 };
            color = Color.cyan;
        }
        else if (pieceIndex == 1)
        {
            yy = new int[4] { 21, 21, 20, 20 };
            xx = new int[4] { 4, 5, 4, 5 };
            color = Color.yellow;
        }
        else if (pieceIndex == 2)
        {
            yy = new int[4] { 21, 20, 20, 20 };
            xx = new int[4] { 4, 3, 4, 5 };
            color = Color.magenta;
        }
        else if (pieceIndex == 3)
        {
            yy = new int[4] { 21, 21, 20, 20 };
            xx = new int[4] { 4, 5, 3, 4 };
            color = Color.green;
        }
        else if (pieceIndex == 4)
        {
            yy = new int[4] { 21, 21, 20, 20 };
            xx = new int[4] { 3, 4, 4, 5 };
            color = Color.red;
        }
        else if (pieceIndex == 5)
        {
            yy = new int[4] { 21, 20, 20, 20 };
            xx = new int[4] { 3, 3, 4, 5 };
            color = Color.blue;
        }
        else if (pieceIndex == 6)
        {
            yy = new int[4] { 21, 20, 20, 20 };
            xx = new int[4] { 5, 3, 4, 5 };
            color = new Color(1.0f, 0.64f, 0.0f);

        }

        for (int i = 0; i < 4; i++)
        {
            field[yy[i], xx[i]].Number = i + 1;
            TetrisPiece[i] = Instantiate(prefabTetramino, new Vector3(xx[i], yy[i], 0), prefabTetramino.transform.rotation);
            TetrisPiece[i].GetComponent<Renderer>().material.SetColor("_Color", color);
            TetrisPiece[i].name = "Tetramino";
            currentPiece = pieceIndex;
            Debug.Log("spawning-" + color.ToString());
        }

    }
    void moveLeftRight(int side)
    {

        findTetraminoes(false);

        if ((x[0] + side >= 0 && x[0] + side <= 9) &&
            (x[1] + side >= 0 && x[1] + side <= 9) &&
            (x[2] + side >= 0 && x[2] + side <= 9) &&
            (x[3] + side >= 0 && x[3] + side <= 9) &&
             (field[y[0], x[0] + side].Number != 5
             && field[y[1], x[1] + side].Number != 5
             && field[y[2], x[2] + side].Number != 5
             && field[y[3], x[3] + side].Number != 5
             ))
        {
            //the set to zero part of the findtetraminoes method
            for (int k = 0; k < 4; k++)
            {
                field[y[k], x[k]].Number = 0;
            }


            for (int i = 0; i < 4; i++)
            {
                field[y[i], x[i] + side].Number = i + 1;
                TetrisPiece[i].transform.position = new Vector3(x[i] + side, y[i], 0);
            }
        }
        else Debug.Log(x[0] + side);
    }
    void rotate()
    {
        void rotateI(int[] xx, int[] yy)
        {
            if (
            x[0] + xx[0] >= 0 && x[0] + xx[0] <= 9 &&
            x[1] + xx[1] >= 0 && x[1] + xx[1] <= 9 &&
            x[2] + xx[2] >= 0 && x[2] + xx[2] <= 9 &&
            x[3] + xx[3] >= 0 && x[3] + xx[3] <= 9 &&
            field[y[0] + yy[0], x[0] + xx[0]].Number != 5 &&
            field[y[1] + yy[1], x[1] + xx[1]].Number != 5 &&
            field[y[2] + yy[2], x[2] + xx[2]].Number != 5 &&
            field[y[3] + yy[3], x[3] + xx[3]].Number != 5 &&
            y[0] + yy[0] >= 0 && y[0] + yy[0] <= 21 &&
            y[1] + yy[1] >= 0 && y[1] + yy[1] <= 21 &&
            y[2] + yy[2] >= 0 && y[2] + yy[2] <= 21 &&
            y[3] + yy[3] >= 0 && y[3] + yy[3] <= 21
            )

            {
                //the set to zero part of the findtetraminoes method
                for (int k = 0; k < 4; k++)
                {
                    field[y[k], x[k]].Number = 0;
                }

                field[y[0] + yy[0], x[0] + xx[0]].Number = 1;
                field[y[1] + yy[1], x[1] + xx[1]].Number = 2;
                field[y[2] + yy[2], x[2] + xx[2]].Number = 3;
                field[y[3] + yy[3], x[3] + xx[3]].Number = 4;

                TetrisPiece[0].transform.position = new Vector3(x[0] + xx[0], y[0] + yy[0], 0);
                TetrisPiece[1].transform.position = new Vector3(x[1] + xx[1], y[1] + yy[1], 0);
                TetrisPiece[2].transform.position = new Vector3(x[2] + xx[2], y[2] + yy[2], 0);
                TetrisPiece[3].transform.position = new Vector3(x[3] + xx[3], y[3] + yy[3], 0);
                rotation++;
                if (rotation == 4) rotation = 0;
            }
        }

        findTetraminoes(false);

        if (currentPiece == 0)
        {
            if (rotation == 0) rotateI(new int[4] { 2, 1, 0, -1 }, new int[4] { 1, 0, -1, -2 });
            else if (rotation == 1) rotateI(new int[4] { 1, 0, -1, -2 }, new int[4] { -2, -1, 0, 1 });
            else if (rotation == 2) rotateI(new int[4] { -2, -1, 0, 1 }, new int[4] { -1, 0, 1, 2 });
            else if (rotation == 3) rotateI(new int[4] { -1, 0, 1, 2 }, new int[4] { 2, 1, 0, -1 });
        }
        else if (currentPiece == 2)
        {
            if (rotation == 0) rotateI(new int[4] { 1, 1, 0, -1 }, new int[4] { -1, 1, 0, -1 });
            else if (rotation == 1) rotateI(new int[4] { -1, 1, 0, -1 }, new int[4] { -1, -1, 0, 1 });
            else if (rotation == 2) rotateI(new int[4] { -1, -1, 0, 1 }, new int[4] { 1, -1, 0, 1 });
            else if (rotation == 3) rotateI(new int[4] { 1, -1, 0, 1 }, new int[4] { 1, 1, 0, -1 });
        }
        else if (currentPiece == 3)
        {
            if (rotation == 0) rotateI(new int[4] { 1, 0, 1, 0 }, new int[4] { -1, -2, 1, 0 });
            else if (rotation == 1) rotateI(new int[4] { -1, -2, 1, 0 }, new int[4] { -1, 0, -1, 0 });
            else if (rotation == 2) rotateI(new int[4] { -1, 0, -1, 0 }, new int[4] { 1, 2, -1, 0 });
            else if (rotation == 3) rotateI(new int[4] { 1, 2, -1, 0 }, new int[4] { 1, 0, 1, 0 });
        }
        else if (currentPiece == 4)
        {
            if (rotation == 0) rotateI(new int[4] { 2, 1, 0, -1 }, new int[4] { 0, -1, 0, -1 });
            else if (rotation == 1) rotateI(new int[4] { 0, -1, 0, -1 }, new int[4] { -2, -1, 0, 1 });
            else if (rotation == 2) rotateI(new int[4] { -2, -1, 0, 1 }, new int[4] { 0, 1, 0, 1 });
            else if (rotation == 3) rotateI(new int[4] { 0, 1, 0, 1 }, new int[4] { 2, 1, 0, -1 });
        }
        else if (currentPiece == 5)
        {
            if (rotation == 0) rotateI(new int[4] { 2, 1, 0, -1 }, new int[4] { 0, 1, 0, -1 });
            else if (rotation == 1) rotateI(new int[4] { 0, 1, 0, -1 }, new int[4] { -2, -1, 0, 1 });
            else if (rotation == 2) rotateI(new int[4] { -2, -1, 0, 1 }, new int[4] { 0, -1, 0, 1 });
            else if (rotation == 3) rotateI(new int[4] { 0, -1, 0, 1 }, new int[4] { 2, 1, 0, -1 });
        }
        else if (currentPiece == 6)
        {
            if (rotation == 0) rotateI(new int[4] { 0, 1, 0, -1 }, new int[4] { -2, 1, 0, -1 });
            else if (rotation == 1) rotateI(new int[4] { -2, 1, 0, -1 }, new int[4] { 0, -1, 0, 1 });
            else if (rotation == 2) rotateI(new int[4] { 0, -1, 0, 1 }, new int[4] { 2, -1, 0, 1 });
            else if (rotation == 3) rotateI(new int[4] { 2, -1, 0, 1 }, new int[4] { 0, 1, 0, -1 });
        }
    }
    void moveDown()
    {

        findTetraminoes();

        if ((y[0] - 1 >= 0
            && y[1] - 1 >= 0
            && y[2] - 1 >= 0
            && y[3] - 1 >= 0)
            &&
             (field[y[0] - 1, x[0]].Number != 5
             && field[y[1] - 1, x[1]].Number != 5
             && field[y[2] - 1, x[2]].Number != 5
             && field[y[3] - 1, x[3]].Number != 5
             )
            )
        {

            for (int i = 0; i < 4; i++)
            {
                field[y[i] - 1, x[i]].Number = i + 1;
                TetrisPiece[i].transform.position = new Vector3(x[i], y[i] - 1, 0);
            }

        }
        else
        {
            for (int i = 0; i < 4; i++)
            {
                field[y[i], x[i]].Number = 5;
            }

            GameObject[] gameObjects = GameObject.FindObjectsOfType<GameObject>();

            //line detection
            for (int i = 0; i < 22; i++)
            {
                bool isLine = true;
                for (int j = 0; j < 10; j++)
                {
                    if (field[i, j].Number != 5)
                    {
                        isLine = false;
                    }
                }
                if (isLine)
                {
                    for (int j = 0; j < 10; j++)
                    {
                        foreach (GameObject item in gameObjects)
                        {
                            if (item.transform.position.x == j && item.transform.position.y == i)
                            {
                                item.GetComponent<Renderer>().material.SetColor("_Color", Color.black);
                            }
                        }
                    }
                }
            }



            if (spawnPiece == -1) spawn(Random.Range(0, 7));
            else spawn(spawnPiece);
        }
    }
}
