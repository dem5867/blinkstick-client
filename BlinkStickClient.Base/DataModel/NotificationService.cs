﻿using System;
using System.ComponentModel;
using System.Threading;
using System.Collections.Generic;
using log4net;

namespace BlinkStickClient.DataModel
{
    public class NotificationService
    {
        protected static readonly ILog log = LogManager.GetLogger("NotificationService");  

        public ApplicationDataModel DataModel;

        public NotificationService()
        {
            log.Debug("Creating service...");
            log.Info("Service created");
        }

        public void Start()
        {
            log.Info("Starting notification monitoring...");
            foreach (CustomNotification n in DataModel.Notifications)
            {
                if (n is PatternNotification)
                {
                    (n as PatternNotification).Triggered += NotificationTriggered;
                }
                n.ColorSend += NotificationColor;
                n.PatternSend += NotificationPattern;
            }

            DataModel.Notifications.CollectionChanged += NotificationListChanged;
            DataModel.Notifications.ItemUpdated += NotificationListItemUpdated;

            foreach (CustomNotification n in DataModel.Notifications)
            {
                n.DataModel = DataModel;
                if (n.RequiresMonitoring() && n.Enabled)
                {
                    n.Start();
                }
            }

            log.Info("Started.");
        }

        void NotificationListItemUpdated (object sender, ItemUpdatedEventArgs e)
        {
            CustomNotification notification = e.Item as CustomNotification;

            if (notification.RequiresMonitoring())
            {
                if (notification.Enabled)
                {
                    if (notification.Running)
                    {
                        log.DebugFormat("Notification {0} restarting", notification.ToString());
                        notification.Stop();
                    }
                    else
                    {
                        log.DebugFormat("Notification {0} starting", notification.ToString());
                    }
                    notification.Start();
                }
                else if (notification.Running)
                {
                    log.DebugFormat("Notification {0} stopping", notification.ToString());
                    notification.Stop();
                }
            }
        }

        public void Stop()
        {
            log.Info("Stopping monitor...");
            foreach (CustomNotification n in DataModel.Notifications)
            {
                if (n is PatternNotification)
                {
                    (n as PatternNotification).Triggered -= NotificationTriggered;
                }
            }

            DataModel.Notifications.CollectionChanged -= NotificationListChanged;
            DataModel.Notifications.ItemUpdated -= NotificationListItemUpdated;

            foreach (CustomNotification n in DataModel.Notifications)
            {
                if (n.RequiresMonitoring() && n.Running)
                {
                    n.Stop();
                }
            }

            log.Debug("Stopping device playback");
            foreach (BlinkStickDeviceSettings settings in DataModel.Devices)
            {
                settings.Stop();
            }

            log.Info("Service stopped.");
        }

        void NotificationListChanged (object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
            {
                foreach (CustomNotification n in e.NewItems)
                {
                    if (n is PatternNotification)
                    {
                        (n as PatternNotification).Triggered += NotificationTriggered;
                    }

                    n.ColorSend += NotificationColor;
                    n.PatternSend += NotificationPattern;
                    n.DataModel = DataModel;

                    if (n.RequiresMonitoring() && n.Enabled)
                    {
                        n.Start();
                    }
                }
            }
            else if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Remove)
            {
                foreach (CustomNotification n in e.OldItems)
                {
                    if (n is PatternNotification)
                    {
                        (n as PatternNotification).Triggered -= NotificationTriggered;
                    }

                    n.ColorSend -= NotificationColor;
                    n.PatternSend -= NotificationPattern;
                    n.DataModel = null;;
    
                    if (n.RequiresMonitoring() && n.Enabled)
                    {
                        n.Stop();
                    }
                }
            }
        }

        void NotificationColor (object sender, ColorSendEventArgs e)
        {
            CustomNotification notification = sender as CustomNotification;

            if (e.Device == null)
            {
                log.WarnFormat("({0}) Device for notification not found", notification.Name);
                return;
            }

            if (e.Device.Led == null)
            {
                log.WarnFormat("({0}) BlinkStick is not connected", notification.Name);
                return;
            }

            e.Device.SetColor(e.Channel, e.FirstLed, e.LastLed, e.R, e.G, e.B);
        }

        void NotificationPattern (object sender, PatternSendEventArgs e)
        {
            Boolean found = false;

            TriggeredEvent ev = new TriggeredEvent(sender as CustomNotification, e.Channel, e.FirstLed, e.LastLed, e.Device, e.Pattern, e.Repeat, e.Duration);

            lock (e.Device.EventQueue)
            {
                foreach (TriggeredEvent pendingEvent in e.Device.EventQueue.ToArray())
                {
                    if (pendingEvent.Notification == sender)
                    {
                        found = true;
                        break;
                    }
                }

                if (found)
                {
                    log.InfoFormat("Notification {0} already pending to play. Skipping.", (sender as CustomNotification).Name);
                }
                else
                {
                    e.Device.EventQueue.Enqueue(ev);
                }
            }

            if (!found)
            {
                e.Device.Start();
            }
        }

        private void NotificationTriggered (object sender, TriggeredEventArgs e)
        {
            log.InfoFormat("Notification [{0}] \"{1}\" triggered. Message: {2}", 
                (sender as CustomNotification).GetTypeName(), 
                (sender as CustomNotification).Name, 
                e.Message);

            PatternNotification notification = sender as PatternNotification;

            TriggeredEvent ev = new TriggeredEvent(notification, e.Message);

            if (e.Message != "")
            {
                DataModel.TriggeredEvents.Add(ev);
            }

            if (notification.Pattern == null)
            {
                log.WarnFormat("({0}) Pattern is not assigned", notification.Name);
                return;
            }

            BlinkStickDeviceSettings settings = DataModel.FindBySerial(notification.BlinkStickSerial);

            if (settings == null)
            {
                log.WarnFormat("({0}) BlinkStick with serial {1} not known", notification.Name, notification.BlinkStickSerial);
                return;
            }

            if (settings.Led == null)
            {
                log.WarnFormat("({0}) BlinkStick with serial {1} is not connected", notification.Name, notification.BlinkStickSerial);
                return;
            }

            Boolean found = false;

            lock (settings.EventQueue)
            {
                foreach (TriggeredEvent pendingEvent in settings.EventQueue.ToArray())
                {
                    if (pendingEvent.Notification == notification)
                    {
                        found = true;
                        break;
                    }
                }

                if (found)
                {
                    log.InfoFormat("Notification {0} already pending to play. Skipping.", notification.Name);
                }
                else
                {
                    settings.EventQueue.Enqueue(ev);
                }
            }

            if (!found)
            {
                settings.Start();
            }
        }
    }
}

