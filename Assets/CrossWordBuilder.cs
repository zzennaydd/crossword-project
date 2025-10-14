using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
public class CrossWordBuilder : MonoBehaviour
{
    List<string> words = new List<string> { "karayip", "kriket", "gül", "test", "astana", "kalabalýk", "biber", "gri", "kesik", "altýn" };
    public TextMeshProUGUI longWord;
    public TextMeshProUGUI targetWord;
    List<string> targetWords = new List<string>();
    char[] targetLetters;
    private void Start()
    { 
        string longestWord = words[0];
        
        for (int i = 0; i < words.Count; i++)
        {
            if (words[i].Length > longestWord.Length)

                longestWord = words[i];
                longWord.text = longestWord;
                targetLetters = longestWord.ToCharArray();

        }
        words.Remove(longestWord);

        foreach(var word in words)
        {
            bool hasLetter = false;
            foreach(var letter in word) {
                if (targetLetters.Contains(letter)){
                    hasLetter = true;
                    break;
                }

            }
            if (hasLetter)
                targetWords.Add(word);
        }
        int randomIndex = Random.Range(0, targetWords.Count);
        string nextWord = targetWords[randomIndex];
        targetWord.text = nextWord;

    }

}

