using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CrossWordGrid : MonoBehaviour
{
    [Header("Grid Settings")]
    int width = 15;
    int height = 15;
    public GameObject cellPrefab;
    public GameObject gridParent;

    private char[,] crosswordGrid;
    private void Awake()
    {
        crosswordGrid = new char[height, width];
        CreateGrid();
      //  PutRandomWord();

    }
    void CreateGrid()
    {
        for(int x = 0; x < width; x++)
        {
            for(int y = 0; y < height; y++)
            {
                GameObject cell = Instantiate(cellPrefab, gridParent.transform);
            }
        }
    }
    void PutRandomWord()
    {
        crosswordGrid[1, 1] = 'H';
        crosswordGrid[1, 2] = 'E';
        crosswordGrid[1, 3] = 'L';
        crosswordGrid[1, 4] = 'L';
        crosswordGrid[1, 5] = 'O';
        crosswordGrid[2, 5] = 'N';
        crosswordGrid[3, 5] = 'L';
        crosswordGrid[4, 5] = 'Y';
    }
    public void SetLetter(int x, int y, char letter)
    {
        crosswordGrid[y, x] = letter;
        int index = y * width + x;
        var cell = gridParent.transform.GetChild(index).GetComponentInChildren<TextMeshProUGUI>();
        cell.text = letter.ToString();
    }

    public void ClearGrid()
    {
        for (int i = 0; i < gridParent.transform.childCount; i++)
        {
            gridParent.transform.GetChild(i).GetComponentInChildren<TextMeshProUGUI>().text = "";
        }
    }
}
