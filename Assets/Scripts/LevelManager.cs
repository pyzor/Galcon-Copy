using System.Collections.Generic;
using UnityEngine;


public class LevelManager : MonoBehaviour {

    [SerializeField] private int PlanetCount;
    [SerializeField] private int InitialShipCount;

    private IPlanetSpawner _planetSpawner;
    private IPlanetSelector _planetSelector;
    private IShipSpawner _shipSpawner;

    private Planet[] _planets;

    private void Awake() {
        _planetSpawner = GetComponent<IPlanetSpawner>();
        _planetSelector = GetComponent<IPlanetSelector>();
        _shipSpawner = GetComponent<IShipSpawner>();

        _planets = new Planet[0];
    }

    private void Start() {
        _planets = _planetSpawner.SpawnPlanets(PlanetCount);
        PlanetCount = _planets.Length;
        if (PlanetCount < 2)
            return;

        int playersPlanet = Random.Range(0, PlanetCount + 1);
        _planets[playersPlanet].ShipCount = InitialShipCount;
        _planets[playersPlanet].TeamID = 1;


        int enemyPlanet = playersPlanet;
        while(enemyPlanet == playersPlanet) {
            enemyPlanet = Random.Range(0, PlanetCount + 1);
        }
        _planets[enemyPlanet].ShipCount = 50;
        _planets[enemyPlanet].TeamID = 2;
    }
    private void Update() {
        var selection = _planetSelector.Select(_planets, 1);

        if (selection.TargetPlanet != null) {
            for (int i = 0; i < selection.SelectedPlanets.Length; i++) {
                var planet = selection.SelectedPlanets[i];

                var direction = (selection.TargetPlanet.transform.position - planet.transform.position).normalized;
                var spawnPoint = planet.transform.position + (direction * planet.Radius * 1.1f);


                int n = Mathf.CeilToInt(planet.ShipCount * 0.5f);
                planet.ShipCount -= n;
                var ships = _shipSpawner.SpawnShips(n, spawnPoint);
                for (int j = 0; j < ships.Length; j++) {
                    ships[j].SetTargetPlanet(selection.TargetPlanet, planet.TeamID);
                }

            }
        }


    }





}
