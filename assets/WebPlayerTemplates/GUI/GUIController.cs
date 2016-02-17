using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GUIController : MonoBehaviour {

	public GameObject GoldCounter;
	public GameObject IncenseCounter;
	public GameObject[] bloodBarSections;

	public GameObject GoldIcon;
	public GameObject BloodIcon;
	public GameObject IncenseIcon;
	public GameObject DotaOverlay;
	public GameObject SwitchToRitualButton;

	private bool[] bloodBarStates = new bool[10];

	private AudioSource audioSource;
	public AudioClip goldGained;
	public AudioClip incenseGained;
	public AudioClip bloodGained;
	public AudioClip ritualSound;
	
	public int tempBlood, tempIncense, tempGold;

    // MASTERMIND COUNTERS
    public GameObject MMGoldCounter;
    public GameObject MMIncenseCounter;
    public GameObject MMBloodCounter;
    
    // track what resources we had before changing things in mastermind
    private int[] savedCounters;

	// Use this for initialization
	void Start () {
		audioSource = GetComponent<AudioSource> ();

        MMBloodCounter.SetActive(false);
        MMIncenseCounter.SetActive(false);
        MMGoldCounter.SetActive(false);

    }

	public void AddToBloodCounter () {
		//BloodCounter.GetComponentInChildren<Text> ().text = ScriptHub.station.gameControlScript.BloodAmount.ToString();
		if (ScriptHub.station.gameControlScript.BloodAmount > 10) return;
		bloodBarSections[ScriptHub.station.gameControlScript.BloodAmount - 1].GetComponent<Animator>().SetBool("isFilled", true);
		audioSource.PlayOneShot (bloodGained, 0.55f);

	}
	public void AddToGoldCounter () {
		GoldCounter.GetComponentInChildren<Text> ().text = ScriptHub.station.gameControlScript.GoldAmount.ToString();
		audioSource.PlayOneShot (goldGained, 1f);
	}
	public void AddToIncenseCounter () {
		IncenseCounter.GetComponentInChildren<Text> ().text = ScriptHub.station.gameControlScript.IncenseAmount.ToString();
		audioSource.PlayOneShot (incenseGained, 0.60f);
	}

    // REMOVE RESOURCES METHODS //
    public void removeBlood(int i)
    {	
		if(ScriptHub.station.gameControlScript.BloodAmount <= 10){
			bloodBarSections[ScriptHub.station.gameControlScript.BloodAmount].GetComponent<Animator>().SetBool("isFilled", false);
			bloodBarStates[i] = false;
			MMBloodCounter.GetComponentInChildren<Text>().text = ScriptHub.station.gameControlScript.BloodAmount.ToString();
		}
    }

    public void removeIncense()
    {
        IncenseCounter.GetComponentInChildren<Text>().text = ScriptHub.station.gameControlScript.IncenseAmount.ToString();
        MMIncenseCounter.GetComponentInChildren<Text>().text = ScriptHub.station.gameControlScript.IncenseAmount.ToString();
    }

    public void removeGold()
    {
        GoldCounter.GetComponentInChildren<Text>().text = ScriptHub.station.gameControlScript.GoldAmount.ToString();
        MMGoldCounter.GetComponentInChildren<Text>().text = ScriptHub.station.gameControlScript.GoldAmount.ToString();
    }
	
	// temporarily set MM counts when one is dragged
	public void tempSetResource (int type, int alreadyUsed) {
		switch (type) {
			case 0:
				MMBloodCounter.GetComponentInChildren<Text>().text = (ScriptHub.station.gameControlScript.BloodAmount - alreadyUsed).ToString();
				tempBlood = (ScriptHub.station.gameControlScript.BloodAmount - alreadyUsed);
				break;
			case 1:
				MMIncenseCounter.GetComponentInChildren<Text>().text = (ScriptHub.station.gameControlScript.IncenseAmount - alreadyUsed).ToString();
				tempIncense = (ScriptHub.station.gameControlScript.IncenseAmount - alreadyUsed);
				break;
			case 2:
				MMGoldCounter.GetComponentInChildren<Text>().text = (ScriptHub.station.gameControlScript.GoldAmount - alreadyUsed).ToString();
				tempGold = (ScriptHub.station.gameControlScript.GoldAmount - alreadyUsed);
				break;
			default:
				print("Passed tempSetResource() wrong type");
				break;
		}
	}
	
	public void SyncResources () {
		while (ScriptHub.station.gameControlScript.BloodAmount > tempBlood) {
			removeBlood(--ScriptHub.station.gameControlScript.BloodAmount);
		}
		ScriptHub.station.gameControlScript.IncenseAmount = tempIncense;
		removeIncense();
		ScriptHub.station.gameControlScript.GoldAmount = tempGold;
		removeGold();
	}


    public void PlayRitualSound () {
		audioSource.PlayOneShot (ritualSound, 0.65f);
	}

	public void EnableGUI () {
		GoldCounter.SetActive (true);
		IncenseCounter.SetActive (true);
		GoldIcon.SetActive (true);
		BloodIcon.SetActive (true);
		IncenseIcon.SetActive (true);
		DotaOverlay.SetActive (true);
		SwitchToRitualButton.SetActive (true);

        MMBloodCounter.SetActive(false);
        MMIncenseCounter.SetActive(false);
        MMGoldCounter.SetActive(false);


		for (int i = 0; i < 10; i++) {
			bloodBarSections[i].GetComponent<Animator>().SetBool("isFilled", bloodBarStates[i]);
		}
	}
	public void DisableGUI () {
		for (int i = 0; i < 10; i++) {
			bloodBarStates[i] = bloodBarSections[i].GetComponent<Animator>().GetBool("isFilled");
		}

		GoldCounter.SetActive (false);
		IncenseCounter.SetActive (false);
		GoldIcon.SetActive (false);
		BloodIcon.SetActive (false);
		IncenseIcon.SetActive (false);
		DotaOverlay.SetActive (false);
		SwitchToRitualButton.SetActive (false);

        MMBloodCounter.SetActive(true);
        MMIncenseCounter.SetActive(true);
        MMGoldCounter.SetActive(true);

        MMGoldCounter.GetComponentInChildren<Text>().text = ScriptHub.station.gameControlScript.GoldAmount.ToString();
        MMIncenseCounter.GetComponentInChildren<Text>().text = ScriptHub.station.gameControlScript.IncenseAmount.ToString();
        MMBloodCounter.GetComponentInChildren<Text>().text = ScriptHub.station.gameControlScript.BloodAmount.ToString();
    }
}
