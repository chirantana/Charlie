using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using GoogleMobileAds.Api;

public class InputField : MonoBehaviour {

 public Text charText;
 public Text placeholderText;
 public GameObject Panel1;
 public GameObject Panel2;
 public GameObject Panel3;
 public GameObject Panel4;

 public Camera Camera2;
 public Camera MenuCam;

 private Animator animatorforEntry;

 private Animator pencilTurner;

 private BannerView bannerView;
 private InterstitialAd interstitial;
 private float Counter = 1;
 private int TimeKeeper = 1;


	// Use this for initialization
	void Start () {
		charText = GameObject.Find("chartext").GetComponent<Text>();
		placeholderText = GameObject.Find("Placeholder").GetComponent<Text>();
		
		Panel1.SetActive(true);
		Panel2.SetActive(false);
		Panel3.SetActive(false);
		Panel4.SetActive(false);
		
		animatorforEntry = GameObject.Find("Menu Camera").GetComponent<Animator>();
		pencilTurner = GameObject.Find("Pencilblend2").GetComponent<Animator>();

		MenuCam.enabled = true;
		Camera2.enabled = false;

		RequestBanner();
		HideBanner();
		RequestInterstitial();
	}

	public void ClickFirst()
	{
		Panel1.SetActive(false);
		Panel2.SetActive(false);
		Panel3.SetActive(false);

		animatorforEntry.SetBool("IsClicked", true);

		Invoke("CameraSwitch", 3.0f);

	}
	
		public void CameraSwitch()
		{
			MenuCam.enabled = false;
			Camera2.enabled = true;
			Panel2.SetActive(true);
			Panel3.SetActive(false);
			ShowBanner();

		}

	public void CharacterField(string inputFieldString)
	{

		charText.text = "Charlie.. Charlie.. "+ inputFieldString +"?";
	}
	public void ConfirmText(){
		
		if(charText.text.Length >= 28)

		{
			Debug.Log("Confirmed to be genuine");

			Panel2.SetActive(false);
			Panel3.SetActive(true);
			ShowBanner();
			pencilTurner.SetInteger("AnswerNumber", Random.Range (1 , 3));

			Invoke("AskedAlready", 6f);
		}

		else
		Debug.Log("Please enter valid question");


	/*	if(placeholderText.enabled == true);
		Debug.Log("Enter a question");

		if (placeholderText.enabled == false);
		Debug.Log("Question confirmed");
	*/
	}
	public void AskedAlready(){
		
		Counter = Counter + 1f;

		pencilTurner.SetInteger("AnswerNumber", 0);
		Panel2.SetActive(true);
		Panel3.SetActive(false);
		RequestInterstitial();
		ShowBanner();

	}
	public void ExitGame(){
		if (interstitial.IsLoaded()) {
       interstitial.Destroy();}
		Application.Quit();
	}
 	public void LoadAgain()
 	{
 		Application.LoadLevel(Application.loadedLevel);
 	}
	void Update()
	{
		TimeKeeper = TimeKeeper + 1;
	if(Input.GetKey("escape")){
		bannerView.Destroy();
		Panel4.SetActive(true);
		Panel1.SetActive(false);
		Panel2.SetActive(false);
		Panel3.SetActive(false);
		if (interstitial.IsLoaded()) {
        interstitial.Show();
 		 }
	}
	if(Counter%2.0f == 0.0f)
	{
			if (interstitial.IsLoaded()) {
        interstitial.Show();
	}
	if(TimeKeeper%10000 == 0)
	{
			if (interstitial.IsLoaded()) {
        interstitial.Show();
	}
	}
	}
}
	private void RequestBanner()
{
    #if UNITY_ANDROID
        string adUnitId = "ca-app-pub-9668220491500664/5036735438";
    #elif UNITY_IPHONE
        string adUnitId = "INSERT_IOS_BANNER_AD_UNIT_ID_HERE";
    #else
        string adUnitId = "unexpected_platform";
    #endif

    // Create a 320x50 banner at the top of the screen.
     bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.Top);
    // Create an empty ad request.
    AdRequest request = new AdRequest.Builder().Build();
    // Load the banner with the request.
    bannerView.LoadAd(request);
}

private void RequestInterstitial()
{
    #if UNITY_ANDROID
        string adUnitId = "ca-app-pub-9668220491500664/6513468637";
    #elif UNITY_IPHONE
        string adUnitId = "INSERT_IOS_INTERSTITIAL_AD_UNIT_ID_HERE";
    #else
        string adUnitId = "unexpected_platform";
    #endif

    // Initialize an InterstitialAd.
     interstitial = new InterstitialAd(adUnitId);
    // Create an empty ad request.
    AdRequest request = new AdRequest.Builder().Build();
    // Load the interstitial with the request.
    interstitial.LoadAd(request);
}
private void ShowBanner(){
		bannerView.Show ();
		
	}
	private void HideBanner(){
		bannerView.Hide ();
		
	}
}
