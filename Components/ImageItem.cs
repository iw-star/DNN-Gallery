using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace IWStar.DNN.Modules.IWStarGallery.Components
{
    [XmlRoot(ElementName = "image")]
    public class ImageItem
    {
        public string path { get; set; }

        public int fileId { get; set; }

        public string title { get; set; }

        public string description { get; set; }

        public string thumbPath { get; set; }
    }
}