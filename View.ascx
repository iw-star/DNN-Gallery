<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="View.ascx.cs" Inherits="IWStar.DNN.Modules.IWStarGallery.View" %>
<%@ Register TagPrefix="dnn" Namespace="DotNetNuke.Web.Client.ClientResourceManagement" Assembly="DotNetNuke.Web.Client" %>
<dnn:DnnJsInclude runat="server" FilePath="/desktopmodules/iwstargallery/resources/bootstrap/js/bootstrap.min.js" />
<dnn:DnnCssInclude runat="server" FilePath="/desktopmodules/iwstargallery/resources/bootstrap/css/bootstrap.min.css" />
<dnn:DnnJsInclude runat="server" FilePath="/desktopmodules/iwstargallery/resources/fancybox/jquery.fancybox-1.3.4.pack.js" />
<dnn:DnnCssInclude runat="server" FilePath="/desktopmodules/iwstargallery/resources/fancybox/jquery.fancybox-1.3.4.css" />
<dnn:DnnJsInclude runat="server" FilePath="/desktopmodules/iwstargallery/resources/iwstargallery.js" />

<asp:PlaceHolder ID="phGallery" Visible="false" runat="server">
    <div id="iw-star-gallery-<%= GetModuleId() %>" class="carousel slide iw-star-gallery lazy carousel-fade" data-ride="carousel">

        <div class='carousel-inner <%= GetPopup() %>' data-width="<%= GetWidth() %>" data-height="<%= GetHeight() %>">
            <asp:Repeater ID="RepeaterGallery" runat="server" OnItemDataBound="RepeaterGallery_ItemDataBound">
                <ItemTemplate>
                    <div class='item <%# Container.ItemIndex == 0? "active" : ""%>' style="height: 0px;">
                        <asp:Image ID="imgCarouselImage" runat="server" />
                        <div id="carouselCaption" class="carousel-caption" visible="false" runat="server">
                            <div id="carouselTitle" class="carousel-title" visible="false" runat="server">
                                <%#Eval("title")%>
                            </div>
                            <div id="carouselDesc" class="carousel-desc" visible="false" runat="server">
                                <%#Eval("description")%>
                            </div>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>

        <asp:PlaceHolder ID="phIndicator" Visible="false" runat="server">
            <ol class="carousel-indicators">
                <asp:Repeater ID="RepeaterGalleryInd" runat="server">
                    <ItemTemplate>
                        <li data-target="#iw-star-gallery-<%# GetModuleId() %>" data-slide-to="<%# Container.ItemIndex %>" class='<%# Container.ItemIndex == 0? "active" : ""%>'></li>
                    </ItemTemplate>
                </asp:Repeater>
            </ol>
        </asp:PlaceHolder>

        <asp:PlaceHolder ID="phThumbnail" Visible="false" runat="server">
            <ul class="thumbnails-carousel clearfix">
                <asp:Repeater ID="RepeaterGalleryThumbnail" runat="server">
                    <ItemTemplate>
                        <li class="<%# Container.ItemIndex == 0? "active-thumbnail" : ""%>">
                            <div class="carousel-thumb-image" style='<%# "background-image: url(\"" + Eval("thumbPath") + "\")"%>'></div>
                        </li>
                    </ItemTemplate>
                </asp:Repeater>
            </ul>
        </asp:PlaceHolder>
    </div>
</asp:PlaceHolder>
