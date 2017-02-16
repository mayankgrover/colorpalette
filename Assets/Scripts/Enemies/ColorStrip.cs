using UnityEngine;

public class ColorStrip : MonoBehaviour {

    public Color myColor { get; private set; }
    SpriteRenderer sprite;

    public bool isCrossedByPlayer { get; private set; }

	void Start () {
        sprite = GetComponent<SpriteRenderer>();

        ControllerEnemies.Instance.AddStrip(this);
        ControllerMainMenu.Instance.GameStarted += ResetColor;
	}

    public void ResetStrip()
    {
        isCrossedByPlayer = false;
        ResetColor();
    }

    private void ResetColor()
    {
        myColor = Colors.GameColors[Random.Range(0, 10000) % ControllerGame.Instance.ColorsToUse];
        sprite.color = myColor;
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if(collider.gameObject.CompareTag("Player")) {
            isCrossedByPlayer = true;
        }
    }
	
}
