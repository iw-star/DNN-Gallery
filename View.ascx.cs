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
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Entities.Modules.Actions;
using DotNetNuke.Security;
using DotNetNuke.Services.Exceptions;
using DotNetNuke.Services.Localization;
using IWStar.DNN.Modules.IWStarGallery.Components;


namespace IWStar.DNN.Modules.IWStarGallery
{
    /// -----------------------------------------------------------------------------
    /// <summary>
    /// The View class displays the content
    /// 
    /// Typically your view control would be used to display content or functionality in your module.
    /// 
    /// View may be the only control you have in your project depending on the complexity of your module
    /// 
    /// Because the control inherits from IWStarGalleryModuleBase you have access to any custom properties
    /// defined there, as well as properties from DNN such as PortalId, ModuleId, TabId, UserId and many more.
    /// 
    /// </summary>
    /// -----------------------------------------------------------------------------
    public partial class View : PortalModuleBase, IActionable
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                LoadGallery();
            }
            catch (Exception exc) //Module failed to load
            {
                Exceptions.ProcessModuleLoadException(this, exc);
            }
        }

        private void LoadGallery()
        {
            XmlController xmlController = new XmlController();
            IList<ImageItem> allImages = xmlController.GetAllImages(this.PortalId, this.ModuleId);
            if (allImages.Count > 0)
            {
                this.phGallery.Visible = true;
                this.RepeaterGallery.DataSource = allImages;
                this.RepeaterGallery.DataBind();

                bool showIndicator = Utils.DEFAULT_SHOW_INDICATOR;
                if (this.Settings.Contains(Utils.SETTINGS_SHOW_INDICATOR))
                {
                    showIndicator = Utils.CBool(this.Settings[Utils.SETTINGS_SHOW_INDICATOR]);
                }
                if (showIndicator)
                {
                    this.phIndicator.Visible = true;
                    this.RepeaterGalleryInd.DataSource = allImages;
                    this.RepeaterGalleryInd.DataBind();
                }

                bool showThumbnail = Utils.DEFAULT_SHOW_THUMBNAIL;
                if (this.Settings.Contains(Utils.SETTINGS_SHOW_THUMBNAIL))
                {
                    showThumbnail = Utils.CBool(this.Settings[Utils.SETTINGS_SHOW_THUMBNAIL]);
                }
                if (showThumbnail)
                {
                    this.phThumbnail.Visible = true;
                    this.RepeaterGalleryThumbnail.DataSource = allImages;
                    this.RepeaterGalleryThumbnail.DataBind();
                }
            }

        }

        protected void RepeaterGallery_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                ImageItem imageItem = (ImageItem)e.Item.DataItem;

                Image imgCarouselImage = (Image)e.Item.FindControl("imgCarouselImage");
                imgCarouselImage.Attributes.Add("data-src", imageItem.path);
                if (e.Item.ItemIndex == 0)
                {
                    imgCarouselImage.ImageUrl = imageItem.path;
                }

                bool showPopupTitle = Utils.DEFAULT_SHOW_POPUP_TITLE;
                if (this.Settings.Contains(Utils.SETTINGS_SHOW_POPUP_TITLE))
                {
                    showPopupTitle = Utils.CBool(this.Settings[Utils.SETTINGS_SHOW_POPUP_TITLE]);
                }
                if (showPopupTitle)
                {
                    imgCarouselImage.Attributes.Add("data-title", imageItem.title);
                }

                bool showPopupDesc = Utils.DEFAULT_SHOW_POPUP_DESCRIPTION;
                if (this.Settings.Contains(Utils.SETTINGS_SHOW_POPUP_DESCRIPTION))
                {
                    showPopupDesc = Utils.CBool(this.Settings[Utils.SETTINGS_SHOW_POPUP_DESCRIPTION]);
                }
                if (showPopupDesc)
                {
                    imgCarouselImage.Attributes.Add("data-desc", imageItem.description);
                }

                HtmlGenericControl carouselCaption = (HtmlGenericControl)e.Item.FindControl("carouselCaption");

                bool showTitle = Utils.DEFAULT_SHOW_TITLE;
                if (this.Settings.Contains(Utils.SETTINGS_SHOW_TITLE))
                {
                    showTitle = Utils.CBool(this.Settings[Utils.SETTINGS_SHOW_TITLE]);
                }
                if (showTitle)
                {
                    if (!string.IsNullOrWhiteSpace(imageItem.title))
                    {
                        HtmlGenericControl carouselTitle = (HtmlGenericControl)e.Item.FindControl("carouselTitle");
                        carouselTitle.Visible = true;
                        carouselCaption.Visible = true;
                    }
                }

                bool showDesc = Utils.DEFAULT_SHOW_DESCRIPTION;
                if (this.Settings.Contains(Utils.SETTINGS_SHOW_DESCRIPTION))
                {
                    showDesc = Utils.CBool(this.Settings[Utils.SETTINGS_SHOW_DESCRIPTION]);
                }
                if (showDesc)
                {
                    if (!string.IsNullOrWhiteSpace(imageItem.description))
                    {
                        HtmlGenericControl carouselDesc = (HtmlGenericControl)e.Item.FindControl("carouselDesc");
                        carouselDesc.Visible = true;
                        carouselCaption.Visible = true;
                    }
                }

            }
        }

        protected string GetModuleId()
        {
            return this.ModuleId.ToString();
        }

        protected float GetWidth()
        {
            if (this.Settings.Contains(Utils.SETTINGS_MAX_WIDTH))
            {
                return Utils.CInt(this.Settings[Utils.SETTINGS_MAX_WIDTH]);
            }
            else
            {
                return Utils.DEFAULT_MAX_WIDTH;
            }
        }

        protected int GetHeight()
        {
            if (this.Settings.Contains(Utils.SETTINGS_MAX_HEIGHT))
            {
                return Utils.CInt(this.Settings[Utils.SETTINGS_MAX_HEIGHT]);
            }
            else
            {
                return Utils.DEFAULT_MAX_HEIGHT;
            }
        }

        protected string GetPopup()
        {
            bool showPopup = Utils.DEFAULT_SHOW_POPUP;
            if (this.Settings.Contains(Utils.SETTINGS_SHOW_POPUP))
            {
                showPopup = Utils.CBool(this.Settings[Utils.SETTINGS_SHOW_POPUP]);
            }
            if (showPopup)
            {
                return "iw-popup";
            }
            else
            {
                return "";
            }
        }

        public ModuleActionCollection ModuleActions
        {
            get
            {
                var actions = new ModuleActionCollection
                    {
                        {
                            GetNextActionID(), Localization.GetString("EditModule", LocalResourceFile), "", "", "",
                            EditUrl(), false, SecurityAccessLevel.Edit, true, false
                        }
                    };
                return actions;
            }
        }
    }
}