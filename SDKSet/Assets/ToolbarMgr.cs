using UnityEngine;
using System.Collections;
using MTUnity.Actions;

public class ToolbarMgr : MonoBehaviour {

    public static ToolbarMgr current;
    void Awake()
    {
        current = this;
        originalPos = toolbar.localPosition;
        originalLayoutPos = toolLayout.localPosition;
        originalHideBtn = HideBtn.localRotation;
    }

    Vector3 originalPos;
    Vector3 originalLayoutPos;
    Quaternion originalHideBtn;

    public GameObject PlayCanvas;
    public GameObject ChallengeCanvas;
    public Transform HideBtn;

    public void ShowPlayMenu()
    {
        if (!IsAllowPress())
        {
            return;
        }
        SoundManager.Current.Play_ui_open(0);
        //Debug.Log("show play menu");

        if (ChallengeMgr.current.ChallengeActive)
        {
            ChallengeCanvas.SetActive(true);
        }
        else
        {
            PlayCanvas.SetActive(true);
        }
    }

    float lastSwitchTime = 0;
    const float switchInterval = 0.2f;
    bool showingToolbar = true;
    public void SwitchToolbar()
    {
        if (Time.time > lastSwitchTime + switchInterval)
        {
           
            ShowToolBar(!showingToolbar);
        }

        
      

    }

    public void SwitchToolbar(bool b)
    {
        if (Time.time > lastSwitchTime + switchInterval)
        {

            ShowToolBar(b);
        }
    }

    bool isSwitching = false;
    float ShowToolBar(bool show)
    {
        lastSwitchTime = Time.time;
        showingToolbar = show;
        float height = 200f;
        float moveTime = 0.2f;
        toolbar.gameObject.StopAllActions();
        toolLayout.gameObject.StopAllActions();
        if (!show)
        {

            toolbar.gameObject.RunAction(new MTMoveTo(moveTime, new Vector3(originalPos.x, originalPos.y - height, originalPos.z)));
            toolLayout.gameObject.RunAction(new MTMoveTo(moveTime, new Vector3(originalLayoutPos.x, originalLayoutPos.y - height, originalLayoutPos.z)));
            HideBtn.localRotation = Quaternion.identity;
            //HideBtn.gameObject.RunAction(new MTRotateTo(moveTime,Quaternion.identity));


        }
        else
        {
            toolbar.gameObject.RunAction(new MTMoveTo(moveTime, originalPos));
            toolLayout.gameObject.RunAction(new MTMoveTo(moveTime, originalLayoutPos));
            HideBtn.localRotation = originalHideBtn;
            //HideBtn.gameObject.RunAction(new MTRotateTo(0,originalHideBtn));
        }
        return moveTime;
    }



    public void HidePlayMenu()
    {


        if (PlayCanvas == null)
        {
            return;
        }

        if (PlayCanvas.activeSelf)
        {
            SoundManager.Current.Play_ui_close(0);
        }
       // PlayCanvas.SetActive(false);
       PlayCanvas.SetActive(false);
        ChallengeCanvas.SetActive(false);

    }

    [Header("ChallengeBtn")]
    public RectTransform UndoBtn;
    public RectTransform ChallengeBtn;
    public RectTransform SettingBtn;
    public RectTransform PlayBtn;
    public RectTransform HintBtn;
    public void HideChallengeBtn()
    {

        ChallengeBtn.gameObject.SetActive(false);
        float len = UndoBtn.localPosition.x - SettingBtn.localPosition.x;
        var playPos = PlayBtn.localPosition;

        PlayBtn.localPosition = new Vector3(SettingBtn.localPosition.x + len / 3, playPos.y, playPos.z);
        HintBtn.localPosition = new Vector3(SettingBtn.localPosition.x + len / 3 * 2, playPos.y, playPos.z);


    }

    public void ShowChallengeBtn()
    {
        ChallengeBtn.gameObject.SetActive(true);
        float len = UndoBtn.localPosition.x - SettingBtn.localPosition.x;
        var playPos = PlayBtn.localPosition;

        PlayBtn.localPosition = new Vector3(SettingBtn.localPosition.x + len / 2, playPos.y, playPos.z);
        HintBtn.localPosition = new Vector3(SettingBtn.localPosition.x + len / 4 * 3, playPos.y, playPos.z);

    }

    bool IsAllowPress()
    {
        if (LevelMgr.current.IsBlockUIBtn())
        {
            return false;
        }
        if (LevelMgr.current.isCardPressing())
        {
            return false;
        }
        return true;
    }

    public void ShowSettingMenu()
    {
        if (!IsAllowPress())
        {
            return;
        }
        SoundManager.Current.Play_ui_open(0);
        SettingMenu.SetActive(true);
        ShowToolBar(false);
        //Debug.Log("showSettingMenu");
    }

    public void HideSettingMenu()
    {
        if (SettingMenu.activeSelf)
        {
            SoundManager.Current.Play_ui_close(0);
        }
        var t = _settingSlid.SlidOut();
        StartCoroutine(HideSetting(t));
       // SettingMenu.SetActive(false);

        //  Debug.Log("hidesettingmenu");
    }

    IEnumerator HideSetting(float t)
    {
        yield return new WaitForSeconds(t);
        SettingMenu.SetActive(false);
    }

    public void ShowDailyMenu()
    {
        if (!IsAllowPress())
        {
            return;
        }
        ChallengeMgr.current.ShowChallenge(); 

    }

    public void HideDailyMenu()
    {
        DailyMenu.SetActive(false);
    }

   // public GameObject PlayCanvas;
    public GameObject SettingMenu;
    public GameObject DailyMenu;

    public RectTransform toolbar;
    public RectTransform toolLayout;

    public void MoveUp(float height = 90f)
    {
        toolbar.localPosition = new Vector3(originalPos.x, originalPos.y + height, originalPos.z);
        toolLayout.localPosition = new Vector3(originalLayoutPos.x, originalLayoutPos.y + height, originalLayoutPos.z);
    }


    public SlideIn _settingSlid;

}
