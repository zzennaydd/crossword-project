using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
public class CrossWordBuilder : MonoBehaviour
{
    List<string> words = new List<string> { "shops", "greeds", "simplify", "game", "puffed", "fig","cosmic","magic","dimension","infect","victim","foreign","supernatural", "kills", "paws", "leopar","freezing","renting","thriller","halloween","ballet", "scene","cake","fines","police","earth","chessboard"};
    public CrossWordGrid grid;
    private int startX = 2;
    private int startY = 7;

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

        for (int i = 0; i < longestWord.Length; i++)
        {
            grid.SetLetter(startX + i, startY, longestWord[i]);
        }

        words.Remove(longestWord);

        int wordCount = 1;
        while ( words.Count > 0)
        {
            int randomIndex = Random.Range(0, words.Count);
            string word = words[randomIndex];

            placedWord = false;
            PlaceWord(word);

            if (placedWord)
                wordCount++;
                words.RemoveAt(randomIndex);
        }

        void PlaceWord(string word)
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

                            TryPlaceWord(word, x, y, targetIndex, vertical: true,hasInteraction:true); //dikey olarak dene
                            TryPlaceWord(word, x, y, targetIndex, vertical: false,hasInteraction:true); //yatay olarak dene

                            if (placedWord)
                                return;
                        }
                    }
                }
            }
        }

        void TryPlaceWord(string word, int x, int y, int targetIndex, bool vertical,bool hasInteraction)
        {
            int startX = x - (vertical ? 0 : targetIndex);
            int startY = y - (vertical ? targetIndex : 0);

           
            if (startX < 0 || startY < 0)
                return;

            if (vertical) 
            {
                if (startY + word.Length > grid.height)
                    return;
            }
            else
            {
                if (startX + word.Length > grid.width)
                    return;
            }

            bool canPlace = true;
            bool hasIntersection = false;


            for (int i = 0; i < word.Length; i++)
            {
                int checkX = startX + (vertical ? 0 : i);
                int checkY = startY + (vertical ? i : 0);
                char existing = grid.GetLetter(checkX, checkY);

                if (existing != '\0')
                {

                    if (existing == word[i])
                    {
                        hasIntersection = true;
                    }
                    else
                    {
                        canPlace = false;
                        break;
                    }
                }
                
            }
                if (canPlace && hasIntersection)
                {
                    for (int i = 0; i < word.Length; i++)
                    {
                        int placeX = startX + (vertical ? 0 : i);
                        int placeY = startY + (vertical ? i : 0);
                        grid.SetLetter(placeX, placeY, word[i]);
                    }
                    placedWord = true;
                }
            }
        }

    }




