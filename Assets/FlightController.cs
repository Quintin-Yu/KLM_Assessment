using UnityEngine;
using System;

[RequireComponent(typeof(Rigidbody), typeof(Transform))]
public class FlightController : MonoBehaviour
{
    public float thrust = 500f;
    public float lift = 25f;
    public float takeOffSpeed = 30f;

	public Action<Plane_State> planeState;

    [SerializeField] private Transform forwardReference;
	private Rigidbody rb;
    private bool isTakingOff = false;
    private bool hasTakenOff = false;

    [Header("Lift")]
    private float maxUpwardTrust = 10f;

   void Awake()
    {
        rb = GetComponent<Rigidbody>();

        if (forwardReference == null)
            Debug.LogWarning($"Forward reference not found on {gameObject.name}");
    }

    public void StartTakeOff()
    {
        isTakingOff = true;
        hasTakenOff = false;
        planeState?.Invoke(Plane_State.TakingOff);
    }

    void FixedUpdate()
    {
        if (!isTakingOff && !hasTakenOff)
        {
            Debug.Log("dead");
            return;
        }
        Debug.Log("Alive");

        ApplyForwardMomentum();
		
		float forwardSpeed = Vector3.Dot(rb.linearVelocity, forwardReference.forward);

        ApplyUpwardMomentum(forwardSpeed);

		Debug.DrawLine(transform.position, transform.position + forwardReference.forward * 10, Color.blue);
		Debug.DrawLine(transform.position, transform.position + transform.up * 10, Color.blue);
	}

    private void ApplyForwardMomentum()
    {
		Vector3 force = forwardReference.forward * thrust * Time.deltaTime;
		float clampedMagnitude = Mathf.Clamp(force.magnitude, -15f, 15f);
		force = force.normalized * clampedMagnitude;

		rb.AddForce(force, ForceMode.Acceleration);
	}

	private void ApplyUpwardMomentum(float forwardMomentum)
	{
        if (!hasTakenOff && forwardMomentum >= takeOffSpeed) ApplyLift();

		if (hasTakenOff)
		{
			float liftAmount = lift * forwardMomentum * Time.deltaTime;

            liftAmount = Mathf.Clamp(liftAmount, 0f, maxUpwardTrust);

			Debug.Log($"Continuous Lift: {liftAmount}");
			rb.AddForce(transform.up * liftAmount, ForceMode.Acceleration);
		}

        float maxHeight = 7f;

        if (transform.position.y >= maxHeight)
        {
            transform.position = new Vector3(transform.position.x , maxHeight, transform.position.z);
        }
	}

	private void ApplyLift()
    {
		hasTakenOff = true;
		isTakingOff = false;

        Vector3 liftForce = Vector3.up * lift * Time.deltaTime;
        Debug.Log(liftForce);

        rb.AddForce(Vector3.up * lift * Time.deltaTime, ForceMode.Impulse);
        rb.constraints = RigidbodyConstraints.None;

        planeState?.Invoke(Plane_State.InFlight);
    }
}
