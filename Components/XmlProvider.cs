using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace IWStar.DNN.Modules.IWStarGallery.Components
{
    public class XmlProvider<T> where T : class, new()
    {
        private readonly string xmlPathName;

        private readonly XmlSerializer xmlSerializer;

        private readonly string rootContainerName;

        private readonly string rootItemName;

        public XmlProvider(string filePath, string rootContainerName, string rootItemName)
        {
            this.xmlPathName = filePath;
            this.xmlSerializer = new XmlSerializer(typeof(T));
            this.rootItemName = rootItemName;
            this.rootContainerName = rootContainerName;
        }

        public static void CreateNewConfigurationFile(string filePath)
        {
            XmlDocument xmlDocument = new XmlDocument();
            XmlDeclaration xmlRoot = xmlDocument.CreateXmlDeclaration("1.0", "UTF-8", string.Empty);
            xmlDocument.AppendChild(xmlRoot);
            XmlNode xmlNode = xmlDocument.CreateNode(XmlNodeType.Element, "data", "");
            xmlDocument.AppendChild(xmlNode);
            XmlNode newChild = xmlDocument.CreateNode(XmlNodeType.Element, "images", string.Empty);
            xmlNode.AppendChild(newChild);
            xmlDocument.Save(filePath);
        }

        public void InsertItemBeforeIndex(int index, T item)
        {
            XmlDocument xmlDocument = this.LoadDocument();
            XmlNodeList xmlNodeList = xmlDocument.SelectNodes(this.GetItemsXPath());
            if (xmlNodeList != null && (xmlNodeList.Count > index || (xmlNodeList.Count == 0 && index == 0)) && index >= 0)
            {
                XmlNode newChild = this.CreateXmlNode(item, xmlDocument);
                XmlNode xmlNode = xmlDocument.SelectSingleNode(this.rootContainerName);
                if (xmlNodeList.Count > 0)
                {
                    xmlNode.InsertBefore(newChild, xmlNodeList[index]);
                }
                else
                {
                    xmlNode.AppendChild(newChild);
                }
                xmlDocument.Save(this.xmlPathName);
            }
        }

        public void InsertItemAfterIndex(int index, T item)
        {
            XmlDocument xmlDocument = this.LoadDocument();
            XmlNodeList xmlNodeList = xmlDocument.SelectNodes(this.GetItemsXPath());
            if (xmlNodeList != null && xmlNodeList.Count > index && index >= 0)
            {
                XmlNode newChild = this.CreateXmlNode(item, xmlDocument);
                XmlNode xmlNode = xmlDocument.SelectSingleNode(this.rootContainerName);
                xmlNode.InsertAfter(newChild, xmlNodeList[index]);
                xmlDocument.Save(this.xmlPathName);
            }
        }

        private XmlDocument LoadDocument()
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(this.xmlPathName);
            return xmlDocument;
        }

        private string GetItemsXPath()
        {
            return this.rootContainerName + "/" + this.rootItemName;
        }

        private XmlNode CreateXmlNode(T item, XmlDocument doc)
        {
            StringBuilder stringBuilder = new StringBuilder();
            using (XmlWriter xmlWriter = XmlWriter.Create(stringBuilder, new XmlWriterSettings
            {
                Encoding = Encoding.UTF8,
                OmitXmlDeclaration = false
            }))
            {
                if (xmlWriter != null)
                {
                    this.xmlSerializer.Serialize(xmlWriter, item);
                }
            }
            return XmlProvider<T>.CreateNodeFromString(doc, stringBuilder.ToString());
        }

        private static XmlNode CreateNodeFromString(XmlDocument doc, string nodeText)
        {
            XmlDocumentFragment xmlDocumentFragment = doc.CreateDocumentFragment();
            xmlDocumentFragment.InnerXml = nodeText;
            foreach (XmlNode xmlNode in xmlDocumentFragment.ChildNodes)
            {
                if (xmlNode.NodeType == XmlNodeType.Element)
                {
                    return xmlNode;
                }
            }
            return null;
        }

        public IList<T> GetItems()
        {
            if (!string.IsNullOrEmpty(this.xmlPathName) && !string.IsNullOrEmpty(this.rootItemName))
            {
                XmlDocument xmlDocument = this.LoadDocument();
                XmlNodeList xmlNodeList = xmlDocument.SelectNodes(this.GetItemsXPath());
                IList<T> list = new List<T>();
                if (xmlNodeList != null)
                {
                    foreach (XmlNode node in xmlNodeList)
                    {
                        XmlReader xmlReader = new XmlNodeReader(node);
                        T item = (T)((object)this.xmlSerializer.Deserialize(xmlReader));
                        list.Add(item);
                    }
                }
                return list;
            }
            else
            {
                return null;
            }
        }

        public int GetItemCount()
        {
            XmlDocument xmlDocument = this.LoadDocument();
            XmlNodeList xmlNodeList = xmlDocument.SelectNodes(this.GetItemsXPath());
            return (xmlNodeList != null) ? xmlNodeList.Count : -1;
        }

        public T DeleteItemByIndex(int index)
        {
            XmlDocument xmlDocument = this.LoadDocument();
            XmlNodeList xmlNodeList = xmlDocument.SelectNodes(this.GetItemsXPath());
            T result;
            if (xmlNodeList != null && xmlNodeList.Count > index && index >= 0)
            {
                using (XmlReader xmlReader = new XmlNodeReader(xmlNodeList[index]))
                {
                    T t = (T)((object)this.xmlSerializer.Deserialize(xmlReader));
                    XmlNode xmlNode = xmlDocument.SelectSingleNode(this.rootContainerName);
                    xmlNode.RemoveChild(xmlNodeList[index]);
                    xmlDocument.Save(this.xmlPathName);
                    result = t;
                    return result;
                }
            }
            result = default(T);
            return result;
        }

        public void UpdateItemByIndex(int index, T item)
        {
            XmlDocument xmlDocument = this.LoadDocument();
            XmlNodeList xmlNodeList = xmlDocument.SelectNodes(this.GetItemsXPath());
            if (xmlNodeList != null && xmlNodeList.Count > index && index >= 0)
            {
                XmlNode newChild = this.CreateXmlNode(item, xmlDocument);
                XmlNode xmlNode = xmlDocument.SelectSingleNode(this.rootContainerName);
                xmlNode.ReplaceChild(newChild, xmlNodeList[index]);
                xmlDocument.Save(this.xmlPathName);
            }
        }

    }
}