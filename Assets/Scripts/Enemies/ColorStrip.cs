using UnityEngine;

public class ColorStrip : MonoBehaviour {

    private static Color[] colors = { Color.red, Color.blue, Color.yellow };

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
        myColor = colors[Random.Range(0, 10000) % colors.Length];
        sprite.color = myColor;
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if(collider.gameObject.CompareTag("Player")) {
            isCrossedByPlayer = true;
        }
    }
	
}
