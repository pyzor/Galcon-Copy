using UnityEngine;

public class PlayerMouseInput : MonoBehaviour {

    public static PlayerMouseInput Instance { get; private set; }

    public bool LeftMouseButtonPressed {
        get { return _leftMouseButtonState != 0; }
    }

    public bool LeftMouseButtonJustPressed {
        get { return _leftMouseButtonState == 2; }
    }

    public bool LeftMouseButtonReleased {
        get { return _leftMouseButtonState == 0; }
    }

    public Vector3 MouseCursorWorldPosition {
        get { return _mouseCursorWorldPosition; }
    }

    [SerializeField] private Camera MainCamera;

    // 0 - released
    // 1 - held
    // 2 - just pressed
    private int _leftMouseButtonState;
    private Vector3 _mouseCursorWorldPosition;

    private void Update() {
        _mouseCursorWorldPosition = MainCamera.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetMouseButtonDown(0)) {
            _leftMouseButtonState = 2;
        }else if (Input.GetMouseButton(0)) {
            _leftMouseButtonState = 1;
        } else {
            _leftMouseButtonState = 0;
        }
    }

    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
        } else {
            Instance = this;
        }
    }

    private void OnDestroy() {
        if(Instance == this) {
            Instance = null;
        }
    }
}
