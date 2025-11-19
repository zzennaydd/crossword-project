using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossWordGenerator : MonoBehaviour
{
    public CrossWordGrid grid;
    public int iterations = 5;
    public int maxWords;
    [HideInInspector]
    public CrossWordBuilder builder;
    void Start()
    {
        builder = GetComponent<CrossWordBuilder>();

        if (builder == null) {
            Debug.Log("builder not found");
            return;
        }
        CrossWordGrid best = RepeatCrossWord();
    }

    CrossWordGrid RepeatCrossWord()
    {

        CrossWordGrid bestGrid = null;
        float bestScore = 0f;

        for (int i = 0; i < iterations; i++)
        {
            grid.ClearGrid();

            builder.grid = grid;
            builder.maxWords = maxWords;
            builder.BuildCrossWord();

            float score = GenerateScore(grid);

            if (score > bestScore)
            {
                bestScore = score;
                bestGrid = grid;
                SaveBestGrid(grid);
            }
        }
        return bestGrid;
    }
    void SaveBestGrid(CrossWordGrid grid)
    {
        Debug.Log("best crossword found");
    }

    float GenerateScore(CrossWordGrid grid)
    {
        int rows = grid.height;
        int cols = grid.width;

        float SizeRatio = (float)rows / cols;
        if (rows > cols)
            SizeRatio = (float)cols / rows;

        int filled = 0;
        int empty = 0;

        for(int y = 0; y < rows; y++)
        {
            for(int x = 0; x < cols; x++)
            {
                char letter = grid.GetLetter(x, y);

                if (letter == '\0')
                    empty++;
                filled++;
            }
        }

        float filledRatio;
        if (empty == 0)
            filledRatio = 1f;
        else
            filledRatio = (float)filled / empty;
        float score = (SizeRatio * 10f) + (filledRatio * 20f);
        return score;
    }
}
