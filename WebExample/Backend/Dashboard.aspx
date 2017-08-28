<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="WebExample.Backend.Dashboard" %>
<%@ Register TagPrefix="asp" Namespace="WebExample" Assembly="App_Code" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Dashboard</title>
    <!-- Cascade Style Sheets -->
    <link href="~/Content/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="~/Content/custom.css" rel="stylesheet" type="text/css" />
    <link href="~/Content/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <link href="~/Content/DataTables/css/dataTables.bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="~/Content/DataTables/css/buttons.dataTables.min.css" rel="stylesheet" type="text/css" />
    <!-- Javascript Scripts -->
    <script src="<%: ResolveUrl("~/Scripts/jquery-3.1.1.min.js") %>" type="text/javascript"></script>
    <script src="<%: ResolveUrl("~/Scripts/bootstrap.min.js") %>" type="text/javascript"></script>
    <script src="<%: ResolveUrl("~/Scripts/DataTables/jquery.dataTables.min.js") %>" type="text/javascript"></script>
    <script src="<%: ResolveUrl("~/Scripts/DataTables/dataTables.bootstrap.min.js") %>" type="text/javascript"></script>
    <script src="<%: ResolveUrl("~/Scripts/DataTables/dataTables.buttons.min.js") %>" type="text/javascript"></script>
    <script src="<%: ResolveUrl("~/Scripts/jszip.min.js") %>" type="text/javascript"></script>
    <script src="<%: ResolveUrl("~/Scripts/pdfmake/pdfmake.min.js") %>" type="text/javascript"></script>
    <script src="<%: ResolveUrl("~/Scripts/pdfmake/vfs_fonts.js") %>" type="text/javascript"></script>
    <script src="<%: ResolveUrl("~/Scripts/DataTables/buttons.html5.min.js") %>" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
        <nav class="navbar navbar-default">
            <div class="container">
                <!-- Brand and toggle get grouped for better mobile display -->
                <div class="navbar-header">
                    <button type="button" class="navbar-toggle collapsed" data-toggle="collapse" data-target="#bs-example-navbar-collapse-1" aria-expanded="false">
                        <span class="sr-only">Toggle navigation</span>
                        <span class="icon-bar"></span>
                        <span class="icon-bar"></span>
                        <span class="icon-bar"></span>
                    </button>
                    <a class="navbar-brand" href="#">Brand</a>
                </div>

                <!-- Collect the nav links, forms, and other content for toggling -->
                <div class="collapse navbar-collapse" id="bs-example-navbar-collapse-1">
                    <ul class="nav navbar-nav">
                        <li class="dropdown">
                            <a href="#" class="dropdown-toggle" data-toggle="dropdown" role="button" aria-haspopup="true" aria-expanded="false">Inicio <span class="caret"></span></a>
                            <ul class="dropdown-menu">
                                <li><a href="#">Nuevo</a></li>
                                <li role="separator" class="divider"></li>
                                <li><a href="#">Abrir</a></li>
                            </ul>
                        </li>
                        <li class="dropdown">
                            <a href="#" class="dropdown-toggle" data-toggle="dropdown" role="button" aria-haspopup="true" aria-expanded="false">Editar <span class="caret"></span></a>
                            <ul class="dropdown-menu">
                                <li><a href="#">Deshacer</a></li>
                                <li><a href="#">Copiar</a></li>
                                <li><a href="#">Pegar</a></li>
                            </ul>
                        </li>
                        <li><a href="#">Vista</a></li>
                        <li><a href="#">Ayuda</a></li>

                    </ul>
                </div>
                <!-- /.navbar-collapse -->
            </div>
            <!-- /.container-fluid -->
        </nav>
        <div class="container">
            <div class="form-group">

                <asp:DropDownListAttributes ID="ddlDepartments" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlDepartments_SelectedIndexChanged" AutoPostBack="True">
                </asp:DropDownListAttributes>

            </div>
            <div class="form-group">
                <asp:GridView ID="gridCourses" runat="server" OnPreRender="gridCourses_PreRender" CssClass="dataTable table table-striped table-bordered table-responsive" OnRowEditing="gridCourses_RowEditing" OnSelectedIndexChanging="gridCourses_SelectedIndexChanging" OnRowCancelingEdit="gridCourses_RowCancelingEdit" OnRowUpdating="gridCourses_RowUpdating" OnPageIndexChanging="gridCourses_PageIndexChanging" AutoGenerateColumns="False">
                    <Columns>
<asp:BoundField DataField="Title" HeaderText="Title"></asp:BoundField>
                        <asp:BoundField DataField="CourseID" HeaderText="Course ID" />
                        <asp:BoundField DataField="Credits" HeaderText="Credits" />
                        <asp:BoundField DataField="DepartmentID" HeaderText="Department ID" />
                        <asp:CommandField ShowEditButton="true" HeaderText="Actions" EditText="<i class='fa fa-pencil' aria-hidden='true'></i>" >
                        <ControlStyle CssClass="edit" />
                        </asp:CommandField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </form>
</body>
<script type="text/javascript">
    $(document).ready(function () {
        $('.dataTable').dataTable({
            dom: 'Bfrtip',
            //"sPaginationType": "full_numbers",
            "iDisplayLength": 25,
            "aLengthMenu": [
                [25, 50, 100, -1],
                [25, 50, 100, "Todo"]
            ],
            "buttons": [
                {
                    extend: 'copyHtml5',
                    exportOptions: {
                        columns: ':contains("Title")'
                    }
                },
                {
                    extend: 'excelHtml5',
                    exportOptions: {
                        columns: [0, 1, 2, 3]
                    }
                },
                {
                    extend: 'csvHtml5',
                    exportOptions: {
                        columns: [0, 1, 2, 3]
                    }
                },
                {
                    extend: 'pdfHtml5',
                    exportOptions: {
                        columns: [0, 1, 2, 3]
                    }
                }
            ]
        });
    });
</script>
</html>
