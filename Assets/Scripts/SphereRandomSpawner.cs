using UnityEngine;

public class SphereRandomSpawner : MonoBehaviour, ISphereSpawner {

    [SerializeField] private Vector2 SpawnBounds;
    [SerializeField] private float MaxPlanetDiameter;
    [SerializeField] private float MinGapInBetweenPlanets;

    public Sphere[] SpawnSpheres(int sphereCount) {
        float universalMaxRadiusPow2 = MaxPlanetDiameter * MaxPlanetDiameter;

        float planet_N = (SpawnBounds.x * SpawnBounds.y) / (universalMaxRadiusPow2 + MinGapInBetweenPlanets);
        sphereCount = Mathf.Min(sphereCount, (int)planet_N);

        Sphere[] spheres = new Sphere[sphereCount];

        Vector2 bounds = SpawnBounds - new Vector2(MaxPlanetDiameter, MaxPlanetDiameter);

        for (int i = 0; i < sphereCount; i++) {
            var pos = GenerateRandomPosition(bounds);

            bool possitionFits = false;
            while (i > 0 && !possitionFits) {
                possitionFits = true;
                for (int j = 0; j < i; j++) {
                    var sdist = Vector3.SqrMagnitude(spheres[j].Position - pos);

                    if (sdist < universalMaxRadiusPow2 + MinGapInBetweenPlanets) {
                        possitionFits = false;
                    }
                }

                if (!possitionFits)
                    pos = GenerateRandomPosition(bounds);
            }

            spheres[i] = new Sphere {
                Position = pos,
                Radius = Random.Range(MaxPlanetDiameter * 0.25f, MaxPlanetDiameter * 0.5f)
            };
        }
        return spheres;
    }

    private static Vector3 GenerateRandomPosition(Vector2 bounds) {
        return new Vector3(Random.Range(-bounds.x, bounds.x), Random.Range(-bounds.y, bounds.y), 0);
    }
}