using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckAnswer : MonoBehaviour
{
    public Text[] answers;
    public int correct;
    public Text scoreTxt;

    public void CheckAns()
    {

        correct = 0;

        if (answers[0].text.Contains("Christopher") || answers[0].text.Contains("크리스토퍼"))
        {
            correct++;
        }

        if (answers[1].text.Contains("Paper Knife") || answers[1].text.Contains("paper knife") || answers[1].text.Contains("페이퍼 나이프"))
        {
            correct++;
        }

        if(answers[2].text.Contains("Michelle") || answers[2].text.Contains("미쉘"))
        {
            correct++;
        }

        if (answers[3].text.Contains("inheritance") || answers[3].text.Contains("유산") || answers[3].text.Contains("유산상속"))
        {
            correct++;
        }


        scoreTxt.text = correct + " / 4";
    }
}
