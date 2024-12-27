using UnityEngine;

public class DeterministicCollider : MonoBehaviour
{
    public int width = 1;
    public int height = 1;

    private int[] collisionGrid;
    private int velocityX;
    private int velocityY;
    private int x; // Declare x as a class-level variable
    private int y; // Declare y as a class-level variable
    public DeterministicCollider other; // Reference to the other object

    void Start()
    {
        collisionGrid = new int[width * height];
    }

    void FixedUpdate()
    {
        // Update the collision grid's position based on the GameObject's position
        x = (int)transform.position.x;
        y = (int)transform.position.y;

        // Update the collision grid
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                int gridIndex = i + j * width;
                collisionGrid[gridIndex] = 1; // Mark the grid cell as occupied
            }
        }

        // Check for collisions with other objects
        if (IsColliding(other))
        {
            PreventCollision(other);
        }

        // Update the position of the object based on its velocity
        x += velocityX;
        y += velocityY;
        transform.position = new Vector3(x, y, 0);
    }

    bool IsColliding(DeterministicCollider other)
    {
        // Check if the two objects are colliding by comparing their collision grids
        int otherX = (int)other.transform.position.x;
        int otherY = (int)other.transform.position.y;

        // Check if the objects are overlapping in the x and y directions
        if (x < otherX + other.width && x + width > otherX && y < otherY + other.height && y + height > otherY)
        {
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    int gridIndex = i + j * width;
                    if (collisionGrid[gridIndex] == 1 && other.collisionGrid[gridIndex] == 1)
                    {
                        return true; // Collision detected
                    }
                }
            }
        }

        return false; // No collision detected
    }

    void PreventCollision(DeterministicCollider other)
    {
        // Move the objects apart based on their velocities
        if (velocityX != 0)
        {
            x -= velocityX;
            other.x += velocityX;
        }
        else if (velocityY != 0)
        {
            y -= velocityY;
            other.y += velocityY;
        }

        // Check if the objects are still colliding after the move
        if (IsColliding(other))
        {
            // If the objects are still colliding, move them apart again
            PreventCollision(other);
        }
    }

    public void SetVelocity(int x, int y)
    {
        velocityX = x;
        velocityY = y;
    }
}