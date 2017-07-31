<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Edit.ascx.cs" Inherits="IWStar.DNN.Modules.IWStarGallery.Edit" %>
<%@ Register TagPrefix="iw" TagName="EditItem" Src="./Components/EditItem.ascx" %>
<%@ Register TagPrefix="dnn" Namespace="DotNetNuke.Web.Client.ClientResourceManagement" Assembly="DotNetNuke.Web.Client" %>
<dnn:DnnCssInclude runat="server" FilePath="/desktopmodules/iwstargallery/resources/bootstrap/css/bootstrap.min.css" />
<dnn:DnnJsInclude runat="server" FilePath="/desktopmodules/iwstargallery/resources/iwstargallery.js" />

<div class="edit-iw-star-gallery">
    <asp:PlaceHolder ID="phTopBar" runat="server">
        <div class="clearfix">
            <asp:LinkButton ID="btnSettings" Text="Settings" OnClick="btnSettings_Click" runat="server" />
            <asp:LinkButton ID="btnOpenNewItemArea" Text="Add new item" OnClick="btnShowNewItemArea_Click" CssClass="pull-right" runat="server" />
        </div>
    </asp:PlaceHolder>

    <asp:PlaceHolder ID="phSettings" Visible="false" runat="server">
        <div class="form-horizontal">
            <div class="form-group">
                <div class="col-sm-offset-4 col-sm-8">
                    <asp:LinkButton ID="btnGenerateThumbnail" Text="Regenerate thumbnail" ToolTip="click this if thumbnail file is missing" 
                        OnClick="btnGenerateThumbnail_Click" Visible="false" runat="server" />
                </div>
            </div>
            <div class="form-group">
                <asp:Label AssociatedControlID="txtMaxWidth" CssClass="col-sm-4 control-label" runat="server">Max Width:</asp:Label>
                <div class="col-sm-8 form-inline">
                    <asp:TextBox ID="txtMaxWidth" CssClass="form-control" runat="server"></asp:TextBox>
                    <span>(Set as the container width)</span>
                </div>
            </div>
            <div class="form-group">
                <asp:Label AssociatedControlID="txtMaxHeight" CssClass="col-sm-4 control-label" runat="server">Max Height:</asp:Label>
                <div class="col-sm-8 form-inline">
                    <asp:TextBox ID="txtMaxHeight" CssClass="form-control" runat="server"></asp:TextBox>
                </div>
            </div>
            <div class="form-group">
                <asp:Label AssociatedControlID="chkIndicator" CssClass="col-sm-4 control-label" runat="server">Show Indicater:</asp:Label>
                <div class="col-sm-8 checkbox">
                    <label>
                        <asp:CheckBox ID="chkIndicator" runat="server" />
                    </label>
                </div>
            </div>
            <div class="form-group">
                <asp:Label AssociatedControlID="chkThumbnail" CssClass="col-sm-4 control-label" runat="server">Show Thumbnail:</asp:Label>
                <div class="col-sm-8 checkbox">
                    <label>
                        <asp:CheckBox ID="chkThumbnail" runat="server" />
                    </label>
                </div>
            </div>
            <div class="form-group">
                <asp:Label AssociatedControlID="chkTitle" CssClass="col-sm-4 control-label" runat="server">Show Title:</asp:Label>
                <div class="col-sm-8 checkbox">
                    <label>
                        <asp:CheckBox ID="chkTitle" runat="server" />
                    </label>
                </div>
            </div>
            <div class="form-group">
                <asp:Label AssociatedControlID="chkDescription" CssClass="col-sm-4 control-label" runat="server">Show Description:</asp:Label>
                <div class="col-sm-8 checkbox">
                    <label>
                        <asp:CheckBox ID="chkDescription" runat="server" />
                    </label>
                </div>
            </div>
            <div class="form-group">
                <asp:Label AssociatedControlID="chkPopup" CssClass="col-sm-4 control-label" runat="server">Popup:</asp:Label>
                <div class="col-sm-8 checkbox">
                    <label>
                        <asp:CheckBox ID="chkPopup" runat="server" />
                        Show &nbsp;
                    </label>
                    <label>
                        <asp:CheckBox ID="chkPopupTitle" runat="server" />
                        Show Title &nbsp;
                    </label>
                    <label>
                        <asp:CheckBox ID="chkPopupDesc" runat="server" />
                        Show Description&nbsp;
                    </label>
                </div>
            </div>
            <div class="iw-btns">
                <asp:LinkButton ID="btnSettingsSave" CssClass="btn btn-default" OnClick="btnSettingsSave_Click" Text="Save" runat="server" />
                <asp:LinkButton ID="btnSettingsCancel" CssClass="btn btn-default" OnClick="btnSettingsCancel_Click" Text="Cancel" runat="server" />
            </div>
        </div>
    </asp:PlaceHolder>

    <asp:PlaceHolder ID="phAddNewItem" Visible="false" runat="server">
        <iw:EditItem ID="iwNewItem" runat="server"></iw:EditItem>
        <div class="iw-btns">
            <asp:LinkButton ID="btnAddNewItem" CssClass="btn btn-default" OnClick="btnAddNewItem_Click" Text="Add" runat="server" />
            <asp:LinkButton ID="btnAddCancel" CssClass="btn btn-default" OnClick="btnAddCancel_Click" Text="Cancel" runat="server" />
        </div>
    </asp:PlaceHolder>

    <div class="gallery-list">
        <asp:Repeater ID="rptImages" runat="server" OnItemDataBound="rptImages_ItemDataBound">
            <ItemTemplate>
                <div class="gallery-item clearfix">
                    <div class="col-sm-2">
                        <img src="<%# Eval("path") %>" />
                    </div>
                    <asp:PlaceHolder ID="phViewItem" runat="server">
                        <div class="col-sm-8">
                            <div>
                                <label>Title:</label>
                                <span><%# Eval("title") %></span>
                            </div>
                            <div>
                                <label>Description:</label>
                                <span><%# Eval("description") %></span>
                            </div>
                        </div>
                        <div class="col-sm-2">
                            <div>
                                <asp:LinkButton ID="btnMoveUp" Text="Move up" OnCommand="btnMoveUp_Command"
                                    CommandArgument="<%# Container.ItemIndex %>" runat="server" />
                            </div>
                            <div>
                                <asp:LinkButton ID="btnMoveDown" Text="Move down" OnCommand="btnMoveDown_Command"
                                    CommandArgument="<%# Container.ItemIndex %>" runat="server" />
                            </div>
                            <div>
                                <asp:LinkButton ID="btnEditItem" Text="Edit item" OnCommand="btnEditItem_Command"
                                    CommandArgument="<%# Container.ItemIndex %>" runat="server" />
                            </div>
                            <div>
                                <asp:LinkButton ID="btnDeleteItem" Text="Delete item" OnCommand="btnDeleteItem_Command"
                                    CommandArgument="<%# Container.ItemIndex %>" OnClientClick="return confirmDeleteItem();" runat="server" />
                            </div>
                        </div>
                    </asp:PlaceHolder>
                    <asp:PlaceHolder ID="phEditItem" Visible="false" runat="server">
                        <div class="col-sm-10">
                            <iw:EditItem ID="iwEditItem" runat="server"></iw:EditItem>
                            <div class="iw-btns">
                                <asp:LinkButton ID="btnSaveItem" CssClass="btn btn-default" Text="Save" OnCommand="btnSaveItem_Command"
                                    CommandArgument="<%# Container.ItemIndex %>" runat="server" />
                                <asp:LinkButton ID="btnSaveCancel" CssClass="btn btn-default" Text="Cancel" OnCommand="btnSaveCancel_Command"
                                    CommandArgument="<%# Container.ItemIndex %>" runat="server" />
                            </div>
                        </div>
                    </asp:PlaceHolder>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>
</div>
