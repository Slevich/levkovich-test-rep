using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorRaycast : MonoBehaviour
{
    #region Variables
    [Header("CardsSpawnComtroller component.")]
    [SerializeField] private CardsSpawnController spawnController;
    [Header("LayerMask of simple card.")]
    [SerializeField] private LayerMask cardMask;
    [Header("LayerMask of attack card on board.")]
    [SerializeField] private LayerMask attackCardMask;
    //Vector3 variable with the coordinates of the main camera.
    private Vector3 mainCameraPosition;
    //Vector3 variable with the coordinates of position of the mouse on screen.
    private Vector3 mouseScreenPosition;
    //Vector3 variable with the global coordinates of mouse.
    private Vector3 mouseWorldPosition;
    //Vector3 variable with the coordinates of the ray direction.
    private Vector3 rayDirection;
    //Vector3 point, in which ray hit the object,
    private Vector3 hitColliderPosition;
    //Game object hit by the ray.
    private GameObject raycastedObject;
    //Previous raycasted game object.
    private GameObject previousObject;
    //Distance of the ray.
    private float rayCastDistance = 10f;
    //Cards amount, which spawned now.
    private int cardsToSpawn;
    //Properties.
    public GameObject RaycastedObject { get { return raycastedObject; } }
    public GameObject PreviousObject { get { return previousObject; } }
    public Vector3 MouseWorldPosition { get { return mouseWorldPosition; } }
    public Vector3 HitColliderPosition { get { return hitColliderPosition; } }

    #endregion

    #region Methods
    /// <summary>
    /// At the start we get the coordinates of the camera.
    /// </summary>
    private void Start()
    {
        mainCameraPosition = Camera.main.transform.position;
    }

    /// <summary>
    /// In Update we calculate the mouse coordinates,
    /// convert them into world coordinates.
    /// If no cards are currently spawning (counter is zero), 
    /// drops a ray from the camera, relative to the mouse position.
    /// We can see the ray in inspector.
    /// </summary>
    private void Update()
    {
        cardsToSpawn = spawnController.StartCardsAmount;
        CalculateMouseWorldPosition();
        if (cardsToSpawn == 0) MouseRaycast();
        Debug.DrawLine(Camera.main.transform.position, mouseWorldPosition, Color.green);
    }

    /// <summary>
    /// The method calculates the mouse position in global coordinates 
    /// based on its position on the screen.
    /// </summary>
    private void CalculateMouseWorldPosition()
    {
        mouseScreenPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, mainCameraPosition.y);
        mouseWorldPosition = Camera.main.ScreenToWorldPoint(mouseScreenPosition);
    }

    /// <summary>
    /// The method calculates the direction of the ray from the camera.
    /// Drops the specified ray in the right direction, gets the game object hit.
    /// This changes the tag of the current object, comparing it to the tag of the previous object. 
    /// If the ray has moved to a new object with the correct tag, its state changes to Selected. 
    /// If not, the previous object's tag is reset.
    /// In this case, two different ray are cast, for two types of cards - in hand and on the board (attack card).
    /// </summary>
    private void MouseRaycast()
    {
        rayDirection = (mouseWorldPosition - mainCameraPosition);
        Ray cameraRay = new Ray(mainCameraPosition, rayDirection);
        RaycastHit cameraHit;

        if (Physics.Raycast(cameraRay, out cameraHit, rayCastDistance, cardMask))
        {
            raycastedObject = cameraHit.transform.gameObject;

            if (raycastedObject.tag == "Card")
            {
                if (previousObject != null)
                {
                    previousObject.GetComponent<CardsStates>().ChangeCardState(2);
                    previousObject.tag = "Card";
                }
                previousObject = cameraHit.transform.gameObject;
                raycastedObject.tag = "SelectedCard";
            }
            else if (raycastedObject.tag == "SelectedCard")
            {
                hitColliderPosition = cameraHit.point;
                raycastedObject.GetComponent<CardsStates>().ChangeCardState(3);
            }
            else 
            {
                if (previousObject != null)
                {
                    previousObject.tag = "Card";
                }
            }
        }
        else if (Physics.Raycast(cameraRay, out cameraHit, rayCastDistance, attackCardMask))
        {
            raycastedObject = cameraHit.transform.gameObject;

            if (raycastedObject.tag == "AttackCard")
            {
                if (previousObject != null)
                {
                    previousObject.GetComponent<CardsStates>().ChangeCardState(5);
                    previousObject.tag = "AttackCard";
                }
                previousObject = cameraHit.transform.gameObject;
                raycastedObject.tag = "SelectedAttackCard";
            }
            else if (raycastedObject.tag == "SelectedAttackCard")
            {
                hitColliderPosition = cameraHit.point;
                raycastedObject.GetComponent<CardsStates>().ChangeCardState(7);
            }
            else
            {
                if (previousObject != null)
                {
                    previousObject.tag = "AttackCard";
                }
            }
        }
        else
        {
            if (previousObject != null)
            {
                if (previousObject.tag == "SelectedAttackCard")
                {
                    previousObject.GetComponent<CardsStates>().ChangeCardState(5);
                    previousObject.tag = "AttackCard";
                    previousObject = null;
                }
                else if (previousObject.tag == "SelectedCard")
                {
                    previousObject.GetComponent<CardsStates>().ChangeCardState(2);
                    previousObject.tag = "Card";
                    previousObject = null;
                }
            }
        }
    }
    #endregion
}
