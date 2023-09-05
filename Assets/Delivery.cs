using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using TMPro.Examples;
using UnityEngine;
using UnityEngine.UI;

public class Delivery : MonoBehaviour
{
    [SerializeField] float destroyDelay = 0.5f;
    [SerializeField] Color32 hasPackageColor = new Color32(1 ,1 ,1 ,255);
    [SerializeField] Color32 noPackageColor = new Color32(255, 255, 255, 255);
    bool hasPackage;
    [SerializeField] string deliveryAddress = string.Empty;
    int packageDelivered = 0;


    SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void EndGame() 
    {
        var a = GameObject.FindGameObjectWithTag("WinText").GetComponent<MeshRenderer>();
        a.enabled = true;
        Debug.Log("Show!");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Ouch!");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Package") && !hasPackage)
        {
            Debug.Log("Package picked up!");
            hasPackage = true;

            var packageSpriteRenderer = collision.GetComponent<SpriteRenderer>();
            hasPackageColor = packageSpriteRenderer.color;
            spriteRenderer.color = hasPackageColor;

            var customers = GameObject.FindGameObjectsWithTag("Customer");
            var randomNumber = new System.Random().Next(0,customers.Length);
            var randomCustomer = customers[randomNumber];
            deliveryAddress = randomCustomer.name;
            randomCustomer.GetComponent<SpriteRenderer>().color = packageSpriteRenderer.color;

            var gameObject = collision.gameObject;
            Destroy(gameObject, destroyDelay);
        }

        if (collision.CompareTag("Customer") && hasPackage && collision.gameObject.name == deliveryAddress)
        {
            Debug.Log("Package Delivered");

            spriteRenderer.color = noPackageColor;
            hasPackage = false;
            var gameObject = collision.gameObject;
            Destroy(gameObject, destroyDelay);
            packageDelivered++;
            if(packageDelivered == 8)
            {
                EndGame();
            }
        }
    }
}
