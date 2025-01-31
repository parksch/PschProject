using System;
using UnityEngine;

[System.Serializable]
public class UNBigStats
{
    [SerializeField] long mini = 0;
    [SerializeField] float token = 0;

    const int referenceValue = 100000000;
    const int referenceDisplayValue = 10000;

    public string Text
    {
        get
        {
            if (token <= 0 && mini < referenceDisplayValue)
                return mini.ToString();

            string unitString = "";
            double totalValue = token * referenceValue + mini;
            char startChar = 'A';
            int alphabetOffset = 26;
            int unitLevel = 0;

            while (totalValue >= referenceDisplayValue)
            {
                totalValue /= referenceDisplayValue;
                unitLevel++;
            }

            totalValue = Math.Floor(totalValue * 100) / 100f;

            while (unitLevel > 0)
            {
                unitLevel--; 
                unitString = (char)(startChar + (unitLevel % alphabetOffset)) + unitString;
                unitLevel /= alphabetOffset;
            }

            return $"{totalValue:0.##}{unitString}"; 
        }
    }
    public void SetZero() { mini = 0; token = 0; } 
    public bool IsZero => mini == 0 && token == 0; 
    public UNBigStats Copy
    {
        get
        {
            UNBigStats bigStats = Zero;
            bigStats += this;

            return bigStats;
        }
    }
    public static UNBigStats Zero
    {
        get
        {
            UNBigStats result = new UNBigStats();
            result.token = 0;
            result.mini = 0;

            return result;
        }
    }
    public static UNBigStats operator +(UNBigStats a, UNBigStats b)
    {
        UNBigStats result = Zero;

        result.mini = a.mini + b.mini;
        result.token = a.token + b.token;

        return FinishingWork(result);
    }
    public static UNBigStats operator +(UNBigStats a, int value)
    {
        UNBigStats b = Zero;

        if (value > 0)
        {
            b.mini += value % referenceValue;
            b.token += value / referenceValue;
            return a + b;
        }
        else
        {
            b.mini += Mathf.Abs(value % referenceValue);
            b.token += Mathf.Abs(value / referenceValue);
            return a - b;
        }
    }
    public static UNBigStats operator +(UNBigStats a, long value)
    {
        UNBigStats b = Zero;

        if (value > 0)
        {
            b.mini += value % referenceValue;
            b.token += value / referenceValue;

            return a + b;
        }
        else
        {
            b.mini += (long)Mathf.Abs(value % referenceValue);
            b.token += Mathf.Abs(value / referenceValue);

            return a - b;
        }
    }
    public static UNBigStats operator +(UNBigStats a, float value)
    {
        UNBigStats b = Zero;

        if (value > 0)
        {
            b.mini += (long)(value % referenceValue);
            b.token += Mathf.Floor(value / referenceValue);

            return a + b;
        }
        else
        {
            b.mini += (long)Mathf.Abs(value % referenceValue);
            b.token += Mathf.Floor(Mathf.Abs(value / referenceValue));

            return a - b;
        }
    }

    public static UNBigStats operator -(UNBigStats a, UNBigStats b)
    {
        UNBigStats result = Zero;

        if (b.token > a.token)
        {
            result.mini = 0;
            result.token = 0;
        }
        else
        {
            result.token = a.token - b.token;

            if (a.mini < b.mini)
            {
                if (result.token > 0)
                {
                    result.token--;
                    result.mini = referenceValue + a.mini - b.mini;
                }
                else
                {
                    result.mini = 0;
                    result.token = 0;
                }
            }
            else
            {
                result.mini = a.mini - b.mini;
            }

        }

        return FinishingWork(result);
    }
    public static UNBigStats operator -(UNBigStats a, int value)
    {
        UNBigStats b = Zero;

        if (value > 0)
        {
            b.mini += value % referenceValue;
            b.token += value / referenceValue;

            return a - b;
        }
        else
        {
            b.mini += Mathf.Abs(value % referenceValue);
            b.token += Mathf.Abs(value / referenceValue);

            return a + b;
        }
    }
    public static UNBigStats operator -(UNBigStats a, long value)
    {
        UNBigStats b = Zero;

        if (value > 0)
        {
            b.mini += value % referenceValue;
            b.token += value / referenceValue;

            return a - b;
        }
        else
        {
            b.mini += (long)Mathf.Abs(value % referenceValue);
            b.token += Mathf.Abs(value / referenceValue);

            return a + b;
        }
    }
    public static UNBigStats operator -(UNBigStats a, float value)
    {
        UNBigStats b = Zero;

        if (value > 0)
        {
            b.mini += (long)(value % referenceValue);
            b.token += Mathf.Floor(value / referenceValue);

            return a - b;
        }
        else
        {
            b.mini += (long)Mathf.Abs(value % referenceValue);
            b.token += Mathf.Floor(Mathf.Abs(value / referenceValue));

            return a + b;
        }
    }

    public static UNBigStats operator *(UNBigStats a, float multiplier)
    {
        UNBigStats result = Zero;

        result.mini = (long)Mathf.Round(a.mini * multiplier);
        result.token = a.token * multiplier;

        float value = result.token - Mathf.Floor(result.token);
        result.mini += (long)Mathf.Round(value * referenceValue);

        return FinishingWork(result);
    }
    public static float operator /(UNBigStats a, UNBigStats b)
    {
        if (a.token == 0 && b.token == 0 && (Mathf.Max(a.mini,b.mini) < 1000000 ))
        {
            return a.mini / (float)b.mini;
        }

        int offset = TargetValue(a.token, b.token);
        long scaleFactor = (100000 * offset) > 0 ? 100000 * offset : 10000;
        float scaledA = 0;
        float scaledB = 0;

        if (scaleFactor < referenceValue)
        {
            scaledA = a.mini / scaleFactor;
            scaledB = b.mini / scaleFactor;
        }

        scaledA += Mathf.Floor(a.token * (referenceValue / scaleFactor));
        scaledB += Mathf.Floor(b.token * (referenceValue / scaleFactor));

        int TargetValue(float a,float b)
        {
            float target = a > b ? a : b;
            int result = 0;

            for (int i = 1; i < target; i *= 10)
            {
                if (target > i)
                {
                    result = i;
                }
            }

            return result;
        }

        return scaledA/scaledB;
    }
    public static bool operator <=(UNBigStats a, UNBigStats b)
    {
        return (a.token <= b.token) && a.mini <= b.mini;
    }
     public static bool operator >=(UNBigStats a, UNBigStats b)
    {
        return (a.token >= b.token) && a.mini >= b.mini;
    }
    public static bool operator >(UNBigStats a, UNBigStats b)
    {
        if (a.token == b.token)
        {
            return a.mini > b.mini;
        }
        else
        {
            return (a.token > b.token);
        }
    }
    public static bool operator <(UNBigStats a, UNBigStats b)
    {
        if (a.token == b.token)
        {
            return a.mini < b.mini;
        }
        else
        {
            return (a.token < b.token);
        }
    }

    static UNBigStats FinishingWork(UNBigStats result)
    {
        if (result.token < 0)
        {
            result.token = 0;
            result.mini = 0;
        }
        else
        {
            result.token += result.mini / referenceValue;
            result.mini %= referenceValue;
        }

        return result;
    }
}
