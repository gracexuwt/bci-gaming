namespace Entity.Utils
{
    using System.Threading.Tasks;
    using System;
    using UnityEngine;
    
    public static class EntityUtils
    {
        public static void MarkForDeath(GameObject obj, float seconds, bool force = false)
        {
            Delay(seconds, () =>
            {
                if (force)
                    UnityEngine.Object.Destroy(obj);
                else
                    obj.SetActive(false);
            });
        }

        public static async void Delay(float seconds, Action action)
        {
            await Task.Delay(Mathf.RoundToInt(seconds * 1000));
            action();
        }
    }
}