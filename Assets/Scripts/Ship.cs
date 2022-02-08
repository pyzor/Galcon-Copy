using System.Collections;
using UnityEngine;


public class Ship : MonoBehaviour {

    [SerializeField] private float Speed;

    private ShipMaterial _shipMaterial;
    private Rigidbody2D _rigidbody2D;
    private CircleCollider2D _circleCollider2D;

    public int ShipSquadID { get; private set; }
    public int TeamID { get; private set; }
    public float Radius { get; private set; }

    private Planet _targetPlanet;


    public void SetTargetPlanet(Planet planet, int teamID) {
        _targetPlanet = planet;
        TeamID = teamID;
        _shipMaterial.SetColor((TeamID == 1) ? Color.green : (TeamID == 2) ? Color.red : Color.gray); // TODO Change coloring
        StartCoroutine(ShipToTarget());
    }

    public void SetShipSquadID(int squadSpawned) {
        ShipSquadID = squadSpawned;
        _circleCollider2D.name = $"ShipSquad{ShipSquadID}";
    }

    private IEnumerator ShipToTarget() {
        var distance = Vector3.SqrMagnitude(_targetPlanet.transform.position - transform.position);
        float closestDist = _targetPlanet.Radius - transform.localScale.x * 0.5f;

        while (distance > closestDist) {
            var direction = (_targetPlanet.transform.position - transform.position).normalized;
            transform.up = (direction + new Vector3(_rigidbody2D.velocity.x, _rigidbody2D.velocity.y, direction.z)).normalized;
            _rigidbody2D.velocity = direction * Time.deltaTime * Speed;
            distance = Vector3.SqrMagnitude(_targetPlanet.transform.position - transform.position);
            yield return null;
        }

        if (_targetPlanet.TeamID != TeamID) {
            _targetPlanet.ShipCount -= 1;
            if (_targetPlanet.ShipCount <= 0)
                _targetPlanet.TeamID = TeamID;
        } else {
            _targetPlanet.ShipCount += 1;
        }
        Destroy(gameObject);
    }

    private void Awake() {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _shipMaterial = GetComponent<ShipMaterial>();
        _circleCollider2D = GetComponent<CircleCollider2D>();
    }

    private void OnCollisionEnter2D(Collision2D col2d) {
        if (!col2d.collider.name.Equals($"ShipSquad{ShipSquadID}") && !col2d.collider.name.Equals("Planet")) {
            Physics2D.IgnoreCollision(col2d.collider, col2d.otherCollider);
        }
    }

}
