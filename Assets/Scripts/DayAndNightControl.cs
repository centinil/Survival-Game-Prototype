//2016 Spyblood Games

using UnityEngine;
using System.Collections;

public class DayAndNightControl : MonoBehaviour {
	Mesh mesh;
	public int currentDay = 0; //day 8287... still stuck in this grass prison... no esacape... no freedom...
	public string DayState;
	public Light directionalLight; //the directional light in the scene we're going to work with
	public float SecondsInAFullDay = 120f; //in realtime, this is about two minutes by default. (every 1 minute/60 seconds is day in game)
	[Range(0,1)]
	public float currentTime = 0; //at default when you press play, it will be nightTime. (0 = night, 1 = day)
	[HideInInspector]
	public float timeMultiplier = 1f; //how fast the day goes by regardless of the secondsInAFullDay var. lower values will make the days go by longer, while higher values make it go faster. This may be useful if you're siumulating seasons where daylight and night times are altered.

	float lightIntensity; //static variable to see what the current light's insensity is in the inspector

	// Use this for initialization
	void Start () {
		lightIntensity = directionalLight.intensity; //what's the current intensity of the light
	
	}
	
	// Update is called once per frame
	void Update () {
		UpdateLight();
		//CheckTimeOfDay ();
		currentTime += (Time.deltaTime / SecondsInAFullDay) * timeMultiplier;
		if (currentTime >= 1) {
			currentTime = 0;//once we hit "midnight"; any time after that sunrise will begin.
			currentDay++; //make the day counter go up
		}
	}

	void UpdateLight()
	{
		directionalLight.transform.localRotation = Quaternion.Euler ((currentTime * 360f) - 90, 170, 0);
		

		float intensityMultiplier = 1;

		if (currentTime <= 0.23f || currentTime >= 0.75f) 
		{
			intensityMultiplier = 0; //when the sun is below the horizon, or setting, the intensity needs to be 0 or else it'll look weird
		}
		else if (currentTime <= 0.25f) 
		{
			intensityMultiplier = Mathf.Clamp01((currentTime - 0.23f) * (1 / 0.02f));
		}
		else if (currentTime <= 0.73f) 
		{
			intensityMultiplier = Mathf.Clamp01(1 - ((currentTime - 0.73f) * (1 / 0.02f)));
		}

		directionalLight.intensity = lightIntensity * intensityMultiplier;
	}
    /*
	void CheckTimeOfDay ()
	{
	if (currentTime < 0.25f || currentTime > 1f) {
			DayState = "Midnight";
		}
		if (currentTime > 0.25f)
		{
			DayState = "Morning";
		}
		if (currentTime > 0.25f && currentTime < 0.5f)
		{
			DayState = "Mid Noon";
		}
		if (currentTime > 0.5f && currentTime < 0.75f)
		{
			DayState = "Evening";
		}
		if (currentTime > 0.75f && currentTime < 1f)
		{
			DayState = "Night";
		}
	}
    */
	void OnGUI()
	{
		//debug GUI on screen visuals
		GUI.Box (new Rect (15, 15, 100, 25), "Day: " + currentDay);
		//GUI.Box (new Rect (40, 40, 200, 30), "" + DayState);
	}
}
