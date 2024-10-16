using UnityEngine;

public class MaintainWheelDistance : MonoBehaviour
{
    [SerializeField] private Transform wheelOne;
    [SerializeField] private Transform wheelTwo;
    [SerializeField] private Transform centralPivot;  // The object that will stay between the wheels
    [SerializeField] private float fixedDistance = 2f; // Desired distance between the two wheels

    void Update()
    {
        MaintainFixedDistance();
        UpdatePivotPosition();
    }

    private void MaintainFixedDistance()
    {
        Vector3 direction = wheelOne.position - wheelTwo.position;
        float currentDistance = direction.magnitude;
        float distanceDifference = currentDistance - fixedDistance;

        if (Mathf.Abs(distanceDifference) > Mathf.Epsilon)
        {
            direction = direction.normalized;
            Vector3 offset = direction * (distanceDifference / 2);
            wheelOne.position -= offset;  // Move wheelOne closer
            wheelTwo.position += offset;  // Move wheelTwo farther
        }
    }

    private void UpdatePivotPosition()
    {
        // Update the centralPivot to stay at the midpoint between the two wheels
        centralPivot.position = (wheelOne.position + wheelTwo.position) / 2;
    }
}