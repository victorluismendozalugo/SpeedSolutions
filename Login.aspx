<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="SpeedSolutions.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>   
    
    <link href="Content/blockUI.css" rel="stylesheet" />
    <link href="Content/toastr.min.css" rel="stylesheet" />

    <link href="Content/bootstrap.min.css" rel="stylesheet" />

</head>
<body class="bg-white">
    <div class="container">
        <div class="row justify-content-md-center">
            <div class="col col-lg-4">
                <div class="card card-login mx-auto mt-5">
                    <div class="card-header" style="text-align: center" id="imagen">
                        <p>Control de acceso</p>
                    </div>
                    <div class="card-body">
                        <form>
                            <div class="form-group">
                                <label for="exampleInputEmail1">Usuario</label>
                                <input class="form-control" id="txtlogin" type="text" aria-describedby="" placeholder="Usuario">
                            </div>
                            <div class="form-group">
                                <label for="exampleInputPassword1">Password</label>
                                <input class="form-control" id="txtpassword" type="password" placeholder="Password">
                            </div>
                            <a class="btn btn-danger btn-block" id="btnaceptar">Login</a>
                        </form>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <script src="Scripts/jquery-3.6.0.js"></script>
    <script src="Scripts/bootstrap.js"></script>


    <script src="js/Components/jquery.blockUI.js"></script>
    <script src="js/Components/toastr.min.js"></script>

    <script src="js/Components/jquery.blockUI.messages.js"></script>
    <script src="js/Components/jquery.AjaxRequest.js"></script>
    <script src="js/Components/jquery.AjaxResult.js"></script>

    <script src="js/Components/bootbox.min.js"></script>

    <script src="js/Login.js"></script>
</body>
</html>
