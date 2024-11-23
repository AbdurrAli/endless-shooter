using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    // Kecepatan pergerakan pemain
    [SerializeField] float moveSpeed = 5f;

    // Padding untuk membatasi pergerakan pemain di area layar
    [SerializeField] float paddingLeft = 0.5f;
    [SerializeField] float paddingRight = 0.5f;
    [SerializeField] float paddingTop = 5f;
    [SerializeField] float paddingBottom = 2f;

    // Input pergerakan pemain
    UnityEngine.Vector2 rawInput;
    // Batas minimum dan maksimum layar dalam koordinat dunia
    UnityEngine.Vector2 minBounds;
    UnityEngine.Vector2 maxBounds;

    Shooter shooter;

    void Awake() {
        shooter = GetComponent<Shooter>();
    }

    // Fungsi yang dipanggil saat game dimulai
    void Start() {
        InitBounds(); // Inisialisasi batas layar
    }

    // Update dipanggil sekali setiap frame
    void Update() {
        MoveMethods(); // Menggerakkan pemain
    }

    // Inisialisasi batas layar berdasarkan ukuran viewport kamera
    void InitBounds() {
        Camera mainCamera = Camera.main;
        // Mendapatkan titik minimum dan maksimum viewport dalam koordinat dunia
        minBounds = mainCamera.ViewportToWorldPoint(new UnityEngine.Vector2(0, 0));
        maxBounds = mainCamera.ViewportToWorldPoint(new UnityEngine.Vector2(1, 1));
    }

    // Mengatur logika pergerakan pemain
    private void MoveMethods()
    {
        // Menghitung pergerakan berdasarkan input, kecepatan, dan delta waktu
        UnityEngine.Vector3 delta = rawInput * moveSpeed * Time.deltaTime;
        // Menentukan posisi baru pemain
        UnityEngine.Vector2 newPosition = new UnityEngine.Vector2();
        // Membatasi posisi baru pemain dengan padding
        newPosition.x = Mathf.Clamp(transform.position.x + delta.x, minBounds.x + paddingLeft, maxBounds.x - paddingRight);
        newPosition.y = Mathf.Clamp(transform.position.y + delta.y, minBounds.y + paddingBottom, maxBounds.y - paddingTop);
        // Memperbarui posisi pemain
        transform.position = newPosition;
    }

    // Mengambil input pergerakan dari sistem Input
    void OnMove(InputValue value) {
        rawInput = value.Get<UnityEngine.Vector2>();
    }

    void OnFire(InputValue value) {
        if(shooter != null) {
            shooter.isFiring = value.isPressed;
        }
    }
}
