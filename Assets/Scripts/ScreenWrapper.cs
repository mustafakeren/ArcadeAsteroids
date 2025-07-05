using UnityEngine;

public class ScreenWrapper : MonoBehaviour
{
    float radiusOfCollider;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        radiusOfCollider = GetComponent<CircleCollider2D>().radius;
    }
    void OnBecameInvisible()
    {
        Vector2 position = transform.position;
        
        if (position.x - radiusOfCollider > ScreenUtils.ScreenRight)
        {
            position.x = ScreenUtils.ScreenLeft - radiusOfCollider;
        }
        
        if (position.x + radiusOfCollider < ScreenUtils.ScreenLeft)
        {
            position.x = ScreenUtils.ScreenRight + radiusOfCollider;
        }
        
        if (position.y - radiusOfCollider > ScreenUtils.ScreenTop)
        {
            position.y = ScreenUtils.ScreenBottom - radiusOfCollider;
        }
        
        if (position.y + radiusOfCollider < ScreenUtils.ScreenBottom)
        {
            position.y = ScreenUtils.ScreenTop + radiusOfCollider;
        }
        
        transform.position = position;
    }
}
