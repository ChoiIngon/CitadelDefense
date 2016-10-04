using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PanelResult : MonoBehaviour {
	public Sprite win;
	public Sprite lose;
	
	public Vector3 start;
	public Vector3 end;
	public float time;

	private float interpolate;
	
    public void Active(GameManager.WaveResult result)
    {
        Image image = GetComponent<Image>();
        if(GameManager.WaveResult.Win == result)
        {
            image.sprite = win;
        }
        else
        {
            image.sprite = lose;
        }
        gameObject.SetActive(true);
		StartCoroutine(_Update());
    }
		
    IEnumerator _Update()
    {
		Image image = GetComponent<Image>();
        interpolate = 0.0f;
		image.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        while (1.0f > interpolate)
        {
            interpolate += Time.deltaTime / time;
			transform.localPosition = Vector3.Lerp(start, end, interpolate);
            yield return null;
        }

        yield return new WaitForSeconds(1.0f);
        
        Color color = image.color;
        while(0.0f < image.color.a)
        {
            color.a -= Time.deltaTime;
            image.color = color;
            yield return null;
        }
        
        gameObject.SetActive(false);
    }
}
