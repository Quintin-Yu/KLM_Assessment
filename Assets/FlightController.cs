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
        if (!isTakingOff) return;

        rb.AddForce(forwardReference.forward * thrust * Time.deltaTime, ForceMode.Acceleration);

        float forwardSpeed = Vector3.Dot(rb.linearVelocity, forwardReference.forward);

        if (!hasTakenOff && forwardSpeed >= takeOffSpeed) ApplyLift();

        Debug.DrawLine(transform.position, transform.position + forwardReference.forward * 10, Color.blue);
    }

    private void ApplyLift()
    {
        isTakingOff = false;
        hasTakenOff = true;

        rb.AddForce(Vector3.up * lift * Time.deltaTime, ForceMode.Force);
        rb.constraints = RigidbodyConstraints.None;

        planeState?.Invoke(Plane_State.InFlight);
    }
}
