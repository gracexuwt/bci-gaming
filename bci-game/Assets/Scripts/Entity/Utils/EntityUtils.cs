namespace Entity.Utils
{
    using System.Threading.Tasks;
    using UnityEngine;
    
    public static class EntityUtils
    {
        public static async void MarkForDeath(GameObject obj, float secondsDelay, bool force = false)
        {
            await Task.Delay(Mathf.RoundToInt(secondsDelay * 1000));
            
            if (force)
                Object.Destroy(obj);
            else
                obj.SetActive(false);
        }
    }
}