using System.Collections.Generic;
using System.IO;
using DotNetNuke.Entities.Portals;

namespace IWStar.DNN.Modules.IWStarGallery.Components
{
    public class XmlController
    {

        public void AddImageToBegin(ImageItem items, int portalId, int moduleId)
        {
            string filePath = this.GetXmlFilePath(portalId, moduleId);
            XmlProvider<ImageItem> xmlProvider = new XmlProvider<ImageItem>(filePath, "data/images", "image");
            xmlProvider.InsertItemBeforeIndex(0, items);
        }

        public string GetXmlFilePath(int portalId, int moduleId)
        {
            string foldPath = PortalController.Instance.GetPortal(portalId).HomeDirectoryMapPath + "/IW_Gallery/" + moduleId.ToString() + "/";
            if (!Directory.Exists(foldPath))
            {
                Directory.CreateDirectory(foldPath);
            }

            string filePath = foldPath + "configurtion.xml";
            if (!File.Exists(filePath))
            {
                XmlProvider<ImageItem>.CreateNewConfigurationFile(filePath);
            }
            return filePath;
        }

        public virtual IList<ImageItem> GetAllImages(int portalId, int moduleId)
        {
            string filePath = this.GetXmlFilePath(portalId, moduleId);
            XmlProvider<ImageItem> xmlProvider = new XmlProvider<ImageItem>(filePath, "data/images", "image");
            return xmlProvider.GetItems();
        }

        public void DeleteImage(int portalId, int moduleId, int index)
        {
            string filePath = this.GetXmlFilePath(portalId, moduleId);
            XmlProvider<ImageItem> xmlProvider = new XmlProvider<ImageItem>(filePath, "data/images", "image");
            if (index < xmlProvider.GetItemCount())
            {
                xmlProvider.DeleteItemByIndex(index);
            }
        }

        public void UpdateImage(int portalId, int moduleId, int index, ImageItem item)
        {
            string filePath = this.GetXmlFilePath(portalId, moduleId);
            XmlProvider<ImageItem> xmlProvider = new XmlProvider<ImageItem>(filePath, "data/images", "image");
            xmlProvider.UpdateItemByIndex(index, item);
        }

        public void MoveUpImage(int portalId, int moduleId, int index)
        {
            if (index > 0)
            {
                string filePath = this.GetXmlFilePath(portalId, moduleId);
                XmlProvider<ImageItem> xmlProvider = new XmlProvider<ImageItem>(filePath, "data/images", "image");
                ImageItem item = xmlProvider.DeleteItemByIndex(index);
                xmlProvider.InsertItemBeforeIndex(index - 1, item);
            }
        }

        public void MoveDownImage(int portalId, int moduleId, int index)
        {
            string filePath = this.GetXmlFilePath(portalId, moduleId);
            XmlProvider<ImageItem> xmlProvider = new XmlProvider<ImageItem>(filePath, "data/images", "image");

            if (index >= 0 && index < xmlProvider.GetItemCount() - 1)
            {
                ImageItem item = xmlProvider.DeleteItemByIndex(index);
                xmlProvider.InsertItemAfterIndex(index, item);
            }
        }
    }
}