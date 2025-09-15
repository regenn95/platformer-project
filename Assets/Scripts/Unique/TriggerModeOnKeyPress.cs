using Platformer.Mechanics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

// this class handles scenery change and tilemap rendering upon activation of dark power by pressing 'Q'
public class TriggerModeOnKeyPress : MonoBehaviour
{
    public TilemapRenderer DisappearingTilemap;
    public TilemapRenderer TilemapToAppear;
    public TilemapCollider2D Collider;
    public SpriteRenderer PlayerSpriteRenderer;
    public SpriteRenderer FlashSpriteRenderer;
    public Camera Camera;

    // player sprite colors
    private Color32 White = new Color32(212, 240, 255, 255);
    private Color32 Black = new Color32(38, 42, 44, 255);
    // background colors
    private Color32 LightPurple = new Color32(236, 227, 240, 255);
    private Color32 Lavender = new Color32(72, 60, 62, 255);

    public GameObject Player;
    public int frames;
    public byte CurrentAlpha;
  
    void Start()
    {
        DisappearingTilemap.enabled = true;
        TilemapToAppear.enabled = false;
        Collider.enabled = false;

        PlayerSpriteRenderer.color = White;
        FlashSpriteRenderer.color = Color.clear;
        Camera.backgroundColor = LightPurple;

        CurrentAlpha = 0;
        frames = 0;
        DataStorage.IsCurrentlyDark = false;
    }
    void Update()
    {
        // if we unlocked mode change
        if (DataStorage.DarkPower)
        {
            // delay frames
            if (frames < 12)
                frames++;

            // alpha flash control
            if (CurrentAlpha >= 0)
            {
                if (DataStorage.IsCurrentlyDark)
                    FlashSpriteRenderer.color = new Color32(0, 0, 0, CurrentAlpha);
                else
                    FlashSpriteRenderer.color = new Color32(255, 255, 255, CurrentAlpha);
                if (CurrentAlpha != 0)
                    CurrentAlpha -= 5;
            }

            if (Input.GetKeyDown(KeyCode.Q) && frames == 12)
            {
                frames = 0;
                CurrentAlpha = 145;
                DataStorage.IsCurrentlyDark = !DataStorage.IsCurrentlyDark;

                DisappearingTilemap.enabled = !DisappearingTilemap.enabled;
                TilemapToAppear.enabled = !TilemapToAppear.enabled;
                Collider.enabled = !Collider.enabled;

                if (DataStorage.IsCurrentlyDark)
                    PlayerSpriteRenderer.color = Black;
                else PlayerSpriteRenderer.color = White;

                if (DataStorage.IsCurrentlyDark)
                    Camera.backgroundColor = Lavender;
                else Camera.backgroundColor = LightPurple;

            }
        }
    }
}
