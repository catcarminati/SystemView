using UnityEngine;

public class SpawnZone : MonoBehaviour {

	public Vector3 SpawnPoint {
		get {
			var t = Random.insideUnitSphere;
			return t;
		}
	}
}