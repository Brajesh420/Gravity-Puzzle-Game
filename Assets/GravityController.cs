using UnityEngine;

public class GravityController : MonoBehaviour
{
    public Vector3 gravityDirection = Vector3.down;
    public float gravityStrength = 20f;

    private Vector3 selectedDirection;

    public GameObject hologram;

    void Update()
    {
        HandleInput();
    }

    void HandleInput()
    {
        // Use player-relative directions instead of world directions

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            selectedDirection = transform.forward;
            ShowHologram();
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            selectedDirection = -transform.forward;
            ShowHologram();
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            selectedDirection = -transform.right;
            ShowHologram();
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            selectedDirection = transform.right;
            ShowHologram();
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            gravityDirection = selectedDirection.normalized;
            hologram.SetActive(false);
        }
    }

    void ShowHologram()
    {
        hologram.SetActive(true);

        // Place hologram in selected direction
        hologram.transform.position = transform.position + selectedDirection.normalized * 1.5f;

        // Make hologram face direction but stay upright
        hologram.transform.rotation = Quaternion.LookRotation(selectedDirection, transform.up);
    }
}