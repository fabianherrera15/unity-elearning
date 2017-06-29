using UnityEngine;
using System.Collections;
using System.Configuration;
using System.IO;



public class submitDataBlock
{
    public string Des;
    public string Title;
    public string Extensions;
    public submitDataBlock(string d, string t, string e)
    {
        Des = d;
        Title = t;
        Extensions = e;
    }
}
   
public class ELEContainer : MonoBehaviour
{
    public Texture2D GUI_BG;
    static public ELE.Services.CMI5.CMI5 mCMI;
    static public ELE.Services.CMI5.LearningRecord mLearningRecord = null;
    static public int ExperianceID;
    static public GameObject character;
    static public int mCoinCount = 0;
    void Start () {
        
        
       // StreamReader objReader = new StreamReader("SELEConfig.ini");
       // string[] config = objReader.ReadToEnd().Split(new char[] {';'});
       // System.Console.WriteLine(config);
       // Debug.Log(config);
       // ExperianceID = System.Convert.ToInt16(config[1]);
       // lrsurl = config[0];
        mLearningRecord = null;
        //if (ELEContainer.mLearningRecord == null)
        //    ELEContainer.mLearningRecord = ELEContainer.mCMI.GetInitializedDataModel(ExperianceID); 
        character = GameObject.Find("Character (Lerpz)");
        character.active = false;
	}
    void Stop()
    {
      
    }
   
    private Rect background = new Rect(0, 0, Screen.width, Screen.height);
    private Rect windowRect = new Rect(0, 0, 250, 100);
    void OnKey()
    {

    }
    void OnGUI()
    {
        if (ELEContainer.mLearningRecord == null)
        {
           
            GUI.BeginGroup(background);
            GUIStyle style = new GUIStyle();
            style.imagePosition = ImagePosition.ImageOnly;
            style.stretchHeight = true;
            style.stretchWidth = true;
            style.fixedHeight = Screen.height;
            style.fixedWidth = Screen.width;
            GUI.DrawTexture(background, GUI_BG, ScaleMode.StretchToFill);
            windowRect = GUILayout.Window(0, windowRect, DoControlsWindow, "ELEDemo");
            GUI.EndGroup();
        }
        GUIStyle style2 = new GUIStyle();
       
       

        // The window can be dragged around by the users - make sure that it doesn't go offscreen.
        windowRect.x = Mathf.Clamp(windowRect.x, 0.0f, Screen.width - windowRect.width);
        windowRect.y = Mathf.Clamp(windowRect.y, 0.0f, Screen.height - windowRect.height);

        windowRect.x = (Screen.width - windowRect.width)/2;
        windowRect.y = (Screen.height - windowRect.height)/2;
    }
    private bool InitializedClicked = false;
    private string expid = "";
    private string lrsurl = "http://localhost/eleservices/cmi5.asmx";
    
    void DoControlsWindow(int id)
    {
        GUI.color = new Color(255.0f, 255.0f, 255.0f, .5f);
        
        GUILayout.Label("Enter Experience PIN");
        //if (ELEContainer.mLearningRecord != null)
         //   GUILayout.Label(ELEContainer.mLearningRecord.UserID.ToString());

        expid = GUILayout.TextField(expid);
        try
        {
           ExperianceID = System.Convert.ToInt16(expid);
        }catch(System.Exception e)
        {
            ExperianceID = -1;
        }
        GUILayout.Label("Enter LRS URL");
        lrsurl = GUILayout.TextField(lrsurl);
        if (ExperianceID != -1)
        {
            if (!InitializedClicked)
                InitializedClicked = GUILayout.Button("Initialize");
            if (InitializedClicked == true && ELEContainer.mLearningRecord == null)
            {
                mCMI = new ELE.Services.CMI5.CMI5(lrsurl);
                ELEContainer.mLearningRecord = ELEContainer.mCMI.GetInitializedDataModel(ExperianceID);
                character.active = true;
            }
        }

    }
    void AddInteraction(object sdb)
    {
        System.Threading.Thread t = new System.Threading.Thread(ELEContainer.submitdata);
        t.Start(sdb);
    }
    public static void submitdata(object data)
    {
        string Des = ((submitDataBlock)data).Des;
        string Title = ((submitDataBlock)data).Title;
        string Extensions = ((submitDataBlock)data).Extensions;

        if (Title == "Coin")
            mCoinCount++;


        if (ELEContainer.mLearningRecord == null)
            return;
        if (ELEContainer.mLearningRecord.PerformanceData == null)
            ELEContainer.mLearningRecord.PerformanceData = new ELE.Services.CMI5.PerformanceData();
        if (ELEContainer.mLearningRecord.PerformanceData.Extensions == null)
            ELEContainer.mLearningRecord.PerformanceData.Extensions = new ELE.Services.CMI5.Extensions();
        if (ELEContainer.mLearningRecord.PerformanceData.Extensions.Interactions == null)
            ELEContainer.mLearningRecord.PerformanceData.Extensions.Interactions = new ELE.Services.CMI5.Interaction[1];

        ELE.Services.CMI5.Interaction[] mNewInts = new ELE.Services.CMI5.Interaction[1];

        //  int count = 0;
        // foreach (ELE.Services.CMI5.Interaction i in mLearningRecord.PerformanceData.Extensions.Interactions)
        //  {
        //     mNewInts[count] = i;
        //      count++;
        //  }

        ELEContainer.mLearningRecord.PerformanceData.Extensions.Interactions = mNewInts;

        ELE.Services.CMI5.Interaction result = new ELE.Services.CMI5.Interaction();
        result.Description = Des;
        result.Title = Title;
        result.Extensions = Extensions;

        mNewInts[mNewInts.Length - 1] = result;


        ELEContainer.mLearningRecord.PerformanceData.Completion = "";
        ELEContainer.mLearningRecord.PerformanceData.Mastery = "";
        ELEContainer.mLearningRecord.PerformanceData.Score = mCoinCount;
        ELEContainer.mCMI.UpdateAttempt(ELEContainer.mLearningRecord);

    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}