using UnityEngine;

[System.Serializable]
public class Wave {

	public GameObject[] enemies;
	public int count;
	public float rate;
	public float healthMultiplier = 1f;
    public float speedMultiplier = 1f;
	public float difficultyMultiplier = 1f;

}
