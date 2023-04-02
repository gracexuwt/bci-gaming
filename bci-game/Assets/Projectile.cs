using UnityEngine;

public class Projectile {
    private Sprite sprite;
    private Vector2 position;
    private Vector2 velocity;

    public Projectile(Sprite sprite, float x, float y, Vector2 velocity) {
        this.sprite = sprite;
        this.position = new Vector2(x, y);
        this.velocity = velocity;
    }

    public void UpdateState(float deltaTime) {
        position += velocity * deltaTime;
    }

    public void Draw() {
        if (sprite != null) {
            Graphics.DrawTexture(new Rect(position.x, position.y, sprite.bounds.size.x, sprite.bounds.size.y), sprite.texture);
        }
    }
}
