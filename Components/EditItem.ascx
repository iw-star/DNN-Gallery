<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EditItem.ascx.cs" Inherits="IWStar.DNN.Modules.IWStarGallery.Components.EditItem" %>

<div class="form-horizontal">
    <div class="form-group">
        <asp:Label AssociatedControlID="txtTitle" CssClass="col-sm-2 control-label" runat="server">Title:</asp:Label>
        <div class="col-sm-10">
            <asp:TextBox ID="txtTitle" CssClass="form-control" runat="server"></asp:TextBox>
        </div>
    </div>
    <div class="form-group">
        <asp:Label AssociatedControlID="txtDescription" CssClass="col-sm-2 control-label" runat="server">Description:</asp:Label>
        <div class="col-sm-10">
            <asp:TextBox ID="txtDescription" CssClass="form-control" TextMode="MultiLine" runat="server"></asp:TextBox>
        </div>
    </div>
    <div class="form-group">
        <asp:Label AssociatedControlID="ddFolder" CssClass="col-sm-2 control-label" runat="server">Folder:</asp:Label>
        <div class="col-sm-10">
            <asp:DropDownList ID="ddFolder" CssClass="form-control" OnSelectedIndexChanged="ddFolder_SelectedIndexChanged" AutoPostBack="true" runat="server"></asp:DropDownList>
        </div>
    </div>
    <asp:PlaceHolder ID="phSelectFileArea" runat="server">
        <div class="form-group">
            <asp:Label AssociatedControlID="ddFile" CssClass="col-sm-2 control-label" runat="server">File:</asp:Label>
            <div class="col-sm-10">
                <asp:DropDownList ID="ddFile" CssClass="form-control" runat="server"></asp:DropDownList>
            </div>
        </div>
        <div class="form-group">
            <div class="col-sm-offset-2 col-sm-10">
                <asp:LinkButton ID="btnOpenUploadArea" OnClick="btnOpenUploadArea_Click" runat="server">Upload File</asp:LinkButton>
            </div>
        </div>
    </asp:PlaceHolder>
    <asp:PlaceHolder ID="phUploadArea" runat="server" Visible="false">
        <div class="form-group">
            <asp:Label AssociatedControlID="fileImage" CssClass="col-sm-2 control-label" runat="server">File:</asp:Label>
            <div class="col-sm-10">
                <asp:FileUpload ID="fileImage" accept="image/*" runat="server" />
            </div>
        </div>
        <div class="form-group">
            <div class="col-sm-offset-2 col-sm-10">
                <asp:LinkButton ID="btnCloseUploadArea" OnClick="btnCloseUploadArea_Click" runat="server">Cancel Upload File</asp:LinkButton>
            </div>
        </div>
    </asp:PlaceHolder>
    <asp:Literal ID="litMessage" runat="server"></asp:Literal>
</div>
