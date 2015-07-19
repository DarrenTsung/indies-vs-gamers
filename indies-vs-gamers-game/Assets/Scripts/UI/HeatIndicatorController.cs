using UnityEngine;
using System.Collections;

public class HeatIndicatorController : MonoBehaviour {
	public float indicatorHeight;
	
	protected HeatController _pHeatController;
	protected GameObject _indicatorObject;
	protected tk2dTextMesh _indicatorTextMesh;
	protected Vector3 _indicatorBasePosition;

	protected void Awake() {
		_pHeatController = GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<HeatController>();
		_indicatorObject = transform.Find("Indicator").gameObject;
		_indicatorTextMesh = _indicatorObject.transform.Find("TextMesh").gameObject.GetComponent<tk2dTextMesh>();
		_indicatorBasePosition = _indicatorObject.transform.position;
	}
	
	protected void Update() {
		UpdateIndicator(_pHeatController.HeatPercentage);
	}
	
	protected void UpdateIndicator(float percentage) {
		_indicatorObject.transform.position = _indicatorBasePosition + new Vector3(0.0f, indicatorHeight * percentage, 0.0f);
		
		int percent = (int)(percentage * 100.0f);
		_indicatorTextMesh.text = "Heat: " + percent.ToString() + "%";
	}
}
