using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using MTUnity.Utils;

public class DayBtn : MonoBehaviour {

    public Image BtnImg;
    public Text BtnText;


    public Image CrownImg;
	// Use this for initialization


    public void UpdateByDate(int date,bool isFuture)
    {
    }


    public void BePressed()
    {
        if (isInDate)
        {
            ChallengeMgr.current.BtnPress(_currentDate);
        }
        else
        {
            ChallengeMgr.current.ShowWrongDate("You cannot try future challenges before they are available");
            //Debug.Log("not the day " + _currentDate);
        }
    }

    DateTime _currentDate;
    public void SetDay(DateTime date)
    {
        _currentDate = date;
        BtnText.text = date.Day.ToString();
    }

    bool isInDate = false;

    public void SetInDate (bool b)
    {
        isInDate = b;
        
        if (b)
        {
            BtnImg.enabled = true;
        }
        else
        {
            BtnImg.enabled = false;
            CrownImg.gameObject.SetActive(false);
        }
    }

    MTJSONObject _js;


    string ToYearMonth(DateTime dt)
    {
        return dt.Year + "_" + dt.Month;
    }

    string ToYMD(DateTime dt)
    {
        return dt.Year + "_" + dt.Month + "_" + dt.Day;
    }

    public bool isSolved = false;
    internal void UpdateDate(DateTime _selectedDate,MTJSONObject js)
    {
        isSolved = false;
        _js = js[ToYMD(_currentDate)];
        DayState dateSate = (DayState)_js.GetInt(CSaveEnum.DayState.ToString());
        Sprite sel = ResMgr.current.CrownSel;
        if (dateSate == DayState.UNSOLVED)
        {
            sel = ResMgr.current.BtnSpSel;
            CrownImg.gameObject.SetActive(false);
            BtnText.gameObject.SetActive(true);
     
        }
        else if (dateSate == DayState.SOVLED_IN_TIME)
        {
            isSolved = true;
            CrownImg.gameObject.SetActive(true);
            BtnText.gameObject.SetActive(false);
            CrownImg.sprite = ResMgr.current.CrownInTime;

        }
        else if (dateSate == DayState.SOVLED_AFTER)
        {
            isSolved = true;
            CrownImg.gameObject.SetActive(true);
            BtnText.gameObject.SetActive(false);
            CrownImg.sprite = ResMgr.current.CrownAfter;
        }

        if (_selectedDate == _currentDate)
        {
            BtnImg.sprite = sel;
            BtnText.color = Color.white;
        }
        else
        {
            BtnImg.sprite = ResMgr.current.BtnSpUnSel;
            BtnText.color = new Color(99f / 255f, 101f / 255f, 102f / 255f);
        }
    }
}
