using UnityEngine.EventSystems;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public Interactable focus;

    public LayerMask movementMask;

    Camera cam;
    PlayerMotor motor;

	void Start ()
    {
        cam = Camera.main;
        motor = GetComponent<PlayerMotor> ();
	}
	
	void Update ()
    {
        if (EventSystem.current.IsPointerOverGameObject ())
            return;

        if (GameManager.instance.gameState != GameManager.GameState.Default)
            return;

		if (Input.GetMouseButtonDown (0))
        {
            Ray ray = cam.ScreenPointToRay (Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray,out hit, 100, movementMask))
            {
                motor.MoveToPoint (hit.point);
                RemoveFocus ();
            }
        }

        if (Input.GetMouseButtonDown (1))
        {
            Ray ray = cam.ScreenPointToRay (Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast (ray, out hit, 100))
            {
                Interactable interactable = hit.collider.GetComponent<Interactable> ();
                if (interactable != null)
                {
                    SetFocus (interactable);
                }
            }
        }
    }

    void SetFocus (Interactable newFocus)
    {
        if (newFocus != focus)
        {
            // If we are currently focusing on some interactable object, defocus it.
            if (focus != null)
                focus.OnDefocused ();

            focus = newFocus;
            motor.FollowTarget (newFocus);
        }

        newFocus.OnFocused (transform);        
    }

    void RemoveFocus ()
    {
        if (focus != null)
            focus.OnDefocused ();

        focus = null;
        motor.StopFollowingTarget ();
    }
}
