using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class reloj : MonoBehaviour {
	public Text timerText;
	private float secondsCount;
	private int minuteCount;
	private int hourCount;

	void Start(){
		secondsCount = 59.9f;
		minuteCount = 16;
		hourCount = 2;
	}

	public void UpdateTimerUI(){
		secondsCount -= Time.deltaTime;
		timerText.text = "0" + hourCount + " : " + minuteCount + " : " + (int)secondsCount;
		if (secondsCount < 10.0f){
			timerText.text = "0" + hourCount + " : " + minuteCount + " : 0" + (int)secondsCount;
		}
		if (minuteCount < 10){
			timerText.text = "0" + hourCount + " : 0" + minuteCount + " : " + (int)secondsCount;
		}
		if (secondsCount <= 0.0f){
			minuteCount--;
			secondsCount = 59.9f;
		}
		if (minuteCount <= 0){
			hourCount--;
			minuteCount = 59;
		}
	}

	void Update(){
		UpdateTimerUI();
	}
}