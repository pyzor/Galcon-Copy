using UnityEngine;


public class SpiralPointDistribution : MonoBehaviour, IPointDistribution {

    [SerializeField] private float Spaceses;
    [SerializeField] private float AngleDiff;


    public Vector3[] GeneratePoints(int Count) {

        float angle = 0;
        float dist = Spaceses;

        var points = new Vector3[Count];
        for(int i = 0; i < Count; i++) {
            var point = new Vector3(
                dist * Mathf.Cos(dist + angle),
                dist * Mathf.Sin(dist + angle),
                0);

            angle += AngleDiff;
            dist += Spaceses;
            points[i] = point;
        }

        return points;
    }
}
