using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private Camera _firstPlaneCamera;

    private Camera activeCamera;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _firstPlaneCamera.enabled = false;
        SetActiveCamera(_mainCamera);
    }

    public void ActivateMainCamera()
    {
        activeCamera.enabled = false;
        SetActiveCamera(_mainCamera);
    }

	public void ActivateFirstPlaneCamera()
	{
		activeCamera.enabled = false;
		SetActiveCamera(_firstPlaneCamera);
	}



	private void SetActiveCamera(Camera camera)
    {
        activeCamera = camera;
        camera.enabled = true;
    }
}
