using System;

namespace Systems.Messaging
{
    public static class NotificationSystem 
    {
        public static Action<NotificationType> OnNotify;
        
        public static void Notify(NotificationType notificationType)
        {
            OnNotify?.Invoke(notificationType);
        }
    }
    
    public enum NotificationType
    {
        AsteroidDespawned,
        AsteroidSpawned,
        SaucerDespawned,
        SaucerSpawned,
        PlayerDied,
        LevelFinished,
        
    }
}