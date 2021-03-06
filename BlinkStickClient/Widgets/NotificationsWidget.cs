﻿using System;
using BlinkStickClient.DataModel;
using BlinkStickClient.Utils;
using BlinkStickClient.Classes;
using log4net;
using Gtk;
using Gdk;

namespace BlinkStickClient
{
    [System.ComponentModel.ToolboxItem(true)]
    public partial class NotificationsWidget : Gtk.Bin
    {
        public ApplicationDataModel DataModel;
        public ApplicationSettings ApplicationSettings;

        protected static readonly ILog log = LogManager.GetLogger("Main");  

        Gtk.ListStore NotificationListStore = new ListStore(typeof(CustomNotification), typeof(String), typeof(String), typeof(String), typeof(Pixbuf));

        private CustomNotification _SelectedNotification = null;

        Boolean ignoreNexClick = false;

        int DeleteColumnIndex = 6;

        public CustomNotification SelectedNotification {
            get {
                return _SelectedNotification;
            }
            set {
                if (_SelectedNotification != value)
                {
                    _SelectedNotification = value;
                }
            }
        }

        public Gtk.Window ParentForm;

        public NotificationsWidget()
        {
            this.Build();
        }

        public void Initialize()
        {
            log.Debug("Setting up treeview");

            Gtk.TreeViewColumn enabledColumn = new Gtk.TreeViewColumn();
            enabledColumn.Title = "";

            Gtk.TreeViewColumn nameColumn = new Gtk.TreeViewColumn();
            nameColumn.Title = "Name";

            Gtk.TreeViewColumn blinkStickColumn = new Gtk.TreeViewColumn();
            blinkStickColumn.Title = "BlinkStick";

            Gtk.TreeViewColumn patternColumn = new Gtk.TreeViewColumn();
            patternColumn.Title = "Pattern";

            Gtk.CellRendererPixbuf enabledCell = new Gtk.CellRendererPixbuf();
            Gtk.CellRendererText nameCell = new Gtk.CellRendererText();
            Gtk.CellRendererText typeCell = new Gtk.CellRendererText();
            Gtk.CellRendererText blinkStickCell = new Gtk.CellRendererText();
            Gtk.CellRendererText patternCell = new Gtk.CellRendererText();

            CellRendererPixbuf iconCell = new CellRendererPixbuf();
            nameColumn.PackStart(iconCell, false);
            nameColumn.AddAttribute(iconCell, "pixbuf", 4);

            enabledColumn.PackEnd(enabledCell, false);
            blinkStickColumn.PackEnd(blinkStickCell, false);
            nameColumn.PackEnd(nameCell, true);
            patternColumn.PackEnd(patternCell, false);

            enabledColumn.SetCellDataFunc(enabledCell, new Gtk.TreeCellDataFunc(RenderEnabledCell));
            nameColumn.SetCellDataFunc(nameCell, new Gtk.TreeCellDataFunc(RenderNameCell));
            blinkStickColumn.SetCellDataFunc(blinkStickCell, new Gtk.TreeCellDataFunc(RenderBlinkStickCell));
            patternColumn.SetCellDataFunc(patternCell, new Gtk.TreeCellDataFunc(RenderPatternCell));

            treeviewEvents.AppendColumn(enabledColumn);

            treeviewEvents.AppendColumn(nameColumn);
            treeviewEvents.Columns[1].Expand = true;
            treeviewEvents.AppendColumn(patternColumn);

            if (this.ApplicationSettings.SingleBlinkStickMode)
            {
                DeleteColumnIndex = 5;
            }
            else
            {
                treeviewEvents.AppendColumn(blinkStickColumn);
                DeleteColumnIndex = 6;
            }

            treeviewEvents.AppendColumn("", new Gtk.CellRendererPixbuf(), "stock_id", 1);
            treeviewEvents.AppendColumn("", new Gtk.CellRendererPixbuf(), "stock_id", 2);
            treeviewEvents.AppendColumn("", new Gtk.CellRendererPixbuf(), "stock_id", 3);
            NotificationListStore.SetSortFunc(0, delegate(TreeModel model, TreeIter a, TreeIter b)
            {
                CustomNotification n1 = (CustomNotification)model.GetValue(a, 0);
                CustomNotification n2 = (CustomNotification)model.GetValue(b, 0);
                if (n1 == null || n2 == null)
                    return 0;
                return String.Compare(n1.Name, n2.Name);
            });

            NotificationListStore.SetSortColumnId(1, SortType.Ascending);
            treeviewEvents.Model = NotificationListStore;

            log.Debug("Adding notifications to the tree");
            foreach (CustomNotification e in DataModel.Notifications) {
                NotificationListStore.AppendValues (e, "icon-dark-pencil-square-o", "icon-dark-clone", "icon-dark-trash", NotificationRegistry.FindIcon(e.GetType()));
            } 
        }

        public void NotificationUpdated(CustomNotification notification)
        {
            TreeIter iter;
            Boolean searchMore = NotificationListStore.GetIterFirst(out iter);
            while (searchMore)
            {
                if (NotificationListStore.GetValue(iter, 0) == notification)
                {
                    NotificationListStore.EmitRowChanged(NotificationListStore.GetPath(iter), iter);
                }

                searchMore = NotificationListStore.IterNext(ref iter);
            }
        }

        protected void OnTreeviewEventsRowActivated (object o, Gtk.RowActivatedArgs args)
        {
            EditNotification();
        }

        protected void OnTreeviewEventsCursorChanged (object sender, EventArgs e)
        {
            if (ignoreNexClick)
            {
                ignoreNexClick = false;
                return;
            }

            TreeModel model;
            TreeIter iter;

            TreeSelection selection = (sender as TreeView).Selection;

            if(selection.GetSelected(out model, out iter)){
                SelectedNotification = (CustomNotification)model.GetValue (iter, 0);

                TreePath path;
                TreeViewColumn column;
                (sender as TreeView).GetCursor(out path, out column);

                if (column == (sender as TreeView).Columns[DeleteColumnIndex]) //Delete clicked
                {
                    if (MainWindow.ConfirmDelete())
                    {
                        ignoreNexClick = true;
                        DataModel.Notifications.Remove(SelectedNotification);
                        NotificationListStore.Remove(ref iter);
                        DataModel.Save();
                    }
                }
                else if (column == (sender as TreeView).Columns[DeleteColumnIndex - 1]) //Copy clicked
                {
                    if (SelectedNotification.IsUnique())
                    {
                        MessageBox.Show(ParentForm, String.Format("Only one {0} notification can be used", SelectedNotification.GetTypeName()), MessageType.Error);
                        return;
                    }

                    CustomNotification notification = SelectedNotification.Copy();
                    notification.Name = DataModel.GetNotificationName(SelectedNotification.Name, 2);

                    if (EditNotification(notification, "Copy Notification"))
                    {
                        NotificationListStore.AppendValues(notification, "icon-dark-pencil-square-o", "icon-dark-clone", "icon-dark-trash", NotificationRegistry.FindIcon(notification.GetType()));
                        DataModel.Notifications.Add(notification);
                        ignoreNexClick = true;
                        SelectNotificationInTree(notification);
                    }
                }
                else if (column == (sender as TreeView).Columns[DeleteColumnIndex - 2]) //Edit clicked
                {
                    EditNotification();
                }
                else if (column == (sender as TreeView).Columns[0]) //Enabled-Disabled clicked
                {
                    SelectedNotification.Enabled = !SelectedNotification.Enabled;
                    DataModel.Save();
                    DataModel.Notifications.NotifyUpdate(SelectedNotification);
                }
            }
        }
        private void EditNotification()
        {
            if (SelectedNotification != null && EditNotification(SelectedNotification))
            {
                log.DebugFormat("Notification {0} edit complete", SelectedNotification.ToString());
                DataModel.Save();
                DataModel.Notifications.NotifyUpdate(SelectedNotification);
            }
        }

        private Boolean EditNotification(CustomNotification notification, String title = "Edit Notification")
        {
            int response;

            using (EditNotificationDialog editDialog = 
                new EditNotificationDialog(title, this.ParentForm, this.DataModel, notification, this.ApplicationSettings))
            {
                response = editDialog.Run();
                editDialog.Destroy();
            }

            log.DebugFormat("Edit notification dialog response {0}", (ResponseType)response);

            return response == (int)ResponseType.Ok;
        }

        private void RenderEnabledCell (Gtk.TreeViewColumn column, Gtk.CellRenderer cell, Gtk.TreeModel model, Gtk.TreeIter iter)
        {
            if (model.GetValue (iter, 0) is CustomNotification) {
                CustomNotification notification = (CustomNotification)model.GetValue (iter, 0);
                (cell as Gtk.CellRendererPixbuf).StockId = notification.Enabled ? "icon-dark-check-square-o" : "icon-dark-square-o";
            }
        }

        private void RenderNameCell (Gtk.TreeViewColumn column, Gtk.CellRenderer cell, Gtk.TreeModel model, Gtk.TreeIter iter)
        {
            if (model.GetValue (iter, 0) is CustomNotification) {
                CustomNotification notification = (CustomNotification)model.GetValue (iter, 0);
                (cell as Gtk.CellRendererText).Text = notification.Name;
            }
        }

        private void RenderBlinkStickCell (Gtk.TreeViewColumn column, Gtk.CellRenderer cell, Gtk.TreeModel model, Gtk.TreeIter iter)
        {
            if (model.GetValue (iter, 0) is DeviceNotification) {
                DeviceNotification notification = (DeviceNotification)model.GetValue (iter, 0);
                (cell as Gtk.CellRendererText).Text = notification.BlinkStickSerial;
            }
            else
            {
                (cell as Gtk.CellRendererText).Text = "";
            }
        }

        private void RenderPatternCell (Gtk.TreeViewColumn column, Gtk.CellRenderer cell, Gtk.TreeModel model, Gtk.TreeIter iter)
        {
            if (model.GetValue(iter, 0) is PatternNotification)
            {
                PatternNotification notification = (PatternNotification)model.GetValue(iter, 0);
                (cell as Gtk.CellRendererText).Text = notification.Pattern == null ? "" : notification.Pattern.Name;
            }
            else
            {
                (cell as Gtk.CellRendererText).Text = "";
            }
        }

        protected void OnButtonAddNotificationClicked(object sender, EventArgs e)
        {
            int response;

            Type notificationType = typeof(CustomNotification);

            using (SelectNotificationDialog dialog = new SelectNotificationDialog())
            {
                dialog.DataModel = this.DataModel;
                response = dialog.Run();
                if (response == (int)ResponseType.Ok)
                {
                    notificationType = dialog.SelectedType.NotificationType;
                }
                dialog.Destroy();
            }

            if (response == (int)ResponseType.Ok)
            {

                CustomNotification notification = (CustomNotification)Activator.CreateInstance(notificationType);
                notification.Name = DataModel.GetNotificationName(notification.GetTypeName());

                if (EditNotification(notification, "New Notification"))
                {
                    NotificationListStore.AppendValues(notification, "icon-dark-pencil-square-o", "icon-dark-clone", "icon-dark-trash", NotificationRegistry.FindIcon(notification.GetType()));
                    DataModel.Notifications.Add(notification);
                    DataModel.Save();

                    SelectNotificationInTree(notification);
                }
            }
        }

        private void SelectNotificationInTree(CustomNotification notification)
        {
            TreeIter iterator;
            NotificationListStore.GetIterFirst(out iterator);

            do
            {
                if (notification == (CustomNotification)NotificationListStore.GetValue(iterator, 0))
                {
                    treeviewEvents.SetCursor(NotificationListStore.GetPath(iterator), treeviewEvents.Columns[0], false);
                    break;
                }
            } 
            while (NotificationListStore.IterNext(ref iterator));
        }
    }
}

