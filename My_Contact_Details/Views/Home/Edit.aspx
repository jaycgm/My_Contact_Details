﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<My_Contact_Details.Models.Tmp_Contact_Details>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Edit
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Edit</h2>

    <% using (Html.BeginForm()) {%>
        <%: Html.ValidationSummary(true) %>
        
        <fieldset>
            <legend>Fields</legend>
            
            <div class="editor-label">
                <%: Html.LabelFor(model => model.Id) %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.Id) %>
                <%: Html.ValidationMessageFor(model => model.Id) %>
            </div>
            
            <div class="editor-label">
                <%: Html.LabelFor(model => model.FirstName) %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.FirstName) %>
                <%: Html.ValidationMessageFor(model => model.FirstName) %>
            </div>
            
            <div class="editor-label">
                <%: Html.LabelFor(model => model.LastName) %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.LastName) %>
                <%: Html.ValidationMessageFor(model => model.LastName) %>
            </div>
            
            <div class="editor-label">
                <%: Html.LabelFor(model => model.Phone) %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.Phone) %>
                <%: Html.ValidationMessageFor(model => model.Phone) %>
            </div>
            
            <div class="editor-label">
                <%: Html.LabelFor(model => model.Email) %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.Email) %>
                <%: Html.ValidationMessageFor(model => model.Email) %>
            </div>
            
            <p>
                <input type="submit" value="Save" />
            </p>
        </fieldset>

    <% } %>

    <div>
        <%: Html.ActionLink("Back to List", "Index") %>
    </div>

</asp:Content>

