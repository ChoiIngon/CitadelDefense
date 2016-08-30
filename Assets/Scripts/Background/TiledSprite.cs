using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SpriteRenderer))]
public class TiledSprite : MonoBehaviour {
	public int row;
	public int col;
	private SpriteRenderer spriteRenderer;
	// Use this for initialization
	void Start () {
		spriteRenderer = GetComponent<SpriteRenderer> ();
		Vector2 spriteSize = new Vector2(spriteRenderer.bounds.size.x / transform.localScale.x, spriteRenderer.bounds.size.y / transform.localScale.y);

		// Generate a child prefab of the sprite renderer
		GameObject childPrefab = new GameObject();
		SpriteRenderer childSprite = childPrefab.AddComponent<SpriteRenderer>();
		childPrefab.transform.position = transform.position;
		childSprite.sprite = spriteRenderer.sprite;
		childSprite.sortingOrder = spriteRenderer.sortingOrder;
		// Loop through and spit out repeated tiles
		GameObject child;
		for(int y=0; y<col; y++)
		{
			for (int x = 0; x < row; x++)
			{
				child = Instantiate(childPrefab) as GameObject;				
				child.transform.position = transform.position + (new Vector3 (x * (spriteSize.x-0.05f), y * (spriteSize.y-0.05f), 0.0f)); 
				child.transform.parent = transform;
			}
		}

		// Set the parent last on the prefab to prevent transform displacement
		childPrefab.transform.parent = transform;

		// Disable the currently existing sprite component since its now a repeated image
		spriteRenderer.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
