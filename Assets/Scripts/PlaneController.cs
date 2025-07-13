using System.Collections.Generic;
using UnityEngine;

public class PlaneController : MonoBehaviour
{
	public SOCommand takeoffCommand;
	public SOCommand landCommand;
	public SOCommand lightOnCommand;
	public SOCommand lightoffCommand;

	[Header("Lights")]
	public List<Light> NavLights = new List<Light>();
	public List<Light> RunwayLight = new List<Light>();

	[SerializeField]
	private Plane_State _state;

	private void OnEnable()
	{
		_state = Plane_State.Grounded;

		Debug.Log($"{gameObject.name} activated");
		takeoffCommand?.RegisterListener(TakeOff);
		landCommand?.RegisterListener(Landing);
		lightOnCommand?.RegisterListener(LightsOn);
		lightoffCommand?.RegisterListener(LightsOff);
	}

	void TakeOff()
	{
		Debug.Log($"{gameObject.name}: Taking Off");
	}

	void Landing()
	{
		Debug.Log($"{gameObject.name}: Landing in progress");
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

	void UpdateLightsBasedOnStatus(bool offsignal = false)
	{
		switch (_state)
		{
			case Plane_State.Grounded:
				if (offsignal)
				{
					UpdateLights(RunwayLight, false);
					break;
				}

				UpdateLights(NavLights, false);
				UpdateLights(RunwayLight, true); 
				break;
			case Plane_State.InFlight:
				if (offsignal)
				{
					UpdateLights(NavLights, false);
					break;
				}

				UpdateLights(RunwayLight, false);
				UpdateLights(NavLights, true);
				break;
			case Plane_State.Landing:
				if (offsignal)
				{
					UpdateLights(NavLights, false);
					UpdateLights(RunwayLight, false);
					break;
				}

				UpdateLights(NavLights, true);
				UpdateLights(RunwayLight, true);
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
}
