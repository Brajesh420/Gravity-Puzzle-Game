using UnityEngine;

public class GravityController : MonoBehaviour
{
    public Vector3 gravityDirection = Vector3.down;

    private Vector3 selectedDirection;

    public GameObject hologram;

    void Update()
    {
        HandleInput();
    }

    void HandleInput()
    {
        // Preview directions (arrow keys)
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

        // Apply gravity (Enter)
        if (Input.GetKeyDown(KeyCode.Return))
        {
            gravityDirection = selectedDirection.normalized;

            if (hologram != null)
                hologram.SetActive(false);
        }
    }

    void ShowHologram()
    {
        if (hologram == null) return;

        hologram.SetActive(true);

        hologram.transform.position =
            transform.position + selectedDirection.normalized * 1.5f;

        hologram.transform.rotation =
            Quaternion.LookRotation(selectedDirection, transform.up);
    }
}