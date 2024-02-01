<%@ Page Language="C#" Async="true" AutoEventWireup="true" CodeFile="Get_Data.aspx.cs" Inherits="Get_Data" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <link href="Library/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <script src="Library/bootstrap/js/bootstrap.min.js"></script>

</head>
<body class="p-0 m-0">
    <form id="form1" runat="server">

        <br />
        <div class="container-fluid bg-dark-subtle pt-4 pb-4">

            <div class="row">
                <div class="col-md-4">
                    <h2 class="p-5 bg-gradient">Insert New Employee</h2>
                    <br />
                    <asp:HiddenField ID="Id_HiddenField" runat="server" />
                   <h5><asp:Label ID="Name_Label" runat="server" Text="Name"></asp:Label></h5> 
                    <asp:TextBox ID="Name_Box" placeholder="Enter Name" CssClass="form-control" runat="server"></asp:TextBox>

                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="Name_Box" ForeColor="Red" Display="Dynamic" SetFocusOnError="true" ErrorMessage="Name is Required"></asp:RequiredFieldValidator>
                    <br />
                   
                   <h5><asp:Label ID="Salary_Label" runat="server" Text="Salary"></asp:Label></h5> 
                    <asp:TextBox ID="Salary_Box" placeholder="Enter Salary" TextMode="Number"  CssClass="form-control" runat="server"></asp:TextBox>
                   
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="Salary_Box" ForeColor="Red" Display="Dynamic" SetFocusOnError="true" ErrorMessage="Salary is Required"></asp:RequiredFieldValidator>
                    
                    <br />
                   
                    <h5> <asp:Label ID="Country_Label" runat="server" Text="Country"></asp:Label></h5>
                    <asp:DropDownList ID="ddlCountry" CssClass="form-control" OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged" AutoPostBack="true" runat="server">
                    </asp:DropDownList>

                    <asp:RequiredFieldValidator InitialValue="-- Select Country --" ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlCountry" ForeColor="Red" Display="Dynamic" SetFocusOnError="true" ErrorMessage="Country is Required"></asp:RequiredFieldValidator>

                    <br />
               
                    <h5><asp:Label ID="State_Label" runat="server" Text="State"></asp:Label></h5>
                    <asp:DropDownList ID="ddlState" CssClass="form-control" OnSelectedIndexChanged="ddlState_SelectedIndexChanged" AutoPostBack="true" runat="server">
                        <asp:ListItem>-- Select State --</asp:ListItem>
                    </asp:DropDownList>

                    <asp:RequiredFieldValidator InitialValue="-- Select State --" ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlState" ForeColor="Red" Display="Dynamic" SetFocusOnError="true" ErrorMessage="State is Required"></asp:RequiredFieldValidator>

                    <br />
                 
                    <h5><asp:Label ID="City_Label" runat="server" Text="City"></asp:Label></h5>
                    <asp:DropDownList ID="ddlCity" CssClass="form-control"  runat="server">
                        <asp:ListItem>-- Select City --</asp:ListItem>
                    </asp:DropDownList>

                    <asp:RequiredFieldValidator InitialValue="-- Select City --" ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlCity" ForeColor="Red" Display="Dynamic" SetFocusOnError="true" ErrorMessage="City is Required"></asp:RequiredFieldValidator>

                    <br />
                   
                    <%--<asp:Label ID="Gender_Label" runat="server" Text="Gender"></asp:Label><br />
                    <asp:TextBox ID="Gender_Box" placehoder="Enter Gender" CssClass="form-control" runat="server"></asp:TextBox>
                  
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="Gender_Box" ForeColor="Red" Display="Dynamic" SetFocusOnError="true" ErrorMessage="Gender is Required"></asp:RequiredFieldValidator>
                    --%>


                     <h5><asp:Label ID="Label8" runat="server" Text="Gender"></asp:Label></h5>
                    <asp:DropDownList ID="Gender_List" CssClass="form-control" runat="server">
                        <asp:ListItem>-- Select Gender --</asp:ListItem>
                        <asp:ListItem>Male</asp:ListItem>
                        <asp:ListItem>Female</asp:ListItem>
                        <asp:ListItem>Other</asp:ListItem>
                    </asp:DropDownList>

                    <asp:RequiredFieldValidator InitialValue="-- Select Gender --" ID="RequiredFieldValidator6" runat="server" ControlToValidate="Gender_List" ForeColor="Red" Display="Dynamic" SetFocusOnError="true" ErrorMessage="Gender is Required"></asp:RequiredFieldValidator>
                         <h6><asp:Label ID="lblMessage" runat="server" Text=""></asp:Label></h6> 
                    <br />
                   
              
                    <br />

                    <asp:Button ID="Insert_Button" CssClass="btn btn-success" runat="server" Text="Insert" OnClick="Insert_Button_Click" />
                    &nbsp;
                &nbsp;
                &nbsp;
               
                <asp:Button ID="Update_Button" runat="server" CssClass="btn btn-info" Text="Update" OnClick="Update_Button_Click" />
                &nbsp;
                &nbsp;
                &nbsp;
                <asp:Button ID="Delete_Button" runat="server" CssClass="btn btn-danger" Text="Delete" OnClick="Delete_Button_Click" />
                 &nbsp;
                &nbsp;
                &nbsp;
                <asp:Button ID="Reset_Button" runat="server" CssClass="btn btn-danger" Text="Reset" OnClick="Reset_Button_Click" />
               
                    
                
                </div>
                <br />
                <br />


                <div class="col-md-8">
                    <br />
                    <br />

                    <h1 class="text-center">EMPLOYEE DETAILS</h1>
                    <br />
                    <br />
                    <br />
                    <br />
                    <asp:GridView ID="Emp_GridView" CssClass="table table-striped table-hover table-info table-bordered" runat="server" OnRowDeleting="Emp_GridView_RowDeleting" AutoGenerateColumns="False" OnSelectedIndexChanged="Emp_GridView_SelectedIndexChanged">
                        <Columns>

                            <asp:TemplateField HeaderText="Employee ID">
                                <ItemTemplate>
                                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("Id") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Employee Name">
                                <ItemTemplate>
                                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Salary">
                                <ItemTemplate>
                                    <asp:Label ID="Label3" runat="server" Text='<%# Bind("Salary") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Gender">
                                <ItemTemplate>
                                    <asp:Label ID="Label4" runat="server" Text='<%# Bind("Gender") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Country">
                                <ItemTemplate>
                                    <asp:Label ID="Label5" runat="server" Text='<%# Bind("Country") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="State">
                                <ItemTemplate>
                                    <asp:Label ID="Label6" runat="server" Text='<%# Bind("State") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="City">
                                <ItemTemplate>
                                    <asp:Label ID="Label7" runat="server" Text='<%# Bind("City") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>


                            <asp:TemplateField HeaderText="Manage Data">
                                <ItemTemplate>
                                    <asp:LinkButton ID="Edit_Button" CssClass="btn btn-success" runat="server" CausesValidation="False" CommandName="Select" Text="Edit"></asp:LinkButton>

                                    &nbsp;
                            
                           
                            <asp:LinkButton ID="Dlt_Button" runat="server" CssClass="btn btn-danger" CausesValidation="False" CommandName="Delete" Text="Delete"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>

                        </Columns>
                    </asp:GridView>


                </div>
            </div>
    </div>
    </form>
</body>
</html>
