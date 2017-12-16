using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using GoogleMobileAds.Api;

public class AdManager : MonoBehaviour {

	[HideInInspector]
	public bool AdSeen = false;
	[HideInInspector]
	public bool AdReturn = false;
	[HideInInspector]
	public bool showBanner = false;

	void Start(){
	}

	void Update (){
//		if (showBanner) {
//			SetupAdBanner ();
//			showBanner = false;
//		}
	}
	public void ShowAdVideo(){
		AdSeen = false;
		AdReturn = false;
		//if (Advertisement.IsReady()) {
			Advertisement.Show ("rewardedVideo", new ShowOptions(){resultCallback = HandleAdResult});
		//}
		
	}

	private void HandleAdResult(ShowResult result){
		switch (result) {
		case ShowResult.Finished:
			print ("Fully watched Ad");
			AdSeen = true;
			AdReturn = true;
			break;
		case ShowResult.Skipped:
			print ("Skipped Ad");
			AdSeen = false;
			AdReturn = true;
			break;
		case ShowResult.Failed:
			print ("Unable to show Ad");
			AdSeen = false;
			AdReturn = true;
			break;
		}

	}

	public void SetupAdBanner (){
		print ("Show banner");
		string bannerId = "ca-app-pub-8966431340100065/9977979958";

		BannerView banner = new BannerView (bannerId, AdSize.MediumRectangle, AdPosition.Top);
		AdRequest request = new AdRequest.Builder ().Build ();
		//AdRequest request = new AdRequest.Builder ().AddTestDevice("").Build ();
		banner.LoadAd (request);

	}
}
