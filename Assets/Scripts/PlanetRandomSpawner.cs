using UnityEngine;


class PlanetRandomSpawner : MonoBehaviour, IPlanetSpawner {

    [SerializeField] private GameObject PlanetPrefab;

    [SerializeField] private int InitialShipsMin;
    [SerializeField] private int InitialShipsMax;
    
    private ISphereSpawner _sphereSpawner;
    

    private void Awake() {
        _sphereSpawner = GetComponent<ISphereSpawner>();
    }
    

    public Planet[] SpawnPlanets(int planetCount) {
        var spheres = _sphereSpawner.SpawnSpheres(planetCount);

        Planet[] planets = new Planet[spheres.Length];

        for(int i = 0; i < planets.Length; i++) {
            var planet = Instantiate(PlanetPrefab, transform).GetComponent<Planet>();
            var sphere = spheres[i];
            planet.Init(sphere.Radius, sphere.Position, Random.Range(InitialShipsMin, InitialShipsMax));
            planets[i] = planet;
        }

        return planets;
    }
}
