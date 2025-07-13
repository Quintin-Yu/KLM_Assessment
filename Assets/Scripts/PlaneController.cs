using UnityEngine;

public class PlaneController : MonoBehaviour
{
	public SOCommand takeoffCommand;
	public SOCommand landCommand;
	public SOCommand lightOnCommand;
	public SOCommand lightoffCommand;


	private void OnEnable()
	{
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
	}

	void LightsOff()
	{
		Debug.Log($"{gameObject.name}: Lights Off");
	}
}
