using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MovementController))]
public class InputController : MonoBehaviour
{
    MovementController movemementController;
    TouchControl moveTouchControl;
    TouchControl attackTouchControl;
    [SerializeField] private float maxInputDistance;
    [SerializeField] private float maxSquareInputDistance;

    void Start()
    {
        movemementController = GetComponent<MovementController>();
        moveTouchControl = new TouchControl(-1, Vector2.zero);
        attackTouchControl = new TouchControl(-1, Vector2.zero);
        maxInputDistance = (Screen.height / 4);
        maxSquareInputDistance = Mathf.Pow(maxInputDistance, 2);
    }

    void Update()
    {
        if (Input.touchCount == 0) return;
        int i = 0;
        while (i < Input.touchCount)
        {
            HandleTouch(Input.GetTouch(i));
            i++;
        }
    }

    void HandleTouch(Touch touch)
    {
        switch (touch.phase)
        {
            case TouchPhase.Stationary:
                break;
            case TouchPhase.Moved:
                HandleTouchUpdate(touch);
                break;
            case TouchPhase.Began:
                HandleTouchStart(touch);
                break;
            default:
                HandleTouchEnd(touch.fingerId);
                break;
        }
    }

    void HandleTouchStart(Touch touch)
    {
        Vector2 touchPosition = touch.position;
        if (IsMovementTouch(touchPosition))
        {
            moveTouchControl.UpdateTouchControl(touch.fingerId, touchPosition);
        }
        else
        {
            // TODO
        }
    }

    void HandleTouchUpdate(Touch touch)
    {
        if (touch.fingerId == moveTouchControl.fingerId)
        {
            movemementController.MoveTowards(
                GetInputVector(moveTouchControl.startPosition, touch.position)
            );
        }
    }

    void HandleTouchEnd(int touchId)
    {
        // if (touchId == moveTouchControl.fingerId)
        // {
        //     movemementController.MoveTowards(null);
        // }
    }

    Vector3 GetInputVector(Vector2 start, Vector2 current, bool isNormalized = false)
    {
        Vector2 result = (current - start);
        if (isNormalized || result.sqrMagnitude > maxSquareInputDistance)
        {
            result = result.normalized * maxInputDistance;
        }
        return result.ToXZVector3();
    }

    bool IsMovementTouch(Vector2 position)
    {
        return position.x <= Screen.width / 2;
    }


}

public class TouchControl
{

    public TouchControl(int fingerId, Vector2 startPosition)
    {
        this.fingerId = fingerId;
        this.startPosition = startPosition;
    }

    public void UpdateTouchControl(int fingerId, Vector2 startPosition)
    {
        this.fingerId = fingerId;
        this.startPosition = startPosition;
    }

    public int fingerId;
    public Vector2 startPosition;
}
