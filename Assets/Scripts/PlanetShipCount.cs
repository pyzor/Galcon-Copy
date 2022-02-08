using UnityEngine;


public class PlanetShipCount : MonoBehaviour {

    [SerializeField] private TMPro.TMP_Text _text;
    
    private int _shipCount;

    public int Count { get { return _shipCount; }
        set {
            if(value >= 0 && value != _shipCount) {
                _shipCount = value;
                UpdateText();
            }
        }
    }

    public void ReScaleText() {
        var textScale = _text.gameObject.transform.localScale;
        var scale = transform.localScale;
        textScale.x *= 1f / scale.x;
        textScale.y *= 1f / scale.y;
        textScale.z = 1f;
        _text.gameObject.transform.localScale = textScale;
    }

    private void UpdateText() {
        _text.SetText($"{_shipCount}");
    }

    private void Awake() {
        ReScaleText();
    }


}
