using UnityEngine;
using UnityEngine.EventSystems;

namespace GameCore.Character.PlayerCharacter
{
    public class PlayerController : MonoBehaviour
    {
        public Interactable focus; // Our current focus: Item, Enemy etc.

        public LayerMask movementMask; // Filter out everything not walkable

        private Camera _cam; // Reference to our camera
        private PlayerMotor _motor; // Reference to our motor

        // Get references
        private void Start()
        {
            _cam = Camera.main;
            _motor = GetComponent<PlayerMotor>();
        }

        // Update is called once per frame
        private void Update()
        {
            // if (EventSystem.current.IsPointerOverGameObject())
            //     return;

            // If we press left mouse
            // if (Input.GetMouseButtonDown(0))
            // {
            //     // We create a ray
            //     var ray = _cam.ScreenPointToRay(Input.mousePosition);
            //     RaycastHit hit;
            //
            //     // If the ray hits
            //     if (Physics.Raycast(ray, out hit, 100, movementMask))
            //     {
            //         _motor.MoveToPoint(hit.point); // Move to where we hit
            //         RemoveFocus();
            //     }
            // }

            // If we press right mouse
            if (Input.GetMouseButtonDown(1))
            {
                // We create a ray
                var ray = _cam.ScreenPointToRay(Input.mousePosition);

                // If the ray hits
                if (Physics.Raycast(ray, out var hit, 100))
                {
                    if (hit.transform.CompareTag("Enemy"))
                    {
                        var interactable = hit.collider.GetComponent<Interactable>();
                        if (interactable != null) 
                            SetFocus(interactable);
                    }
                }
            }
        }

        // Set our focus to a new focus
        private void SetFocus(Interactable newFocus)
        {
            // If our focus has changed
            if (newFocus != focus)
            {
                // Defocus the old one
                if (focus != null)
                    focus.OnDefocused();

                focus = newFocus; // Set our new focus
                _motor.FollowTarget(newFocus); // Follow the new focus
            }

            newFocus.OnFocused(transform);
        }

        // Remove our current focus
        private void RemoveFocus()
        {
            if (focus != null)
                focus.OnDefocused();

            focus = null;
            _motor.StopFollowingTarget();
        }
    }
}