using UnityEngine;
using UnityEngine.InputSystem;

public class RaycastDestinationSetter : MonoBehaviour
{
    [SerializeField] private DirectedAgent _directedAgent;
    private void Update()
    {
        if (Mouse.current.leftButton.isPressed)
        {
            Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
            if (Physics.Raycast(ray, out RaycastHit hitInfo))
            {
                Vector3 destination = hitInfo.point;
                _directedAgent.GoToLocation(destination);
            }
        }
    }
}
