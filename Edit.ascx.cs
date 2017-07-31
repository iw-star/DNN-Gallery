/*
' Copyright (c) 2017  IW Star
'  All rights reserved.
' 
' THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED
' TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL
' THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF
' CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
' DEALINGS IN THE SOFTWARE.
' 
*/


using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Services.Exceptions;
using DotNetNuke.Services.FileSystem;
using IWStar.DNN.Modules.IWStarGallery.Components;


namespace IWStar.DNN.Modules.IWStarGallery
{
    /// -----------------------------------------------------------------------------
    /// <summary>
    /// The EditIWStarGallery class is used to manage content
    /// 
    /// Typically your edit control would be used to create new content, or edit existing content within your module.
    /// The ControlKey for this control is "Edit", and is defined in the manifest (.dnn) file.
    /// 
    /// Because the control inherits from IWStarGalleryModuleBase you have access to any custom properties
    /// defined there, as well as properties from DNN such as PortalId, ModuleId, TabId, UserId and many more.
    /// 
    /// </summary>
    /// -----------------------------------------------------------------------------
    public partial class Edit : PortalModuleBase
    {
        private int itemCount = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!this.IsPostBack)
                {
                    this.BindImages();
                }
            }
            catch (Exception exc) //Module failed to load
            {
                Exceptions.ProcessModuleLoadException(this, exc);
            }
        }

        protected void btnShowNewItemArea_Click(object sender, EventArgs e)
        {
            this.SetNewItemArea(true);
            this.iwNewItem.SetImageInfo(new ImageItem());
        }

        protected void btnAddNewItem_Click(object sender, EventArgs e)
        {
            ImageItem image = this.iwNewItem.GetImageInfo();
            if (image.fileId != 0)
            {
                XmlController xmlController = new XmlController();
                xmlController.AddImageToBegin(image, this.PortalId, this.ModuleId);
                this.SetNewItemArea(false);
                this.BindImages();
            }
        }

        protected void btnAddCancel_Click(object sender, EventArgs e)
        {
            this.SetNewItemArea(false);
        }

        private void SetNewItemArea(bool editable)
        {
            this.phTopBar.Visible = !editable;
            this.phAddNewItem.Visible = editable;
        }

        protected void btnGenerateThumbnail_Click(object sender, EventArgs e)
        {
            XmlController xmlController = new XmlController();
            IList<ImageItem> allImages = xmlController.GetAllImages(this.PortalId, this.ModuleId);
            for (int i=0; i < allImages.Count; i++)
            {
                ImageItem image = allImages[i];
                IFileInfo iFileInfo = FileManager.Instance.GetFile(image.fileId);
                if (iFileInfo != null)
                {
                    string thumbFilePath = this.PortalSettings.HomeDirectoryMapPath + iFileInfo.Folder + "thumb_" + iFileInfo.FileName;
                    Utils.GenerateThumbnailImage(50, iFileInfo.PhysicalPath, thumbFilePath);
                    image.thumbPath = this.PortalSettings.HomeDirectory + iFileInfo.Folder + "thumb_" + iFileInfo.FileName;
                    xmlController.UpdateImage(this.PortalId, this.ModuleId, i, image);
                }
            }
        }

        private void BindImages()
        {
            XmlController xmlController = new XmlController();
            IList<ImageItem> allImages = xmlController.GetAllImages(this.PortalId, this.ModuleId);
            this.itemCount = allImages.Count;
            this.rptImages.DataSource = allImages;
            this.rptImages.DataBind();
        }

        protected void btnSettings_Click(object sender, EventArgs e)
        {
            this.SetSettingsArea(true);
            this.LoadSettings();
        }

        private void SetSettingsArea(bool editable)
        {
            this.phTopBar.Visible = !editable;
            this.phSettings.Visible = editable;
        }

        private void LoadSettings()
        {
            if (this.Settings.Contains(Utils.SETTINGS_MAX_WIDTH))
            {
                this.txtMaxWidth.Text = this.Settings[Utils.SETTINGS_MAX_WIDTH].ToString();
            }
            else
            {
                this.txtMaxWidth.Text = Utils.DEFAULT_MAX_WIDTH.ToString();
            }

            if (this.Settings.Contains(Utils.SETTINGS_MAX_HEIGHT))
            {
                this.txtMaxHeight.Text = this.Settings[Utils.SETTINGS_MAX_HEIGHT].ToString();
            }
            else
            {
                this.txtMaxHeight.Text = Utils.DEFAULT_MAX_HEIGHT.ToString();
            }

            if (this.Settings.Contains(Utils.SETTINGS_SHOW_INDICATOR))
            {
                this.chkIndicator.Checked = Utils.CBool(this.Settings[Utils.SETTINGS_SHOW_INDICATOR]);
            }
            else
            {
                this.chkIndicator.Checked = Utils.DEFAULT_SHOW_INDICATOR;
            }

            if (this.Settings.Contains(Utils.SETTINGS_SHOW_THUMBNAIL))
            {
                this.chkThumbnail.Checked = Utils.CBool(this.Settings[Utils.SETTINGS_SHOW_THUMBNAIL]);
            }
            else
            {
                this.chkThumbnail.Checked = Utils.DEFAULT_SHOW_THUMBNAIL;
            }

            if (this.Settings.Contains(Utils.SETTINGS_SHOW_TITLE))
            {
                this.chkTitle.Checked = Utils.CBool(this.Settings[Utils.SETTINGS_SHOW_TITLE]);
            }
            else
            {
                this.chkTitle.Checked = Utils.DEFAULT_SHOW_TITLE;
            }

            if (this.Settings.Contains(Utils.SETTINGS_SHOW_DESCRIPTION))
            {
                this.chkDescription.Checked = Utils.CBool(this.Settings[Utils.SETTINGS_SHOW_DESCRIPTION]);
            }
            else
            {
                this.chkDescription.Checked = Utils.DEFAULT_SHOW_DESCRIPTION;
            }

            if (this.Settings.Contains(Utils.SETTINGS_SHOW_POPUP))
            {
                this.chkPopup.Checked = Utils.CBool(this.Settings[Utils.SETTINGS_SHOW_POPUP]);
            }
            else
            {
                this.chkPopup.Checked = Utils.DEFAULT_SHOW_POPUP;
            }

            if (this.Settings.Contains(Utils.SETTINGS_SHOW_POPUP_TITLE))
            {
                this.chkPopupTitle.Checked = Utils.CBool(this.Settings[Utils.SETTINGS_SHOW_POPUP_TITLE]);
            }
            else
            {
                this.chkPopupTitle.Checked = Utils.DEFAULT_SHOW_POPUP_TITLE;
            }

            if (this.Settings.Contains(Utils.SETTINGS_SHOW_POPUP_DESCRIPTION))
            {
                this.chkPopupDesc.Checked = Utils.CBool(this.Settings[Utils.SETTINGS_SHOW_POPUP_DESCRIPTION]);
            }
            else
            {
                this.chkPopupDesc.Checked = Utils.DEFAULT_SHOW_POPUP_DESCRIPTION;
            }
        }

        protected void btnSettingsCancel_Click(object sender, EventArgs e)
        {
            this.SetSettingsArea(false);
        }

        protected void btnSettingsSave_Click(object sender, EventArgs e)
        {
            ModuleController.Instance.UpdateModuleSetting(this.ModuleId, Utils.SETTINGS_MAX_WIDTH, Utils.CInt(this.txtMaxWidth.Text).ToString());
            ModuleController.Instance.UpdateModuleSetting(this.ModuleId, Utils.SETTINGS_MAX_HEIGHT, Utils.CInt(this.txtMaxHeight.Text).ToString());
            ModuleController.Instance.UpdateModuleSetting(this.ModuleId, Utils.SETTINGS_SHOW_INDICATOR, this.chkIndicator.Checked.ToString());
            ModuleController.Instance.UpdateModuleSetting(this.ModuleId, Utils.SETTINGS_SHOW_THUMBNAIL, this.chkThumbnail.Checked.ToString());
            ModuleController.Instance.UpdateModuleSetting(this.ModuleId, Utils.SETTINGS_SHOW_TITLE, this.chkTitle.Checked.ToString());
            ModuleController.Instance.UpdateModuleSetting(this.ModuleId, Utils.SETTINGS_SHOW_DESCRIPTION, this.chkDescription.Checked.ToString());
            ModuleController.Instance.UpdateModuleSetting(this.ModuleId, Utils.SETTINGS_SHOW_POPUP, this.chkPopup.Checked.ToString());
            ModuleController.Instance.UpdateModuleSetting(this.ModuleId, Utils.SETTINGS_SHOW_POPUP_TITLE, this.chkPopupTitle.Checked.ToString());
            ModuleController.Instance.UpdateModuleSetting(this.ModuleId, Utils.SETTINGS_SHOW_POPUP_DESCRIPTION, this.chkPopupDesc.Checked.ToString());
            this.SetSettingsArea(false);
        }

        protected void btnDeleteItem_Command(object sender, CommandEventArgs e)
        {
            int index = Utils.CInt(e.CommandArgument);
            XmlController xmlController = new XmlController();
            xmlController.DeleteImage(this.PortalId, this.ModuleId, index);
            this.BindImages();
        }

        protected void btnEditItem_Command(object sender, CommandEventArgs e)
        {
            int index = Utils.CInt(e.CommandArgument);
            RepeaterItem repeaterItem = this.rptImages.Items[index];
            this.SetEditItemArea(repeaterItem, true);
        }

        protected void rptImages_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                ImageItem imageItem = (ImageItem)e.Item.DataItem;

                EditItem iwEditItem = (EditItem)e.Item.FindControl("iwEditItem");
                iwEditItem.SetImageInfo(imageItem);

                LinkButton btnMoveUp = (LinkButton)e.Item.FindControl("btnMoveUp");
                if (e.Item.ItemIndex == 0)
                {
                    btnMoveUp.Visible = false;
                }

                LinkButton btnMoveDown = (LinkButton)e.Item.FindControl("btnMoveDown");
                if (e.Item.ItemIndex == this.itemCount - 1)
                {
                    btnMoveDown.Visible = false;
                }
            }
        }

        protected void btnSaveItem_Command(object sender, CommandEventArgs e)
        {
            int index = Utils.CInt(e.CommandArgument);
            RepeaterItem repeaterItem = this.rptImages.Items[index];
            EditItem iwEditItem = (EditItem)repeaterItem.FindControl("iwEditItem");

            ImageItem image = iwEditItem.GetImageInfo();
            if (image.fileId != 0)
            {
                XmlController xmlController = new XmlController();
                xmlController.UpdateImage(this.PortalId, this.ModuleId, index, image);
                this.SetEditItemArea(repeaterItem, false);
                this.BindImages();
            }
        }

        protected void btnSaveCancel_Command(object sender, CommandEventArgs e)
        {
            int index = Utils.CInt(e.CommandArgument);
            RepeaterItem repeaterItem = this.rptImages.Items[index];
            this.SetEditItemArea(repeaterItem, false);
        }

        private void SetEditItemArea(RepeaterItem repeaterItem, bool editable)
        {
            PlaceHolder phViewItem = (PlaceHolder)repeaterItem.FindControl("phViewItem");
            phViewItem.Visible = !editable;

            PlaceHolder phEditItem = (PlaceHolder)repeaterItem.FindControl("phEditItem");
            phEditItem.Visible = editable;
        }

        protected void btnMoveUp_Command(object sender, CommandEventArgs e)
        {
            int index = Utils.CInt(e.CommandArgument);
            XmlController xmlController = new XmlController();
            xmlController.MoveUpImage(this.PortalId, this.ModuleId, index);
            this.BindImages();
        }

        protected void btnMoveDown_Command(object sender, CommandEventArgs e)
        {
            int index = Utils.CInt(e.CommandArgument);
            XmlController xmlController = new XmlController();
            xmlController.MoveDownImage(this.PortalId, this.ModuleId, index);
            this.BindImages();
        }
    }
}