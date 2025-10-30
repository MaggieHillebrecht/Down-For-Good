using UnityEngine;

public class Pin : MonoBehaviour
{
    private void OnCollisionEnter( Collision collision )
    {
        // When the ball hits this pin, give 1 point
        if (collision.gameObject.CompareTag( "Ball" ))
        {
            BasicArcadeGameLogic.Instance.AddScore( 1 );
        }
    }
}