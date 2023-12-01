using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] private Transform weaponTransform;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private float firingRate = 1f;

    private Coroutine firingCoroutine;

    void Update()
    {
        HandleShooting();
        HandleRotation();
    }

    private void HandleShooting()
    {
        bool isOverUI = UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject();
        if (InputManager.Instance.IsMouseLeftPressedDown() && !isOverUI)
        {
            firingCoroutine = StartCoroutine(Shoot());
        }

        if (InputManager.Instance.IsMouseLeftPressedUp())
        {
            if (firingCoroutine == null) return;
            StopCoroutine(firingCoroutine);
        }
    }

    private void HandleRotation()
    {
        Vector3 mousePos = InputManager.Instance.GetMousePosition();

        Vector3 weaponWorldTransform = Camera.main.WorldToScreenPoint(transform.position);
        Vector3 weaponScreenTransform = Camera.main.ScreenToWorldPoint(mousePos);
        mousePos.x -= weaponWorldTransform.x;
        mousePos.y -= weaponWorldTransform.y;

        float weaponAngle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;

        if (weaponScreenTransform.x < transform.position.x)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            weaponTransform.transform.localScale = new Vector3(-1.2f, 1.2f, 1.2f);
            weaponTransform.transform.rotation = Quaternion.Euler(new Vector3(180f, 0f, -weaponAngle));
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
            weaponTransform.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
            weaponTransform.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, weaponAngle));
        }
    }

    IEnumerator Shoot()
    {
        while (true)
        {
            GameObject weapon = PlayerObjectPool.Instance.GetPooledObject();
            weapon.transform.SetPositionAndRotation(shootPoint.position, shootPoint.rotation);
            weapon.SetActive(true);
            weapon.GetComponent<WeaponController>().MoveToDirection(weaponTransform.right);
            yield return new WaitForSeconds(firingRate);
        }
    }
}