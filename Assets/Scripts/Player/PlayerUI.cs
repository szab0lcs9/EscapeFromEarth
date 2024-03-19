using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    internal GameObject healthBar;
    internal GameObject shieldBar;

    Canvas canvas;
    Vector3 barPositionAdjust = new Vector3(0f, 10f, 0f);
    Vector3 bottomPositionAdjust = new Vector3(0f, 20f, 0f);

    [SerializeField] GameObject healthBarPrefab;
    [SerializeField] GameObject shieldBarPrefab;


    // Start is called before the first frame update
    void Start()
    {
        canvas = FindObjectOfType<Canvas>();

        healthBar = Instantiate(healthBarPrefab, canvas.transform);
        shieldBar = Instantiate(shieldBarPrefab, canvas.transform);


    }

    // Update is called once per frame
    void Update()
    {
        //PositionHealthAndShieldBar();
    }

    private void PositionHealthAndShieldBar()
    {
        Vector3 canvasPosition = canvas.transform.position;
        Vector3 bottomPosition = new Vector3(canvasPosition.x, canvasPosition.y - canvasPosition.y, canvasPosition.z);

        healthBar.GetComponent<RectTransform>().position = bottomPosition + bottomPositionAdjust + barPositionAdjust;
        shieldBar.GetComponent<RectTransform>().position = bottomPosition + bottomPositionAdjust + barPositionAdjust * 2;
    }

}
