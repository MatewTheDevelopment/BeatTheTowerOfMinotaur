using UnityEngine;

public class MinotaurManagement : MonoBehaviour
{
    [SerializeField] private Vector3 targetPoint;

    [SerializeField] private LayerMask playerSolid, wallSolid;

    [SerializeField] private AudioSource collisionSource;

    [SerializeField] private GameObject playerCamera, minotaurTail;

    [SerializeField] private float distance;

    private void Update()
    {
        RaycastHitCheck();
    }

    private void RaycastHitCheck()
    {
        RaycastHit playerHit;

        if (Physics.Raycast(transform.position, targetPoint, out playerHit, 100, playerSolid))
        {
            gameObject.GetComponent<AudioSource>().Play();

            Invoke("Charge", gameObject.GetComponent<AudioSource>().clip.length);
        }
    }

    private void Charge()
    {
        for (bool endOfState = false; endOfState != true;)
        {
            RaycastHit wallHit;

            if (Physics.Raycast(transform.position, targetPoint, out wallHit, distance, wallSolid))
            {
                playerCamera.GetComponent<Animation>().Play();

                collisionSource.Play();

                break;
            }
            else
            {
                var currentTail = Instantiate(minotaurTail, transform.position, transform.rotation);

                Destroy(currentTail, 0.15f);

                transform.Translate(targetPoint);
            }
        }
    }
}
