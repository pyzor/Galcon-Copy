using System.Collections;
using UnityEngine;


public class Planet : MonoBehaviour {

    [SerializeField] private float PassiveShipGenerationPerSecond;

    private PlanetMaterial _planetMaterial;
    private PlanetShipCount _planetShipCount;
    private CircleCollider2D _circleCollider2D;

    private int _teamID;

    public bool Selected { set { _planetMaterial.SetSelected(value); } }
    public int ShipCount { get { return _planetShipCount.Count; } set { _planetShipCount.Count = value; } }
    public int TeamID {
        get { return _teamID; }
        set {
            _teamID = value;
            _planetMaterial.SetColor((_teamID == 1) ? Color.green : (_teamID == 2) ? Color.red : Color.gray); // TODO Change coloring
        }
    }
    public float Radius { get; private set; }

    public void Init(float radius, Vector3 position, int shipCount) {
        Radius = radius;
        transform.localScale = new Vector3(Radius * 2f, Radius * 2f, 1);
        transform.localPosition = position;
        _planetShipCount.Count = shipCount;
        _planetShipCount.ReScaleText();
        _circleCollider2D.name = "Planet";
        StartCoroutine(ShipGenerator());
    }

    private void Awake() {
        _planetMaterial = GetComponent<PlanetMaterial>();
        _planetShipCount = GetComponent<PlanetShipCount>();
        _circleCollider2D = GetComponent<CircleCollider2D>();
    }

    private IEnumerator ShipGenerator() {
        float shipDelta = 0;
        while (true) {
            if (TeamID > 0) {
                shipDelta += Time.deltaTime * PassiveShipGenerationPerSecond;
                if (shipDelta >= 1) {
                    shipDelta -= 1;
                    ShipCount += 1;
                }
            }
            yield return null;
        }
    }
}
