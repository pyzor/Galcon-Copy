using UnityEngine;

public struct PlanetSelection {
    public Planet[] SelectedPlanets;
    public Planet TargetPlanet;
}

public class SinglePlanetMouseSelector : MonoBehaviour, IPlanetSelector {

    private Planet _firstSelectedPlanet = null;

    public PlanetSelection Select(Planet[] planets, int TeamID) {
        if (PlayerMouseInput.Instance.LeftMouseButtonJustPressed) {
            if (_firstSelectedPlanet == null) {
                _firstSelectedPlanet = CheckPlanets(planets, TeamID);
            } else {
                Planet targetPlanet = CheckPlanets(planets);
                if (targetPlanet == null || targetPlanet == _firstSelectedPlanet) {
                    _firstSelectedPlanet.Selected = false;
                    _firstSelectedPlanet = null;
                } else {
                    var planet = _firstSelectedPlanet;
                    _firstSelectedPlanet.Selected = false;
                    _firstSelectedPlanet = null;
                    return new PlanetSelection {
                        SelectedPlanets = new Planet[] { planet },
                        TargetPlanet = targetPlanet
                    };
                }
            }
            if(_firstSelectedPlanet != null) {
                _firstSelectedPlanet.Selected = true;
            }
        }

        return new PlanetSelection {
            SelectedPlanets = (_firstSelectedPlanet != null) ? new Planet[] { _firstSelectedPlanet } : new Planet[0],
            TargetPlanet = null
        };
    }

    private static Planet CheckPlanets(Planet[] planets, int teamID = -1) {
        for (int i = 0; i < planets.Length; i++) {
            if (PlanetCloseEnough(planets[i])) {
                return (teamID >= 0) ? ((planets[i].TeamID == teamID) ? planets[i] : null) : planets[i];
            }
        }
        return null;
    }

    private static bool PlanetCloseEnough(Planet planet) {
        var position = planet.transform.position;
        var mousePos = PlayerMouseInput.Instance.MouseCursorWorldPosition;
        mousePos.z = position.z;

        if (Vector3.SqrMagnitude(mousePos - position) <= (planet.Radius * planet.Radius)) {
            return true;
        }

        return false;
    }
}

