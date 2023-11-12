using UnityEngine;
using DG.Tweening;

public class WheelLuck : MonoBehaviour
{
    public PickRandomBoost _pickRanomBoost;

    public Transform wheelTransform;
    public int numberOfSegments;
    public int spinCount;
    public int stopSegmentIndex;
    public float rotateDuration;
    public Ease rotateEase;

    private float _segmentAngle;
    private bool _isSpinning = false;

    private void Start()
    {
        _segmentAngle = 360f / numberOfSegments;
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            SpinWheel();
        }
    }

    public void SpinWheel()
    {
        stopSegmentIndex = _pickRanomBoost.GetRandomItemNumber();

        Debug.Log(stopSegmentIndex);

        if (_isSpinning)
            return;

        _isSpinning = true;

        int targetSpinCount = Random.Range(spinCount, spinCount + 3); 
        float targetAngle = (stopSegmentIndex * _segmentAngle + spinCount * 360f); 

        Debug.Log(targetAngle);

        wheelTransform.DORotate(new Vector3(0f, 0f, targetAngle), rotateDuration * (targetSpinCount + 1), RotateMode.FastBeyond360)

            .SetEase(rotateEase)
            .OnComplete(() =>
            {
                Debug.Log("You landed on segment: " + stopSegmentIndex);
                _isSpinning = false;
            });
    }
}