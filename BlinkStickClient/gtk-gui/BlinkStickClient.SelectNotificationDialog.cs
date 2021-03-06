
// This file has been generated by the GUI designer. Do not modify.
namespace BlinkStickClient
{
	public partial class SelectNotificationDialog
	{
		private global::Gtk.HBox hbox1;
		
		private global::Gtk.VBox vbox2;
		
		private global::Gtk.ScrolledWindow GtkScrolledWindow;
		
		private global::Gtk.TreeView treeviewNotifications;
		
		private global::Gtk.CheckButton checkbuttonDisplayOnlySupported;
		
		private global::Gtk.Table table1;
		
		private global::Gtk.Label labelCategory;
		
		private global::Gtk.Label labelCategoryInfo;
		
		private global::Gtk.Label labelDescription;
		
		private global::Gtk.Label labelDescriptionInfo;
		
		private global::Gtk.Label labelName;
		
		private global::Gtk.Label labelNameInfo;
		
		private global::Gtk.Label labelUniqueWarning;
		
		private global::Gtk.Button buttonCancel;
		
		private global::Gtk.Button buttonOk;

		protected virtual void Build ()
		{
			global::Stetic.Gui.Initialize (this);
			// Widget BlinkStickClient.SelectNotificationDialog
			this.Name = "BlinkStickClient.SelectNotificationDialog";
			this.Title = global::Mono.Unix.Catalog.GetString ("Add notification");
			this.WindowPosition = ((global::Gtk.WindowPosition)(1));
			this.Modal = true;
			this.SkipPagerHint = true;
			this.SkipTaskbarHint = true;
			// Internal child BlinkStickClient.SelectNotificationDialog.VBox
			global::Gtk.VBox w1 = this.VBox;
			w1.Name = "dialog1_VBox";
			w1.BorderWidth = ((uint)(2));
			// Container child dialog1_VBox.Gtk.Box+BoxChild
			this.hbox1 = new global::Gtk.HBox ();
			this.hbox1.Name = "hbox1";
			this.hbox1.Spacing = 6;
			// Container child hbox1.Gtk.Box+BoxChild
			this.vbox2 = new global::Gtk.VBox ();
			this.vbox2.Name = "vbox2";
			this.vbox2.Spacing = 6;
			// Container child vbox2.Gtk.Box+BoxChild
			this.GtkScrolledWindow = new global::Gtk.ScrolledWindow ();
			this.GtkScrolledWindow.Name = "GtkScrolledWindow";
			this.GtkScrolledWindow.ShadowType = ((global::Gtk.ShadowType)(1));
			// Container child GtkScrolledWindow.Gtk.Container+ContainerChild
			this.treeviewNotifications = new global::Gtk.TreeView ();
			this.treeviewNotifications.CanFocus = true;
			this.treeviewNotifications.Name = "treeviewNotifications";
			this.GtkScrolledWindow.Add (this.treeviewNotifications);
			this.vbox2.Add (this.GtkScrolledWindow);
			global::Gtk.Box.BoxChild w3 = ((global::Gtk.Box.BoxChild)(this.vbox2 [this.GtkScrolledWindow]));
			w3.Position = 0;
			// Container child vbox2.Gtk.Box+BoxChild
			this.checkbuttonDisplayOnlySupported = new global::Gtk.CheckButton ();
			this.checkbuttonDisplayOnlySupported.CanFocus = true;
			this.checkbuttonDisplayOnlySupported.Name = "checkbuttonDisplayOnlySupported";
			this.checkbuttonDisplayOnlySupported.Label = global::Mono.Unix.Catalog.GetString ("Display only supported notification types");
			this.checkbuttonDisplayOnlySupported.Active = true;
			this.checkbuttonDisplayOnlySupported.DrawIndicator = true;
			this.checkbuttonDisplayOnlySupported.UseUnderline = true;
			this.vbox2.Add (this.checkbuttonDisplayOnlySupported);
			global::Gtk.Box.BoxChild w4 = ((global::Gtk.Box.BoxChild)(this.vbox2 [this.checkbuttonDisplayOnlySupported]));
			w4.Position = 1;
			w4.Expand = false;
			w4.Fill = false;
			this.hbox1.Add (this.vbox2);
			global::Gtk.Box.BoxChild w5 = ((global::Gtk.Box.BoxChild)(this.hbox1 [this.vbox2]));
			w5.Position = 0;
			// Container child hbox1.Gtk.Box+BoxChild
			this.table1 = new global::Gtk.Table (((uint)(5)), ((uint)(2)), false);
			this.table1.Name = "table1";
			this.table1.RowSpacing = ((uint)(6));
			this.table1.ColumnSpacing = ((uint)(6));
			this.table1.BorderWidth = ((uint)(12));
			// Container child table1.Gtk.Table+TableChild
			this.labelCategory = new global::Gtk.Label ();
			this.labelCategory.Name = "labelCategory";
			this.labelCategory.Xalign = 1F;
			this.labelCategory.Yalign = 0F;
			this.labelCategory.LabelProp = global::Mono.Unix.Catalog.GetString ("Category:");
			this.table1.Add (this.labelCategory);
			global::Gtk.Table.TableChild w6 = ((global::Gtk.Table.TableChild)(this.table1 [this.labelCategory]));
			w6.XOptions = ((global::Gtk.AttachOptions)(4));
			w6.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.labelCategoryInfo = new global::Gtk.Label ();
			this.labelCategoryInfo.WidthRequest = 200;
			this.labelCategoryInfo.Name = "labelCategoryInfo";
			this.labelCategoryInfo.Xalign = 0F;
			this.labelCategoryInfo.Yalign = 0F;
			this.labelCategoryInfo.LabelProp = global::Mono.Unix.Catalog.GetString ("labelCategoryInfo");
			this.table1.Add (this.labelCategoryInfo);
			global::Gtk.Table.TableChild w7 = ((global::Gtk.Table.TableChild)(this.table1 [this.labelCategoryInfo]));
			w7.LeftAttach = ((uint)(1));
			w7.RightAttach = ((uint)(2));
			w7.XOptions = ((global::Gtk.AttachOptions)(0));
			w7.YOptions = ((global::Gtk.AttachOptions)(0));
			// Container child table1.Gtk.Table+TableChild
			this.labelDescription = new global::Gtk.Label ();
			this.labelDescription.Name = "labelDescription";
			this.labelDescription.Xalign = 1F;
			this.labelDescription.Yalign = 0F;
			this.labelDescription.LabelProp = global::Mono.Unix.Catalog.GetString ("Description:");
			this.table1.Add (this.labelDescription);
			global::Gtk.Table.TableChild w8 = ((global::Gtk.Table.TableChild)(this.table1 [this.labelDescription]));
			w8.TopAttach = ((uint)(2));
			w8.BottomAttach = ((uint)(3));
			w8.XOptions = ((global::Gtk.AttachOptions)(4));
			w8.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.labelDescriptionInfo = new global::Gtk.Label ();
			this.labelDescriptionInfo.WidthRequest = 200;
			this.labelDescriptionInfo.Name = "labelDescriptionInfo";
			this.labelDescriptionInfo.Xalign = 0F;
			this.labelDescriptionInfo.Yalign = 0F;
			this.labelDescriptionInfo.LabelProp = global::Mono.Unix.Catalog.GetString ("labelDescriptionInfo");
			this.labelDescriptionInfo.Wrap = true;
			this.table1.Add (this.labelDescriptionInfo);
			global::Gtk.Table.TableChild w9 = ((global::Gtk.Table.TableChild)(this.table1 [this.labelDescriptionInfo]));
			w9.TopAttach = ((uint)(2));
			w9.BottomAttach = ((uint)(3));
			w9.LeftAttach = ((uint)(1));
			w9.RightAttach = ((uint)(2));
			w9.XOptions = ((global::Gtk.AttachOptions)(4));
			w9.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.labelName = new global::Gtk.Label ();
			this.labelName.Name = "labelName";
			this.labelName.Xalign = 1F;
			this.labelName.Yalign = 0F;
			this.labelName.LabelProp = global::Mono.Unix.Catalog.GetString ("Name:");
			this.table1.Add (this.labelName);
			global::Gtk.Table.TableChild w10 = ((global::Gtk.Table.TableChild)(this.table1 [this.labelName]));
			w10.TopAttach = ((uint)(1));
			w10.BottomAttach = ((uint)(2));
			w10.XOptions = ((global::Gtk.AttachOptions)(4));
			w10.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.labelNameInfo = new global::Gtk.Label ();
			this.labelNameInfo.WidthRequest = 200;
			this.labelNameInfo.Name = "labelNameInfo";
			this.labelNameInfo.Xalign = 0F;
			this.labelNameInfo.Yalign = 0F;
			this.labelNameInfo.LabelProp = global::Mono.Unix.Catalog.GetString ("labelNameInfo");
			this.table1.Add (this.labelNameInfo);
			global::Gtk.Table.TableChild w11 = ((global::Gtk.Table.TableChild)(this.table1 [this.labelNameInfo]));
			w11.TopAttach = ((uint)(1));
			w11.BottomAttach = ((uint)(2));
			w11.LeftAttach = ((uint)(1));
			w11.RightAttach = ((uint)(2));
			w11.XOptions = ((global::Gtk.AttachOptions)(4));
			w11.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.labelUniqueWarning = new global::Gtk.Label ();
			this.labelUniqueWarning.WidthRequest = 200;
			this.labelUniqueWarning.Name = "labelUniqueWarning";
			this.labelUniqueWarning.LabelProp = global::Mono.Unix.Catalog.GetString ("<b>You already have this notification configured. This type of notification can o" +
			"nly be added once.</b>");
			this.labelUniqueWarning.UseMarkup = true;
			this.labelUniqueWarning.Wrap = true;
			this.table1.Add (this.labelUniqueWarning);
			global::Gtk.Table.TableChild w12 = ((global::Gtk.Table.TableChild)(this.table1 [this.labelUniqueWarning]));
			w12.TopAttach = ((uint)(3));
			w12.BottomAttach = ((uint)(4));
			w12.LeftAttach = ((uint)(1));
			w12.RightAttach = ((uint)(2));
			w12.XOptions = ((global::Gtk.AttachOptions)(4));
			w12.YOptions = ((global::Gtk.AttachOptions)(4));
			this.hbox1.Add (this.table1);
			global::Gtk.Box.BoxChild w13 = ((global::Gtk.Box.BoxChild)(this.hbox1 [this.table1]));
			w13.Position = 1;
			w13.Expand = false;
			w13.Fill = false;
			w1.Add (this.hbox1);
			global::Gtk.Box.BoxChild w14 = ((global::Gtk.Box.BoxChild)(w1 [this.hbox1]));
			w14.Position = 0;
			// Internal child BlinkStickClient.SelectNotificationDialog.ActionArea
			global::Gtk.HButtonBox w15 = this.ActionArea;
			w15.Name = "dialog1_ActionArea";
			w15.Spacing = 10;
			w15.BorderWidth = ((uint)(5));
			w15.LayoutStyle = ((global::Gtk.ButtonBoxStyle)(4));
			// Container child dialog1_ActionArea.Gtk.ButtonBox+ButtonBoxChild
			this.buttonCancel = new global::Gtk.Button ();
			this.buttonCancel.CanDefault = true;
			this.buttonCancel.CanFocus = true;
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.UseStock = true;
			this.buttonCancel.UseUnderline = true;
			this.buttonCancel.Label = "gtk-cancel";
			this.AddActionWidget (this.buttonCancel, -6);
			global::Gtk.ButtonBox.ButtonBoxChild w16 = ((global::Gtk.ButtonBox.ButtonBoxChild)(w15 [this.buttonCancel]));
			w16.Expand = false;
			w16.Fill = false;
			// Container child dialog1_ActionArea.Gtk.ButtonBox+ButtonBoxChild
			this.buttonOk = new global::Gtk.Button ();
			this.buttonOk.CanDefault = true;
			this.buttonOk.CanFocus = true;
			this.buttonOk.Name = "buttonOk";
			this.buttonOk.UseStock = true;
			this.buttonOk.UseUnderline = true;
			this.buttonOk.Label = "gtk-ok";
			this.AddActionWidget (this.buttonOk, -5);
			global::Gtk.ButtonBox.ButtonBoxChild w17 = ((global::Gtk.ButtonBox.ButtonBoxChild)(w15 [this.buttonOk]));
			w17.Position = 1;
			w17.Expand = false;
			w17.Fill = false;
			if ((this.Child != null)) {
				this.Child.ShowAll ();
			}
			this.DefaultWidth = 642;
			this.DefaultHeight = 389;
			this.Show ();
			this.treeviewNotifications.CursorChanged += new global::System.EventHandler (this.OnTreeviewNotificationsCursorChanged);
			this.checkbuttonDisplayOnlySupported.Toggled += new global::System.EventHandler (this.OnCheckbuttonDisplayOnlySupportedToggled);
		}
	}
}
