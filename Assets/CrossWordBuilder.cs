using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
public class CrossWordBuilder : MonoBehaviour
{
    public List<string> words = new List<string> { "shops", "greeds", "simplify", "game", "puffed", "fig","cosmic","magic","dimension","infect","victim","foreign","supernatural", "kills", "paws", "leopar","freezing","renting","thriller","halloween","ballet", "scene","fines","police","earth","chessboard"};
    public CrossWordGrid grid;
    private int startX = 2;
    private int startY = 7;

    public int maxWords = 5;
    bool placedWord = false;
    private void Start()
    {
        BuildCrossWord();
    }
    public void BuildCrossWord()
    {

        if (words == null || words.Count == 0)
        {
            Debug.LogWarning("No words to build crossword!");
            return;
        }

        string longestWord = words[0];

        for (int i = 1; i < words.Count; i++)
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
        while (maxWords>0 && words.Count >0)
        {
            int randomIndex = Random.Range(0, words.Count);
            string word = words[randomIndex];

            placedWord = false;
            PlaceWord(word);

            if (placedWord)
            {
                maxWords--;
                wordCount++;
                words.RemoveAt(randomIndex);
            }
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

                            TryPlaceWord(word, x, y, targetIndex, vertical: true,hasInteraction:true); 
                            TryPlaceWord(word, x, y, targetIndex, vertical: false,hasInteraction:true);

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

            if (vertical)
            {
                if (startY > 0 && grid.GetLetter(x, startY - 1) != '\0')
                    return;
            }
            else
            {
                if (startX > 0 && grid.GetLetter(startX - 1, y) != '\0')
                    return;
            }

            for (int i = 0; i < word.Length; i++)
            {
                int checkX = startX + (vertical ? 0 : i);
                int checkY = startY + (vertical ? i : 0);
                char existing = grid.GetLetter(checkX, checkY);

                if (existing != '\0' && existing != word[i])
                {
                    canPlace = false;
                    break;
                }

                if (existing == word[i])
                {
                    hasIntersection = true;
                    continue;
                }

                if (vertical)
                {
                    if (checkX > 0 && grid.GetLetter(checkX - 1, checkY) != '\0') { canPlace = false; break; }
                    if (checkX < grid.width - 1 && grid.GetLetter(checkX + 1, checkY) != '\0') { canPlace = false; break; }
                }
                else
                { 
                    if (checkY > 0 && grid.GetLetter(checkX, checkY - 1) != '\0') { canPlace = false; break; }
                    if (checkY < grid.height - 1 && grid.GetLetter(checkX, checkY + 1) != '\0') { canPlace = false; break; }
                }
            }

            if (canPlace)
            {
                if (vertical)
                {
                    if (startY + word.Length < grid.height && grid.GetLetter(x, startY + word.Length) != '\0')
                        canPlace = false;
                }
                else
                {
                    if (startX + word.Length < grid.width && grid.GetLetter(startX + word.Length, y) != '\0')
                        canPlace = false;
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




