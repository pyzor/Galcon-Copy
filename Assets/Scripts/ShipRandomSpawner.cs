using UnityEngine;


public class ShipRandomSpawner : MonoBehaviour, IShipSpawner {

    [SerializeField] private GameObject ShipPrefab;
    [SerializeField] private float ShipScale;

    private static int _SquadSpawned = 0;

    private IPointDistribution _pointDistribution;
    
    private void Awake() {
        _pointDistribution = GetComponent<IPointDistribution>();
    }
    
    public Ship[] SpawnShips(int shipCount, Vector3 spawnPoint) {
        var points = _pointDistribution.GeneratePoints(shipCount);
        
        Ship[] ships = new Ship[points.Length];

        for (int i = 0; i < ships.Length; i++) {
            var ship = Instantiate(ShipPrefab, transform).GetComponent<Ship>();

            ship.transform.localScale = Vector3.one * ShipScale;
            ship.transform.localPosition = spawnPoint + points[i];

            ship.SetShipSquadID(_SquadSpawned);

            ships[i] = ship;
        }
        _SquadSpawned += 1;
        return ships;
    }
}
