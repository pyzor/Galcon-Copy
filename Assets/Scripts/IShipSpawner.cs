using UnityEngine;

public interface IShipSpawner {
    public Ship[] SpawnShips(int shipCount, Vector3 spawnPoint);
}