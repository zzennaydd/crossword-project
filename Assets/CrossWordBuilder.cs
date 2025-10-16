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

        foreach (var word in words)
        {
            bool hasLetter = false;
            foreach (var letter in word)
            {
                if (targetLetters.Contains(letter))
                {
                    hasLetter = true;
                    break;
                }

            }
            if (hasLetter)
            {
                targetWords.Add(word);
                Debug.Log(word);
            }
                
        }
       // int randomIndex = Random.Range(0, targetWords.Count);
        // string nextWord = targetWords[randomIndex];
 
    }

}

