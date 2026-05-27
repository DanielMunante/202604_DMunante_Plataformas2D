using UnityEngine;
using UnityEngine.InputSystem;

public class AIControl : MonoBehaviour
{
    CharacterController2D characterController2D;
    [SerializeField] Transform target;
    [SerializeField] float attackDistance=2f;

    internal void SetTarget(Transform target)
    {
        this.target = target;
    }

    private void Awake()
    {
        characterController2D = GetComponent<CharacterController2D>();
    }

    private void Update()
    {
        Vector2 rawMove = Vector2.zero;

        if (target)
        {
            if (transform.position.x > target.position.x) { rawMove = Vector2.left; }
            else { rawMove = Vector2.right; }

            if (Mathf.Abs(target.transform.position.x -  transform.position.x) < attackDistance)
            { 
                rawMove = Vector2.zero;
                characterController2D.Punch();
            }

        }

        characterController2D.SetRawMove(rawMove);
    }
}
