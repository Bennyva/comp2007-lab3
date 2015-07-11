<%@ Page Title="" Language="C#" MasterPageFile="~/site.Master" AutoEventWireup="true" CodeBehind="course.aspx.cs" Inherits="lesson9.course" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <a href="course.aspx">Add/Edit Course</a>


    <fieldset>
        <label for="txtTitle" class="col-sm-2">Course Name:</label>
        <asp:TextBox ID="txtTitle" runat="server" required="true" MaxLength="50"></asp:TextBox>
    </fieldset>
    <fieldset>
        <label for="txtCredits" class="col-sm-2">Credits:</label>
        <asp:TextBox ID="txtCredits" runat="server" required="true" MaxLength="50"></asp:TextBox>
    </fieldset>
    <fieldset>
        <label for="ddDepartment" class="col-sm-2">Department:</label>
        <asp:DropDownList ID="ddDepartment" runat="server" ></asp:DropDownList>
    </fieldset>

    <div class="col-sm-offset-2">
        <asp:Button id="btnSave" runat="server" Text="Save" CssClass="btn btn-primary" OnClick="btnSave_Click"
            />
    </div>
</asp:Content>
