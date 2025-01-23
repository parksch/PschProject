using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIText : MonoBehaviour
{
    const int referenceDisplayValue = 10000;

    [SerializeField] protected Text text;

    public void SetText(string str, long num) => text.text = str + GetValue(num);
    public void SetText(string str, int num) => text.text = str + GetValue(num);
    public void SetText(string str) => text.text = str;
    public void SetText(int num)
    {
        text.text = GetValue(num);
    }
    public void SetText(long num)
    {
        text.text = GetValue(num);
    }
    public void SetText(float num)
    {
        if (num - Mathf.Floor(num) == 0)
        {
            text.text = GetValue((long)num);
        }
        else
        {
            text.text = num.ToString();
        }
    }

    string GetValue(long num)
    {
        string value = string.Empty;

        if (num < referenceDisplayValue)
        {
            value = (num == 0 ? "0" : string.Format("{0:#,###}", num));
        }
        else
        {
            float target = num;
            int alphabetOffset = 26;
            char startChar = 'A';

            int unitLevel = 0;
            while (target >= referenceDisplayValue)
            {
                target /= referenceDisplayValue;
                unitLevel++;
            }

            string unitString = "";
            while (unitLevel > 0)
            {
                unitLevel--;
                unitString = (char)(startChar + (unitLevel % alphabetOffset)) + unitString;
                unitLevel /= alphabetOffset;
            }

            value = $"{target:0.##}{unitString}";
        }

        return value; 
    }

}
