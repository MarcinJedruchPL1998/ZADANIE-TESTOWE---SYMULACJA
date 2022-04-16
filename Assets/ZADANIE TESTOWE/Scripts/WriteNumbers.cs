using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WriteNumbers : MonoBehaviour
{
    [SerializeField] GameObject numbersPanel;
    [SerializeField] Text numbersText;
    public void OpenNumbersPanel()
    {
        numbersPanel.SetActive(true);

        for(int i = 0; i < 100; i++)
        {
            int number = i + 1;
            string numberTxt;

            if(number % 3 == 0) //The number is divisible by 3
            {
                numberTxt = "Marko, ";

                if(number % 5 == 0) //The number is divisible by 3 and 5
                {
                    numberTxt = "MarkoPolo, ";
                }
            }
            else if(number % 5 == 0) //The number is divisible by 5
            {
                numberTxt = "Polo, ";

                if(number % 3 == 0) //The number is divisible by 5 and 3
                {
                    numberTxt = "MarkoPolo, ";
                }
            }
            
            else //The number is non divisible by 3 or 5
            {
                numberTxt = number.ToString() + ", ";
            }

            numbersText.text += numberTxt;
        }
    }

    public void CloseNumbersPanel()
    {
        numbersPanel.SetActive(false);
        numbersText.text = "";
    }
}
