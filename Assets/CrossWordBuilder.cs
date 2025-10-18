using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
public class CrossWordBuilder : MonoBehaviour
{
    List<string> words = new List<string> { "karayip", "kriket", "gül", "test", "astana", "kalabalýk", "biber", "gri", "kesik", "altýn" };
    List<string> targetWords = new List<string>();
    char[] targetLetters;
    public CrossWordGrid grid;
    private int startX = 2;
    private int startY = 7;

    private int maxWords = 5;
    private int wordCount = 0;
    bool placedWord = false;
    private void Start()
    {
        BuildCrossWord();
    }

    void BuildCrossWord()
    {
        string longestWord = words[0];

        for (int i = 0; i < words.Count; i++)
        {
            if (words[i].Length > longestWord.Length)

                longestWord = words[i];

        }
        targetLetters = longestWord.ToCharArray();

        for (int i = 0; i < longestWord.Length; i++)
        {
            grid.SetLetter(startX + i, startY, longestWord[i]);
        }

        words.Remove(longestWord);

        wordCount = 1;
        while (wordCount < maxWords && words.Count > 0)
        {
            int randomIndex = Random.Range(0, words.Count);
            string word = words[randomIndex];
            words.RemoveAt(randomIndex);

            PlaceWord(word);
            if (placedWord)
                wordCount++;
        }

        void PlaceWord(string word) //inprogress
        {
            foreach (char letter in word)
            {
                for (int y = 0; y < grid.height; y++)
                {
                    for (int x = 0; x < grid.width; x++)
                    {
                        if (grid.GetLetter(x, y) == letter)
                        {
                            int targetIndex = word.IndexOf(letter);
                            int startYPosition = y - targetIndex;

                            if (startYPosition >= 0 && startYPosition + word.Length <= grid.height)
                            {
                                bool canPlace = true;

                                for (int i = 0; i < word.Length; i++)
                                {
                                    char existing = grid.GetLetter(x, startYPosition + i);
                                    if (existing != '\0' && existing != word[i])
                                    {
                                        canPlace = false;
                                        break;
                                    }
                                }

                                if (canPlace)
                                {
                                    for (int i = 0; i < word.Length; i++)
                                    {
                                        grid.SetLetter(x, startYPosition + i, word[i]);
                                        placedWord = true;
                                    }
                                    return; 
                                }
                            }
                        }
                    }
                }

            }
        }

    }
}



