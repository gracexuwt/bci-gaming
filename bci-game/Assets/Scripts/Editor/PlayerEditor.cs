namespace Editor
{
    using UnityEngine;
    using UnityEditor;
    using Entity.Player;
    
    [CustomEditor(typeof(Player))]
    public class PlayerEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            
            Player player = (Player) target;

            EditorGUILayout.Separator();
            
            GUILayout.Label("Health", EditorStyles.boldLabel);
            EditorGUI.BeginDisabledGroup(true);
            EditorGUILayout.Slider("Health Points", player.Health, 0, player.MaxHealth);
            EditorGUI.EndDisabledGroup();
        }
    }
}