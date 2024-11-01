using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIText : MonoBehaviour
{
    [SerializeField] Text text;

    public void SetText(string str, long num) => text.text = str + (num == 0 ? "0" : string.Format("{0:#,###}", num));
    public void SetText(string str, int num) => text.text = str + (num == 0 ? "0" : string.Format("{0:#,###}", num));
    public void SetText(string str) => text.text = str;
    public void SetText(int num) => text.text = (num == 0 ? "0" : string.Format("{0:#,###}", num));
    public void SetText(long num) => text.text = (num == 0 ? "0" : string.Format("{0:#,###}", num));
    public void SetText(float num) => text.text = (num == 0 ? "0" : string.Format("{0:#,###}", num));
}
