using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneController : MonoBehaviour
{
	public SOCommand takeoffCommand;
	public SOCommand landCommand;
	public SOCommand lightOnCommand;
	public SOCommand lightoffCommand;
	public SOCommand parkCommand;

	[Header("Lights")]
	public List<Light> NavLights = new List<Light>();
	public List<Light> RunwayLight = new List<Light>();

	[SerializeField] private Plane_State _state;
	[SerializeField] private FlightController _flightController;

	private Coroutine _coroutine;

	private void Awake()
	{
		if (_flightController == null)
			_flightController = GetComponent<FlightController>();

		_flightController.planeState += PlaneStateChange;
	}

	private void OnEnable()
	{
		_state = Plane_State.Grounded;

		Debug.Log($"{gameObject.name} activated");
		takeoffCommand?.RegisterListener(TakeOff);
		landCommand?.RegisterListener(Landing);
		lightOnCommand?.RegisterListener(LightsOn);
		lightoffCommand?.RegisterListener(LightsOff);
		parkCommand?.RegisterListener(Park);
	}

	#region Events_Region
	void TakeOff()
	{
		Debug.Log($"{gameObject.name}: Taking Off");
		_flightController.StartTakeOff();
		_state = Plane_State.TakingOff;
		UpdateLightsBasedOnStatus();
	}

	void Landing()
	{
		Debug.Log($"{gameObject.name}: Landing in progress");
		_state = Plane_State.Landing;
		UpdateLightsBasedOnStatus();
	}

	void Park()
	{
		Debug.Log($"{gameObject.name}: Parking");
		_state = Plane_State.Grounded;
		UpdateLightsBasedOnStatus();
	}

	void LightsOn()
	{
		Debug.Log($"{gameObject.name}: Lights On");
		UpdateLightsBasedOnStatus();
	}

	void LightsOff()
	{
		Debug.Log($"{gameObject.name}: Lights Off");
		UpdateLightsBasedOnStatus(true);
	}

	#endregion

	void UpdateLightsBasedOnStatus(bool shouldTurnOff = false)
	{
		switch (_state)
		{
			case Plane_State.Grounded:
				if (shouldTurnOff)
				{
					UpdateLights(RunwayLight, false);
					break;
				}

				UpdateLights(NavLights, false);
				UpdateLights(RunwayLight, true); 
				break;
			case Plane_State.InFlight:
				if (shouldTurnOff)
				{
					UpdateLights(NavLights, false);
					break;
				}

				UpdateLights(RunwayLight, false);
				UpdateLights(NavLights, true);
				break;
			case Plane_State.Landing:
				if (shouldTurnOff)
				{
					UpdateLights(NavLights, false);
					UpdateLights(RunwayLight, false);
					break;
				}

				UpdateLights(NavLights, true);
				UpdateLights(RunwayLight, true);

				if (_coroutine != null) StopCoroutine(_coroutine);
				_coroutine = StartCoroutine(LandingSequence(5));
				break;
			case Plane_State.TakingOff:
				if (shouldTurnOff)
				{
					UpdateLights(NavLights, false);
					UpdateLights(RunwayLight, false);
					break;
				}

				UpdateLights(NavLights, true);
				UpdateLights(RunwayLight, true);

				if (_coroutine != null) StopCoroutine(_coroutine);
				_coroutine = StartCoroutine(TakeOffSequence(5));
				break;
			case Plane_State.Parked:
				UpdateLights(NavLights, false);
				UpdateLights(RunwayLight, false);
				break;
		}
	}

	void UpdateLights(List<Light> lights, bool lightStatus)
	{
		foreach (var light in lights)
		{
			light.enabled = lightStatus;
		}
	}

	private void PlaneStateChange(Plane_State state)
	{
		_state = state;
		UpdateLightsBasedOnStatus();
	}

	private IEnumerator LandingSequence(float waitTime)
	{
		yield return new WaitForSeconds(waitTime);
		_state = Plane_State.Grounded;
		UpdateLightsBasedOnStatus();
	}

	private IEnumerator TakeOffSequence(float waitTime)
	{
		yield return new WaitForSeconds(waitTime);
		_state = Plane_State.InFlight;
		UpdateLightsBasedOnStatus();
	}
}
