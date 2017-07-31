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
using System.IO;
using DotNetNuke.Services.Exceptions;
using DotNetNuke.Services.FileSystem;
using DotNetNuke.Entities.Modules;


namespace IWStar.DNN.Modules.IWStarGallery.Components
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
    public partial class EditItem : PortalModuleBase
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                this.litMessage.Text = "";
            }
            catch (Exception exc) //Module failed to load
            {
                Exceptions.ProcessModuleLoadException(this, exc);
            }
        }

        public ImageItem GetImageInfo()
        {
            int fileId = 0;
            if (this.phUploadArea.Visible)
            {
                fileId = this.UploadImage();
            }
            else if (this.phSelectFileArea.Visible)
            {
                fileId = Utils.CInt(ddFile.SelectedValue);
            }

            ImageItem itemInfo = new ImageItem();
            itemInfo.title = this.txtTitle.Text;
            itemInfo.description = this.txtDescription.Text;
            itemInfo.fileId = fileId;

            if (fileId > 0)
            {
                IFileInfo iFileInfo = FileManager.Instance.GetFile(fileId);
                itemInfo.path = this.PortalSettings.HomeDirectory + iFileInfo.Folder + iFileInfo.FileName;

                string thumbPhysicalPath = this.PortalSettings.HomeDirectoryMapPath + iFileInfo.Folder + "thumb_" + iFileInfo.FileName;
                Utils.GenerateThumbnailImage(50, iFileInfo.PhysicalPath, thumbPhysicalPath);
                itemInfo.thumbPath = this.PortalSettings.HomeDirectory + iFileInfo.Folder + "thumb_" + iFileInfo.FileName;
            }
            else
            {
                this.litMessage.Text = "Please select or upload an image.";
            }

            return itemInfo;
        }

        public void SetImageInfo(ImageItem imageInfo)
        {
            this.LoadFolders();
            this.LoadFiles();

            if (imageInfo != null)
            {
                this.txtTitle.Text = imageInfo.title;
                this.txtDescription.Text = imageInfo.description;
                if (imageInfo.fileId != 0)
                {
                    IFileInfo iFileInfo = FileManager.Instance.GetFile(imageInfo.fileId);
                    this.ddFolder.SelectedValue = iFileInfo.FolderId.ToString();
                    this.LoadFiles();
                    this.ddFile.SelectedValue = imageInfo.fileId.ToString();
                }
                else
                {
                    this.ddFile.SelectedValue = "";
                }
            }
        }

        private int UploadImage()
        {
            int fileId = 0;

            if (this.fileImage.HasFile && !string.IsNullOrEmpty(this.ddFolder.SelectedValue))
            {
                try
                {
                    int folderId = Utils.CInt(this.ddFolder.SelectedValue);
                    IFolderInfo iFolderInfo = FolderManager.Instance.GetFolder(folderId);

                    string fileType = this.fileImage.PostedFile.ContentType;
                    if ((fileType == "image/jpeg" | fileType == "image/gif" | fileType == "image/png" | fileType == "image/x-png" | fileType == "image/pjpeg"))
                    {
                        string filePath = Path.Combine(iFolderInfo.PhysicalPath, this.fileImage.FileName);
                        if (File.Exists(filePath))
                        {
                            this.litMessage.Text = string.Format("File upload failed. File with name {0} already exist.", this.fileImage.FileName);
                        }
                        else
                        {
                            IFileInfo iFileInfo = FileManager.Instance.AddFile(iFolderInfo, this.fileImage.FileName, this.fileImage.FileContent);
                            if (iFileInfo != null)
                            {
                                fileId = iFileInfo.FileId;
                                //FolderManager.Instance.Synchronize(this.PortalId, iFolderInfo.FolderPath, false, true);
                                this.LoadFiles();
                            }
                        }
                    }
                    else
                    {
                        this.litMessage.Text = "You have uploaded an invalid image.";
                    }

                }
                catch (Exception exc)
                {
                    Exceptions.ProcessModuleLoadException(this, exc);
                }
            }

            return fileId;
        }

        private void LoadFolders()
        {
            IEnumerable<IFolderInfo> folders = FolderManager.Instance.GetFolders(this.UserInfo);
            foreach (IFolderInfo iFolderInfo in folders)
            {
                if (string.IsNullOrEmpty(iFolderInfo.FolderName))
                {
                    iFolderInfo.FolderPath = "Root";
                }
            }
            this.ddFolder.DataSource = folders;
            this.ddFolder.DataTextField = "FolderPath";
            this.ddFolder.DataValueField = "FolderID";
            this.ddFolder.DataBind();
        }

        private void LoadFiles()
        {
            int folderID = Utils.CInt(ddFolder.SelectedValue);
            IFolderInfo iFolderInfo = FolderManager.Instance.GetFolder(folderID);
            IEnumerable<IFileInfo> files = FolderManager.Instance.GetFiles(iFolderInfo);
            this.ddFile.DataSource = files;
            this.ddFile.DataTextField = "FileName";
            this.ddFile.DataValueField = "FileId";
            this.ddFile.DataBind();
            this.ddFile.Items.Insert(0, string.Empty);
        }

        protected void btnOpenUploadArea_Click(object sender, EventArgs e)
        {
            this.phSelectFileArea.Visible = false;
            this.phUploadArea.Visible = true;
            this.ddFile.SelectedValue = "";
        }

        protected void btnCloseUploadArea_Click(object sender, EventArgs e)
        {
            this.phSelectFileArea.Visible = true;
            this.phUploadArea.Visible = false;
        }

        protected void ddFolder_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.LoadFiles();
        }
    }
}