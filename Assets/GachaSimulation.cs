using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GachaSimulation : MonoBehaviour
{
    public Button pullButton;
    public Text displayText;
    public float mean=0.5f;                // mean value of log-normal distribution
    public float standardDeviation=0.5f;
    public float displayRandom=0;

    public int pulls;
    private int guaranteedCharacterIndex;
    private bool guaranteedCharacterObtained;

    private readonly Character[] characters =
    {
        new Character { name = "Character 1", dropRate = 0.05f },
        new Character { name = "Character 2", dropRate = 0.2f },
        new Character { name = "Character 3", dropRate = 0.3f },
        new Character { name = "Character 4", dropRate = 0.35f },
        new Character { name = "Character 5", dropRate = 0.1f }
    };

    private void Start()
    {
        guaranteedCharacterIndex = 0;
        pullButton.onClick.AddListener(Pull);
    }

    private void Pull()
    {
        if (pulls == 10 && !guaranteedCharacterObtained)
        {
            guaranteedCharacterObtained = true;
            displayText.text = "You have obtained " + characters[guaranteedCharacterIndex].name + "!";
            return;
        }

        float standardNormal = Random.Range(-1f, 1f);

        // calculate log-normal distributed random number
        float randomNumber = Mathf.Exp(mean + standardDeviation * standardNormal);
        randomNumber = randomNumber / (Mathf.Exp(mean + standardDeviation));
        float currentRate = 0f;
        displayRandom=randomNumber;

        int characterIndex = 0;
        for (int i = 0; i < characters.Length; i++)
        {
            currentRate += characters[i].dropRate;
            if (randomNumber <= currentRate)
            {
                characterIndex = i;
                break;
            }
        }

        pulls++;
        displayText.text = "You have obtained " + characters[characterIndex].name + "!";
    }

    private class Character
    {
        public string name;
        public float dropRate;
    }
}